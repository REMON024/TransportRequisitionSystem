import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { PaginationInstance } from 'ngx-pagination';

@Component({
  selector: 'custom-pagination',
  templateUrl: './custom-template.component.html',
  styleUrls: ['./custom-template.component.scss']
})
export class CustomPaginationComponent implements OnInit {

  @Input() config: PaginationInstance;
  @Input() previousButtonText: string = 'Previous';
  @Input() nextButtonText: string = 'Next';
  @Output() pageChange: EventEmitter<number> = new EventEmitter();
  
  constructor() { }

  ngOnInit(): void {

  }

  public PageChangeTrack(pageNumber) {
    this.pageChange.emit(pageNumber);
  }

}
