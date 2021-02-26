import { Component, OnInit } from '@angular/core';
import {UserService} from '../services/user/user.service';
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
avatarUrl='';
  constructor(private _userService:UserService) { }
  avatarClicked()
  {
    document.getElementById('myInput')?.click();
  }
  avatarChanged($event)
  {
    console.log($event);


let reader = new FileReader();

reader.readAsDataURL($event.target.files[0]);

reader.onload=(event)=>{
  this.avatarUrl=event.target.result as string;
  console.log(event);
}
  }
    ngOnInit(): void {
     this.avatarUrl= this._userService.avatarUrl
  }

}
