import { Component, OnInit } from '@angular/core';
import {FormControl, Validators} from '@angular/forms';
import {IdentityService} from '../services/identity/identity.service';
import {Router} from '@angular/router';
import {LoginModel} from '../models/login-model';
import { UserService } from '../services/user/user.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  errorMessage:string=''
  loginModel:LoginModel;
isDisabled:boolean=true;
  constructor(private _identityService:IdentityService,private _router:Router,private _userService:UserService) {this.loginModel=<LoginModel>{} }
    
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
  signIn()
  {
    this.errorMessage=''
    console.log('login')
    this.loginModel.email=this.emailFormControl.value;
    this.loginModel.password=this.passwordFormControl.value;
   
    this._identityService.Login(this.loginModel).subscribe(response=>{
      console.log(response,'login response');
      this._identityService.tokens.jwtToken=response.jwtToken;
      this._identityService.tokens.refreshToken=response.refreshToken
      localStorage.setItem('refreshToken',this._identityService.tokens.refreshToken)
      localStorage.setItem('jwtToken',this._identityService.tokens.jwtToken);
    
      
      this._router.navigateByUrl('/main')
    },error=>{
      this.errorMessage= error.error.errors[0] as string;
    });
  }

}
