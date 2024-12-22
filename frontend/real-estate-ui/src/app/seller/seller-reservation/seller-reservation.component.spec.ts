import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SellerReservationComponent } from './seller-reservation.component';

describe('SellerReservationComponent', () => {
  let component: SellerReservationComponent;
  let fixture: ComponentFixture<SellerReservationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SellerReservationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SellerReservationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
