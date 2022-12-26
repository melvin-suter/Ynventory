import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private router:Router) { }

  checkLogin():boolean {
    return document.cookie.indexOf("username") >= 0;
  }

  tryLogin(data:{username:string,password:string}):boolean {
    if(data.username == "admin" && data.password == "admin"){
      document.cookie = "username=admin"
      this.router.navigateByUrl('/home');
      return true;
    }
    return false;
  }

  getUsername(){
    if(this.checkLogin()){
      return document.cookie.substring(document.cookie.indexOf("username=") +9);
    }else {
      return "";
    }
  }

  logout(){
    document.cookie = "username=;Max-Age=-9999999";
    this.router.navigateByUrl('/login');
  }
}
