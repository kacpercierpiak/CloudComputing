import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Car, FuelTypes } from '../../Car';

@Component({
  selector: 'app-addcar',
  templateUrl: './addcar.component.html',
  styleUrls: ['./addcar.component.css']
})
export class AddcarComponent implements OnInit {
  public car: Car | null = null ;
  fuelTypes: typeof FuelTypes = FuelTypes;
  id:string;
  keys: string[] = [];
  

  constructor(private http: HttpClient,  @Inject('BASE_URL') private baseUrl: string,private route: ActivatedRoute,private router: Router) { 
    this.car = new Car('','','',this.fuelTypes.Diesel,true,'','',new Date(),'');
    this.keys = Object.keys(this.fuelTypes).filter(k => !isNaN(Number(k)));
   
  }

  ngOnInit() {
    this.id = this.route.snapshot.paramMap.get('id');
  }

  public onSubmit() {
   this.car.fuelType = Object.keys(FuelTypes).indexOf(this.car.fuelType.toString());
    this.http.post<Car>(this.baseUrl+'api/Cars/' + this.id + '/car',this.car).subscribe(result => {
      this.router.navigate(["/cars/"+this.id]);
    }, error => console.error(error));
  }

}
