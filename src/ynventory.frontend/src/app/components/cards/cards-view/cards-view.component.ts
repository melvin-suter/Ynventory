import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CardModel } from 'src/app/models/card.model';
import { ScryfallCardModel } from 'src/app/models/scryfall-card.model';
import { CardService } from 'src/app/services/card.service';
import { ScryfallService } from 'src/app/services/scryfall.service';

@Component({
  selector: 'app-cards-view',
  templateUrl: './cards-view.component.html',
  styleUrls: ['./cards-view.component.scss']
})
export class CardsViewComponent implements OnInit {


  scryfallData?:ScryfallCardModel;
  getManaCostList = ScryfallCardModel.getManaCostList;
  imageShowModal:boolean = false;
  imageShowUrl:string = "";

  constructor(private cardService:CardService, private scryfallService:ScryfallService, private route:ActivatedRoute) { 
    this.route.params.subscribe(params => {
      scryfallService.getCard(params['cardid']).subscribe( (item:ScryfallCardModel) => {
        this.scryfallData = item;
      });
    });
  }

  ngOnInit(): void {
  }

  openImageShowModal(url?:string){
    this.imageShowUrl = <string>url;
    this.imageShowModal = true;
  }

}
