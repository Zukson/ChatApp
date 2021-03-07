import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import * as SignalR from "@aspnet/signalr";
import { Observable, Subject } from 'rxjs';

import { ChatRoomModel } from 'src/app/models/chat-room-model';
import { environment } from 'src/environments/environment';
import { IdentityService } from '../identity/identity.service';
import {ChatUserModel} from '../../models/chat-user-model';
import { UserService } from '../user/user.service';
import { ChatResponse } from 'src/app/apiResponses/chat-response';
import { request } from 'http';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  chatRooms:Subject<ChatRoomModel>
  connectionId:string;
  
  message:Subject<string>

  
  connection
  constructor(private _identityService:IdentityService,public _userService:UserService,private _httpClient:HttpClient) { 

    this.chatRooms= new Subject<ChatRoomModel>();
    this.message=new Subject<string>();
   
      this.connection= new
      
       SignalR.HubConnectionBuilder().withUrl(environment.chat.chathub).build();
     
      // this.connection.start().then(()=>console.log('Nawiazuje polaczenie')).catch(err=>{console.log(err)});
    
       this.connection.on('receiveConnId',(conId)=>{this.receiveConId(conId)});
       this.connection.on('chatCreated',(data)=>this.chatCreated(data));
    
   
 console.log('connection',this.connection)
  }

  chatCreated(data)
  {
    console.log('chatCreated',data);

    this._userService.getUserThumbnail(data.userName).subscribe((blob)=>
    {
      let reader = new FileReader();

      reader.readAsDataURL(blob)

      reader.onload=(event)=>{
        let chatUser= <ChatUserModel>{Username:data.userName,UserThumbnail:event.target.result as string}

        let chatRoom = <ChatRoomModel>{ChatId:data.chatRoomId,ChatUser:chatUser}
        this.chatRooms.next(chatRoom);
      }
    },error=>
    {
      let chatUser= <ChatUserModel>{Username:data.userName,UserThumbnail:'assets/default.png'};

        let chatRoom = <ChatRoomModel>{ChatId:data.chatRoomId,ChatUser:chatUser}
        this.chatRooms.next(chatRoom);
    });
   
  } 

  
  getChatRooms() 
  {
let headers = this._identityService.authorizeClient();

let chatRooms = <ChatRoomModel[]>[];
this._httpClient.get<any>(environment.chat.getChatRooms,{headers:headers}).subscribe(response=>
  {
      console.log('get chats response',response.chats[0])

     
      response.chats.forEach(chatRoom => {
        this._userService.getUserThumbnail(chatRoom.friendName).subscribe((blob)=>{

          let reader = new FileReader();

          reader.readAsDataURL(blob)
    
          reader.onload=(event)=>{
           
           let chatUser = <ChatUserModel>{Username:chatRoom.friendName,UserThumbnail:event.target.result as string}

           let chat=  <ChatRoomModel>{ChatUser:chatUser,ChatId:chatRoom.chatRoomId,LastActivityDate:chatRoom.lastActivityDate}
           chatRooms.push(chat);
          }
        },error=>{
          let chatUser = <ChatUserModel>{Username:chatRoom.friendName,UserThumbnail:'assets/default.png'}

          let chat=  <ChatRoomModel>{ChatUser:chatUser,ChatId:chatRoom.chatRoomId,LastActivityDate:chatRoom.lastActivityDate}
          chatRooms.push(chat);
        })
      
      });


  },error=>{
    console.log('Get chatrooms err',error)
  })
return chatRooms;
  }
 
  createChatRoom( friendName:string) :Observable<ChatResponse>
  {
    console.log('create chatrrom request')
    let request :any= 
    {
      ConnectionId:this.connectionId,
      FriendName:friendName
      
       };
     let headers = this._identityService.authorizeClient();
         
     console.log('cretechatrequest',request);
   return  this._httpClient.post<ChatResponse>(environment.chat.createChat,request,{headers:headers})
  }

  getChatUsers(chatRoomId)

  {
    let params = new HttpParams().set('chatRoomId', chatRoomId);
    let headers= this._identityService.authorizeClient();

     this._httpClient.get(environment.chat.getChatUsers,{headers:headers,params:params}).subscribe(response=>
      {
        console.log(response);
      })

    
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

  logout()
  {
    this.connection.stop();
    console.log(this.connection,'to polaczenie')
  }

  login()
  {
    this.connection.start();
    console.log('startuje ponownie ' ,this.connection)
  }
}
