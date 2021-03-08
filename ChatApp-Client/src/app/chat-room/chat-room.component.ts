import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Console } from 'console';
import { ChatUserModel } from '../models/chat-user-model';
import { MessageModel } from '../models/message-model';
import { ChatService } from '../services/chat/chat.service';

@Component({
  selector: 'app-chat-room',
  templateUrl: './chat-room.component.html',
  styleUrls: ['./chat-room.component.css']
})
export class ChatRoomComponent implements OnInit {
text:string="";
chatUsers:ChatUserModel[];
  messages:MessageModel[] = [];
 chatroomId:string
  constructor(private router:Router,private _chatService :ChatService) 
  { }

  ngOnInit(): void {
     
   this.chatroomId= this.router.url.split('/').pop();

   this.chatUsers= this._chatService.getChatUsers(this.chatroomId);
  }

  sendMessage()
  {
    this._chatService.sendMessage(this.chatroomId,this.text)
    this.text='';
  }
}
