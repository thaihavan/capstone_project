/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { InterestedTopicComponent } from './interested-toppic.component';

describe('InterestedTopicComponent', () => {
  let component: InterestedTopicComponent;
  let fixture: ComponentFixture<InterestedTopicComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ InterestedTopicComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(InterestedTopicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
