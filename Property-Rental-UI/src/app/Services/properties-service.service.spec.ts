import { TestBed } from '@angular/core/testing';

import { PropertiesService } from './properties-service.service';

describe('PropertiesServiceService', () => {
  let service: PropertiesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PropertiesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
