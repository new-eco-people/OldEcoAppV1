import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  private readonly url = environment.baseUrlApi + 'project';

  constructor() { }

  publicProjects() {
    return of();
  }
}
