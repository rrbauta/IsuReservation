import {ComponentFixture, TestBed} from '@angular/core/testing';

import {CustomPhoneInputComponent} from './custom-phone-input.component';

describe('CustomPhoneInputComponent', () => {
  let component: CustomPhoneInputComponent;
  let fixture: ComponentFixture<CustomPhoneInputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CustomPhoneInputComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomPhoneInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
