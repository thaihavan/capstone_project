import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OverviewPageAdminComponent } from './overview-page-admin.component';

describe('OverviewPageAdminComponent', () => {
  let component: OverviewPageAdminComponent;
  let fixture: ComponentFixture<OverviewPageAdminComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OverviewPageAdminComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OverviewPageAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
