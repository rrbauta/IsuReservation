export interface ContactType {
  id: number,
  name: string
}

export interface ContactTypeResponse {
  data: ContactType[];
  exception: string;
  isSuccess: boolean
}
