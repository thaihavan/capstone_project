/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { FindingCompanionService } from './finding-companion.service';

describe('Service: FindingCompanion', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FindingCompanionService]
    });
  });

  it('should ...', inject([FindingCompanionService], (service: FindingCompanionService) => {
    expect(service).toBeTruthy();
  }));
});
