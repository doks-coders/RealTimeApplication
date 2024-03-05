import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { UserResponse } from '../_models/user.response';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  baseUrl=environment.apiUrl;
  constructor(private httpClient:HttpClient) { }

  getAllUsers(){
    return this.httpClient.get<UserResponse []>(this.baseUrl+"/users/all-users")
  }

}
