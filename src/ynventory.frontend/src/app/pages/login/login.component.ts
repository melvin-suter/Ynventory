import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginData:{username:string,password:string} = {username: "", password: ""}
  lastTry:boolean = false;

  constructor(private authService:AuthService) { }

  ngOnInit(): void {
  }

  login() {
    this.lastTry = !this.authService.tryLogin(this.loginData);
  }

}
