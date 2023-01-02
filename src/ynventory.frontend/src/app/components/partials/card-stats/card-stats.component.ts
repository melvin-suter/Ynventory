import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { CardModel } from 'src/app/models/card.model';
import { CollectionItemModel } from 'src/app/models/collectionItem.model';
import { ScryfallCardModel } from 'src/app/models/scryfall-card.model';
import { CollectionService } from 'src/app/services/collection.service';
import { ScryfallService } from 'src/app/services/scryfall.service';

@Component({
  selector: 'app-card-stats',
  templateUrl: './card-stats.component.html',
  styleUrls: ['./card-stats.component.scss']
})
export class CardStatsComponent implements OnInit, OnChanges {
  stats_totalCards:number = 0;
  stats_totalCreatures:number = 0;
  stats_totalLands:number = 0;
  stats_deckLegality = {
    commander: true,
    historic: true,
    legacy: true,
    modern: true,
    standard: true,
    pioneer: true
  };
  
  stats_chartColorDistribution = {
    labels: ['White','Red','Green', 'Blue', 'Black', 'Neutral'],
    datasets: [
      {
        data: [300, 50, 100, 400, 500],
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
  stats_chartLevelDistribution = {
    labels: ['1'],
    datasets: [
      {
        data: [10, 15, 17, 5, 3],
        label: 'Card Count',
        backgroundColor: [
          "#673AB7"
        ],
        hoverBackgroundColor: [
          "#673AB7"
        ]
      }
    ]
  };

  @Input() cards:CardModel[] = [];
  chartOptionsBar:any = {
    plugins: {
      legend: {
        display: false
      }
    },
    scales: {
      x: {
        grid: {
          color: 'transparent'
        }
      }
    }
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

  constructor(private scryfallService:ScryfallService) {}
  ngOnInit(): void {}

  ngOnChanges(changes: any) {
    this.loadStats();
  }



  loadStats() {
    // Reset Counters
    this.stats_totalCreatures = 0;
    this.stats_totalLands = 0;
    this.stats_totalCards = 0;

    // Reset Charts
    this.stats_chartColorDistribution.datasets[0].data.forEach( (val:number, index:number) => this.stats_chartColorDistribution.datasets[0].data[index] = 0);
    this.stats_chartLevelDistribution.datasets[0].data.forEach( (val:number, index:number) => this.stats_chartLevelDistribution.datasets[0].data[index] = 0);

    // Reset Legality
    this.stats_deckLegality = {
      commander: true,
      historic: true,
      legacy: true,
      modern: true,
      standard: true,
      pioneer: true
    };

    // Start Stats calc
    this.cards.forEach( (card:CardModel) => {
      // Counters
      this.stats_totalCards += Number(card.quantity);
      if(card.metadata?.type.toLowerCase().includes('creature')){
        this.stats_totalCreatures += Number(card.quantity);
      }
      if(card.metadata?.type.toLowerCase().includes('land')){
        this.stats_totalLands += Number(card.quantity);
      }

      // Color Charts
      this.stats_chartColorDistribution.datasets[0].data[5] += card.metadata!.colorIdentity.length <= 0 ? Number(card.quantity) : 0;
      this.stats_chartColorDistribution.datasets[0].data[0] += card.metadata!.colorIdentity.includes('W') ? Number(card.quantity) : 0;
      this.stats_chartColorDistribution.datasets[0].data[4] += card.metadata!.colorIdentity.includes('B') ? Number(card.quantity) : 0;
      this.stats_chartColorDistribution.datasets[0].data[1] += card.metadata!.colorIdentity.includes('R') ? Number(card.quantity) : 0;
      this.stats_chartColorDistribution.datasets[0].data[3] += card.metadata!.colorIdentity.includes('U') ? Number(card.quantity) : 0;
      this.stats_chartColorDistribution.datasets[0].data[2] += card.metadata!.colorIdentity.includes('G') ? Number(card.quantity) : 0;

      // Cost Distribution
      if(!card.metadata?.type.toLowerCase().includes('land')){
        this.stats_chartLevelDistribution.datasets[0].data[card.metadata!.manaCostTotal] += Number(card.quantity)
        this.stats_chartLevelDistribution.labels[card.metadata!.manaCostTotal] = String(card.metadata!.manaCostTotal);
      }

      // Card Legality
      this.scryfallService.getCard(card.metadata!.id).subscribe( (data:ScryfallCardModel) => {
        if(data.legalities?.commander == 'not_legal'){
          this.stats_deckLegality.commander = false;
        }
        if(data.legalities?.historic == 'not_legal'){
          this.stats_deckLegality.historic = false;
        }
        if(data.legalities?.legacy == 'not_legal'){
          this.stats_deckLegality.legacy = false;
        }
        if(data.legalities?.modern == 'not_legal'){
          this.stats_deckLegality.modern = false;
        }
        if(data.legalities?.pioneer == 'not_legal'){
          this.stats_deckLegality.pioneer = false;
        }
        if(data.legalities?.standard == 'not_legal'){
          this.stats_deckLegality.standard = false;
        }
      });

    });

    // Reset Mana Cost Labels
    let maxVal = this.stats_chartLevelDistribution.labels[this.stats_chartLevelDistribution.labels.length - 1];
    for(let i = 0; i <= Number(maxVal); i++) {
      this.stats_chartLevelDistribution.labels[i] = String(i);
    }

  }



}
