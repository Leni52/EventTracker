import { Category } from "../enums/Category";

export interface EventModelResponse{
    id: string;
    name: string;
    category: Category;
    location: string;
    description: string;
    startDate: Date;
    endDate: Date;
}
