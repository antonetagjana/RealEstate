import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { SearchComponent } from './search.component';
import { HeaderComponent } from '../header/header.component';


describe('SearchComponent', () => {
  let component: SearchComponent;
  let fixture: ComponentFixture<SearchComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchComponent, HeaderComponent ],
      imports: [ FormsModule, RouterTestingModule ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display search filters', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.search-filters')).toBeTruthy();
  });

  it('should display property list', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.property-list')).toBeTruthy();
  });

  it('should display correct number of properties', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    const propertyCards = compiled.querySelectorAll('.property-card');
    expect(propertyCards.length).toBe(component.properties.length);
  });

  it('should call applyFilters method when form is submitted', () => {
    spyOn(component, 'applyFilters');
    const form = fixture.nativeElement.querySelector('form');
    form.dispatchEvent(new Event('submit'));
    expect(component.applyFilters).toHaveBeenCalled();
  });

  it('should call viewDetails method when view details button is clicked', () => {
    spyOn(component, 'viewDetails');
    const viewDetailsBtn = fixture.nativeElement.querySelector('.view-details-btn');
    viewDetailsBtn.click();
    expect(component.viewDetails).toHaveBeenCalled();
  });
});