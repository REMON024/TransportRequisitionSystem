import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdvancedReportComponent } from './advanced-report.component';

describe('AdvancedReportComponent', () => {
  let component: AdvancedReportComponent;
  let fixture: ComponentFixture<AdvancedReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdvancedReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdvancedReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
