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
@NgModule({
  declarations: [ ToolbarComponent,MenulistComponent],
  imports: [
    CommonModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatMenuModule,
    AppRoutingModule,
    MatInputModule,
    MatStepperModule
  ],
  exports:[ToolbarComponent,MenulistComponent,  MatButtonModule, MatInputModule,MatStepperModule]
})
export class SharedModule { }
