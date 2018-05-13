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
  itemsperpage: number = 5;
  index: number = 0;
  products: Product[];
  productcount : number
  constructor(private mainservice: MainService) { }

  ngOnInit() {

    this.getProducts();
    
  }

  updateItemsPerPage($event){

    this.getProducts();

  }

  paginate($event){
    this.itemsperpage = $event.rows;
    this.index = $event.page;
    this.getProducts();
  }

  getProducts(){
    this.loading = true;
    this.mainservice.GetProductsCount().subscribe(t => {
      this.productcount = t;
      this.mainservice.GetProducts(this.index, this.itemsperpage).subscribe(res => {
        console.log(res);
        this.loading = false;
        this.products = res;
      }, err => {
        this.loading = false;
      })
    }, () => {
      this.loading = false;
    });
  }

onRowSelect($event){
  console.log($event)
}

}
