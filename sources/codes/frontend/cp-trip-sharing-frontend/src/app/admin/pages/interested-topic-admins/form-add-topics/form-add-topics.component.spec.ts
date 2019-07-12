import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FormAddTopicsComponent } from './form-add-topics.component';

describe('FormAddTopicsComponent', () => {
  let component: FormAddTopicsComponent;
  let fixture: ComponentFixture<FormAddTopicsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FormAddTopicsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FormAddTopicsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
