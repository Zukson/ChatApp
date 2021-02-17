import { Component, OnInit } from '@angular/core';
import { FormBuilder,FormGroup,Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;
 isDisabled:boolean=true;
  constructor(private _formBuilder: FormBuilder)
   { 
    this.firstFormGroup = this._formBuilder.group({
      name: ['', Validators.required],
      email:['',[Validators.required,Validators.email]],
      password:['',[Validators.required,Validators.minLength(5)]]
    });
    this.secondFormGroup = this._formBuilder.group({
      secondCtrl: ['', Validators.required]
    });
  }
  ngOnInit(): void {

  }
 
  buttonState()
  {
         this.isDisabled=this.checkbuttonState();
  }
  checkbuttonState()
  { 
    console.log(this.firstFormGroup)
  return   this.firstFormGroup.invalid;
  
  }
avatarClicked()
{
  document.getElementById('myInput')?.click();
}


}
