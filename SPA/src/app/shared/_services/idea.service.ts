import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class IdeaService {
  private readonly url = environment.baseUrlApi + 'idea';

  constructor(private http: HttpClient) { }

  save(data: any) {
    return this.http.post(`${this.url}`, data);
  }

  getPublic() {
    return this.http.get(`${this.url}/public`);
  }

  likeProblemUsingEmail(data: any) {
    return this.http.post(`${this.url}/like-email`, data);
  }

  likeProblemUsingId(problemId: string) {
    return this.http.post(`${this.url}/like-id`, {problemId});
  }

  search(data: any) {
    return this.http.get(`${this.url}/q?${this.queryString(data)}`);
  }

  totalProblems() {
    return this.http.get(`${this.url}/total`);
  }

  private queryString(data: any) {
    let newData = [];
    for (const key in data) {
      if (data.hasOwnProperty(key) && data[key]) {
        newData.push(encodeURIComponent(key) + '=' + encodeURIComponent(data[key]))
      }
    }
    return newData.join('&');
  }
}
