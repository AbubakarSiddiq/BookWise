import { Component } from '@angular/core';
import { CatalogService } from '../services/catalog.service';
import { CatalogModel } from '../models/book-catalog-model';
import { Observable, Subscribable, Subscription } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  public books: CatalogModel[] = [];
  public book = {} as CatalogModel;
  constructor(private catalogService: CatalogService){
    this.getCatalog();
  }

  getCatalog(){
    this.catalogService.GetCatalog().subscribe(books => this.books = books.reverse());
  }


  submitForm(){
    this.catalogService.CreateCatalog(this.book).subscribe();
    this.books.push(this.book);
    this.books.reverse();
    this.book = {} as CatalogModel;
  }
}
