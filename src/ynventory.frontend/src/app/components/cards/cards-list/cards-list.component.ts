import { Component, OnInit } from '@angular/core';
import { CardModel } from 'src/app/models/card.model';
import { CollectionModel } from 'src/app/models/collection.model';
import { CollectionItemModel } from 'src/app/models/collectionItem.model';

import { CollectionService } from 'src/app/services/collection.service';

@Component({
  selector: 'app-cards-list',
  templateUrl: './cards-list.component.html',
  styleUrls: ['./cards-list.component.scss']
})
export class CardsListComponent implements OnInit {

  cards:CardModel[] = [];
  selectedCards:CardModel[] = [];

  constructor(private collectionService: CollectionService) { 
    // TODO: replace with good stuff from backend
    this.collectionService.getCollections().subscribe((cols) => {
      cols.forEach( (col:CollectionModel) => {
        this.collectionService.getCollectionItems(col.id!).subscribe( (colItems) => {
          colItems.forEach( (colItem:CollectionItemModel) => {
            this.collectionService.getCollectionItemCards(col.id!,colItem.id!).subscribe( (cards) => {
              this.cards = this.cards.concat(cards);
            });
          });
        });
      });
    });
  }

  ngOnInit(): void {
  }

}
