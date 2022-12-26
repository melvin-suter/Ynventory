import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';

@Component({
  selector: 'app-navbar-item',
  templateUrl: './navbar-item.component.html',
  styleUrls: ['./navbar-item.component.scss']
})
export class NavbarItemComponent implements OnInit {

  @Input() route:string = "";
  @Output() routeChange = new EventEmitter<string>();

  @Input() icon:string = "link";
  @Output() iconChange = new EventEmitter<string>();

  @Input() label:string = "";
  @Output() labelChange = new EventEmitter<string>();

  constructor(private primengConfig: PrimeNGConfig) { }

  ngOnInit(): void {
    this.primengConfig.ripple = true;
  }

}
