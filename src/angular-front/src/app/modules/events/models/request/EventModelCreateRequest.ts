import { Category } from "../enums/Category";

export interface EventModelCreateRequest{
    name: string;
    category: Number;
    location: string;
    description: string;
    startDate: Date;
    endDate: Date;
}