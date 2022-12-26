import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CardModel } from 'src/app/models/card.model';
import { ScryfallCardModel } from 'src/app/models/scryfall-card.model';
import { CardService } from 'src/app/services/card.service';
import { ScryfallService } from 'src/app/services/scryfall.service';

@Component({
  selector: 'app-card-info',
  templateUrl: './card-info.component.html',
  styleUrls: ['./card-info.component.scss']
})
export class CardInfoComponent implements OnInit {

  card?:CardModel;
  scryfallData?:ScryfallCardModel;
  getManaCostList = ScryfallCardModel.getManaCostList;
  imageShowModal:boolean = false;
  imageShowUrl:string = "";

  constructor(private cardService:CardService, private scryfallService:ScryfallService, private route:ActivatedRoute) { 
    this.route.params.subscribe(params => {
      this.card = cardService.getCard(params['cardid']);
      scryfallService.getCard(this.card.scryfallID).subscribe( (item:ScryfallCardModel) => {
        this.scryfallData = item;
      });
      this.card = cardService.getCard(params['cardid']);
    });
  }

  ngOnInit(): void {
  }

  openImageShowModal(url?:string){
    this.imageShowUrl = <string>url;
    this.imageShowModal = true;
  }

}
