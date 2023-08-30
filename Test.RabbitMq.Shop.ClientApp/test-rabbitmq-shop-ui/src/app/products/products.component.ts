import { Component } from '@angular/core';
import { Product } from '../product';
import { ProductService } from '../product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})

export class ProductsComponent {
  products: Product[] = [];
  selectedProduct?: Product;

  constructor(
    private productService: ProductService
  ) { }

  ngOnInit(): void {
    this.getProducts();
  }

  getProducts() {
    this.productService.getProducts()
      .subscribe(products => this.products = products);
  }

  onSelect(product: Product) {
    this.selectedProduct = product;
  }
}
