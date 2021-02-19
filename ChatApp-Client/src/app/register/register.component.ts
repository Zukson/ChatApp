import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder,FormGroup,Validators } from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';
import { UserModel } from '../models/user-model';
import {UserService} from '../services/user/user.service';
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
  constructor(private _formBuilder: FormBuilder,private _userService:UserService)
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
avatarClicked()
{
  document.getElementById('myInput')?.click();
}
 register()
{
if(this.firstFormGroup.invalid)
{
  console.log(this.stepper);
 this.stepper.previous();
 this.stepper.previous();
}
else
{
  console.log('sending')
  console.log(this.firstFormGroup.controls['username'])
  this.userModel.email=this.firstFormGroup.controls['email'].value;
this.userModel.username=this.firstFormGroup.controls['username'].value;
this.userModel.password=this.firstFormGroup.controls['password'].value;
this._userService.registerUser(this.userModel).subscribe(response=>console.log(response));
}

}

}
