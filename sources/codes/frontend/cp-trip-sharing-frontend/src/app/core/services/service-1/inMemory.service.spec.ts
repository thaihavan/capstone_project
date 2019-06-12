/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { InMemoryService } from './inMemory.service';

describe('Service: InMemory', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [InMemoryService]
    });
  });

  it('should ...', inject([InMemoryService], (service: InMemoryService) => {
    expect(service).toBeTruthy();
  }));
});
