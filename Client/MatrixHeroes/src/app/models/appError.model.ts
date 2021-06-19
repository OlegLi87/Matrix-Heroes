export enum ErrorLevel {
  WARNING,
  FATAL,
}

export class AppError extends Error {
  constructor(
    message: string,
    public statusCode: number,
    public errorLevel: ErrorLevel
  ) {
    super(message);
  }
}
