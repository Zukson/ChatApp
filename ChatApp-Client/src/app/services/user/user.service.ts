import { Injectable } from '@angular/core';
import { from, Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import{UserModel} from '../../models/user-model'
@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor( private _httpClient:HttpClient) { }



  registerUser(userModel:UserModel) :Observable<object> 
  {
    return this._httpClient.post(environment.identity.register,userModel)


    
  }
}
