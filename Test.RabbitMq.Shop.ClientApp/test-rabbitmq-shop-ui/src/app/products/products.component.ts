import { Component } from '@angular/core';
import { PRODUCTS } from '../mock-products';
import { Product } from '../product';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})

export class ProductsComponent {
  products = PRODUCTS;

  selectedProduct?: Product;

  onSelect(product: Product) {
    this.selectedProduct = product;
  }
}
