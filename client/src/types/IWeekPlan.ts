import { IDayPlan } from "./IDayPlan";

export interface IWeekPlan {
    monday: IDayPlan,
    tuesday: IDayPlan,
    wednesday: IDayPlan,
    thursday: IDayPlan,
    friday: IDayPlan,
    saturday: IDayPlan,
    sunday: IDayPlan
}