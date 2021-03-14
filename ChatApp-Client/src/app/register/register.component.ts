import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder,FormGroup,Validators } from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';
import { RegisterModel } from '../models/register-model';
import {UserService} from '../services/user/user.service';
import{IdentityService} from '../services/identity/identity.service';
import{TokensModel} from '../models/tokens-model';
import {catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';
import {Router} from '@angular/router';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @ViewChild('stepper') stepper;
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
 isDisabled:boolean=true;
 registerModel:RegisterModel;
 avatar:HTMLImageElement;
 avatarUrl:string='assets/default.png';

 errorMessage:string=''
 usernameErrorExists:boolean=false
 emailExists:boolean=false
 
  constructor(private _formBuilder: FormBuilder,private _userService:UserService,private _identityService:IdentityService,private _router:Router)
   { 
    this.firstFormGroup = this._formBuilder.group({
      username: ['', Validators.required],
      email:['',[Validators.required,Validators.email]],
      password:['',[Validators.required,Validators.minLength(5)]]
    });
    this.secondFormGroup = this._formBuilder.group({
      secondCtrl: ['', Validators.required]
    });
    this.registerModel= <RegisterModel>{}  }
  ngOnInit(): void {

  }
 
  buttonState()
  {
         this.isDisabled=this.checkbuttonState();
  }
  checkbuttonState()
  { 
   
  return   this.firstFormGroup.invalid;
  
  }
  avatarChanged($event)
  {
    console.log($event);
this.avatar = $event.target.files[0];

let reader = new FileReader();

reader.readAsDataURL($event.target.files[0]);

reader.onload=(event)=>{
  this.avatarUrl=event.target.result as string;
  console.log(event);
}

  }
avatarClicked()
{
  document.getElementById('myInput')?.click();
}
  register()
{
  this.errorMessage='';
  console.log('rejestruje');
if(this.firstFormGroup.invalid)
{
this.resetStepper();
  console.log(this.stepper);
 
}
else
{

  console.log('spelnia warunki walidacji');
  this.registerModel.email=this.firstFormGroup.controls['email'].value;
this.registerModel.username=this.firstFormGroup.controls['username'].value;
this.registerModel.password=this.firstFormGroup.controls['password'].value;

 this._identityService.registerUser(this.registerModel).subscribe(response=>{
   this.SuccesResponse(response);
  
  
},error=>
{
  console.log(error);
 this.errorMessage= error.error.errors[0] as string;
 this.resetStepper();
 

});



}

}
SuccesResponse(response:TokensModel)

{
  console.log(response);
  this._identityService.tokens.jwtToken=response.jwtToken;
  this._identityService.tokens.refreshToken=response.refreshToken
  localStorage.setItem('refreshToken',this._identityService.tokens.refreshToken)
  localStorage.setItem('jwtToken',this._identityService.tokens.jwtToken);
  
  this._identityService.isAuthorize=true;
  console.log(this._identityService.tokens);
  if(this.avatar )
  {
    console.log(this.avatar); 
  console.log('sending avatar');
  this._userService.setUserAvatar(this.avatar);
  this._userService.avatarUrl=this.avatarUrl;
  this._userService.isAvatarSet=true;
  

}
this._userService.userModel.UserName=this.registerModel.username=this.firstFormGroup.controls['username'].value;
  this._userService.userModel.Email=
  this.registerModel.email=this.firstFormGroup.controls['email'].value;
this._router.navigateByUrl('/main')


}
resetStepper()
{
  this.stepper.previous();
  this.stepper.previous();
}
}
