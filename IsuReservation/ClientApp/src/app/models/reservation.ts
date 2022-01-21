import {Time} from "@angular/common";
import {Destination} from "./destination.model";
import {Contact} from "./contact.model";

export interface Reservation {
  id: string;
  description: string;
  date: Date;
  time: Time;
  contact: Contact;
  destination: Destination;
}

