import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Movie } from '../../app/models/movie.model';

@Injectable({
  providedIn: 'root'
})
export class ApiServiceService {

  baseUrl = 'https://localhost:54594';
  constructor(private http: HttpClient) { }

  getNowPlayng():Observable<Movie[]>{
    return this.http.get<Movie[]>(`${this.baseUrl}/movies/now-playing`);
  }

  getPopular():Observable<Movie[]>{
    return this.http.get<Movie[]>(`${this.baseUrl}/movies/popular`);
  }

  getTopRated():Observable<Movie[]>{
    return this.http.get<Movie[]>(`${this.baseUrl}/movies/top-rated`);
  }

  getUpcoming():Observable<Movie[]>{
    return this.http.get<Movie[]>(`${this.baseUrl}/movies/upcoming`);
  }

  getDetails(id: string):Observable<any>{
    return this.http.get<any>(`${this.baseUrl}/movies/${id}`);
  }
}
