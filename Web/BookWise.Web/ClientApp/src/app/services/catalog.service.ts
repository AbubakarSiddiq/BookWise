import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ConfigStore } from '../core/services';
import { Observable } from 'rxjs';
import { CatalogModel } from '../models/book-catalog-model';

@Injectable({
  providedIn: 'root'
})
export class CatalogService {

  constructor(private httpClient: HttpClient,
    private configStore: ConfigStore) { }

    public GetCatalog(): Observable<CatalogModel[]> 
    {
      return this.httpClient.get<CatalogModel[]>(`${this.configStore.apiUrl}/Catalog`);
    }
  
    public GetCatalogById(id: string): Observable<CatalogModel>
    {
      return this.httpClient.get<CatalogModel>(`${this.configStore.apiUrl}/Catalog/${id}`);
    }
  
    public GetCatalogByCategory(title: string): Observable<CatalogModel[]>
    {
        return this.httpClient.get<CatalogModel[]>(`${this.configStore.apiUrl}/Catalog/GetBookByTitle/${title}`);
    }
  
    public CreateCatalog(model: CatalogModel): Observable<CatalogModel>
    {
      return this.httpClient.post<CatalogModel>(`${this.configStore.apiUrl}/Catalog`, model);
    }      
}
