import { Observable } from 'rxjs/Observable';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class ComputervisionService {


  apikey : string = "AIzaSyAlflzCys9izjOqyeXxbfjcY_kkD_wUmNI"

  baselink = 'https://vision.googleapis.com/v1/images:annotate'

  constructor(private http: HttpClient) { 


  }


  public GetAnnotationsofFile(file:File){



    return new Observable<any[]>((observer) => {

      var req = {
        requests:[
          {
            image:{
              content:""
            },
            features:[
              {
                type:"LABEL_DETECTION",
                maxResults:10
              }
            ]
          }
        ]
      }
  
  
      var reader = new FileReader();
  
      reader.addEventListener("load", (event:any) => {
      req.requests[0].image.content = event.target.result.replace('data:image/png;base64,','').replace('data:image/jpeg;base64,','').replace('data:image/jpg;base64,','');
      
       this.http.post<any>(this.baselink + "?key="+ this.apikey, req, {}).subscribe(data => {
        
        observer.next(data.responses);
        observer.complete();
        
        
       });
  
        }, false);
  
      reader.readAsDataURL(file);
      
  })
    

   

    

    
    
  }


}




