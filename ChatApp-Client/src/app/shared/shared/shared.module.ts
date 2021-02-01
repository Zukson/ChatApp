import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {MatToolbarModule} from '@angular/material/toolbar';
import { ToolbarComponent } from '../toolbar/toolbar.component';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatMenuModule} from '@angular/material/menu';
import { AppRoutingModule } from 'src/app/app-routing.module';
@NgModule({
  declarations: [ ToolbarComponent],
  imports: [
    CommonModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatMenuModule,
    AppRoutingModule
 
  ],
  exports:[ToolbarComponent]
})
export class SharedModule { }
