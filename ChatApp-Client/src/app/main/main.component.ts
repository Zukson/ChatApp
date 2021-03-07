import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ChatRoomModel } from '../models/chat-room-model';
import { ChatService } from '../services/chat/chat.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {

  chatRooms:ChatRoomModel[]=[];


  constructor(public _chatService:ChatService,private router:Router) { }

  ngOnInit(): void {
    this._chatService.login();
    this._chatService.chatRooms.subscribe((chatRoom)=>{
      console.log('dostaje chatroom',chatRoom)
      this.chatRooms.unshift(chatRoom);
    
    }
    
    
    )
  
 this.chatRooms= this._chatService.getChatRooms();
   
 
  }

  navigate(chatroom)
  {
    this.router.navigate(['/main/chatRoom',chatroom.ChatId]);
    
  }

}
