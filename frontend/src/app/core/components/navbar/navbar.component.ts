import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../features/auth/services/auth.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  username?: string;

  constructor(public authService: AuthService,
    private cookieService: CookieService,
    private router: Router) { }

  ngOnInit(): void {
    this.authService.checkAuthorizationCookieExists();
  }

  logout() {
    this.cookieService.delete('Authorization', '/');
    this.router.navigateByUrl('/');
  }

}
