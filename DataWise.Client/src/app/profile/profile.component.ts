import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, FormsModule, HttpClientModule],
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})

export class ProfileComponent implements OnInit {
  user = {
    id: '',
    email: '',
    firstName: '',
    lastName: '',
    points: 0
  };

  isLoggedIn = false;
  isLoginMode = true;

  loginEmail: string = '';
  loginPassword: string = '';
  rememberMe: boolean = false;
  loginError: string = '';

  regEmail: string = '';
  regPassword: string = '';
  regFirstName: string = '';
  regLastName: string = '';
  regError: string = '';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    const userId = sessionStorage.getItem('userId');
    if (userId) {
      this.user.id = userId;
      this.fetchUserProfile();
    }
  }

  fetchUserProfile() {
    const userId = sessionStorage.getItem('userId');

    const payload = {
      userId: userId
    };

    const url = `${environment.apiUrl}/api/user/profile`;

    this.http.post<any>(url, payload).subscribe(
      data => {
        this.user.email = data.email;
        this.user.firstName = data.firstName;
        this.user.lastName = data.lastName;
        this.user.points = data.points;
        this.isLoggedIn = true;
      },
      error => {
        console.error('Error fetching profile', error);
      }
    );
  }

  login() {
    const payload = {
      email: this.loginEmail,
      password: this.loginPassword,
      rememberMe: this.rememberMe
    };

    const url = `${environment.apiUrl}/api/user/login`;
    this.http.post<any>(url, payload).subscribe(
      data => {
        this.loginError = '';
        this.user.id = data.userId;
        sessionStorage.setItem('userId', this.user.id);
        this.fetchUserProfile(); 

        this.isLoggedIn = true;
        this.loginEmail = '';
        this.loginPassword = '';
      },
      error => {
        console.error('Login error', error);
        this.loginError = error.error.message || 'Login failed. Please try again.';
      }
    );
  }

  register() {
    const payload = {
      email: this.regEmail,
      password: this.regPassword,
      firstName: this.regFirstName,
      lastName: this.regLastName
    };

    const url = `${environment.apiUrl}/api/user/register`;
    this.http.post<any>(url, payload).subscribe(
      data => {
        this.regError = '';
        this.user.id = data.userId;

        sessionStorage.setItem('userId', this.user.id);
        this.fetchUserProfile();

        this.isLoggedIn = true;
        this.regEmail = '';
        this.regPassword = '';
        this.regFirstName = '';
        this.regLastName = '';
      },
      error => {
        console.error('Registration error', error);
        this.regError = error.error.message || 'Registration failed. Please try again.';
      }
    );
  }

  logout() {
    const url = `${environment.apiUrl}/api/user/logout`;
    this.http.post<any>(url, {}).subscribe(
      data => {
        console.log('Logged out successfully', data.message);
        this.isLoggedIn = false;
        sessionStorage.removeItem('userId');
        this.user = { id: '', email: '', firstName: '', lastName: '', points: 0 };
      },
      error => {
        console.error('Error during logout', error);
      }
    );
  }

  updateProfile() {
    const url = `${environment.apiUrl}/api/user/profile`;
    this.http.put<any>(url, {
      email: this.user.email,
      firstName: this.user.firstName,
      lastName: this.user.lastName
    }).subscribe(
      data => {
        console.log('Profile updated successfully', data.message);
      },
      error => {
        console.error('Error updating profile', error);
      }
    );
  }

  toggleAuthMode() {
    this.isLoginMode = !this.isLoginMode;
    this.loginError = '';
    this.regError = '';
  }
}