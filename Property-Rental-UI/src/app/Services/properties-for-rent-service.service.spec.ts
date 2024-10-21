import { TestBed } from '@angular/core/testing';

import { PropertiesForRentServiceService } from './properties-for-rent-service.service';

describe('PropertiesForRentServiceService', () => {
  let service: PropertiesForRentServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PropertiesForRentServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
