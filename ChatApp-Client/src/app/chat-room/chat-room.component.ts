import { Component, OnInit } from '@angular/core';
import { ChatUserModel } from '../models/chat-user-model';
import { MessageModel } from '../models/message-model';

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
  constructor() { }

  ngOnInit(): void {
    
  }

}
