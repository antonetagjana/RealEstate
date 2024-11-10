import { TestBed } from '@angular/core/testing';

import { PropertyPhotoServiceService } from './property-photo-service.service';

describe('PropertyPhotoServiceService', () => {
  let service: PropertyPhotoServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PropertyPhotoServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
