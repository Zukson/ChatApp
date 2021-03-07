import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ChatService } from 'src/app/services/chat/chat.service';
import { IdentityService } from 'src/app/services/identity/identity.service';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-menulist',
  templateUrl: './menulist.component.html',
  styleUrls: ['./menulist.component.css']
})
export class MenulistComponent implements OnInit {

  constructor(private router:Router,private _identityService:IdentityService,private _userService:UserService,private _chatService:ChatService) { }

  ngOnInit(): void {
  }

  profile()
  {
    console.log(this.router.url,'url router')
    this.router.navigate(['main'])
  }
  logout()
  {
this._identityService.logout();
this._userService.logout();
this._chatService.logout();
this.router.navigate(['login']);
  }
}
