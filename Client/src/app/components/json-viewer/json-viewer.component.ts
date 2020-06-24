import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-json-viewer',
  templateUrl: './json-viewer.component.html',
  styleUrls: ['./json-viewer.component.scss']
})
export class JsonViewerComponent implements OnInit {

  public json_data: any;

  constructor(
    public dialogRef: MatDialogRef<JsonViewerComponent>,
    @Inject(MAT_DIALOG_DATA) public data: string
  ) { 
    this.json_data = JSON.parse(data);
  }

  ngOnInit() {
  }

  public CloseDialog() {
    this.dialogRef.close();
  }

}
