import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { UserService } from './user.service';

describe('UserService', () => {
  let service: UserService;
  let httpMock: HttpTestingController;
  const apiUrl = 'https://api.example.com/users'; // Replace with your actual API URL

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [UserService]
    });
    service = TestBed.inject(UserService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  // Test getUserProfile
  it('should retrieve the user profile', () => {
    const dummyProfile = { name: 'John Doe', email: 'johndoe@example.com' };

    service.getUserProfile().subscribe(profile => {
      expect(profile).toEqual(dummyProfile);
    });

    const req = httpMock.expectOne(`${apiUrl}/profile`);
    expect(req.request.method).toBe('GET');
    req.flush(dummyProfile);
  });

  // Test updateUserProfile
  it('should update the user profile', () => {
    const userData = { name: 'Jane Doe', email: 'janedoe@example.com' };

    service.updateUserProfile(userData).subscribe(response => {
      expect(response).toBeTruthy();
    });

    const req = httpMock.expectOne(`${apiUrl}/profile`);
    expect(req.request.method).toBe('PUT');
    req.flush({ success: true });
  });

  // Test changePassword
  it('should change the user password', () => {
    const passwordData = { oldPassword: 'oldPass', newPassword: 'newPass' };

    service.changePassword(passwordData).subscribe(response => {
      expect(response).toBeTruthy();
    });

    const req = httpMock.expectOne(`${apiUrl}/change-password`);
    expect(req.request.method).toBe('POST');
    req.flush({ success: true });
  });
});
