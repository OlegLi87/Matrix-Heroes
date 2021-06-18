export class AppError extends Error {
  constructor(message: string, public isCritical: boolean) {
    super(message);
  }
}
