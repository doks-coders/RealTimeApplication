import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map } from 'rxjs';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';


@Injectable({
  providedIn: 'root'
})
export class RealTimeDataService {
  userSource = new BehaviorSubject<string|null>(null);
  currentUser$ = this.userSource.asObservable();
  baseUrl=environment.hubUrl;
  private hubConnection?:HubConnection;
  constructor(private http:HttpClient) { 

  }

  //Intialising Connection
  intialiseConnection(){
    this.hubConnection = new HubConnectionBuilder()
    .withUrl(this.baseUrl+"updates")
    .withAutomaticReconnect().build();

    this.hubConnection.start().then(val=>{
      console.log("Connection Started");
    }).catch(err=>{
      console.log(err);
    })

    this.hubConnection.on("UpdateInformation",(e)=>{
      this.userSource.next(e.array)
    })
  }

}
