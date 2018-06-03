import { Product } from './../models';
import { AuthService } from './../auth.service';
import { MainService } from './../main.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  loading : boolean
  products : Product[];
  itemsperpage: number = 5;
  productcount: number;
  index: number = 0;
  sorting : string;
  constructor(private mainservice : MainService, private auth: AuthService, private router : Router) {



   }

  ngOnInit() {
    this.getProducts();
  }

  updateItemsPerPage($event){

    this.getProducts();

  }

  paginate($event){
    this.itemsperpage = $event.rows;
    this.index = $event.page;
    this.onSort();
  }

  getProducts(){
    this.loading = true;
    this.mainservice.GetProductsCount().subscribe(t => {
      this.productcount = t;
      this.mainservice.GetProducts(this.index, this.itemsperpage, 'ProductID', false  ).subscribe(res => {        this.loading = false;
        this.products = res;
      }, err => {
        this.loading = false;
      })
    }, () => {
      this.loading = false;
    });
  }

  onSort(){
    this.loading = true;
    this.mainservice.GetProductsCount().subscribe(t => {
      this.productcount = t;
      this.mainservice.GetProducts(this.index, this.itemsperpage, this.sorting , true  ).subscribe(res => {        this.loading = false;
        this.products = res;
      }, err => {
        this.loading = false;
      })
    }, () => {
      this.loading = false;
    });
  }



}
