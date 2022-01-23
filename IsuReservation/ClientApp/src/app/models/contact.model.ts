export interface Contact {
  id: string;
  name: string;
  phoneNumber: string;
  birthDate: string;
  contactTypeId: string;
  contactTypeName: string;
}

export interface ContactResponse {
  data: Contact;
  exception: string;
  isSuccess: boolean
}
