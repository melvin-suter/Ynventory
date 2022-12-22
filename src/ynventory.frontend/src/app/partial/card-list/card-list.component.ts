import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Form, FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CardModel } from 'src/app/models/card.model';
import { ScryfallCardModel } from 'src/app/models/scryfall-card.model';
import { ScryfallService } from 'src/app/services/scryfall.service';
import { debounceTime } from 'rxjs';


@Component({
  selector: 'app-card-list',
  templateUrl: './card-list.component.html',
  styleUrls: ['./card-list.component.scss']
})
export class CardListComponent implements OnInit {

  @Input() cards?:CardModel[] = [];
  @Output() cardsChange:EventEmitter<CardModel[]> = new EventEmitter<CardModel[]>();
  searchText:string = "";
  searchResults:ScryfallCardModel[] = [];

  getManaCostList = ScryfallCardModel.getManaCostList;

  createModalOpen:boolean = false;
  imageShowModal:boolean = false;
  searchControl:FormControl;
  imageShowUrl:string = "";

 
  constructor( private scryfallService:ScryfallService) { 
    this.searchControl = new FormControl();
    this.searchControl.valueChanges.pipe(debounceTime(400)).subscribe((value:string) => this.searchChanged(value));
  }
  ngOnInit(): void {
  }

  openImageShowModal(url?:string){
    this.imageShowUrl = <string>url;
    this.imageShowModal = true;
  }

  openAddModal(){
    this.createModalOpen = true;
  }

  searchChanged(newValue:string){
    this.scryfallService.searchCard(newValue).subscribe((data:any) => {
      this.searchResults = data;
    });
  }
}
