import { Occupation } from "../models/occupation.model";

export interface Member {
  id: number;
  name: string;
  dob: Date;
  sumInsured: number;
  occupation: Occupation;
  premium: number;
}
