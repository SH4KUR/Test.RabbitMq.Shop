import { Product } from "./product";

export interface Order {
    id: string;
    product: Product;
    orderPrice: number;
    creationDateTime: Date;
}