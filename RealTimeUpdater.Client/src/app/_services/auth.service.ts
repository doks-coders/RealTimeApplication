import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserResponse } from '../_models/user.response';
import { BehaviorSubject, map, take } from 'rxjs';
import { LoginRequest } from '../_models/login.request';
import { RegisterRequest } from '../_models/register.request';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  userSource = new BehaviorSubject<UserResponse|null>(null);
  $currentUser = this.userSource.asObservable();
  baseUrl:string=environment.apiUrl;

  constructor(private httpClient:HttpClient) { }

  registerUser(body:RegisterRequest){
    return this.httpClient.post<UserResponse>(this.baseUrl+"register",body).pipe(map(user=>{
      if(user){
        let userString = JSON.stringify(user);
        localStorage.setItem("user",userString);
        this.userSource.next(user);
      }
     return user;
    }))
    }
  loginUser(body:LoginRequest){
    return this.httpClient.post<UserResponse>(this.baseUrl+"login",body).pipe(map((user:UserResponse)=>{
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
    let user:UserResponse = JSON.parse(userString);
    this.userSource.next(user);
  }
  private addUser(user:UserResponse){
    let userString = JSON.stringify(user);
    localStorage.setItem("user",userString);
    this.userSource.next(user);
  }
  private removeUser(){
    localStorage.removeItem("user");
  }
}
