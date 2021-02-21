import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder,FormGroup,Validators } from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';
import { UserModel } from '../models/user-model';
import {UserService} from '../services/user/user.service';
import{IdentityService} from '../services/identity/identity.service';
import{TokensModel} from '../models/tokens-model';
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
 userModel:UserModel;
 avatar:HTMLImageElement;
 avatarUrl:string='assets/avatar.jpg';
  constructor(private _formBuilder: FormBuilder,private _userService:UserService,private _identityService:IdentityService)
   { 
    this.firstFormGroup = this._formBuilder.group({
      username: ['', Validators.required],
      email:['',[Validators.required,Validators.email]],
      password:['',[Validators.required,Validators.minLength(5)]]
    });
    this.secondFormGroup = this._formBuilder.group({
      secondCtrl: ['', Validators.required]
    });
    this.userModel= <UserModel>{}  }
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
  async register()
{
if(this.firstFormGroup.invalid)
{
  console.log(this.stepper);
 this.stepper.previous();
 this.stepper.previous();
}
else
{

  this.userModel.email=this.firstFormGroup.controls['email'].value;
this.userModel.username=this.firstFormGroup.controls['username'].value;
this.userModel.password=this.firstFormGroup.controls['password'].value;

await this._identityService.registerUser(this.userModel);

if(!this.avatar )
{
  console.log('sending avatar');
  await this._userService.setUserAvatar(this.avatar);
}

}
}
}
