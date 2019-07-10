/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { StepFindingCompanionsComponent } from './step-finding-companions.component';

describe('StepFindingCompanionsComponent', () => {
  let component: StepFindingCompanionsComponent;
  let fixture: ComponentFixture<StepFindingCompanionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StepFindingCompanionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StepFindingCompanionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
