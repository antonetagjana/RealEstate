import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { FavoriteService } from './favorite.service';

describe('FavoriteService', () => {
  let service: FavoriteService;
  let httpMock: HttpTestingController;
  const apiUrl = 'https://your-api-url.com/api/favorites'; // Replace with your actual API URL

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [FavoriteService],
    });

    service = TestBed.inject(FavoriteService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should retrieve all favorites (GET)', () => {
    const mockFavorites = [
      { id: '1', title: 'Favorite Property 1' },
      { id: '2', title: 'Favorite Property 2' },
    ];

    service.getFavorites().subscribe((favorites) => {
      expect(favorites.length).toBe(2);
      expect(favorites).toEqual(mockFavorites);
    });

    const req = httpMock.expectOne(`${apiUrl}`);
    expect(req.request.method).toBe('GET');
    req.flush(mockFavorites);
  });

  it('should add a favorite (POST)', () => {
    const propertyId = '1';
    const mockResponse = { message: 'Property added to favorites' };

    service.addFavorite(propertyId).subscribe((response) => {
      expect(response).toEqual(mockResponse);
    });

    const req = httpMock.expectOne(`${apiUrl}/add`);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual({ propertyId });
    req.flush(mockResponse);
  });

  it('should remove a favorite (DELETE)', () => {
    const propertyId = '1';
    const mockResponse = { message: 'Property removed from favorites' };

    service.removeFavorite(propertyId).subscribe((response) => {
      expect(response).toEqual(mockResponse);
    });

    const req = httpMock.expectOne(`${apiUrl}/remove/${propertyId}`);
    expect(req.request.method).toBe('DELETE');
    req.flush(mockResponse);
  });
});
