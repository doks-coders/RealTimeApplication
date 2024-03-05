import { Component, OnInit } from '@angular/core';
import {  RealTimeDataService } from './_services/realtime-data.service';
import { AuthService } from './_services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
 
  constructor(private authService:AuthService){}

  ngOnInit(): void {
    this.authService.initialiseUser();
    //this.authService.intialiseConnection();

  }

}
