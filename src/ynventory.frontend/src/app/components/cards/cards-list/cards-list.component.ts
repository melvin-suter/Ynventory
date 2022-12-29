import { Component, OnInit } from '@angular/core';
import { CardModel } from 'src/app/models/card.model';
import { CardService } from 'src/app/services/card.service';

@Component({
  selector: 'app-cards-list',
  templateUrl: './cards-list.component.html',
  styleUrls: ['./cards-list.component.scss']
})
export class CardsListComponent implements OnInit {

  cards:CardModel[] = [];
  selectedCards:CardModel[] = [];

  constructor(private cardService: CardService) { 
    this.cards = cardService.getAllCards();
  }

  ngOnInit(): void {
  }

}
