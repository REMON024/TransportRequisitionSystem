import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class AppUtils {

    constructor() {}

    
    public GetSerial(index : number, pageNumber: number, pageSize: number) : number {
        return ((pageNumber - 1) * pageSize + (index + 1));
    }
}