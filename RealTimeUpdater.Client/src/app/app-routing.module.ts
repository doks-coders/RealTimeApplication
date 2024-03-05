import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';


import { HomeComponent } from './_components/home/home.component';
import { LoginComponent } from './_components/authentication/login/login.component';
import { RegisterComponent } from './_components/authentication/register/register.component';
import { MessagesComponent } from './_components/messages/messages.component';
import { ChatComponent } from './_components/chat/chat.component';


const routes: Routes = [
  {path:"",component:HomeComponent},
  {path:"login",component:LoginComponent},
  {path:"register",component:RegisterComponent},
  {path:"messages",component:MessagesComponent},
  {path:"chat",component:ChatComponent},
  {path:"**",component:HomeComponent, pathMatch:"full"},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
