import { Component, Input } from '@angular/core';
import { Product } from '../product';
import { OrderService } from '../order.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})

export class ProductDetailComponent {
  @Input() product?: Product;
  
  productQuantity: number = 1;

  constructor(
    private orderService: OrderService,
    private router: Router
  ) { }

  orderProduct(): void {
    if (this.product) {
      this.orderService.addOrder(this.product.id, this.productQuantity)
        .subscribe(() => this.goToOrders());
    }
  }

  goToOrders() {
    this.router.navigate(['/', 'orders']);
  }
}
