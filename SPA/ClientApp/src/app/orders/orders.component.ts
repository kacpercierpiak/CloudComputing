import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component } from '@angular/core';
import { environment } from '../../environments/environment.prod';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
})
export class OrdersComponent{
  constructor(private http: HttpClient) { }

  
}


