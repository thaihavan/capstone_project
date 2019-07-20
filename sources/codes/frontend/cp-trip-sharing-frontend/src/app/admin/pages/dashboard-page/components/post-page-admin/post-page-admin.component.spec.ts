import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PostPageAdminComponent } from './post-page-admin.component';

describe('PostPageAdminComponent', () => {
  let component: PostPageAdminComponent;
  let fixture: ComponentFixture<PostPageAdminComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PostPageAdminComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PostPageAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
