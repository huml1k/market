import { Product } from "./product.model";

export interface orderItems{
    orderId: string;
    productId: string;
    quantity: number;
    product?: Product;
}