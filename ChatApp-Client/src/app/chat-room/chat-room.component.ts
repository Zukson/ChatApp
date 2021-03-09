import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { ChangeDetectionStrategy,Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Console } from 'console';
import { Observable } from 'rxjs';
import { concatMap, last } from 'rxjs/operators';
import { MessageResponse } from '../apiResponses/message-response';
import { ChatUserModel } from '../models/chat-user-model';
import { MessageModel } from '../models/message-model';
import { ChatService } from '../services/chat/chat.service';
import { UserService } from '../services/user/user.service';
import { ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-chat-room',
  templateUrl: './chat-room.component.html',
  styleUrls: ['./chat-room.component.css'],
  
})
export class ChatRoomComponent implements OnInit {
text:string="";
chatUsers:ChatUserModel[]=[];
  messages:MessageModel[] = [];
 chatroomId:string
 messageRequested =0;
  constructor(private activeRoute: ActivatedRoute,private router:Router,private _userService:UserService,private _chatService :ChatService) 
  { }

  ngOnInit(): void {
     
  

    this.activeRoute.params.subscribe(routeParams => {
    
     this.chatroomId= routeParams.id
     console.log(this.chatroomId,'Router change')
     this.messageRequested=0;
        this.chatUsers=<ChatUserModel[]>[]
        this.messages=<MessageModel[]>[]
     this._chatService.getChatUsers(this.chatroomId).subscribe(response=>{
       console.log(response,'getchatUsersResponseWhenRouterChange')
      this.getUsersChat(response);
 
      
      
    },err=>{},()=>{
     
    });
    });
  
 
 
  
   
  }

  mapUserToMessage(messageResponse:MessageResponse): MessageModel
  {


   let nickname=messageResponse.SenderName;
   console.log(nickname);
    let user = this.chatUsers.find(x=>x.Username===nickname) 

 
    let message = <MessageModel>{SendDate:messageResponse.SendDate,ChatUser:user,Text:messageResponse.Text}

 
    return message;
  }
  getUsersChat(response:any[])
  {
   
        
      
      response.forEach(nickname=>{
        console.log('SignleNick',nickname)
        this._userService.getUserThumbnail(nickname).subscribe(blob=>{
          let reader = new FileReader();

          console.log('blob ktorzy pyszedl',blob)
          reader.readAsDataURL(blob)
    
          reader.onload=(event)=>{
            let chatUser= <ChatUserModel>{Username:nickname,UserThumbnail:event.target.result as string}
    
            this.chatUsers.push(chatUser);
           
          }

        },error=>{
          let chatUser= <ChatUserModel>{Username:nickname,UserThumbnail:'assets/default.png' as string};
          this.chatUsers.push(chatUser);
                console.log('error');   
                this.getMessages()
        },()=>{
      
            console.log('observable complete');
            this.getMessages()
    
      })
      })
   

  }
  getMessages()
  {
    
  

    if(this.messageRequested===0)
    {
      this._chatService.getMessages(this.chatroomId).subscribe(response=>{
          
        response.forEach(message=>{
        
          let mess = <MessageResponse>{SendDate:message.sendDate,SenderName:message.senderName,Text:message.text};
     
        
          let mappedMessage=this.mapUserToMessage(mess);
           this.messages.push(mappedMessage);
        })
        
         })
    }
   
       this.messageRequested++
  }
  mapUsersToMessages(messages:MessageResponse[])
  {
   
    messages.forEach(message=>{
 
      

    

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
