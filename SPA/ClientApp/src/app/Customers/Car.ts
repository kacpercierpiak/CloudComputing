import { Customer } from "./Customer";

export class CustomerCars extends Customer {
    cars:Car[];
 

    constructor(firstName: string, lastName: string, phone:string,cars?:Car[],oId?: string ) {
        if(oId)
        oId = '';
        super(oId,firstName,lastName,phone)
        this.cars = cars;
    }

}

export class Car {
    oId:string;
    brand: string;
    engine: string;      
    fuelType: FuelTypes;
    manualGearbox: boolean;
    model:string;
    numberPlate: string;
    productionDate:Date;
    vinNo:string;
    carHistories?: CarHistory[];

 

    constructor(oId:string,brand: string, engine: string, fuelType: FuelTypes, manualGearbox:boolean, model: string, numberPlate: string, productionDate: Date,vinNo: string, carHistories?: CarHistory[], ) {
        this.oId = oId;
        this.brand = brand;
        this.engine = engine;
        this.fuelType = fuelType;
        this.manualGearbox = manualGearbox;   
        this.model = model;
        this.numberPlate = numberPlate;
        this.productionDate = productionDate;
        this.vinNo = vinNo;   
        this.carHistories =carHistories;
    }

}

export class CarHistory {
    startDate: Date;
    endDate?: Date;      
    cost: number;
    parts?: Part[];
    comment: string;
    orderStatus:OrderStatus;

 

    constructor(startDate: Date, cost: number, comment:string,orderStatus:OrderStatus, endDate?:Date,parts?: Part[] ) {
        this.startDate = startDate;
        this.endDate = endDate;
        this.cost = cost;
        this.parts = parts;  
        this.comment = comment;
        this.orderStatus = orderStatus; 
    }

}

export class Part {
    partName: string;
    brand: string;      
    catalogNumber: string;
    qty: number;
 

    constructor(partName: string, brand: string, catalogNumber: string, qty:number ) {
        this.partName = partName;
        this.brand = brand;
        this.catalogNumber = catalogNumber;
        this.qty = qty;   
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


