import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailpostPageComponent } from './detailpost-page.component';

describe('DetailpostPageComponent', () => {
  let component: DetailpostPageComponent;
  let fixture: ComponentFixture<DetailpostPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DetailpostPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DetailpostPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
