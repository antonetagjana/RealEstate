import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { PropertyService } from './property.service';

describe('PropertyService', () => {
  let service: PropertyService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [PropertyService]
    });
    service = TestBed.inject(PropertyService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  it('should retrieve all properties', () => {
    const dummyProperties = [
      { id: 1, title: 'Test Property 1' },
      { id: 2, title: 'Test Property 2' }
    ];
  
    service.getAllProperties().subscribe((properties) => {
      expect(properties.length).toBe(2);
      expect(properties).toEqual(dummyProperties);
    });
  
    const request = httpMock.expectOne('https://your-backend-api-url.com/api/properties'); // Replace with actual URL
    expect(request.request.method).toBe('GET');
    request.flush(dummyProperties);
  });
  

  // Add tests for other methods...

  afterEach(() => {
    httpMock.verify();
  });
});
