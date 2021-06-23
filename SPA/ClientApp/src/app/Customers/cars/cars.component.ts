import { NgModule,Component, Inject, OnInit, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MatDialog, MatDialogRef, MatPaginator, MatTableDataSource, MAT_DIALOG_DATA } from '@angular/material';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute } from '@angular/router';
import { Car, CustomerCars, FuelTypes } from '../Car';



@Component({
  selector: 'app-cars',
  templateUrl: './cars.component.html',
  styleUrls: ['./cars.component.css']
})
export class CarsComponent implements OnInit {

  public customers: CustomerCars;
  dataSource = new MatTableDataSource<Car>();
  fuelType : typeof  FuelTypes = FuelTypes;
 

  id:string;

  displayedColumns: string[] = ['ProjectId', 'Brand', 'Model', 'Engine','FuelType','NumberPlate','ManualGearbox','settings'];

  @ViewChild(MatPaginator, {static: false})
  paginator!: MatPaginator;

  constructor(private http: HttpClient,  @Inject('BASE_URL') private baseUrl: string, private route: ActivatedRoute, private dialog: MatDialog) {
   
 
  }
  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
   
  }
  ngAfterViewInit() {
    this.refreshData();
  }

  refreshData()
  {
    this.http.get<CustomerCars>(this.baseUrl+'api/Users/' + this.id + '/details').subscribe(result => {
      this.customers = result;
      console.log(this.customers.cars);
      this.dataSource = new MatTableDataSource(Array.from(this.customers.cars));
      console.log(this.dataSource);
      this.dataSource.paginator = this.paginator;  
    }, error => console.error(error));
  }

  openDeleteDialog(car: Car) {     
      const dialogRef = this.dialog.open(DeleteCarDialog, {
        width: '250px',
        data: {
          car: car,
          id: this.id
        }
      });
      
      dialogRef.afterClosed().subscribe(result => {    
        this.refreshData();
      });    
    
  }


  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

}

@Component({
  selector: 'delete-car-dialog',
  templateUrl: 'delete-car-dialog.html',
})
export class DeleteCarDialog {
  public errors: string[] = [""];
  public data: Car;
  id:string;
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private route: ActivatedRoute,
    
    private dialogRef: MatDialogRef<DeleteCarDialog>,
    @Inject(MAT_DIALOG_DATA) public dataAll: any) {
      this.data = dataAll.car;  
      this.id = dataAll.id;

     }

  onNoClick(): void {
    this.dialogRef.close();
  }

  removeCar(car: Car) {
    
    console.dir(car);
    this.dialogRef.close();
    if (car) {
      console.log
      this.http.delete<CustomerCars>(this.baseUrl + 'api/Cars/' + this.id + '/car/'+car.oId).subscribe(result => {
      }, error => console.error(error));
    } else {
      alert("Car not found");
    }
  }

}


