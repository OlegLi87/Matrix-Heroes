import { APP_INITIALIZER, Provider } from '@angular/core';
import { AuthService } from './../services/auth.service';

function streamUser(authService: AuthService): () => void {
  return () => {
    authService.streamUserAtAppInit();
  };
}

export const appInitProvider: Provider = {
  provide: APP_INITIALIZER,
  useFactory: streamUser,
  multi: true,
  deps: [AuthService],
};
