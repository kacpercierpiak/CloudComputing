import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Car, FuelTypes } from '../../Car';

@Component({
  selector: 'app-edit-car',
  templateUrl: './edit-car.component.html',
  styleUrls: ['./edit-car.component.css']
})
export class EditCarComponent implements OnInit {

  brand: string;
  engine: string;      
  fuelType: number;
  manualGearbox: boolean;
  model:string;
  numberPlate: string;
  productionDate:Date;
  vinNo:string;
  selected:any = '1';


  fuelTypes: typeof FuelTypes = FuelTypes;
  id:string;
  no:string;
  keys: string[] = [];

  constructor(private http: HttpClient,  @Inject('BASE_URL') private baseUrl: string,private route: ActivatedRoute,private router: Router) { 
   
    this.keys = Object.keys(this.fuelTypes).filter(k => !isNaN(Number(k)));
    this.id = this.route.snapshot.paramMap.get('id');
    this.no = this.route.snapshot.paramMap.get('no');
   
   
  }

  ngOnInit() {
    
  }
  ngAfterViewInit()
  {
    this.http.get<Car>(this.baseUrl+'api/Cars/' + this.id + '/car/' + this.no).subscribe(result => {
      this.brand = result.brand;
      this.engine = result.engine;
      this.selected = result.fuelType.toString();
      
      this.manualGearbox = result.manualGearbox;
      this.model = result.model;
      this.numberPlate = result.numberPlate;
      this.productionDate = result.productionDate;
      this.vinNo = result.vinNo;
     
    }, error => console.error(error));
  }

  public onSubmit() {
    
    this.http.put<Car>(this.baseUrl+'api/Cars/' + this.id + '/car/'+this.no,new Car(this.brand,this.engine,Object.keys(FuelTypes).indexOf(this.selected),this.manualGearbox,this.model,this.numberPlate,this.productionDate,this.vinNo)).subscribe(result => {
       this.router.navigate(["/cars/"+this.id]);
     }, error => console.error(error));
   }

}
