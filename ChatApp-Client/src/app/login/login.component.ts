import { Component, OnInit } from '@angular/core';
import {FormControl, Validators} from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
isDisabled:boolean=true;
  constructor() { }
    
  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email,
  ]);
  passwordFormControl = new FormControl('', [
    Validators.required,
   Validators.minLength(5)
   
  ]);
  ngOnInit(): void {
  }
  buttonState():void{
   if(this.emailFormControl.hasError('required') || 
   this.emailFormControl.hasError('email') ||
    this.passwordFormControl.hasError('required')||
    this.passwordFormControl.hasError('minlength'))
    {
      this.isDisabled=true;
    }
    else{
      this.isDisabled=false;
    }
  }

}
