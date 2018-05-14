import { AuthService } from './auth.service';
import { Product, ProductCategorie } from './models';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Utils } from './utils';

@Injectable()
export class MainService {

  root : string;

  constructor(private http: HttpClient, private auth: AuthService) {

    this.root = Utils.getRoot();
   }


  public GetEmailUser(){
    return this.http.get(this.root + 'General/email', {headers : this.auth.getAuthorizationHeaders()});
  }


  public AddProduct(product: Product){
    return this.http.post<number>(this.root + 'Product', product, {headers : this.auth.getAuthorizationHeaders()})
  }

  public PostProductPhoto(file: File, id: number) {
    let formData: FormData = new FormData();
    formData.append('uploadFile', file);
    return this.http.post(this.root + "Product/File/"+id, formData,{headers : this.auth.getAuthorizationHeaders()});

  }

  public GetProducts(index : number, count : number){
    return this.http.get<Product[]>(this.root + 'Product?index=' + index + '&count=' + count, {headers : this.auth.getAuthorizationHeaders()});
  }

  public GetProductsCount(){
    return this.http.get<number>(this.root + 'Product/count', {headers : this.auth.getAuthorizationHeaders()});
  }

  public GetProduct(id : number){
    return this.http.get<Product>(this.root + '/Product/'+id, {headers : this.auth.getAuthorizationHeaders()});
  }

  public UpdateProduct(id: number, product: Product){


    return this.http.post(this.root + 'Product/update/' + id, product, {headers : this.auth.getAuthorizationHeaders()} )
  }

  public DeleteProduct(id: number, product: Product){
    return this.http.delete(this.root + 'Product/' + id, {headers : this.auth.getAuthorizationHeaders()} )

  }


  public GetProductCategoriesCount(){
    return this.http.get<number>(this.root + 'ProductCategory/count', {headers : this.auth.getAuthorizationHeaders()});
  }
  public GetProductCategories(index : number, count : number){
    return this.http.get<ProductCategorie[]>(this.root + 'ProductCategory?index=' + index + '&count=' + count, {headers : this.auth.getAuthorizationHeaders()});
  }

  public UpdateProductCategory(id : number, newtitle : string){
    return this.http.post(this.root + 'ProductCategory/'+id+"?newtitle="+newtitle, null,{headers : this.auth.getAuthorizationHeaders()});
  }

  public DeleteProduct(id: number){
    return this.http.delete(this.root + 'ProductCategory/' + id, {headers : this.auth.getAuthorizationHeaders()} )

  }
}
