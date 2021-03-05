import { Component, OnInit } from '@angular/core';
import { ChatRoomModel } from '../models/chat-room-model';
import { ChatService } from '../services/chat/chat.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {


  constructor(public _chatService:ChatService) { }

  ngOnInit(): void {
  }

}
