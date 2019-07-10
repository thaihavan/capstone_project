import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VirtualTripSmallComponent } from './virtual-trip-small.component';

describe('VirtualTripSmallComponent', () => {
  let component: VirtualTripSmallComponent;
  let fixture: ComponentFixture<VirtualTripSmallComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VirtualTripSmallComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VirtualTripSmallComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
