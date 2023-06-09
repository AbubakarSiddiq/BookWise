import { Injectable } from '@angular/core';
import { ConfigStore } from './config.store';
import { Observable, from, map, of, switchMap, tap } from 'rxjs';
import { ServiceConfig } from './service-config';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {
  protected defaultHttpOptions: RequestInit = { headers: { 'Content-Type': 'application/json' } };

  constructor(private configStore: ConfigStore) { }

  /**
   * Get the service config from server and save to config store.
   */
  public getServiceConfig(): Observable<ConfigStore> {
    if (this.configStore.apiUrl) {
      return of(this.configStore);
    }

    return from(fetch('api/config', this.defaultHttpOptions))
      .pipe(
        switchMap(response => response.json() as Promise<ServiceConfig>),
        tap(config => this.configStore.load(config)),
        map(() => this.configStore)
      );
  }
}
