/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { DialogStepFindingCompanionsComponent } from './dialog-step-finding-companions.component';

describe('DialogStepFindingCompanionsComponent', () => {
  let component: DialogStepFindingCompanionsComponent;
  let fixture: ComponentFixture<DialogStepFindingCompanionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DialogStepFindingCompanionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogStepFindingCompanionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
