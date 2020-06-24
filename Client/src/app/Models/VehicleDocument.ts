export class VehicleDocument {
    constructor()
    {
      this.ValidDate=  new  Date().toISOString().slice(0,10);
    this.RenewalDate=new  Date().toISOString().slice(0,10);
      
    }
    VehicleDocumentID: number;
    VehicleID: number;
    DocumentName: string;
    ValidDate: string;
    RenewalDate: string;
}