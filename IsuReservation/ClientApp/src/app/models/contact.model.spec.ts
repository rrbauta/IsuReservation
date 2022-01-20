import {Contact

.
Model
}
from
'./contact.model';

describe('Contact.Model', () => {
  it('should create an instance', () => {
    expect(new Contact.Model()).toBeTruthy();
  });
});
