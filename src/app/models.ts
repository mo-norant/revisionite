export class ProductCategorie{

    constructor(public productCategoryID : number, public productCategoryTitle: string){}
}

export class Product{
        public productID: number
        public userID: string
        public productTitle: string
        public description: string
        public vieuws : number
        public price: number
        public create : Date
        public endTime: Date
        public base64: string
        public productCategories: ProductCategorie[]

}

