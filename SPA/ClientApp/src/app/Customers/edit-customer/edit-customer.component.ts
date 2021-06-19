import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Customer } from '../Customer';

@Component({
  selector: 'app-edit-customer',
  templateUrl: './edit-customer.component.html',
  styleUrls: ['./edit-customer.component.css']
})
export class EditCustomerComponent implements OnInit {
  firstName: string;
  lastName: string;      
  phone: string;
  id:string;

  constructor(private http: HttpClient,  @Inject('BASE_URL') private baseUrl: string,private route: ActivatedRoute,private router: Router) {

    this.id = this.route.snapshot.paramMap.get('id');
   }

  ngOnInit() {
  }

  ngAfterViewInit()
  {
    this.http.get<Customer>(this.baseUrl+'api/Users/' + this.id).subscribe(result => {
      this.firstName = result.firstName;
      this.lastName = result.lastName;
      this.phone = result.phone;     
    }, error => console.error(error));
  }


  public onSubmit() {
    let user = new Customer(this.id,this.firstName,this.lastName,this.phone);
    
    this.http.put<Customer>(this.baseUrl+'api/Users' , user).subscribe(result => {
       this.router.navigate(["/customers"]);
     }, error => console.error(error));
   }

}
