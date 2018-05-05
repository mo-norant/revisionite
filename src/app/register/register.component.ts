import { AuthService, RegisterUser } from './../auth.service';
import { MainService } from './../main.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {


  registerForm: FormGroup;
  user: RegisterUser;
  err;
  loading : boolean;

  constructor(private auth: AuthService,private formBuilder: FormBuilder,
  private router: Router) {

   }

  ngOnInit() {

    this.registerForm = this.formBuilder.group({
      givenname: ['', Validators.required],
      familyname:  ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      passwordvalidate: ['', [Validators.required]]
  });

  }


  register(){

    if(this.checkPasswords(this.registerForm.value.password, this.registerForm.value.passwordvalidate)){
      this.user = new RegisterUser(this.registerForm.value.email,this.registerForm.value.password, this.registerForm.value.passwordvalidate, this.registerForm.value.givenname, this.registerForm.value.familyname);
      this.auth.createUser(this.user).subscribe(data => {
        this.auth.login(this.registerForm.value.email, this.registerForm.value.password).subscribe( token => {
          this.auth.saveToken(token);
          this.router.navigate(['/dashboard'])

        })
      })
    }


  }
  private checkPasswords(password: string, password2: string){
    if(password === password2){
        return true
    }
    return false;
}

}
