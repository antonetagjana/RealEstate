import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminPropertyComponent } from './admin-property.component';

describe('AdminPropertyComponent', () => {
  let component: AdminPropertyComponent;
  let fixture: ComponentFixture<AdminPropertyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdminPropertyComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdminPropertyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
