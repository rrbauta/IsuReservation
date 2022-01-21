import {Contact} from "./contact.model";

export interface ContactPagingModel {
  data: Data;
  exception: string;
  isSuccess: boolean
}

export interface Data {
  actualPage: number;
  outcome: Contact[];
  recordsPerPage: number;
  totalPages: number;
  totalRecords: number;
}
