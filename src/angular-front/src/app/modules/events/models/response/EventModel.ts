import { Category } from "./Category";

export interface EventModel{
    id: string;
    name: string;
    category: number;
    location: string;
    description: string;
    startDate: Date;
    endDate: Date;
}
