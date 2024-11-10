import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/authService/auth.service';
import { RegistrationComponent } from './registration.component';
import { of } from 'rxjs';

describe('RegisterComponent', () => {
  let component: RegistrationComponent;
  let fixture: ComponentFixture<RegistrationComponent>;
  let authServiceSpy: jasmine.SpyObj<AuthService>;
  let routerSpy: jasmine.SpyObj<Router>;

  beforeEach(async () => {
    // Create spies for the services
    const authSpy = jasmine.createSpyObj('AuthService', ['register']);
    const routeSpy = jasmine.createSpyObj('Router', ['navigate']);

    await TestBed.configureTestingModule({
      declarations: [RegistrationComponent],
      imports: [FormsModule],
      providers: [
        { provide: AuthService, useValue: authSpy },
        { provide: Router, useValue: routeSpy }
      ]
    })
    .compileComponents();

    authServiceSpy = TestBed.inject(AuthService) as jasmine.SpyObj<AuthService>;
    routerSpy = TestBed.inject(Router) as jasmine.SpyObj<Router>;

    fixture = TestBed.createComponent(RegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should call authService.register with user data', () => {
    component.name = 'New User'; // Add name
    component.email = 'newuser@test.com';
    component.password = 'password';
    component.confirmPassword = 'password';
    component.register();
  
    expect(authServiceSpy.register).toHaveBeenCalledWith({
      name: 'New User',      // Pass the name as part of the object
      email: 'newuser@test.com',
      password: 'password'
    });
  
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/']);
  });
  

  it('should display error if passwords do not match', () => {
    component.password = 'password';
    component.confirmPassword = 'differentpassword';
    component.register();

    expect(component.errorMessage).toEqual('Passwords do not match');
  });

  it('should display error if registration fails', () => {
    authServiceSpy.register.and.returnValue(of({ error: 'Registration failed' }));

    component.email = 'newuser@test.com';
    component.password = 'password';
    component.confirmPassword = 'password';
    component.register();

    expect(component.errorMessage).toEqual('Registration failed');
  });
});
