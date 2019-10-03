import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LocationService {

  private readonly url = environment.baseUrlApi + 'location';

  constructor(private http: HttpClient) { }

  getCountries() {
    return this.http.get(`${this.url}/countries`);
  }

  getStates(countryId: any) {
    // /quota/?key=
    return this.http.get(`${this.url}/states/${countryId}`);
  }
}
