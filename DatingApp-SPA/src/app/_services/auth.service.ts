import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

// Injectable decorator
// this allows us inject into our service
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'http://localhost:5000/api/auth/';

constructor(private http: HttpClient) { }


login(model: any) {
  return this.http.post(this.baseUrl + 'login', model)
  // allows us to chain our express operators to our request
  .pipe(
    map((response: any) => {
      const user = response;
      if (user) {
        localStorage.setItem('token', user.token);
      }
    })
  );
}

// register method to create new user
register(model: any) {
  return this.http.post(this.baseUrl + 'register', model);
}
}
