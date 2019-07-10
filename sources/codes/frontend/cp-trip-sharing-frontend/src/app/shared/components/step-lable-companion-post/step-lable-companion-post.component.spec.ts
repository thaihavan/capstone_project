/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { StepLableCompanionPostComponent } from './step-lable-companion-post.component';

describe('StepLableCompanionPostComponent', () => {
  let component: StepLableCompanionPostComponent;
  let fixture: ComponentFixture<StepLableCompanionPostComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StepLableCompanionPostComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StepLableCompanionPostComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
