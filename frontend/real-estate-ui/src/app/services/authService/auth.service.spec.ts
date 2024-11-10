import { TestBed } from '@angular/core/testing';
import { AuthService } from './auth.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

describe('AuthService', () => {
  let service: AuthService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [AuthService]
    });
    service = TestBed.inject(AuthService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify(); // Verifikon që nuk ka kërkesa të mbetura
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should send login request and store token', () => {
    const mockToken = '1234567890';
    const credentials = { email: 'test@test.com', password: 'password123' };

    service.login(credentials).subscribe(response => {
      expect(response.token).toBe(mockToken);
      service.setToken(mockToken); // Ruaj token-in
      expect(service.getToken()).toBe(mockToken); // Kontrollo që është ruajtur token-i
    });

    const req = httpMock.expectOne(`${service['apiUrl']}/login`);
    expect(req.request.method).toBe('POST');
    req.flush({ token: mockToken }); // Simulo një përgjigje nga serveri
  });

  it('should send register request and store token', () => {
    const mockToken = '0987654321';
    const userData = { fullName: 'John Doe', email: 'john@example.com', password: 'password123',phoneNumber: '0896754123',role:'buyer' };

    service.register(userData).subscribe(response => {
      expect(response.token).toBe(mockToken);
      service.setToken(mockToken);
      expect(service.getToken()).toBe(mockToken);
    });

    const req = httpMock.expectOne(`${service['apiUrl']}/register`);
    expect(req.request.method).toBe('POST');
    req.flush({ token: mockToken });
  });

  it('should clear token on logout', () => {
    service.setToken('some-token');
    service.logout();
    expect(service.getToken()).toBeNull(); // Kontrollo që token-i është fshirë
  });
});
