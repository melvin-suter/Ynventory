import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-image-viewer',
  templateUrl: './image-viewer.component.html',
  styleUrls: ['./image-viewer.component.scss']
})
export class ImageViewerComponent implements OnInit {

  @Input('visible') visible:boolean = true;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Input('imgUrl') imgUrl:string = "";
  @Output() imgUrlChange = new EventEmitter<string>();

  constructor() { }

  ngOnInit(): void {
  }

  closeViewer() {
    this.visible = false;
    this.visibleChange.emit(this.visible);
  }

}
