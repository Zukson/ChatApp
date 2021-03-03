import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import {UserService} from '../services/user/user.service';
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
avatarUrl='';
login=''
  constructor(public  _userService:UserService,private sanitizer: DomSanitizer) { }
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
  ngOnChanges()
  {
   
  }
    ngOnInit(): void {
      if(!this._userService.userModel.UserName)
      {
        this._userService.getUserInfo().subscribe(response=>
         {
           this.login=response.UserName;
           console.log('odpowiedz logowania',response);
           let username = response.username
           console.log(username);
           this._userService.userModel.Email=response.email
           this._userService.userModel.UserName=response.username
           console.log("uzytkwownik po prawidlowylowym zalogowaniu ",this._userService.userModel)
           this.login=this._userService.userModel.UserName;
           console.log(this.login);
         });;
      }
     this.setAvatar();
     
     
  }
  ngAfterViewInit() {
    document.getElementsByClassName('mat-card-header-text')[0].setAttribute('style', 'margin: auto');
  }
  setAvatar()
  {
    console.log('setting')
    console.log(this._userService.isAvatarSet);
    if(this._userService.isAvatarSet)
    {
      this.avatarUrl=this._userService.getDefaultAvatar();
    }

    else{
    
      this._userService.getUserAvatar().subscribe(response=>
        {
          let reader = new FileReader();

    reader.readAsDataURL(response);
    
    reader.onload=(event)=>{
      console.log('laduje')
      this.avatarUrl=event.target?.result as string 
    
    }},error=>{
      console.log('error');
      this.avatarUrl='assets/default.png'
    }
        )
    }
    console.log('ustawiam')
    
  }
}


