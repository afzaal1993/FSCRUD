import { Injectable } from '@angular/core';
import { LoginRequest } from '../models/login-request.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { genericResult } from '../../../core/models/generic-result.model';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment.development';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  public $userExists = new BehaviorSubject<boolean | undefined>(false);
  constructor(private http: HttpClient, private cookieService: CookieService) { }

  login(request: LoginRequest): Observable<genericResult> {
    return this.http.post<genericResult>(`${environment.apiBaseUrl}/auth/login`, request);
  }

  checkAuthorizationCookieExists(): void {
    if (this.cookieService.check('Authorization')) {
      this.$userExists.next(true);
    }
    else {
      this.$userExists.next(false);
    }
  }
}
