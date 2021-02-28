import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import{RegisterModel} from '../../models/register-model'
import { Observable } from 'rxjs/internal/Observable';
import{TokensModel} from '../../models/tokens-model';
import { stringify } from '@angular/compiler/src/util';
import{LoginModel} from '../../models/login-model';
@Injectable({
  providedIn: 'root'
})
export class IdentityService {

  isAuthorize:boolean=false

 tokens:TokensModel= {
   refreshToken:'',
 jwtToken:''}
  constructor( private _httpClient:HttpClient) { 

    console.log('identity constructor')
    if(localStorage.getItem('jwtToken'))
    {
      console.log('jwt is there')
      this.isAuthorize=true;
    }
    

    
  }
    registerUser(registerModel:RegisterModel) :Observable<TokensModel>
  {

   return  this._httpClient.post<TokensModel>(environment.identity.register,registerModel);

  }

  Login(loginModel:LoginModel) :Observable<TokensModel>
  {
  
 return    this._httpClient.post<TokensModel>(environment.identity.login,loginModel) 
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
      
      let headers = new HttpHeaders({'Authorization':`bearer ${this.tokens.jwtToken} `});
      
      
       
      return headers;
    }
}
