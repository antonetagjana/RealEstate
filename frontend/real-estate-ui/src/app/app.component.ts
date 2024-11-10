import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './components/header/header.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,HeaderComponent],
  template: 
  `
  <app-header></app-header>
  <div class="container mt-4">
    <router-outlet></router-outlet>
  </div>
`,
  styles: []
})
export class AppComponent { }