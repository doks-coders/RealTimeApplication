import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  formGroup:FormGroup=new FormGroup({});

  constructor(private formBuilder:FormBuilder,private authService:AuthService,private router:Router){}

  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      email:["",Validators.email],
      password:["",Validators.minLength(6)]
    })
  }

  submitForm(){
    if(this.formGroup.invalid) return;
    this.authService.loginUser(this.formGroup.value).subscribe({
      next:(_)=>{
        this.router.navigateByUrl("/");
      }
    })
    console.log(this.formGroup.value);
  }

}
