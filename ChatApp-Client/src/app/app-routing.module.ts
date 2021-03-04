import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import{RegisterComponent} from './register/register.component';
import { ProfileComponent } from './profile/profile.component';
import{MainComponent} from './main/main.component'
import { ChatRoomComponent } from './chat-room/chat-room.component';
const routes: Routes = [{path:'register',component:RegisterComponent},
{path:'login',component:LoginComponent},{
  path:'',component:LoginComponent},
  {path:'main',component:MainComponent,children:[{path:'profile',component:ProfileComponent},{path:'',component:ProfileComponent},  {path:'chatroom',component:ChatRoomComponent} ]},
 

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
