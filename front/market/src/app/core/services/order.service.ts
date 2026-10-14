import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Order } from "../models/order.model";
import { CreateOrderDto, UpdateOrderDto } from "../models/dto";

@Injectable({providedIn: 'root'})
export class OrderService{
    private http = inject(HttpClient)

    getAll(): Observable<Order[]>{
            return this.http.get<Order[]>(`/Order/list/order`)
        }
        getById(id: string): Observable<Order>{
            return this.http.get<Order>(`/Order/order/${id}`)
        }
        create(dto: CreateOrderDto): Observable<unknown>{
            return this.http.post(`/Order/create/order`, dto)
        }
        update(dto: UpdateOrderDto): Observable<unknown>{
            return this.http.post(`/Order/update/order`, dto)
        }
        remove(id: string): Observable<unknown>{
            return this.http.delete(`/Order/order/${id}`)
        }
}