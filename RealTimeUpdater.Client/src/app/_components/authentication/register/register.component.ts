import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  
  formGroup:FormGroup=new FormGroup({});

  constructor(private formBuilder:FormBuilder){}

  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      email:["",Validators.required],
      password:["",Validators.required]
    })
  }

  submitForm(){
    console.log(this.formGroup.value);
  }

}
