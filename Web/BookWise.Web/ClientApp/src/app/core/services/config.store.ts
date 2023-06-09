import { Injectable } from "@angular/core";
import { ServiceConfig } from "./service-config";

@Injectable({
    providedIn: 'root',
  })
  export class ConfigStore {
    private config: ServiceConfig;
  
    private allowedPaths = [
        'Catalog',
    ];
  
    constructor() {
        this.config = {} as ServiceConfig;
    }
  
    /**
     * allowed paths for api requests.
     */
    public isApiUrl(url: string): boolean {
        return this.allowedPaths.some(p => {
            return url.startsWith(p);
        });
    }
    /**
     * The base url for api.
     */
    get apiUrl(): string {
        const apiUrl = this.config.apiUrl;
        return apiUrl || '';
    }
  
    /**
     * load the config into store.
     * @param config the service config.
     */
    load(config: ServiceConfig) {
        this.config = config;
    }
  }