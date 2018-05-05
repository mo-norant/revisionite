import { Router } from '@angular/router';
import { AuthService } from './../auth.service';
import { MainService } from './../main.service';
import { Component, OnInit, Input, Output, EventEmitter, OnDestroy } from '@angular/core'; //importing output and eventEmitter
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {

  err: string;
  form: FormGroup;

  constructor(private mainservice: MainService, private auth: AuthService, private router: Router) {

    if(!this.auth.tokenExpired()){
        if(this.auth.hasActiveSession){
          this.routeToDashboard();
        }
    }

    this.form = new FormGroup({
      email: new FormControl('', []),
      password: new FormControl(''),
    });
    
   }

  ngOnInit() {


  }


  ngOnDestroy() {
  }


  login() {

      this.auth.login(this.form.value.email, this.form.value.password).subscribe(data => {
        this.auth.saveToken(data);
        this.routeToDashboard();
      });

  }
  keyDownFunction(event) {
    if (event.keyCode == 13) {
      this.login();
    }
  }

  validateEmail(email) {
    //var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
   //return re.test(String(email).toLowerCase());
  }
  errcorrected(){
    this.err = null;
  }

  private routeToDashboard(){
    this.router.navigate(['dashboard']);
  }

}
