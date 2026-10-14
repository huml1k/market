export interface CreateProductDto { name: string; price: number; }
export interface UpdateProductDto { id: string; name: string; price: number; }

export interface OrderItemsDto { productId: string; quantity: number; }

export interface CreateOrderDto { address: string; products: OrderItemsDto[]; }
export interface UpdateOrderDto { id: string; address: string; products: OrderItemsDto[]; }