import { Component, OnInit } from '@angular/core';
import { ChatService } from 'src/app/services/chat/chat.service';

@Component({
  selector: 'app-search-user',
  templateUrl: './search-user.component.html',
  styleUrls: ['./search-user.component.css']
})
export class SearchUserComponent implements OnInit {
friendName:string="";
 errorMessage="";
  constructor(private _chatService:ChatService) { }
 
  ngOnInit(): void {
  }

  create()
  {
    this.errorMessage='';
    console.log("tworze czat")

    this._chatService.createChatRoom(this.friendName).subscribe(response=>{
        console.log("tworzenie czatu odpowiedz")
        console.log(response)
    },error=>{
        console.log('blad')
        console.log(error);
        this.errorMessage=error.error.error
    })
  }
  
}
