import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserResponse } from '../_models/user.response';
import { HttpClient } from '@angular/common/http';
import { map, of } from 'rxjs';
import { MessageRequest } from '../_models/message.request';
import { MessageResponse } from '../_models/message.response';


@Injectable({
  providedIn: 'root'
})
export class MessageService {
  baseUrl=environment.apiUrl;
  users:UserResponse []=[];
  constructor(private httpClient:HttpClient) { }

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

  getMessages(id:number){
    return this.httpClient.get<MessageResponse []>(this.baseUrl+"message/get-chatmessages/"+id)
  }

}
