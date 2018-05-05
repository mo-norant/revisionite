import { TestBed, inject } from '@angular/core/testing';

import { ComputervisionService } from './computervision.service';

describe('ComputervisionService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ComputervisionService]
    });
  });

  it('should be created', inject([ComputervisionService], (service: ComputervisionService) => {
    expect(service).toBeTruthy();
  }));
});
