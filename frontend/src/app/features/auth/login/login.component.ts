import { Component, OnInit } from '@angular/core';
import { LoginRequest } from '../models/login-request.model';
import { AuthService } from '../services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  model: LoginRequest;

  constructor(private authService: AuthService,
    private cookieService: CookieService,
    private router: Router) {
    this.model = {
      username: '',
      password: ''
    }
  }
  ngOnInit(): void {
    this.cookieService.delete('Authorization', '/');
    this.authService.checkAuthorizationCookieExists();
  }

  onFormSubmit(): void {
    this.authService.login(this.model).subscribe({
      next: (response) => {
        this.cookieService.set('Authorization', `Bearer ${response.data}`,
          undefined, '/', undefined, true, 'Strict');

        this.authService.checkAuthorizationCookieExists();

        this.router.navigateByUrl('admin/batches');
      },
      error: (err) => {
        console.log(err.error);
      }
    });
  }
}
