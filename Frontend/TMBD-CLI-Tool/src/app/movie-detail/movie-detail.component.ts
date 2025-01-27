import { Component, Input } from '@angular/core';
import { Movie } from '../models/movie.model';
import { ApiServiceService } from '../../core/services/api-service.service';
import { ActivatedRoute } from '@angular/router';
import { DatePipe } from '@angular/common';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-movie-detail',
  imports: [DatePipe, CommonModule],
  templateUrl: './movie-detail.component.html',
  styleUrl: './movie-detail.component.css'
})
export class MovieDetailComponent {
  movideId: string | null = null;
  movieDetail: any = null;

  constructor(
    private route: ActivatedRoute,
    private service: ApiServiceService
  ){}
  ngOnInit(): void {
    this.movideId = this.route.snapshot.paramMap.get('id'); // Obtén el id de la URL

    if (this.movideId) {
      this.getMovieDetails(this.movideId);
    }
  }

  getMovieDetails(id: string): void {
    this.service.getDetails(id).subscribe((data) => {
      this.movieDetail = data;
    });
  }
}
