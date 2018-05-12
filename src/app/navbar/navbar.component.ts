import { MainService } from './../main.service';
import { AuthService } from './../auth.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  email;

  constructor(private auth: AuthService, private router: Router, private mainservice: MainService) { }

  ngOnInit() {

    this.mainservice.GetEmailUser().subscribe(data => {
      this.email = data;
    })
  
  }


  logout(){

    this.auth.removeToken();
    this.router.navigate(['/'])
  }
}
