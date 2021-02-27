import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component } from '@angular/core';
import { environment } from '../../environments/environment.prod';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements AfterViewInit {
  constructor(private http: HttpClient) { }

    ngAfterViewInit(): void {
      this.http.get(`${environment.urls.api}api/test`).subscribe((data) => {       
        console.dir(data);    
      },
        (error) => console.log(error)
      );

    }
}


