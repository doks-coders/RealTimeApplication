import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserResponse } from '../_models/user.response';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map, of, take } from 'rxjs';
import { MessageRequest } from '../_models/message.request';
import { MessageResponse } from '../_models/message.response';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { AuthUserResponse } from '../_models/auth-user.response';


@Injectable({
  providedIn: 'root'
})
export class MessageService {
  baseUrl=environment.apiUrl;
  hubUrl = environment.hubUrl
  users:UserResponse []=[];
  hubConnection?:HubConnection;
  messagesSource= new BehaviorSubject<MessageResponse []>([]);
  $messagesObserved = this.messagesSource.asObservable();

  constructor(private httpClient:HttpClient) { }

  intialiseConnection(user:AuthUserResponse, recieverId:string){
    this.hubConnection = new HubConnectionBuilder().withUrl(this.hubUrl+`messages?RecieverId=${recieverId}`,{
      accessTokenFactory() {
        return user.token;
      }
    })
    .withAutomaticReconnect()
    .build();
    
    this.hubConnection.start();

    this.hubConnection.on("UserMessages",(element)=>{
      console.log({element})
      this.messagesSource.next(element);
    })

    this.hubConnection.on("NewMessage",(element)=>{
      console.log({element})
      
      this.$messagesObserved.pipe(take(1)).subscribe({
        next:(messages)=>{
          let newMessages = [...messages, element];
          this.messagesSource.next(newMessages);
        }
      })
      
    })

  }
  getAllUsers(){
    if(this.users.length) return of(this.users);
    return this.httpClient.get<UserResponse []>(this.baseUrl+"users/all-users").pipe(map(val=>{
      if(val){
        this.users = val;
      }
      return val;
    }))
  }

  sendMessage(message:MessageRequest){
    return this.httpClient.post(this.baseUrl+"message/send-message",message)
  }

  sendMessageUsingHub(message:MessageRequest){
    return this.hubConnection?.invoke("SendMessage",message);
  }

  getMessages(id:number){
    return this.httpClient.get<MessageResponse []>(this.baseUrl+"message/get-chatmessages/"+id)
  }
  getMailMessages(mode:string){
    return this.httpClient.get<MessageResponse []>(this.baseUrl+`message/get-mail-message/?mode=${mode}`)
  }

}
