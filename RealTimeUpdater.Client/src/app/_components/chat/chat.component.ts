import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { MessageRequest } from 'src/app/_models/message.request';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  constructor(private route:ActivatedRoute,private messageService:MessageService,private formBuilder:FormBuilder){}
  formGroup:FormGroup = new FormGroup({});
  recieverId?:number;
  ngOnInit(): void {
    this.getUserMessages();
    this.formGroup = this.formBuilder.group({
      content:["",Validators.required]
    })
  }

  getUserMessages(){
    this.route.paramMap.subscribe({
      next:(data)=>{
       this.recieverId =  Number(data.get("id"));
        
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
      this.messageService.sendMessage(message)
    }
    
  }
}
