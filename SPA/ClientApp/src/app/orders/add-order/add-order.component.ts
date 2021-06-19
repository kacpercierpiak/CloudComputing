import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Car, CustomerCars } from 'src/app/Customers/Car';
import { Customer } from 'src/app/Customers/Customer';

@Component({
  selector: 'app-add-order',
  templateUrl: './add-order.component.html',
  styleUrls: ['./add-order.component.css']
})
export class AddOrderComponent implements OnInit {
  description:string;
  customerSelect:string;
  carSelect:string;

  customers:SelectValues[];
  cars:SelectValues[];


  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string,private router: Router) { 
    
  }

  ngOnInit() {
    this.http.get<Customer[]>(this.baseUrl+'api/Users/all').subscribe(result => {
         
      this.customers = result.map(this.parseCustomer);     
  
    }, error => console.error(error));
  }

  parseCustomer(customer:Customer){
  return new SelectValues(customer.firstName + ' ' + customer.lastName,customer.oId);

  }
  parseCar(car:Car)
  {
    return new SelectValues(car.brand+' '+car.model+' '+car.numberPlate,car.oId);
  }

  refreshData(e:Event)
  {
    this.http.get<CustomerCars>(this.baseUrl+'api/Users/' + this.customerSelect + '/details').subscribe(result => {
      console.log(1);
      this.cars = result.cars.map(this.parseCar);         
    }, error => console.error(error));
    
  }

  public onSubmit() {
    
    this.http.post<Customer>(this.baseUrl+'api/Orders/' , {UserOId: this.customerSelect, CarOId: this.carSelect,comment:this.description}).subscribe(result => {
      this.router.navigate(["/"]);
    }, error => console.error(error));
  }

}

export class SelectValues{
  name:string;
  id:string;
  constructor(name:string,id:string)
  {
    this.name = name;
    this.id = id;
  }

}
