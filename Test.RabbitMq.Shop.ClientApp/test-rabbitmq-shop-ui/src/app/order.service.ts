import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';
import { Order } from './order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private ordersApiUrl = 'https://localhost:7019/api/orders';
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(this.ordersApiUrl)
      .pipe(
        tap(_ => console.log('orders fetched')),
        catchError(this.handleError<Order[]>('getOrders', []))
      );
  }

  addOrder(productId: number, productQuantity: number): Observable<any> {
    return this.http.post(this.ordersApiUrl, { productId, productQuantity }, this.httpOptions)
      .pipe(
        tap(_ => console.log('order added')),
        catchError(this.handleError<any>('addOrder', []))
      );
  }

  private handleError<T>(_operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
