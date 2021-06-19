import { NgModule,Component, Inject, OnInit, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MatDialog, MatDialogRef, MatPaginator, MatTableDataSource, MAT_DIALOG_DATA } from '@angular/material';
import { MatSort } from '@angular/material/sort';
import { Customer } from './Customer';
import { ActivatedRoute } from '@angular/router';



@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css'],

})
export class CustomersComponent implements OnInit {
 
  public customers: Customer[];
  dataSource = new MatTableDataSource<Customer>();


  displayedColumns: string[] = ['ProjectId', 'FirstName', 'LastName', 'Phone','settings'];

  @ViewChild(MatPaginator, {static: false})
  paginator!: MatPaginator;


  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string,private dialog: MatDialog) {

  }
  ngOnInit(): void { 
  }
  ngAfterViewInit() {
   this.refreshData();
  }
  refreshData()
  {
    this.http.get<Customer[]>(this.baseUrl+'api/Users/all').subscribe(result => {
         
      this.customers = result;     
      this.dataSource = new MatTableDataSource(Array.from(this.customers));
      this.dataSource.paginator = this.paginator;  
    }, error => console.error(error));
  }


  openDeleteDialog(customer: Customer) {     
    const dialogRef = this.dialog.open(DeleteCustomerDialog, {
      width: '250px',
      data: customer   
 
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
  selector: 'delete-customer-dialog',
  templateUrl: 'delete-customer-dialog.html',
})
export class DeleteCustomerDialog {
  public errors: string[] = [""];
  
  id:string;
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private route: ActivatedRoute,
    
    private dialogRef: MatDialogRef<DeleteCustomerDialog>,
    @Inject(MAT_DIALOG_DATA) public data: Customer) {    
      

     }

  onNoClick(): void {
    this.dialogRef.close();
  }

  removeCustomer() {    
   
    this.dialogRef.close(); 
      this.http.delete(this.baseUrl + 'api/Users/' + this.data.oId).subscribe(result => {
      }, error => console.error(error));   
  }

}





