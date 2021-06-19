import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { OrdersComponent,DeleteOrderDialog } from './orders/orders.component';
import { CustomersComponent,DeleteCustomerDialog } from './Customers/customers.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule  } from '@angular/material';
import { MatFormFieldModule   } from '@angular/material';
import { MatInputModule  } from '@angular/material';
import { MatIconModule  } from '@angular/material';
import { MatButtonModule } from '@angular/material/button';
import { CarsComponent, DeleteCarDialog } from './Customers/cars/cars.component';
import { EditCarComponent } from './Customers/cars/edit-car/edit-car.component';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { MatDialogModule } from '@angular/material/dialog';
import { AddcarComponent } from './Customers/cars/addcar/addcar.component';
import {MatSelectModule} from '@angular/material/select';
import { EditCustomerComponent } from './Customers/edit-customer/edit-customer.component';
import { AddCustomerComponent } from './Customers/add-customer/add-customer.component';
import { AddOrderComponent } from './orders/add-order/add-order.component';
import { EditOrderComponent } from './orders/edit-order/edit-order.component';



@NgModule({
  entryComponents:[DeleteCarDialog,DeleteCustomerDialog,DeleteOrderDialog],
  declarations: [
    AppComponent,
    NavMenuComponent,
    OrdersComponent,
    CustomersComponent,
    CarsComponent,
    EditCarComponent,
    DeleteCarDialog,
    DeleteCustomerDialog,
    AddcarComponent,
    EditCustomerComponent,
    AddCustomerComponent,
    DeleteOrderDialog,
    AddOrderComponent,
    EditOrderComponent
    
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    MatTableModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatButtonModule,
    MatCheckboxModule,
    MatSelectModule,
    MatIconModule,
    MatInputModule,    
    MatDialogModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: OrdersComponent, pathMatch: 'full' },
      { path: 'customers', component: CustomersComponent },
      { path: 'cars/:id', component:CarsComponent,pathMatch: 'full'},
      { path: 'editCar/:id/:carId', component:EditCarComponent,pathMatch: 'full'},
      { path: 'addCar/:id', component:AddcarComponent,pathMatch: 'full'},
      { path: 'editCustomer/:id', component:EditCustomerComponent,pathMatch: 'full'},
      { path: 'addCustomer', component:AddCustomerComponent,pathMatch: 'full'},
      { path: 'addOrder', component:AddOrderComponent,pathMatch: 'full'}
      
      

    ]),
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
