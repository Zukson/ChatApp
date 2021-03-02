import { Injectable } from '@angular/core';
import * as SignalR from "@aspnet/signalr";
import { environment } from 'src/environments/environment';
import { IdentityService } from '../identity/identity.service';


@Injectable({
  providedIn: 'root'
})
export class ChatService {


  connectionId:string;
  
  connection
  constructor(private _identityService:IdentityService) { 

  this.connection= new
   SignalR.HubConnectionBuilder().withUrl(environment.chat.chathub).build();

   this.connection.start().then(()=>console.log('Nawiazuje polaczenie')).catch(err=>{console.log(err)});

   this.connection.on('receiveConnId',(conId)=>{console.log(conId)});

  }
}
