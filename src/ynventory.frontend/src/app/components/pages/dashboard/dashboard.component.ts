import { Component, OnInit } from '@angular/core';
import { CardModel } from 'src/app/models/card.model';
import { CollectionModel } from 'src/app/models/collection.model';
import { CollectionItemModel } from 'src/app/models/collectionItem.model';
import { DeckModel } from 'src/app/models/deck.model';
import { CollectionService } from 'src/app/services/collection.service';
import { DeckService } from 'src/app/services/deck.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  stats_totalDecks:number = 0;
  stats_totalCards:number = 0;
  stats_totalFolders:number = 0;
  stats_totalVirtualDecks:number = 0;
  stats_chartColorDistribution = {
    labels: ['White','Red','Green', 'Blue', 'Black', 'Neutral'],
    datasets: [
      {
        data: [300, 50, 100, 400, 500, 0],
        backgroundColor: [
          "rgb(248,231,185)",
          "rgb(211,32,42)",
          "rgb(0,115,62)",
          "rgb(14,104,171)",
          "rgb(21,11,0)",
          "#CDC3C1",
        ],
        hoverBackgroundColor: [
          "rgb(248,231,185)",
          "rgb(211,32,42)",
          "rgb(0,115,62)",
          "rgb(14,104,171)",
          "rgb(21,11,0)",
          "#CDC3C1",
        ]
      }
    ]
  };

  chartOptionsPie:any = {
    plugins: {
      legend: {
        position: 'bottom'
      }
    },
    scales: {
      x: {
        display: false,
      },
      y: {
        display: false,
      }
    }
  };

  constructor(private collectionService: CollectionService, private deckService: DeckService) {

    this.deckService.getDecks().subscribe( (decks:DeckModel[]) => {
      this.stats_totalVirtualDecks = decks.length;
    });

    // Reset Chart
    this.stats_chartColorDistribution.datasets[0].data.forEach( (val:number, index:number) => this.stats_chartColorDistribution.datasets[0].data[index] = 0);

    this.collectionService.getCollections().subscribe( (cols:CollectionModel[]) => { // Get Collections
      cols.forEach( (col:CollectionModel) => { // Foreach Collection
        this.collectionService.getCollectionItems(col.id!).subscribe( (colItems:CollectionItemModel[]) => { // Get Colleciton Items
          colItems.forEach( (colItem:CollectionItemModel) => { // Foreach collection item

            this.stats_totalDecks += colItem.type == 'Deck' ? 1 : 0;
            this.stats_totalFolders += colItem.type == 'Folder' ? 1 : 0;

            // TODO: Replace with api/cards
            this.collectionService.getCollectionItemCards(col.id!, colItem.id!).subscribe( (cards:CardModel[]) => {
              // Total Cards
              this.stats_totalCards += cards.length;

              // Color Charts
              cards.forEach( (card:CardModel) => {
                this.stats_chartColorDistribution.datasets[0].data[5] += card.metadata!.colorIdentity.length <= 0 ? Number(card.quantity) : 0;
                this.stats_chartColorDistribution.datasets[0].data[0] += card.metadata!.colorIdentity.includes('W') ? Number(card.quantity) : 0;
                this.stats_chartColorDistribution.datasets[0].data[4] += card.metadata!.colorIdentity.includes('B') ? Number(card.quantity) : 0;
                this.stats_chartColorDistribution.datasets[0].data[1] += card.metadata!.colorIdentity.includes('R') ? Number(card.quantity) : 0;
                this.stats_chartColorDistribution.datasets[0].data[3] += card.metadata!.colorIdentity.includes('U') ? Number(card.quantity) : 0;
                this.stats_chartColorDistribution.datasets[0].data[2] += card.metadata!.colorIdentity.includes('G') ? Number(card.quantity) : 0;
              });
              
            });

          });
        });
      });
    });

  }


  ngOnInit(): void {
    
  }

}
