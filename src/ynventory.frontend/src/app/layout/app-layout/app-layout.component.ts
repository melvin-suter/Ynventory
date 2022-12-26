import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-app-layout',
  templateUrl: './app-layout.component.html',
  styleUrls: ['./app-layout.component.scss']
})
export class AppLayoutComponent implements OnInit {

  sidebarVisible:boolean = false;

  constructor() { }

  ngOnInit(): void {}

  toggleSidebar(){
    this.sidebarVisible = !this.sidebarVisible;
  }
  

  isLargeView():boolean{
    let width = window.innerWidth;
    return width > 720;
  }

}
