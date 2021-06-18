import { Injectable } from '@angular/core';
import jwtDecode from 'jwt-decode';
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
}
