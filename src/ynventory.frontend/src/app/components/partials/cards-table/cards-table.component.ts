import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CardModel } from 'src/app/models/card.model';

@Component({
  selector: 'app-cards-table',
  templateUrl: './cards-table.component.html',
  styleUrls: ['./cards-table.component.scss']
})
export class CardsTableComponent implements OnInit {

  @Input() cards:CardModel[] = [];
  @Output() cardsChange = new EventEmitter<CardModel[]>();
  @Input() selectedCards:CardModel[] = [];
  @Output() selectedCardsChange = new EventEmitter<CardModel[]>();

  imageShowModal:boolean = false;
  imageShowUrl:string = "";

  getImageUrl = CardModel.getImageUrl;

  constructor() { }

  ngOnInit(): void {
  }
  
  openImageShowModal(url?:string){
    this.imageShowUrl = <string>url;
    this.imageShowModal = true;
  }

}
