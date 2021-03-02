import { Component, OnInit } from '@angular/core';
import { ChatService } from '../services/chat/chat.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {

  constructor(private _chatService:ChatService) { }

  ngOnInit(): void {
  }

}
