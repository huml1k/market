import { Component, inject, signal } from '@angular/core';
import { FormArray, FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { OrderService } from '../../core/services/order.service';
import { ProductService } from '../../core/services/product.service';
import { Product } from '../../core/models/product.model';

@Component({
  selector: 'app-order-form',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './order-form.component.html',
})
export class OrderFormComponent {
  private fb = inject(FormBuilder);
  private orders = inject(OrderService);
  private productsSvc = inject(ProductService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  editId = signal<string | null>(this.route.snapshot.paramMap.get('id'));
  products = signal<Product[]>([]);

  form = this.fb.group({
    address: ['', Validators.required],
    items:   this.fb.array([]),          
  });

  get items() { return this.form.get('items') as FormArray; }

  constructor() {
    this.productsSvc.getAll().subscribe(p => this.products.set(p));
    const id = this.editId();
    if (id) {
      this.orders.getById(id).subscribe(o => {
        this.form.patchValue({ address: o.address });
        o.orderItems.forEach(it => this.addItem(it.productId, it.quantity));
      });
    } else {
      this.addItem();
    }
  }

  addItem(productId = '', quantity = 1) {
    this.items.push(this.fb.group({
      productId: [productId, Validators.required],
      quantity:  [quantity,  [Validators.required, Validators.min(1)]],
    }));
  }
  removeItem(i: number) { this.items.removeAt(i); }

  save() {
    if (this.form.invalid) return;
    const v = this.form.value;
    const payload = {
      address: v.address!,
      products: (v.items as Array<{ productId: string; quantity: number }>)
            .map(it => ({ productId: it.productId, quantity: it.quantity })),
    };
    const id = this.editId();
    const req = id ? this.orders.update({ id, ...payload }) : this.orders.create(payload);
    req.subscribe(() => this.router.navigate(['/orders']));
  }
}