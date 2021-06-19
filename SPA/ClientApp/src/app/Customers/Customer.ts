export class Customer {
    oId: string;
    firstName: string;      
    lastName: string;
    phone: string;
 

    constructor(oId: string, firstName: string, lastName: string, phone:string ) {
        this.oId = oId;
        this.firstName = firstName;
        this.lastName = lastName;
        this.phone = phone;   
    }

}

