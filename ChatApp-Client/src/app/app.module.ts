import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {SharedModule} from './shared/shared/shared.module';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AvatarModule } from 'ngx-avatar';
import { HttpClientModule } from '@angular/common/http';
import{UserService} from './services/user/user.service';

import { ProfileComponent } from './profile/profile.component';
import { MainComponent } from './main/main.component';
import { IdentityService } from './services/identity/identity.service';
import { ChatService } from './services/chat/chat.service';
import { ChatRoomComponent } from './chat-room/chat-room.component';
import { RouterModule } from '@angular/router';

import {MatSidenavModule} from '@angular/material/sidenav';

import {ScrollingModule} from '@angular/cdk/scrolling';
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
  
    ProfileComponent,
  
    MainComponent,
  
    ChatRoomComponent,
  
  
    
   
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    SharedModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    AvatarModule,
    HttpClientModule,
    MatSidenavModule,
    ScrollingModule
  ],
  providers: [ UserService,IdentityService,ChatService],
  bootstrap: [AppComponent]
})
export class AppModule { }
