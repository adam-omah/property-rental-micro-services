import { TestBed } from '@angular/core/testing';

import { OwnersServiceService } from './owners-service.service';

describe('OwnersServiceService', () => {
  let service: OwnersServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OwnersServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
