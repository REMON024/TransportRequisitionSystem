import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RequisitionApprovalComponent } from './requisition-approval.component';

describe('RequisitionApprovalComponent', () => {
  let component: RequisitionApprovalComponent;
  let fixture: ComponentFixture<RequisitionApprovalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RequisitionApprovalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RequisitionApprovalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
