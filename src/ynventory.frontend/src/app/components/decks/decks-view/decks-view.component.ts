import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CardModel } from 'src/app/models/card.model';
import { DeckModel } from 'src/app/models/deck.model';
import { CardService } from 'src/app/services/card.service';
import { DeckService } from 'src/app/services/deck.service';
import { ScryfallService } from 'src/app/services/scryfall.service';


@Component({
  selector: 'app-decks-view',
  templateUrl: './decks-view.component.html',
  styleUrls: ['./decks-view.component.scss']
})
export class DecksViewComponent implements OnInit {

  deck?:DeckModel;
  cards:CardModel[] = [];
  selectedCards:CardModel[] = [];
  chartColorDistribution:any;
  chartLevelDistribution:any;

  showAddModal:boolean = false;
  showEditModal:boolean = false;
  showDeleteModal:boolean = false;

  modalData:CardModel = new CardModel();

  chartOptions:any = {
    plugins: {
      legend: {
        position: 'bottom'
      }
    }
  }
  constructor(private deckService: DeckService, private cardService: CardService, private route: ActivatedRoute, private scryfallService:ScryfallService) { 
    this.chartColorDistribution = {
      labels: ['White','Red','Green', 'Blue', 'Black'],
      datasets: [
        {
          data: [300, 50, 100, 400, 500],
          backgroundColor: [
            "rgb(248,231,185)",
            "rgb(211,32,42)",
            "rgb(0,115,62)",
            "rgb(14,104,171)",
            "rgb(21,11,0)"
          ],
          hoverBackgroundColor: [
            "rgb(248,231,185)",
            "rgb(211,32,42)",
            "rgb(0,115,62)",
            "rgb(14,104,171)",
            "rgb(21,11,0)"
          ]
        }
      ]
    };

    this.chartLevelDistribution = {
      labels: ['1','2','3', '4', '5'],
      datasets: [
        {
          data: [10, 15, 17, 5, 3],
          backgroundColor: [
            "#673AB7"
          ],
          hoverBackgroundColor: [
            "#673AB7"
          ]
        }
      ]
    };

    this.route.params.subscribe(params => {
      this.deck = deckService.getDeck(params['id']);
      this.cards = cardService.getCards(params['id']);
    });

  }

  ngOnInit(): void {
  }

  createItem(){
    this.showAddModal = false;
  }

  openEditModal(){
    this.modalData = this.selectedCards[0];
    this.showEditModal = true;
  }

  saveItem(){
    this.showEditModal = false;
  }

  deleteItem(){
    this.showDeleteModal = false;
  }


}
