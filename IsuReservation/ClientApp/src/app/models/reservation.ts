import {Time} from "@angular/common";
import {Destination} from "./destination.model";
import {Contact} from "./contact.model";

export interface Reservation {
  id: string;
  description: string;
  date: Date;
  displayDate: Date;
  time: Time;
  contact: Contact;
  destination: Destination;
}

export interface ReservationResponse {
  data: Reservation;
  exception: string;
  isSuccess: boolean
}

export interface ReservationPagingModel {
  data: Data;
  exception: string;
  isSuccess: boolean
}

export interface Data {
  actualPage: number;
  outcome: Reservation[];
  recordsPerPage: number;
  totalPages: number;
  totalRecords: number;
}

