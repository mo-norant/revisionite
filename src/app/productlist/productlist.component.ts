import { ProductProjection } from './../models';
import { MainService } from './../main.service';
import { Component, OnInit } from '@angular/core';
import { Product } from '../models';

@Component({
  selector: 'app-productlist',
  templateUrl: './productlist.component.html',
  styleUrls: ['./productlist.component.css']
})
export class ProductlistComponent implements OnInit {


  loading : boolean

  productprojection: ProductProjection[];

  productcount : number

  constructor(private mainservice: MainService) { }

  ngOnInit() {

    this.mainservice.GetProductsCount().subscribe(t => {
      this.productcount = t;

      this.mainservice.GetProducts(66, 3).subscribe(res => {
        this.productprojection = res;
      }, err => {
        if(err.status === 404){
          alert(err.error)
        }
      })
    })

    

  }

}
