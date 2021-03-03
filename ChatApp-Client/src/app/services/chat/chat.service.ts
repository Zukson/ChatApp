import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as SignalR from "@aspnet/signalr";
import { Observable, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IdentityService } from '../identity/identity.service';


@Injectable({
  providedIn: 'root'
})
export class ChatService {


  connectionId:string;
  
  message:Subject<string>
  connection
  constructor(private _identityService:IdentityService,private _httpClient:HttpClient) { 

    this.message=new Subject<string>();
  this.connection= new
   SignalR.HubConnectionBuilder().withUrl(environment.chat.chathub).build();

   this.connection.start().then(()=>console.log('Nawiazuje polaczenie')).catch(err=>{console.log(err)});

   this.connection.on('receiveConnId',(conId)=>{console.log(conId);this.connectionId=conId});
   this.connection.on('message')

  }

  createChatRoom( friendName:string) :Observable<string>
  {
    console.log('create chatrrom request')
    let request :any= 
    {
      ConnectionId:this.connectionId,
      FriendName:friendName
      
       };
     let headers = this._identityService.authorizeClient();
         
     console.log(request);
    return  this._httpClient.post<string>(environment.chat.createChat,request,{headers:headers});
  }

  receivedMessage(msg)
  {
    this.message.next(msg);
  }
}
