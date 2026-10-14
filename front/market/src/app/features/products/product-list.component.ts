
import { Component, inject, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ProductService } from '../../core/services/product.service';
import { Product } from '../../core/models/product.model';

@Component({
  selector: 'app-product-list',
  imports: [RouterLink],
  templateUrl: './product-list.component.html',
})
export class ProductListComponent {
  private svc = inject(ProductService);
  products = signal<Product[]>([]);

  constructor() {  this.load(); }

  load() { this.svc.getAll().subscribe(p => this.products.set(p)); }

  remove(id: string) {
    if (!confirm('Удалить товар?')) return;
    this.svc.remove(id).subscribe(() => this.load());
  }
}