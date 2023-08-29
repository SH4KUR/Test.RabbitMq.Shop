import { Injectable } from '@angular/core';
import { Product } from './product';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private productsApiUrl = 'https://localhost:7019/api/products'

  constructor(private http: HttpClient) { }

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.productsApiUrl)
      .pipe(
        tap(_ => console.log('products fetched')),
        catchError(this.handleError<Product[]>('getProducts', []))
      );
  }

  private handleError<T>(_operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
