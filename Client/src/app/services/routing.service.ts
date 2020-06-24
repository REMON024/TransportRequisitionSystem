import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import * as alaSQL from 'alasql';
import {  Subject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { Routing, ChildRouting } from '../Models/Routing';
import { TokenService } from './token.service';
import { map } from 'rxjs/operators';

const JSON_ROUTING_LOCATION = 'assets/route.json';

@Injectable({
  providedIn : 'root'
})
export class RoutingService {


    public JsoRouting =  new Subject<Routing[]>();

    constructor(
        private http: Http,
        private router: Router,
        private tokenValue: TokenService) {

    }

    // public GetRoutingList() : Routing[] {
    //     this.GetJsonReouting().subscribe((res: Routing[]) => {
    //         return res;
    //     })
    // }

    public GetJsonReouting() : Observable<Routing[]>{
        return this.http.get(JSON_ROUTING_LOCATION).pipe(map((res => res.json())));
        
    }

    public async GetRoutingByAccessRight(accessRight: string): Promise<Routing[]> {
        var routing = new Array<Routing>();
       this.GetJsonReouting().subscribe(res => {
        Promise.all(res.map(async (route) => {
            var sqlResult = alaSQL('SELECT _ FROM ? WHERE _ = ?', [route.AccessRights, accessRight]);
                if(sqlResult.length > 0) {
                    this.GetDeepRouting(accessRight, route).then(res => {
                        routing.push(res);
                    })
                };
          }));

       })
       return routing;
       
    }

    private GetDeepRouting(accessRight: string, routing: Routing) : Promise<Routing> {
       return new Promise<Routing>((resolve, reject) => {
           var child = new Array<ChildRouting>();
           try {
            Promise.all(routing.Child.map(async (route) => {
                var sqlResult = alaSQL('SELECT _ FROM ? WHERE _ = ?', [route.AccessRights, accessRight]);
                    if(sqlResult.length > 0) {
                        child.push(route);
                    };
              }));
           }catch{

           }
          routing.Child = child.length > 0 ? child : null;
          resolve(routing);
       });
    }
}