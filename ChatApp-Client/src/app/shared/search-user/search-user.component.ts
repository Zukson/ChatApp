import { Component, OnInit } from '@angular/core';
import { ChatRoomModel } from 'src/app/models/chat-room-model';
import { ChatUserModel } from 'src/app/models/chat-user-model';
import { ChatService } from 'src/app/services/chat/chat.service';

@Component({
  selector: 'app-search-user',
  templateUrl: './search-user.component.html',
  styleUrls: ['./search-user.component.css']
})
export class SearchUserComponent implements OnInit {
friendName:string="";
 errorMessage="";
  constructor( private _chatService:ChatService) { }
 
  ngOnInit(): void {
  }


  create()
  {
    this.errorMessage='';
    console.log("tworze czat")

    this._chatService.createChatRoom(this.friendName).subscribe(response=>{
     
      
      this._chatService._userService.getUserThumbnail(response.FriendName).subscribe(blobResponse=>
        {
          let reader = new FileReader();

            reader.readAsDataURL(blobResponse)

            reader.onload=(event)=>{
              let user =<ChatUserModel>{Username:response.FriendName,UserThumbnail:event.target.result as string};
              let chatRoom = <ChatRoomModel>{ChatId:response.ChatRoomId,ChatUser:user,LastActivityDate:response.LastActivityDate}

              console.log(event);
              console.log('chatRoom',chatRoom);
              this._chatService.chatRooms.push(chatRoom);
            }
        },error=>{
          
          let user = <ChatUserModel>{Username:response.FriendName,UserThumbnail:'assets/default.png'}
          let chatRoom = <ChatRoomModel>{ChatId:response.ChatRoomId,ChatUser:user,LastActivityDate:response.LastActivityDate}
          this._chatService.chatRooms.push(chatRoom);
          
        })
      let chatRoom = <ChatRoomModel>{}         
    },error=>{
      this.errorMessage= error.error.error
    });
  }
  
}
