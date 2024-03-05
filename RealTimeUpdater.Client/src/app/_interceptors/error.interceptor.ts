import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private toastr:ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(catchError((err:HttpErrorResponse)=>{
      if (err.status <299) throw(err);
      switch(err.status){
        case 400:
          if(Array.isArray(err.error)){
            err.error.forEach(val=>{
              this.toastr.error(val.description,"Bad Request");
            })
          }else{
            this.toastr.error(err.error,"Bad Request");
          }
          break;
        case 401:
          this.toastr.error("Unauthorised",err.status.toString());
          break;
        case 404:
          this.toastr.error("Not Found",err.status.toString())
          break;
        case 500:
          this.toastr.error("Server Error",err.status.toString());
          break;
        default:
          this.toastr.error(err.error,err.status.toString())
      }
      throw(err);
    }));
  }
}
