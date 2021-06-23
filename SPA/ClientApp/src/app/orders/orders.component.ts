import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component, Inject, ViewChild } from '@angular/core';
import { MatDialog, MatDialogRef, MatPaginator, MatTableDataSource, MAT_DIALOG_DATA } from '@angular/material';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment.prod';
import { Car, OrderStatus, Part } from '../Customers/Car';
import { Customer } from '../Customers/Customer';
import { Order } from './Order';
import { animate, state, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class OrdersComponent{

  dataSource = new MatTableDataSource<OrderDto>();
  response: Order[];
  expandedElement: OrderDto | null;

  displayedColumns: string[] = ['ProjectId', 'CustomerName', 'NumberPlate', 'Brand','Model','startDate','endDate','status','settings'];

  @ViewChild(MatPaginator, {static: false})
  paginator!: MatPaginator;

  orders:OrderDto[] = [];
  
  constructor(private http: HttpClient,@Inject('BASE_URL') private baseUrl: string,private dialog: MatDialog,private router: Router) { }
  ngAfterViewInit() {
    this.refreshData();
   }
  refreshData()
  {
    this.http.get<Order[]>(this.baseUrl+'api/Orders').subscribe((result: Order[]) => {
      console.log(result);
    
      this.getDetails(result)    ;

  
      
    }, error => console.error(error));


  }
  getDetails(order:Order[])
  {
    if(order.length==0)
    {
      this.dataSource = new MatTableDataSource([]);
    }
    order.forEach(element =>{
      let orderDto = new OrderDto(element,'','','','');
      this.http.get<Customer>(this.baseUrl+'api/Users/'+element.userOId).subscribe(result => {
  
        orderDto.name = result.firstName +' '+ result.lastName;
        
      }, error => console.error(error));
      this.http.get<Car>(this.baseUrl+'api/Cars/' +element.userOId + '/car/' + element.carOId).subscribe(result => {
        
        orderDto.brand = result.brand;
        orderDto.model = result.model;
        orderDto.numberPlate = result.numberPlate;
      }, error => console.error(error));

      this.orders.push(orderDto)
      this.dataSource = new MatTableDataSource(Array.from(this.orders));
      this.dataSource.paginator = this.paginator;  
    }) 
  
  }

  openDeleteDialog(order: OrderDto) {     
    const dialogRef = this.dialog.open(DeleteOrderDialog, {
      width: '250px',
      data: order   
 
    });
    
    dialogRef.afterClosed().subscribe(result => {    
      this.router.navigate(["/"]);
    });    
  
}


  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }
}

@Component({
  selector: 'delete-order-dialog',
  templateUrl: 'delete-order-dialog.html',
})
export class DeleteOrderDialog {
  public errors: string[] = [""];
  
  id:string;
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string,
    
    private dialogRef: MatDialogRef<DeleteOrderDialog>,
    @Inject(MAT_DIALOG_DATA) public data: any) {    
      

     }

  onNoClick(): void {
    this.dialogRef.close();
  }

  removeOrder() {    
   console.log(this.data);
    this.dialogRef.close(); 
      this.http.delete(this.baseUrl + 'api/Orders/' + this.data.oId).subscribe(result => {
      }, error => console.error(error));   
  }

}





export class OrderDto  extends Order
{
  name:string;
  brand:string;
  model:string;
  numberPlate: string;
  status: string;

  constructor(order:Order,name:string,brand:string,model:string,numberPlate:string) {
    super(order.oId,order.carOId,order.startDate,order.cost,order.comment,order.orderStatus,order.userOId,order.endDate,order.parts);
    this.name = name;
    this.brand = brand;
    this.model = model;
    this.numberPlate = numberPlate;
    this.status = OrderStatus[this.orderStatus];
}
}


