import { Injectable } from '@angular/core';
import { from, Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import{UserModel} from '../../models/user-model'
import{IdentityService} from '../identity/identity.service';
@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor( private _httpClient:HttpClient,private _identityService:IdentityService) { }


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
