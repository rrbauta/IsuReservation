export interface Destination {
  id: string;
  name: string;
  rating: number;
  favorite: boolean;
  description: string;
  image: string;
}

export interface DestinationModel {
  data: Destination;
  exception: string;
  isSuccess: boolean
}

export interface DestinationPagingModel {
  data: Data;
  exception: string;
  isSuccess: boolean
}

export interface DestinationSingleListModel {
  data: Destination[];
  exception: string;
  isSuccess: boolean
}

export interface Data {
  actualPage: number;
  outcome: Destination[];
  recordsPerPage: number;
  totalPages: number;
  totalRecords: number;
}
