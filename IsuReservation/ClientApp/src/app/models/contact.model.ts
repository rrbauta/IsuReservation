export interface Contact {
  id: number;
  name: string;
  phoneNumber: string;
  birthDate: string;
  contactTypeId: number;
  contactTypeName: string;
}

export interface ContactResponse {
  data: Contact;
  exception: string;
  isSuccess: boolean
}
