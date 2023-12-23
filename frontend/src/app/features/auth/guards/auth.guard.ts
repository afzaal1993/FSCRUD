import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { jwtDecode } from 'jwt-decode';

export const authGuard: CanActivateFn = (route, state) => {
  const cookieService = inject(CookieService);
  const router = inject(Router);

  let token = cookieService.get('Authorization');

  if (token) {
    token = token.replace('Bearer', '');
    const decodedToken: any = jwtDecode(token);

    const expirationDate = decodedToken.exp * 1000;
    const currentTime = new Date().getTime();

    if (expirationDate < currentTime) {
      return router.createUrlTree(['/login'], {
        queryParams: { returnUrl: state.url },
      });
    } else {
      return true;
    }
  } else {
    return router.createUrlTree(['/login'], {
      queryParams: { returnUrl: state.url },
    });
  }
};
