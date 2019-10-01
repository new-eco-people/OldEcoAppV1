import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  private readonly url = environment.baseUrlApi + 'comments';

  constructor(private http: HttpClient) { }

  public sendCommentViaEmail(data: any) {
    return this.http.post(`${this.url}`, data);
  }

  public sendCommentViaId(data: any) {
    return this.http.post(`${this.url}/id`, data);
  }

  public sendIdeaViaEmail(data: any) {
    return this.http.post(`${this.url}/idea`, data);
  }

  public sendIdeaViaId(data: any) {
    return this.http.post(`${this.url}/idea-id`, data);
  }

  public getComments(data: any) {
    return this.http.get(`${this.url}?${this.queryString(data)}`);
  }

  public getIdeas(data: any) {
    return this.http.get(`${this.url}/ideas?${this.queryString(data)}`);
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
