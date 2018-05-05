export class Utils {

static      devlink = 'http://10.211.55.3:45457/api/'
static     productionlink = 'http://localhost/api/'
static     inDevelopment : boolean = true;

     static getRoot(): any {

        if(this.inDevelopment){
            return this.devlink
        }
    
        return this.productionlink;
        }
}
