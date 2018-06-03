export class Utils {

static      devlink = 'http://localhost:5000/api/'
static     productionlink = 'http://localhost/api/'
static     inDevelopment : boolean = true;

     static getRoot(): any {

        if(this.inDevelopment){
            return this.devlink
        }

        return this.productionlink;
        }
}
