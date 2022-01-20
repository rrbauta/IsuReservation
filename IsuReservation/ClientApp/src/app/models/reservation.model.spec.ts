import {Reservation

.
Model
}
from
'./reservation.model';

describe('Reservation.Model', () => {
  it('should create an instance', () => {
    expect(new Reservation.Model()).toBeTruthy();
  });
});
