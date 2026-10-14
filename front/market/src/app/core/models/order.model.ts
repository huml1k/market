import { orderItems } from "./order-product.model";

export interface Order{
    id: string;
    numberId: string;
    address: string;
    orderItems: orderItems[];
}