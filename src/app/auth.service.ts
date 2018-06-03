import { Injectable } from '@angular/core';
import { HttpHeaders, HttpParams, HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Utils } from './utils';

@Injectable()
export class AuthService {

  constructor(private http: HttpClient) {


    this.root = Utils.getRoot();

  }
  private root: string;

  helper : JwtHelperService = new JwtHelperService();

  private connectlink = 'connect/token'
  private client_id =  'AngularSPA'
  private grant_type =  'password'
  private scope  = 'WebAPI'


  public createUser(user : RegisterUser){
    return this.http.post(this.root.concat(Endpoints.RegisterUser), user)
  }

  public login(username: string, password: string){

    const body = new HttpParams()
    .set('username', username)
    .set('password', password)
    .set('scope', this.scope)
    .set('client_id', this.client_id)
    .set('grant_type', this.grant_type)


    return this.http.post<JWTToken>(this.root.replace("api/","") + this.connectlink, body.toString(), {
      headers: new HttpHeaders()
        .set('Content-Type', 'application/x-www-form-urlencoded')
    })

  }

  public signOut(){
    return this.http.post(this.root + "general/SignOut", null, { headers: this.getAuthorizationHeaders() });
  }

  public saveToken(token: JWTToken){
    this.startSession();
    localStorage.setItem('jwttoken', JSON.stringify(token));
  }

  public getToken() : JWTToken {
    if(this.hasToken()){
      return JSON.parse(localStorage.getItem('jwttoken'));
    }
    return null;
  }

  public tokenExpired(){

    if(!this.hasToken()){
      return true;
    }

    if(this.helper.isTokenExpired(this.getToken().access_token)){
      this.removeToken();
      this.finishSession();
      return true;
    }
    return false

  }

  public decodeToken(){
    return this.helper.decodeToken(this.getToken().access_token);
  }

  public hasToken(){
    if (localStorage.getItem("jwttoken") !== null) {
      return true
    }
    return false;
  }

  public removeToken(){
    localStorage.removeItem('jwttoken');
    this.finishSession();
  }

  public getAuthorizationHeaders() : HttpHeaders{
    return new HttpHeaders({
       'Authorization': 'Bearer ' + this.getToken().access_token
     })
 }

 public startSession(){
   sessionStorage.setItem('active', 'true');
 }

 public finishSession(){
   sessionStorage.removeItem('active');
 }

 public hasActiveSession(){
   if(sessionStorage.getItem('active') !== null){
     return true;
   }
   else{
     return false;
   }
 }

}

export class JWTToken{


  constructor(public access_token: string,
    public expires_in : number,
   public token_type: string){}
}


export class RegisterUser {
  constructor(public username: string,
    public password: string,
    public password2: string,
    public givenName: string,
    public familyName: string) { }


  toString() : String{
    return this.username
  }
}


export class Endpoints{
 static RegisterUser: string = "Identity/Create"
}
