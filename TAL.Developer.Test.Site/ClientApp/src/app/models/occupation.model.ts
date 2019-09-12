import { OccupationRating } from "../models/occupation-rating.model";

export interface Occupation {
  id: number;
  name: string;
  occupationRating: OccupationRating;
}
