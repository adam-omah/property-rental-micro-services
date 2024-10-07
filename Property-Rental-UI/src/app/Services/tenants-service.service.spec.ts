import { TestBed } from '@angular/core/testing';

import { TenantsServiceService } from './tenants-service.service';

describe('TenantsServiceService', () => {
  let service: TenantsServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TenantsServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
