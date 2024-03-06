
import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs';
import { MessageResponse } from 'src/app/_models/message.response';
import { UserResponse } from 'src/app/_models/user.response';
import { AuthService } from 'src/app/_services/auth.service';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit{
  mode:string="inbox"
  user:UserResponse []=[];
  messages:MessageResponse []=[];
  constructor(public messageService:MessageService, private authService:AuthService){}
  ngOnInit(): void {
   
    this.messageService.getMailMessages(this.mode).subscribe({
      next:(value)=>{
        console.log(value);
        this.messages = value;
      }
    })
    //Sending Messages to User
  }

  getMailMessages(mode:string){
    this.mode = mode;
    this.messages=[];
    this.messageService.getMailMessages(this.mode).subscribe({
      next:(value)=>{
        console.log(value);
        this.messages = value;
      }
    })
  }
}
