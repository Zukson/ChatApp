import { Component, OnInit } from '@angular/core';
import { ChatRoomModel } from 'src/app/models/chat-room-model';
import { ChatUserModel } from 'src/app/models/chat-user-model';
import { ChatService } from 'src/app/services/chat/chat.service';
import { Router,  ParamMap } from '@angular/router';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-search-user',
  templateUrl: './search-user.component.html',
  styleUrls: ['./search-user.component.css']
})
export class SearchUserComponent implements OnInit {
friendName:string="";
 errorMessage="";
  constructor(private dialogRef:MatDialogRef<SearchUserComponent>, private _chatService:ChatService,private _router:Router) { }
 
  ngOnInit(): void {
  }


  create()
  {
    this.errorMessage='';
    console.log("tworze czat")

    this._chatService.createChatRoom(this.friendName).subscribe(response=>{
     
      console.log('createChatResponse',response)
      this._chatService._userService.getUserThumbnail(response.friendName).subscribe(blobResponse=>
        {
          let reader = new FileReader();

            reader.readAsDataURL(blobResponse)

            reader.onload=(event)=>{
              let user =<ChatUserModel>{Username:response.friendName,UserThumbnail:event.target.result as string};
              let chatRoom = <ChatRoomModel>{ChatId:response.chatRoomId,ChatUser:user,LastActivityDate:response.lastActivityDate}

              console.log(event);
              console.log('chatRoom',chatRoom);
              this._chatService.chatRooms.next(chatRoom);
              
            this._router.navigate(['/main/chatRoom',response.chatRoomId]);
            this.dialogRef.close();
            }
        },error=>{
          
          console.log('user has not thumbnail');
          let user = <ChatUserModel>{Username:response.friendName,UserThumbnail:'assets/default.png'}
          let chatRoom = <ChatRoomModel>{ChatId:response.chatRoomId,ChatUser:user,LastActivityDate:response.lastActivityDate}
          this._chatService.chatRooms.next(chatRoom); 
          this._router.navigate(['/main/chatRoom',response.chatRoomId]);
          this.dialogRef.close();
        })
      let chatRoom = <ChatRoomModel>{}         
    },error=>{
      this.errorMessage= error.error.error
    });
  }
  
}
