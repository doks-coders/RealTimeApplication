import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './_components/home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from './_modules/shared.module';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { provideCharts, withDefaultRegisterables } from 'ng2-charts';
import { NavbarComponent } from './_components/navbar/navbar.component';
import { RegisterComponent } from './_components/authentication/register/register.component';
import { LoginComponent } from './_components/authentication/login/login.component';
import { UserListComponent } from './_components/user/user-list/user-list.component';
import { UserCardComponent } from './_components/user/user-card/user-card.component';
import { MessagesComponent } from './_components/messages/messages.component';
import { ChatComponent } from './_components/chat/chat.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavbarComponent,
    RegisterComponent,
    LoginComponent,
    UserListComponent,
    UserCardComponent,
    MessagesComponent,
    ChatComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    BsDropdownModule.forRoot(),
    BsDatepickerModule.forRoot(),
  ],
  providers: [
  { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
  provideCharts(withDefaultRegisterables())

],
  bootstrap: [AppComponent]
})
export class AppModule { }
