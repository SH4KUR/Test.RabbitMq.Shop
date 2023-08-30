import { Product } from "./product";

export interface Order {
    id: string;
    product: Product;
    orderPrice: number;
    productQuantity: number;
    creationDateTime: Date;
}