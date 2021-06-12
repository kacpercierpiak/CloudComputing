import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component } from '@angular/core';
import { environment } from '../../environments/environment.prod';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent{
  constructor(private http: HttpClient) { }

  
}


