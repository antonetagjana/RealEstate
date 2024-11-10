import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReservationComponent } from './reservation.component';
import { ReservationService } from '../../services/reservationService/reservation.service';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';

describe('ReservationComponent', () => {
  let component: ReservationComponent;
  let fixture: ComponentFixture<ReservationComponent>;
  let reservationService: jasmine.SpyObj<ReservationService>;

  beforeEach(async () => {
    const reservationServiceSpy = jasmine.createSpyObj('ReservationService', ['getReservationsByProperty', 'createReservation']);
    
    await TestBed.configureTestingModule({
      declarations: [ReservationComponent],
      providers: [
        { provide: ReservationService, useValue: reservationServiceSpy },
        { provide: ActivatedRoute, useValue: { snapshot: { paramMap: { get: () => '123' } } } }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(ReservationComponent);
    component = fixture.componentInstance;
    reservationService = TestBed.inject(ReservationService) as jasmine.SpyObj<ReservationService>;
    fixture.detectChanges();
  });

  it('should load reservations on init', () => {
    const dummyReservations = [{ date: '2023-10-20', guests: 4 }];
    reservationService.getReservationsByPropertyId.and.returnValue(of(dummyReservations));
    component.ngOnInit();
    expect(component.reservations).toEqual(dummyReservations);
  });

  it('should create a reservation', () => {
    reservationService.createReservation.and.returnValue(of({}));
    component.newReservation = { date: '2023-10-21', guests: 2 };
    component.createReservation();
    expect(reservationService.createReservation).toHaveBeenCalled();
  });
});
