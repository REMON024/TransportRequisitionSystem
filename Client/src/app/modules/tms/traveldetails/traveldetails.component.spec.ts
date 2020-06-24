import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TraveldetailsComponent } from './traveldetails.component';

describe('TraveldetailsComponent', () => {
  let component: TraveldetailsComponent;
  let fixture: ComponentFixture<TraveldetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TraveldetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TraveldetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
