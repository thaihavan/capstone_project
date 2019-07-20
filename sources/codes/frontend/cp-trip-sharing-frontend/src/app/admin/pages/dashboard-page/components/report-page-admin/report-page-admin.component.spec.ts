import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportPageAdminComponent } from './report-page-admin.component';

describe('ReportPageAdminComponent', () => {
  let component: ReportPageAdminComponent;
  let fixture: ComponentFixture<ReportPageAdminComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReportPageAdminComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReportPageAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
