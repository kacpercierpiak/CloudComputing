import { Customer } from "./Customer";

export class CustomerCars extends Customer {
    Cars:Car[];
 

    constructor(firstName: string, lastName: string, phone:string,cars?:Car[],oId?: string ) {
        if(oId)
        oId = '';
        super(oId,firstName,lastName,phone)
        this.Cars = cars;
    }

}

export class Car {
    brand: string;
    engine: string;      
    fuelType: FuelTypes;
    manualGearbox: boolean;
    model:string;
    numberPlate: string;
    productionDate:Date;
    vinNo:string;
    carHistories?: CarHistory[];

 

    constructor(Brand: string, Engine: string, FuelType: FuelTypes, ManualGearbox:boolean, Model: string, NumberPlate: string, ProductionDate: Date,VinNo: string, CarHistories?: CarHistory[], ) {
        this.brand = Brand;
        this.engine = Engine;
        this.fuelType = FuelType;
        this.manualGearbox = ManualGearbox;   
        this.model = Model;
        this.numberPlate = NumberPlate;
        this.productionDate = ProductionDate;
        this.vinNo = VinNo;   
        this.carHistories =CarHistories;
    }

}

export class CarHistory {
    StartDate: Date;
    EndDate?: Date;      
    Cost: Number;
    Parts: Part[];
    Comment: string;
    OrderStatus:OrderStatus;

 

    constructor(StartDate: Date, Cost: number, Parts: Part[],Comment:string,OrderStatus:OrderStatus, EndDate?:Date ) {
        this.StartDate = StartDate;
        this.EndDate = EndDate;
        this.Cost = Cost;
        this.Parts = Parts;  
        this.Comment = Comment;
        this.OrderStatus = OrderStatus; 
    }

}

export class Part {
    PartName: string;
    Brand: string;      
    CatalogNumber: string;
    Qty: number;
 

    constructor(PartName: string, Brand: string, CatalogNumber: string, Qty:number ) {
        this.PartName = PartName;
        this.Brand = Brand;
        this.CatalogNumber = CatalogNumber;
        this.Qty = Qty;   
    }

    
}
export enum OrderStatus {
    New,
    InProgress,
    OnHold,
    Done
  }

  export enum FuelTypes 
    { 
        Gasoline,
        Diesel,
        LPG,
        Hybrid,
        Electric
    }


