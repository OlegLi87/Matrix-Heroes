import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import jwtDecode from 'jwt-decode';
import { AppError, ErrorLevel } from '../models/appError.model';
import { User } from '../models/user.model';

interface TokenPayload {
  unique_name: string;
  role: string;
}

@Injectable({ providedIn: 'root' })
export class AppUtilsService {
  createUserFromToken(token: string): User {
    try {
      const decodedToken: TokenPayload = jwtDecode(token);
      return {
        name: decodedToken.unique_name,
        role: decodedToken.role,
        token,
      };
    } catch (e) {
      return null;
    }
  }

  createAppError(error: HttpErrorResponse): AppError {
    if (!error.status)
      return new AppError(
        'Request wasnt delivered because of error.',
        0,
        ErrorLevel.FATAL
      );

    if (error.error) {
      let errorLevel =
        error.status >= 500 ? ErrorLevel.FATAL : ErrorLevel.WARNING;
      let message = '';
      (error.error.errors as string[]).forEach((err) => (message += err + ';'));
      message = message.slice(0, message.length - 1);
      return new AppError(message, error.status, errorLevel);
    }

    if (error.status === 401)
      return new AppError('UnAuthorized', 401, ErrorLevel.WARNING);

    return new AppError('Unknown Network Error', 0, ErrorLevel.FATAL);
  }
}
