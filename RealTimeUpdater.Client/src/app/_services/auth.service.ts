import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AuthUserResponse } from '../_models/auth-user.response';
import { BehaviorSubject, map, take } from 'rxjs';
import { LoginRequest } from '../_models/login.request';
import { RegisterRequest } from '../_models/register.request';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  userSource = new BehaviorSubject<AuthUserResponse|null>(null);
  $currentUser = this.userSource.asObservable();
  baseUrl:string=environment.apiUrl;

  constructor(private httpClient:HttpClient) { }

  registerUser(body:RegisterRequest){
    return this.httpClient.post<AuthUserResponse>(this.baseUrl+"auth/register",body).pipe(map(user=>{
      if(user){
        let userString = JSON.stringify(user);
        localStorage.setItem("user",userString);
        this.userSource.next(user);
      }
     return user;
    }))
    }
  loginUser(body:LoginRequest){
    return this.httpClient.post<AuthUserResponse>(this.baseUrl+"auth/login",body).pipe(map((user:AuthUserResponse)=>{
      if(user){
        this.addUser(user);
      }
      return user;
    }))
  }
  

  logOut(){
    this.removeUser();
    this.userSource.next(null);
  }

  initialiseUser(){
    let userString =  localStorage.getItem("user");
    if(!userString)return;
    let user:AuthUserResponse = JSON.parse(userString);
    this.userSource.next(user);
  }
  private addUser(user:AuthUserResponse){
    let userString = JSON.stringify(user);
    localStorage.setItem("user",userString);
    this.userSource.next(user);
  }
  private removeUser(){
    localStorage.removeItem("user");
  }
}
