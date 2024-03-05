import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  
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
    this.authService.registerUser(this.formGroup.value).subscribe({
      next:_=>{
        this.router.navigateByUrl("/");
      }
    })
  }

}
