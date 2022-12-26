import { Component, OnInit } from '@angular/core';
import { MenuItem, PrimeNGConfig } from 'primeng/api';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  username:string = "";

  constructor(private authService:AuthService, private primengConfig: PrimeNGConfig) { }

  ngOnInit(): void {
    this.username = this.authService.getUsername();
    this.primengConfig.ripple = true;
  }

  logout(){
    console.log("logout");
    this.authService.logout();
  }

}
