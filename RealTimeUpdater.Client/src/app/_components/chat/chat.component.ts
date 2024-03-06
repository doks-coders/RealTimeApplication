import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { take } from 'rxjs';
import { MessageRequest } from 'src/app/_models/message.request';
import { MessageResponse } from 'src/app/_models/message.response';
import { AuthService } from 'src/app/_services/auth.service';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  formGroup:FormGroup = new FormGroup({});

  chatMessages:MessageResponse[] = [];

  recieverId?:number;
  recieverEmail?:string;

  constructor(private route:ActivatedRoute,
    public messageService:MessageService,
    private authService:AuthService,
    private formBuilder:FormBuilder){}

  ngOnInit(): void {
    this.getUserDetails();
    this.formGroup = this.formBuilder.group({
      content:["",Validators.required]
    })

  }

  getUserDetails(){
    this.route.paramMap.subscribe({
      next:(data)=>{
       this.recieverId =  Number(data.get("id"));
        this.intialiseConnectionToHub(this.recieverId.toString());
       
       this.getUserMessages(this.recieverId);
      }
    })
  }

  intialiseConnectionToHub(recieverName:string){
    this.authService.$currentUser.pipe(take(1)).subscribe({
      next:(user)=>{
        if(user){
          this.messageService.intialiseConnection(user,recieverName);
        }
      }
    })

    //The magic happens here
    this.messageService.$messagesObserved.subscribe({
      next:(value)=>{
        console.log("Magic area");
        console.log(value);
      }
    })
  }

  getUserMessages(id:number){
    this.messageService.getMessages(id).subscribe({
      next:(users)=>{
        this.chatMessages = users;
      }
    })
  }

  submitMessage(){
    if(this.formGroup.valid){
      let content = this.formGroup.value["content"] as string
      if(!this.recieverId)return;
      let message:MessageRequest = {
        content,
        recieverId:this.recieverId,
      }

      this.messageService.sendMessageUsingHub(message);
      this.formGroup.reset();
      /*
      this.messageService.sendMessage(message).subscribe({
        next:(_)=>{
          console.log("Message Sent");
          if(!this.recieverId) return;
          this.getUserMessages(this.recieverId);
          this.formGroup.reset();
        }
      })
      */
    }
    
  }
}
