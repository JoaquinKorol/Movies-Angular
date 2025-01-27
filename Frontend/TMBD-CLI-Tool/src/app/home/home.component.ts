import { Component, OnInit } from '@angular/core';
import { ApiServiceService } from '../../core/services/api-service.service';
import { Movie } from '../models/movie.model';
import { HeaderComponent } from '../header/header.component';
import { CommonModule } from '@angular/common';
import { DatePipe } from '@angular/common';
import { Router, RouterModule} from '@angular/router';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, DatePipe, HeaderComponent, RouterModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  nowPlayngMovies: Movie[] = [];
  popularMovies: Movie[] = [];
  topRatedMovies: Movie[] = [];
  upcomingMovies: Movie[] = [];
  category: string = '';
  allMovies: Movie[] = [];


  constructor(private service: ApiServiceService, private router: Router) { }

  ngOnInit(): void {
    // Cargar las primeras 4 películas de cada categoría
    this.loadMovies();
  }

  loadMovies() {
    this.allMovies = [];
    // Aquí se cargan todas las películas de cada categoría
    this.service.getNowPlayng().subscribe((data: Movie[]) => {
      this.nowPlayngMovies = data;  // No limitamos los resultados
    });
    this.service.getPopular().subscribe((data: Movie[]) => {
      this.popularMovies = data;  // No limitamos los resultados
    });
    this.service.getTopRated().subscribe((data: Movie[]) => {
      this.topRatedMovies = data;  // No limitamos los resultados
    });
    this.service.getUpcoming().subscribe((data: Movie[]) => {
      this.upcomingMovies = data;  // No limitamos los resultados
    });
}

  onCategorySelected(category: string): void {
    this.category = category; // Actualizar la categoría seleccionada
    this.loadCategoryMovies(category); // Cargar las películas correspondientes a la categoría
  }

  // Cargar las películas de la categoría seleccionada
  loadCategoryMovies(category: string) {
    switch (category) {
      case 'nowPlaying':
        this.allMovies = this.nowPlayngMovies;
        break;
      case 'popular':
        this.allMovies = this.popularMovies;
        break;
      case 'topRated':
        this.allMovies = this.topRatedMovies;
        break;
      case 'upcoming':
        this.allMovies = this.upcomingMovies;
        break;
      default:
        this.allMovies = [];
        break;
    }
  }

  isValidPoster(posterPath: string): boolean {
    return !!(posterPath && posterPath !== 'null' && posterPath.trim() !== '');
  }

  getRatingColor(vote: number): string {
    if (vote >= 8) {
      return 'green'; // Verde para calificaciones altas
    } else if (vote >= 6) {
      return 'yellow'; // Amarillo para calificaciones medias
    } else {
      return 'red'; // Rojo para calificaciones bajas
    }
  }

 
}

