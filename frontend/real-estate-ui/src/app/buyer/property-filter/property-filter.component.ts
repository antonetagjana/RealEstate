import { Component, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-property-filter',
  templateUrl: './property-filter.component.html',
  styleUrls: ['./property-filter.component.scss'],
})
export class PropertyFilterComponent {
  @Output() filtersChanged = new EventEmitter<any>(); // Event për të dërguar filtrat në komponentin prind

  // Fushat për filtrat
  filters = {
    minPrice: null,
    maxPrice: null,
    category: '',
    location: '',
    floors: null,
  };

  // Funksioni që dërgon filtrat kur përdoruesi klikon
  applyFilters() {
    this.filtersChanged.emit(this.filters);
  }
}
