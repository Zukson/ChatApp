import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as SignalR from "@aspnet/signalr";
import { Observable, Subject } from 'rxjs';
import { CreateChatResponse } from 'src/app/apiResponses/create-chat-response';
import { ChatRoomModel } from 'src/app/models/chat-room-model';
import { environment } from 'src/environments/environment';
import { IdentityService } from '../identity/identity.service';
import {ChatUserModel} from '../../models/chat-user-model';
import { UserService } from '../user/user.service';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  chatRooms:ChatRoomModel[]=[];
  connectionId:string;
  
  message:Subject<string>

  
  connection
  constructor(private _identityService:IdentityService,public _userService:UserService,private _httpClient:HttpClient) { 

    this.message=new Subject<string>();
   
      this.connection= new
      
       SignalR.HubConnectionBuilder().withUrl(environment.chat.chathub).build();
     
       this.connection.start().then(()=>console.log('Nawiazuje polaczenie')).catch(err=>{console.log(err)});
    
       this.connection.on('receiveConnId',(conId)=>{this.receiveConId(conId)});
       this.connection.on('message')
    
   

  }

  createChatRoom( friendName:string) :Observable<CreateChatResponse>
  {
    console.log('create chatrrom request')
    let request :any= 
    {
      ConnectionId:this.connectionId,
      FriendName:friendName
      
       };
     let headers = this._identityService.authorizeClient();
         
     console.log(request);
   return  this._httpClient.post<CreateChatResponse>(environment.chat.createChat,request,{headers:headers})
  }

  receiveConId(conId)
  {
    console.log('received conId',conId);
    this.connectionId=conId;
      let headers = this._identityService.authorizeClient();
      let params = new HttpParams().set('connectionId', this.connectionId);
    this._httpClient.get(environment.chat.connectChats,{headers:headers,params:params}).subscribe(response=>console.log(response,'join chat'))
  }
  receiveMessage(msg)
  {
    this.message.next(msg);
  }
}
