import { TestBed } from '@angular/core/testing';

import { PropertiesServiceService } from './properties-service.service';

describe('PropertiesServiceService', () => {
  let service: PropertiesServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PropertiesServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
