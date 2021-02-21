import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import{UserModel} from '../../models/user-model'
import { Observable } from 'rxjs/internal/Observable';
import{TokensModel} from '../../models/tokens-model';
import { stringify } from '@angular/compiler/src/util';
@Injectable({
  providedIn: 'root'
})
export class IdentityService {

userModel:UserModel;
 tokens:TokensModel;
  constructor( private _httpClient:HttpClient) { }
    registerUser(userModel:UserModel) 
  {

    return this._httpClient.post<TokensModel>(environment.identity.register,userModel).subscribe(response=>{

      this.tokens.jwtToken=response.jwtToken;
      this.tokens.refreshToken=response.jwtToken
      localStorage.setItem('refreshToken',this.tokens.refreshToken)
      sessionStorage.setItem('jwtToken',this.tokens.refreshToken);
      this.userModel.email=userModel.email;
      this.userModel.password=userModel.password;
      this.userModel.username=userModel.username;
      
    })

  }

async  Login(userModel:UserModel) : Promise<void>
  {
  
    this._httpClient.post<TokensModel>(environment.identity.login,userModel).subscribe(response=>{


      this.tokens.jwtToken=response.jwtToken;
      this.tokens.refreshToken=response.jwtToken
      localStorage.setItem('refreshToken',this.tokens.refreshToken)
   localStorage.setItem('jwtToken',this.tokens.refreshToken);
      this.userModel.email=userModel.email;
      this.userModel.password=userModel.password;
      this.userModel.username=userModel.username;

    })
  }


 
 async getAuthorizationToken() :Promise<string>
  {
     
      if(!this.tokens.jwtToken)
      {
          if(!localStorage.getItem('jwtToken'))
          {
           await this.refreshToken();

           return this.tokens.jwtToken;
          }

          else{
            return localStorage.getItem('jwtToken');
          }
      }
      else
      {
        return this.tokens.jwtToken;
      }
    }


   async refreshToken() : Promise<void>
    {
        if(!this.tokens.refreshToken)
        {
          this.tokens.refreshToken=localStorage.getItem('refreshToken');
          this.tokens.jwtToken=localStorage.getItem('jwtToken');
        }
        
        this._httpClient.post<TokensModel>(environment.identity.refreshToken,this.tokens).subscribe(response=>{

          this.tokens.jwtToken=response.jwtToken;
      this.tokens.refreshToken=response.jwtToken
      localStorage.setItem('refreshToken',this.tokens.refreshToken)
   localStorage.setItem('jwtToken',this.tokens.refreshToken);
        });
        
    }

    authorizeClient()
    {
      
      let headers = new HttpHeaders();
      headers.set('Authorization',`bearer ${this.tokens.jwtToken} `);

      return headers;
    }
}
