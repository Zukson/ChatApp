import { Injectable } from '@angular/core';
import { from, Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders, HttpResponse,HttpParams  } from '@angular/common/http';
import {UserModel} from '../../models/user-model';
import{IdentityService} from '../identity/identity.service';
@Injectable({
  providedIn: 'root'
})
export class UserService {

  userModel:UserModel;
  constructor( private _httpClient:HttpClient,private _identityService:IdentityService) {this.userModel=<UserModel>{} ;this.isAvatarSet=false;}
avatarUrl:string='assets/default.png';

isAvatarSet:boolean;


getDefaultAvatar()
{
  return this.avatarUrl;
}
  getUserAvatar() :Observable<Blob>
  {
    
      let headers = this._identityService.authorizeClient();


      return this._httpClient.get(environment.user.getAvatar,{headers:headers,responseType:'blob'  });
       
    
    
  }
  getUserInfo()  :Observable<any>
  {
    let headers = this._identityService.authorizeClient();
   return  this._httpClient.get(environment.user.getInfo,{headers:headers});

  }
  setUserAvatar(file)
  {
    console.log('setuUserAvatar')
    console.log(this._identityService.authorizeClient());
    let headers = this._identityService.authorizeClient();
    console.log(headers);
    
   let formData =new FormData();
   formData.append('avatar',file)
    this._httpClient.post(environment.user.postAvatar,formData,{headers: headers}).subscribe(response=>{
      console.log(response)
     
    });
  }



 
}
