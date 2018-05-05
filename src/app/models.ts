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

{
    "productID": 0,
    "userID": "string",
    "productTitle": "string",
    "description": "string",
    "views": 0,
    "price": 0,
    "create": "2018-05-05T19:51:22.399Z",
    "endTime": "2018-05-05T19:51:22.399Z",
    "uri": "string",
    "productProductCategories": [
      {
        "productID": 0,
        "productCategoryID": 0,
        "productCategory": {
          "productCategoryID": 0,
          "productCategoryTitle": "string",
          "date": "2018-05-05T19:51:22.399Z",
          "productProductCategories": [
            null
          ]
        }
      }
    ]
  }
