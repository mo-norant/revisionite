import { MainService } from './../main.service';
import { ProductCategorie } from './../models';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-productcategories',
  templateUrl: './productcategories.component.html',
  styleUrls: ['./productcategories.component.css']
})
export class ProductcategoriesComponent implements OnInit {


  cols = [
    { field: 'productCategoryID', header: 'ID' },
    { field: 'productCategoryTitle', header: 'Title' }
];

productcategories : ProductCategorie[]
count : number;
selectedcategory : ProductCategorie;
displayDialog: true;
dialogcategory: ProductCategorie;
itemsperpage: number = 5;
index: number = 0;
productcount : number;


  constructor(private mainservice: MainService) { }

  ngOnInit() {
    this.getProductsCats();
  }


 

edit($event){
  this.mainservice.UpdateProductCategory($event.data.productCategoryID, $event.data.productCategoryTitle).subscribe(data =>{
    
  })
}

paginate($event){
  this.itemsperpage = $event.rows;
  this.index = $event.page;
  this.getProductsCats();
}

private getProductsCats(){

  this.mainservice.GetProductCategoriesCount().subscribe(t => {
    this.productcount = t;
    this.mainservice.GetProductCategories(this.index, this.itemsperpage).subscribe(res => {
      this.productcategories = res;
    }, err => {
    })
  }, () => {
  });
}

delete(item) {
  this.mainservice.DeleteProductCategory(item.productCategoryID).subscribe(data => {
    this.productcategories = [];
    this.getProductsCats();

  });
}

}
