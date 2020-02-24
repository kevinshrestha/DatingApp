import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/user';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

// method to get all Users
getUsers(): Observable<User[]> {
  return this.http.get<User[]>(this.baseUrl + 'users');
}

// method to get specific User
getUser(id): Observable<User> {
  return this.http.get<User>(this.baseUrl + 'users/' + id);
}

}
