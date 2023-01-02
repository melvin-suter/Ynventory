import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MessageService } from 'primeng/api';
import { skip } from 'rxjs';
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

  public get filteredCards():CardModel[] {
    return this.cards.filter( (card) => {
      let filtersRun = 0;
      let filtersHit = 0;

      // Start Fulltext foreach words
      if(this.filter_fullText.trim().length > 0 ){
        let wordsSearched = 0;
        let wordsHit = 0;

        this.filter_fullText.split(' ').forEach((word:string) => {
          if(word.trim().length > 0 ){ // Dont run on spaces
            wordsSearched++;
            if( // Search all fields
                card.metadata?.oracleText.toLowerCase().includes(word.toLowerCase()) ||
                //card.metadata?.name.toLowerCase().includes(word.toLowerCase()) ||
                card.metadata?.type.toLowerCase().includes(word.toLowerCase())
              ){
              wordsHit++
            }
          }
        });

        filtersRun++;
        filtersHit += wordsHit >= wordsSearched ? 1 : 0;
      }

      // Filter for manacost
      if(card.metadata!.manaCostTotal < this.filter_cmc[0] || card.metadata!.manaCostTotal > this.filter_cmc[1]){
        return false;
      }

      // Filter for colors
      this.filter_selectedColors.forEach( (color:string) => {

        filtersRun++;

        // Run normal Color filters
        if(card.metadata!.colors.includes(color)){
          filtersHit++;
        }

        // Run neutral color filter
        if(color == "neutral" && card.metadata!.colors.length <= 0){
          filtersHit++;
        }
      });

      // Filter for legalities
      let legalitiesHit = false;
      this.filter_legalities.forEach((filter:string) => {
        if(card.metadata?.legalities[filter.toLowerCase()] != "legal"){
          legalitiesHit = true;
        }
      });
      if(legalitiesHit) {return false;}

  

      return filtersRun > 0 ? filtersHit > 0 : true;
      // card.metadata?.oracleText.includes(this.filter_fullText)
    });
  }


  filter_selectedColors:string[] = [];
  filter_cmc:number[] = [0,20];
  filter_fullText:string = "";
  filter_legalities:string[] = [];

  imageShowModal:boolean = false;
  imageShowUrl:string = "";

  getImageUrl = CardModel.getImageUrl;

  constructor(private messageService: MessageService) {}

  ngOnInit(): void {
  }
  
  openImageShowModal(url?:string){
    this.imageShowUrl = <string>url;
    this.imageShowModal = true;
  }

  fireSelection(event?:any){
    this.selectedCardsChange.emit(this.selectedCards);
  }

}
