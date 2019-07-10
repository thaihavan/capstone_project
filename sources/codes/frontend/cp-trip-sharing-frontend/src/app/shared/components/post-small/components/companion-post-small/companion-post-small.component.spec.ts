import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanionPostSmallComponent } from './companion-post-small.component';

describe('CompanionPostSmallComponent', () => {
  let component: CompanionPostSmallComponent;
  let fixture: ComponentFixture<CompanionPostSmallComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CompanionPostSmallComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CompanionPostSmallComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
