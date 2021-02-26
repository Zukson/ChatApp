import { Injectable } from '@angular/core';
import { from, Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import{UserModel} from '../../models/user-model'
import{IdentityService} from '../identity/identity.service';
@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor( private _httpClient:HttpClient,private _identityService:IdentityService) { }
avatarUrl:string='assets/default.jpg';

  getUserAvatar()
  {
    let headers = this._identityService.authorizeClient();

this._httpClient.get(environment.user.getAvatar,{headers:headers,responseType:'blob'}).subscribe(response=>
  {
    let reader = new FileReader();

      reader.readAsDataURL(response);
      
      reader.onload=(event)=>{
     
        this.avatarUrl=event.target.result as string ;
  },error=>
  {
console.log(error);
  }});

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
