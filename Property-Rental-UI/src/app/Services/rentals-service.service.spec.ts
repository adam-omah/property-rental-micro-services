import { TestBed } from '@angular/core/testing';

import { RentalsServiceService } from './rentals-service.service';

describe('RentalsServiceService', () => {
  let service: RentalsServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RentalsServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
