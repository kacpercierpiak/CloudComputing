import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Car, OrderStatus } from 'src/app/Customers/Car';
import { Customer } from 'src/app/Customers/Customer';
import { Order } from '../Order';

@Component({
  selector: 'app-edit-order',
  templateUrl: './edit-order.component.html',
  styleUrls: ['./edit-order.component.css']
})
export class EditOrderComponent implements OnInit {

  brand: string;
  name:string;
  model: string;
  numberPlate: string;
  orderStatus: typeof OrderStatus = OrderStatus;
  keys: string[] = [];
  selected:string = '1';
  id:string;
  description:string;
  order: Order;


  constructor(private http: HttpClient,  @Inject('BASE_URL') private baseUrl: string,private route: ActivatedRoute,private router: Router) { 
    this.keys = Object.keys(this.orderStatus).filter(k => !isNaN(Number(k)));
    this.id = this.route.snapshot.paramMap.get('id');
  }

  ngOnInit() {
    this.http.get<Order>(this.baseUrl+'api/Orders/'+this.id).subscribe((result: Order) => {
      console.log(result);
      this.order = result;
      this.description = result.comment;
      this.selected = result.orderStatus.toString();
      this.http.get<Customer>(this.baseUrl+'api/Users/'+result.userOId).subscribe(result => {
  
        this.name = result.firstName +' '+ result.lastName;
        
      }, error => console.error(error));
      this.http.get<Car>(this.baseUrl+'api/Cars/' +result.userOId + '/car/' + result.carOId).subscribe(result => {
        
        this.brand = result.brand;
        this.model = result.model;
        this.numberPlate = result.numberPlate;
      }, error => console.error(error));

  
      
    }, error => console.error(error));
  }

  public onSubmit() {
  
    this.order.orderStatus = Number(this.selected);
    this.order.comment = this.description;
    this.http.put<Order>(this.baseUrl+'api/Orders/'+this.id , this.order).subscribe(result => {
       this.router.navigate(["/"]);
     }, error => console.error(error));
   }

}
