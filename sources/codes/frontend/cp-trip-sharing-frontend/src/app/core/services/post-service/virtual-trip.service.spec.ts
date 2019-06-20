/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { VirtualTripService } from './virtual-trip.service';

describe('Service: VirtualTrip', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [VirtualTripService]
    });
  });

  it('should ...', inject([VirtualTripService], (service: VirtualTripService) => {
    expect(service).toBeTruthy();
  }));
});
