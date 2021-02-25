import { Component, OnInit } from '@angular/core';
import{IdentityService} from './services/identity/identity.service'
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  constructor(private _identityService:IdentityService,private _router:Router){}
  ngOnInit(): void {
    console.log(this._identityService,'identity')
    console.log(this._router,'router');
    if(this._identityService.isAuthorize)
    {
      this._router.navigate(['main']);
    }
    else{
      this._router.navigate(['login'])
    }
    this._router.navigate(['register']);
  
  }
  title = 'ChatApp-Client';
}
