import { AuthService } from './auth.service';
import { Product } from './models';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Utils } from './utils';

@Injectable()
export class MainService {

  root : string;

  constructor(private http: HttpClient, private auth: AuthService) {

    this.root = Utils.getRoot();
   }


  public AddProduct(product: Product){
    return this.http.post<Product>(this.root + '/Product', product, {headers : this.auth.getAuthorizationHeaders()})
  }

  public PostProductPhoto(file: File, id: number) {
    let formData: FormData = new FormData();
    formData.append('uploadFile', file);

    let headers = new HttpHeaders();
    headers.append('Authorization', 'Bearer ' + this.auth.getToken().access_token);
   // headers.append('Content-Type', 'multipart/form-data');
    headers.append('Accept', 'application/json')

    return this.http.post(this.root + "/Product/File/"+id, formData,{headers:headers});

  }
  

}
