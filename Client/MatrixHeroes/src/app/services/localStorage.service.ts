import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LocalStorageService {
  saveItem(key: string, item: string): void {
    localStorage.setItem(key, item);
  }

  getItem(key: string): string | null {
    return localStorage.getItem(key);
  }

  removeItem(key: string): void {
    localStorage.removeItem(key);
  }

  clearStorage(): void {
    localStorage.clear();
  }
}
