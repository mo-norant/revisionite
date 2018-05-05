import { ComputervisionService } from './../computervision.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { ProductCategories } from '../models';

@Component({
  selector: 'app-productform',
  templateUrl: './productform.component.html',
  styleUrls: ['./productform.component.css']
})
export class ProductformComponent implements OnInit {


  productname: string
  form: FormGroup
  productcategories: ProductCategories[];

  constructor(private formBuilder: FormBuilder, private visionapi: ComputervisionService) {

    this.form = this.formBuilder.group({
      price: ['', [Validators.required]],
      productname: ['', [Validators.required]],
      date: ['', [Validators.required]]
    });


  }

  ngOnInit() {
  }


  uploadpicture($event){
    this.visionapi.GetAnnotationsofFile($event.files[0]).subscribe(data => {
     this.productcategories = []
      data[0].labelAnnotations.forEach(element => {

        if(element.score >= 0.75){
          this.productcategories.push(new ProductCategories(element.description))
        }

      });
      
    });
  }

  imageRemoved($event){
    console.log($event);
    this.productcategories = null;
  }

}
