import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatToolbarModule} from '@angular/material/toolbar';
import { ToolbarComponent } from '../toolbar/toolbar.component';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatMenuModule} from '@angular/material/menu';
import { AppRoutingModule } from 'src/app/app-routing.module';
import {MenulistComponent} from '../menulist/menulist.component';
import {MatInputModule} from '@angular/material/input';
import {MatStepperModule} from '@angular/material/stepper';
import { SidebarComponent } from '../sidebar/sidebar.component';
import {MatSidenavModule} from '@angular/material/sidenav';
import {ScrollingModule} from '@angular/cdk/scrolling';
import {MatCardModule} from '@angular/material/card';
@NgModule({
  declarations: [ ToolbarComponent,MenulistComponent,SidebarComponent],
  imports: [
    CommonModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatMenuModule,
    AppRoutingModule,
    MatInputModule,
    MatStepperModule,
    MatSidenavModule,
    ScrollingModule,
    MatCardModule
  ],
  exports:[SidebarComponent,ToolbarComponent,MenulistComponent,  MatButtonModule, MatInputModule,MatStepperModule,ScrollingModule,MatCardModule]
})
export class SharedModule { }
