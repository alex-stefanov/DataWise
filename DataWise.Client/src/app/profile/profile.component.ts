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
    const storedUser = localStorage.getItem('user');
    if (storedUser) {
      this.user = JSON.parse(storedUser);
      this.isLoggedIn = true;
    }
  }

  updateProfile() {
    const url = `${environment.apiUrl}/api/user/profile`;
    this.http.put<any>(url, {
      email: this.user.email,
      firstName: this.user.firstName,
      lastName: this.user.lastName
    }).subscribe(
      data => {
        console.log('Profile updated successfully', data);
        localStorage.setItem('user', JSON.stringify(this.user));
      },
      error => {
        console.error('Error updating profile', error);
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
        console.log('Logged in successfully', data);
        this.loginError = '';
        this.isLoggedIn = true;
        this.user.email = this.loginEmail;
        if (data.user) {
          this.user.firstName = data.user.firstName;
          this.user.lastName = data.user.lastName;
          this.user.points = data.user.points;
        }
        localStorage.setItem('user', JSON.stringify(this.user));
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
        console.log('Registered successfully', data);
        this.regError = '';
        this.isLoggedIn = true;
        this.user.email = this.regEmail;
        this.user.firstName = this.regFirstName;
        this.user.lastName = this.regLastName;
        this.user.points = 0;
        localStorage.setItem('user', JSON.stringify(this.user));
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
        console.log('Logged out successfully', data);
        this.isLoggedIn = false;
        localStorage.removeItem('user');
      },
      error => {
        console.error('Error during logout', error);
      }
    );
  }

  toggleAuthMode() {
    this.isLoginMode = !this.isLoginMode;
    this.loginError = '';
    this.regError = '';
  }
}