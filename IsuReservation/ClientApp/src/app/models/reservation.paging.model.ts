import {Reservation} from "./reservation";
import {Destination} from "./destination.model";

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

export interface DestinationModel {
  data: Destination;
  exception: string;
  isSuccess: boolean
}
