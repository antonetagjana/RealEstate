import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { FavoritesComponent } from './favorites.component';
import { HeaderComponent } from '../../header/header.component';

describe('FavoritesComponent', () => {
  let component: FavoritesComponent;
  let fixture: ComponentFixture<FavoritesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FavoritesComponent, HeaderComponent ],
      imports: [ RouterTestingModule ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FavoritesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display favorite properties', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelectorAll('.property-card').length).toBe(3);
  });

  it('should remove a property when remove button is clicked', () => {
    const initialCount = component.favorites.length;
    const removeButton = fixture.nativeElement.querySelector('.remove-favorite-btn');
    removeButton.click();
    expect(component.favorites.length).toBe(initialCount - 1);
  });

  it('should display no favorites message when there are no favorites', () => {
    component.favorites = [];
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.no-favorites-message')).toBeTruthy();
  });
});