import { CarHistory, OrderStatus, Part } from "../Customers/Car";

export class Order extends CarHistory {
    oId: string;
    carOId: string;
    userOId:string;
 

    constructor(oId: string, carOId: string, startDate:Date, cost:number,comment:string,orderStatus:OrderStatus,userOId:string,endDate?:Date, parts?:Part[] ) {
        super(startDate,cost,comment,orderStatus,endDate,parts);
        this.oId = oId;
        this.carOId = carOId;
        this.userOId = userOId;
    }

}




