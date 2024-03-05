import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, delay, finalize } from 'rxjs';
import { NgxSpinner, NgxSpinnerService } from 'ngx-spinner';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

  constructor(private spinnerService:NgxSpinnerService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this.spinnerService.show(undefined,{
      type:"ball-scale-multiple",
      bdColor:"rgba(255,255,255,0)",
      color:"#333333"
    });
    return next.handle(request).pipe(delay(1000),finalize(()=>{
      this.spinnerService.hide();
    }));
  }
}
