import { Component, Output, EventEmitter } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-header',
  imports: [RouterModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  @Output() categorySelected = new EventEmitter<string>();

  // Función que emite el evento con la categoría seleccionada
  onCategorySelect(category: string) {
    this.categorySelected.emit(category);
  }
  scrollToTop() {
    console.log('scrolling to top');
    window.scrollTo({top: 0, behavior: 'smooth'});
  }
}
