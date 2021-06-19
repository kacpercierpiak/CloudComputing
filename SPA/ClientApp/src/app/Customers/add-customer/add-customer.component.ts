import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Customer } from '../Customer';

@Component({
  selector: 'app-add-customer',
  templateUrl: './add-customer.component.html',
  styleUrls: ['./add-customer.component.css']
})
export class AddCustomerComponent implements OnInit {
  firstName: string;
  lastName: string;      
  phone: string;

  constructor(private http: HttpClient,  @Inject('BASE_URL') private baseUrl: string,private route: ActivatedRoute,private router: Router) { }

  ngOnInit() {
  }

  public onSubmit() {
    
     this.http.post<Customer>(this.baseUrl+'api/Users/' , new Customer('',this.firstName,this.lastName,this.phone)).subscribe(result => {
       this.router.navigate(["/customers"]);
     }, error => console.error(error));
   }
}
