import {ReservationModel} from "./reservation.model";

export interface ReservationPagingModel {
  data: Data;
  exception: string;
  isSuccess: boolean
}

export interface Data {
  actualPage: number;
  outcome: ReservationModel[];
  recordsPerPage: number;
  totalPages: number;
  totalRecords: number;
}
