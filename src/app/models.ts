import { Product } from './models';
export class Models {
}

export class ProductCategorie{
    constructor( public productCategoryTitle :string){}
}

export class Product{
        public productID: number
         public userID: string
        public productTitle: string
        public description: string
        public views : number
        public price: number
        public create : Date
        public endTime: Date
        public uri: string
        public productcategories: ProductCategorie[]
    
}

export class ProductProjection{
  public product: Product
  public productcategories : ProductCategorie[]
}