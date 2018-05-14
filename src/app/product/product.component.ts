import { Product } from './../models';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MainService } from '../main.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  form: FormGroup
  product: Product;
  loading: boolean;
  changed: boolean;
  ID: number;
  constructor(private formBuilder: FormBuilder,  private mainservice: MainService, private router: Router,
    private route: ActivatedRoute,  ) { 
      
    }

  ngOnInit() {

    this.route.params.subscribe( params => {
      this.mainservice.GetProduct(params['id']).subscribe(res => {
        this.ID = params['id'];
        this.product = res;
        this.form = this.formBuilder.group({
          price: [this.product.price, [Validators.required]],
          productname: [this.product.productTitle, [Validators.required]],
          date: [new Date(this.product.endTime), [Validators.required]],
          description: [this.product.description, [Validators.required]],
          productcategories: [this.product.productCategories, [Validators.required]]
        });
        this.onChanges();


      });
    });

   

   
   }

   back(){
        this.router.navigate(['products'])
   }

   update(){
      this.mainservice.UpdateProduct(this.ID, this.product).subscribe(res => {
        this.router.navigate(['products'])
      })
     }

   delete(){
    this.mainservice.DeleteProduct(this.ID, this.product).subscribe(res => {
      console.log('product verwijderd')
      this.router.navigate(['products'])
    })
   }

   private onChanges(): void {
    this.form.valueChanges.subscribe(val => {
      this.changedstate();
      this.product = val;

    });
  }

  private changedstate(){
    if(this.form.valid){
      this.changed = true;

    }else{
      this.changed = false;
    }
  }


    

  


  }


