import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { MessageRequest } from 'src/app/_models/message.request';
import { MessageResponse } from 'src/app/_models/message.response';
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
    private messageService:MessageService,
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
       let user = this.messageService.users.find(e=>e.id==this.recieverId);
       if(user){
        this.recieverEmail = user.email;
       }
       this.getUserMessages(this.recieverId);
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
      this.messageService.sendMessage(message).subscribe({
        next:(_)=>{
          console.log("Message Sent");
          if(!this.recieverId) return;
          this.getUserMessages(this.recieverId);
          this.formGroup.reset();
        }
      })
    }
    
  }
}
