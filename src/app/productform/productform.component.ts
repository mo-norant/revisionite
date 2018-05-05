import { NavbarComponent } from './../navbar/navbar.component';
import { Router } from '@angular/router';
import { MainService } from './../main.service';
import { ComputervisionService } from './../computervision.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { ProductCategorie, Product } from '../models';

@Component({
  selector: 'app-productform',
  templateUrl: './productform.component.html',
  styleUrls: ['./productform.component.css']
})
export class ProductformComponent implements OnInit {


  productname: string
  form: FormGroup
  productcategories: ProductCategorie[];
  loading: boolean;
  file : File;

  postedproduct: Product;

  constructor(private formBuilder: FormBuilder, private visionapi: ComputervisionService, private mainservice: MainService, private router: Router) {

    this.form = this.formBuilder.group({
      price: ['', [Validators.required]],
      productname: ['', [Validators.required]],
      date: ['', [Validators.required]],
      description: ['', [Validators.required]],

    });

    
  }

  ngOnInit() {
  }


  uploadpicture($event){
   this.productcategories = null;
    this.loading = true;
    this.file = $event.files[0];
    this.visionapi.GetAnnotationsofFile($event.files[0]).subscribe(data => {
      this.productcategories = [];

      data[0].labelAnnotations.forEach(element => {

        if(element.score >= 0.75){
          this.productcategories.push(new ProductCategorie(element.description))
        }

      });
      this.loading = false;
    });
  }

  imageRemoved($event){
    console.log($event);
    this.productcategories = null;
    this.file = null;
  }


  postproduct(){

    this.loading = true;
    let product : Product = new Product();
    product.endTime = this.form.value.date,
    product.productTitle = this.form.value.productname,
    product.description = this.form.value.description,
    product.price = this.form.value.price;
    product.productcategories = this.productcategories;

    this.mainservice.AddProduct(product).subscribe(data => {
      this.postedproduct = data; 
      this.loading = false
      this.mainservice.PostProductPhoto(this.file, data.productID).subscribe(
        succes => {
          this.router.navigate(['dashboard'])
        }
      )
    
    
    }, err => this.loading = false, () => this.loading = false);

  }
  
}
