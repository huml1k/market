import { Component, inject, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { OrderService } from '../../core/services/order.service';
import { Order } from '../../core/models/order.model';

@Component({
  selector: 'app-order-list',
  imports: [RouterLink],
  templateUrl: './order-list.component.html',
})
export class OrderListComponent {
  private svc = inject(OrderService);
  orders = signal<Order[]>([]);

  constructor() { this.load(); }
  load() { this.svc.getAll().subscribe(o => this.orders.set(o)); }
  remove(id: string) {
    if (!confirm('Удалить заказ?')) return;
    this.svc.remove(id).subscribe(() => this.load());
  }
}