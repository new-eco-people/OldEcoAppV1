import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly url = environment.baseUrlApi + 'user';

  constructor(private http: HttpClient) { }

  get() {
    // console.log('ehello');
    return this.http.get(`${this.url}`);
  }

}
