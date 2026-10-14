import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ProductService } from '../../core/services/product.service';

@Component({
  selector: 'app-product-form',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './product-form.component.html',
})
export class ProductFormComponent {
  private fb = inject(FormBuilder);
  private svc = inject(ProductService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  editId = signal<string | null>(this.route.snapshot.paramMap.get('id'));

  form = this.fb.group({
    name:  ['', [Validators.required]],
    price: [0,  [Validators.required, Validators.min(0)]],
  });

  constructor() {
    const id = this.editId();
    if (id) this.svc.getById(id).subscribe(p => this.form.patchValue({ name: p.name, price: p.price }));
  }

  save() {
    if (this.form.invalid) return;
    const v = this.form.value;
    const id = this.editId();
    const req = id
      ? this.svc.update({ id, name: v.name!, price: v.price! })
      : this.svc.create({ name: v.name!, price: v.price! });
    req.subscribe(() => this.router.navigate(['/products']));
  }
}