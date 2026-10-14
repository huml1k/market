import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Product } from "../models/product.model";
import { CreateProductDto, UpdateOrderDto, UpdateProductDto } from "../models/dto";

@Injectable({ providedIn: 'root'})
export class ProductService{
    private http = inject(HttpClient)

    getAll(): Observable<Product[]>{
        return this.http.get<Product[]>(`/Product/list/product`)
    }
    getById(id: string): Observable<Product>{
        return this.http.get<Product>(`/Product/product/${id}`)
    }
    create(dto: CreateProductDto): Observable<unknown>{
        return this.http.post(`/Product/create/product`, dto)
    }
    update(dto: UpdateProductDto): Observable<unknown>{
        return this.http.post(`/Product/update/product`, dto)
    }
    remove(id: string): Observable<unknown>{
        return this.http.delete(`/Product/product/${id}`)
    }
}