import { Component, OnInit } from '@angular/core';
import { UserResponse } from 'src/app/_models/user.response';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  users:UserResponse [] = [];

  constructor(private messageService:MessageService){}

  ngOnInit(): void {
    this.messageService.getAllUsers().subscribe({
      next:(users)=>{
        this.users = users;
      }
    })
  }


}
