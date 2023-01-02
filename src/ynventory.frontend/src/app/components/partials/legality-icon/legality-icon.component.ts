import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-legality-icon',
  templateUrl: './legality-icon.component.html',
  styleUrls: ['./legality-icon.component.scss']
})
export class LegalityIconComponent implements OnInit {

  @Input() value:boolean = false;

  constructor() { }

  ngOnInit(): void {
  }

}
