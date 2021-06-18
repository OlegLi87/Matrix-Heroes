export interface Hero {
  name: string;
  startingPower: number;
  id?: string;
  suitColors?: string[];
  trainingStartDate?: Date;
  currentPower?: number;
  trainingsInCurrentSession?: number;
  abilityName?: string;
}
