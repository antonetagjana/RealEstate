import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { ProfileComponent } from './profile.component';
import { UserService } from '../../services/userService/user.service';
import { of, throwError } from 'rxjs';

describe('ProfileComponent', () => {
  let component: ProfileComponent;
  let fixture: ComponentFixture<ProfileComponent>;
  let userServiceSpy: jasmine.SpyObj<UserService>;

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('UserService', ['getUserProfile', 'updateUserProfile', 'changePassword']);

    await TestBed.configureTestingModule({
      declarations: [ ProfileComponent ],
      imports: [ FormsModule ],
      providers: [{ provide: UserService, useValue: spy }]
    })
    .compileComponents();

    userServiceSpy = TestBed.inject(UserService) as jasmine.SpyObj<UserService>;
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load user profile on init', () => {
    const mockProfile = { fullName: 'John Doe', email: 'john@example.com', phone: '123456', address: '123 Main St' };
    userServiceSpy.getUserProfile.and.returnValue(of(mockProfile));

    component.ngOnInit();
    expect(component.profile).toEqual(mockProfile);
  });

  it('should show error if profile load fails', () => {
    userServiceSpy.getUserProfile.and.returnValue(throwError('Error'));
    
    component.ngOnInit();
    expect(component.errorMessage).toBe('Error loading profile');
  });

  it('should save updated profile', () => {
    userServiceSpy.updateUserProfile.and.returnValue(of({}));

    component.updateProfile();
    expect(component.successMessage).toBe('Profile updated successfully!');
  });
});
