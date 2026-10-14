import { Routes } from '@angular/router';
import { ProductListComponent } from './features/products/product-list.component';
import { ProductFormComponent } from './features/products/product-form.component';
import { OrderListComponent } from './features/orders/order-list.component';
import { OrderFormComponent } from './features/orders/order-form.component';

export const routes: Routes = [
  { path: '', redirectTo: 'products', pathMatch: 'full' },
  { path: 'products',           component: ProductListComponent },
  { path: 'products/new',       component: ProductFormComponent },
  { path: 'products/:id/edit',  component: ProductFormComponent },
  { path: 'orders',             component: OrderListComponent },
  { path: 'orders/new',         component: OrderFormComponent },
  { path: 'orders/:id/edit',    component: OrderFormComponent },
  { path: '**', redirectTo: 'products' }
];
