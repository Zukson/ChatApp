import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Console } from 'console';
import { Observable } from 'rxjs';
import { concatMap, last } from 'rxjs/operators';
import { MessageResponse } from '../apiResponses/message-response';
import { ChatUserModel } from '../models/chat-user-model';
import { MessageModel } from '../models/message-model';
import { ChatService } from '../services/chat/chat.service';
import { UserService } from '../services/user/user.service';

@Component({
  selector: 'app-chat-room',
  templateUrl: './chat-room.component.html',
  styleUrls: ['./chat-room.component.css']
})
export class ChatRoomComponent implements OnInit {
text:string="";
chatUsers:ChatUserModel[]=[];
  messages:MessageModel[] = [];
 chatroomId:string
  constructor(private router:Router,private _userService:UserService,private _chatService :ChatService) 
  { }

  ngOnInit(): void {
     
   this.chatroomId= this.router.url.split('/').pop();

   this._chatService.getChatUsers(this.chatroomId).subscribe(response=>{
     this.getUsersChat(response);

     
     
   },err=>{},()=>{
    
   });
 
 
  
   
  }

  mapUserToMessage(messageResponse:MessageResponse): MessageModel
  {

     console.log('mapUserToMessage',this.chatUsers)
   let nickname=messageResponse.SenderName;
   console.log(nickname);
    let user = this.chatUsers.find(x=>x.Username===nickname) 
   console.log(user,"founded user");
 
    let message = <MessageModel>{SendDate:messageResponse.SendDate,ChatUser:user,Text:messageResponse.Text}

    console.log('mapped message',message)
    return message;
  }
  getUsersChat(response:any[])
  {
   
        
      
      response.forEach(nickname=>{
        this._userService.getUserThumbnail(nickname).subscribe(blob=>{
          let reader = new FileReader();

          reader.readAsDataURL(blob)
    
          reader.onload=(event)=>{
            let chatUser= <ChatUserModel>{Username:nickname,UserThumbnail:event.target.result as string}
    
            this.chatUsers.push(chatUser);
           
          }

        },error=>{
          let chatUser= <ChatUserModel>{Username:nickname,UserThumbnail:'assets/default.png' as string};
          this.chatUsers.push(chatUser);
          console.log('chatusers',this.chatUsers)
        },()=>{console.log('completing getUsersChatInner')
      
      
        this._chatService.getMessages(this.chatroomId).subscribe(response=>{
          console.log('completing getUsersChatNgOnINt')
          console.log('response get messages',response)
        
        response.forEach(message=>{
        
          let mess = <MessageResponse>{SendDate:message.sendDate,SenderName:message.senderName,Text:message.text};
          console.log(mess,'mess');
        
          let mappedMessage=this.mapUserToMessage(mess);
           this.messages.push(mappedMessage);
        })
        
         })
      })
      })
   

  }
  mapUsersToMessages(messages:MessageResponse[])
  {
   
    messages.forEach(message=>{
 
      

      console.log('user',this.chatUsers[0])

      let mappedMessage : MessageModel={ ChatUser:this.chatUsers[0],Text:message.Text, SendDate:message.SendDate};
    
      this.messages.push(mappedMessage);
    })
  }
  sendMessage()
  {
    
    this._chatService.sendMessage(this.chatroomId,this.text)
    this.text='';
  }
}
