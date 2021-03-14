import { Component, OnInit } from '@angular/core';
import{IdentityService} from './services/identity/identity.service'
import { Router, ActivatedRoute, ParamMap, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit  {
  constructor(private _identityService:IdentityService,private _router:Router){}
 
  ngOnInit(): void {
    
    
  
  }
  title = 'ChatApp-Client';
}
