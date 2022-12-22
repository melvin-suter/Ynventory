import { Component, Input, OnInit } from '@angular/core';
import { Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss']
})
export class ModalComponent implements OnInit {

  @Input('title') title:string = "Modal";
  @Input('visible') visible:boolean = true;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Input('yesText') yesText:string = "Ok";
  @Input('yesClass') yesClass:string = "primary";
  @Input('yesHide') yesHide:boolean = false;
  @Input('noText') noText:string = "Cancel";
  @Input('noClass') noClass:string = "secondary";
  @Input('noHide') noHide:boolean = false;
  @Input('modalClass') modalClass:string = "";
  @Output('modalClosed') modalClosedEmitter = new EventEmitter<boolean>();


  constructor() { }

  ngOnInit(): void {  }

  buttonClick(value:boolean) {
    this.visible = false;
    this.visibleChange.emit(this.visible);
    this.modalClosedEmitter.emit(value);
  }

  stopPropagation(event: any){
    event.stopPropagation();

  }

}
