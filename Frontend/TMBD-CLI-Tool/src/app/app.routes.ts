import { Routes } from '@angular/router';
import { MovieDetailComponent } from './movie-detail/movie-detail.component';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './header/header.component';

export const routes: Routes = [
    { path: '', redirectTo: '/home', pathMatch: 'full' }, // Redirige a una ruta concreta
  { path: 'home', component: HomeComponent },
    { path: 'movie/:id', component: MovieDetailComponent },
  ];