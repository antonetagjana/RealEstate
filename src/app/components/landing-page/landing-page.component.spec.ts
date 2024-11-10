import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule } from '@angular/forms';
import { LandingPageComponent } from './landing-page.component';
import { HeaderComponent } from '../header/header.component';

describe('LandingPageComponent', () => {
  let component: LandingPageComponent;
  let fixture: ComponentFixture<LandingPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RouterTestingModule, FormsModule],
      declarations: [ LandingPageComponent, HeaderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LandingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have a search input', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('input.search-input')).toBeTruthy();
  });

  it('should have featured properties', () => {
    expect(component.featuredProperties.length).toBeGreaterThan(0);
  });

  it('should display featured properties', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    const propertyCards = compiled.querySelectorAll('.property-card');
    expect(propertyCards.length).toBe(component.featuredProperties.length);
  });

  it('should have a CTA section', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.cta')).toBeTruthy();
  });

  it('should have a footer', () => {
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('footer')).toBeTruthy();
  });

  it('should call search method when search button is clicked', () => {
    spyOn(component, 'search');
    const searchButton = fixture.nativeElement.querySelector('.search-button');
    searchButton.click();
    expect(component.search).toHaveBeenCalled();
  });

  it('should call viewDetails method when view details button is clicked', () => {
    spyOn(component, 'viewDetails');
    const viewDetailsBtn = fixture.nativeElement.querySelector('.view-details-btn');
    viewDetailsBtn.click();
    expect(component.viewDetails).toHaveBeenCalled();
  });
});