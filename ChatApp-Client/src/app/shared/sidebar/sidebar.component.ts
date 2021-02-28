import { Component, OnInit,ChangeDetectionStrategy } from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import {SearchUserComponent} from '../search-user/search-user.component'
@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  items = Array.from({length: 100000}).map((_, i) => `Item #${i}`);

  constructor(public dialog: MatDialog  ) { }
  openDialog() {
    this.dialog.open(SearchUserComponent);
  }
  ngOnInit(): void {
  }

}
