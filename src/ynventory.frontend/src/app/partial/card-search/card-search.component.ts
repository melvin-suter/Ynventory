import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Card } from 'primeng/card';
import { debounceTime } from 'rxjs';
import { CardModel } from 'src/app/models/card.model';
import { ScryfallCardModel } from 'src/app/models/scryfall-card.model';
import { ScryfallService } from 'src/app/services/scryfall.service';

@Component({
  selector: 'app-card-search',
  templateUrl: './card-search.component.html',
  styleUrls: ['./card-search.component.scss']
})
export class CardSearchComponent implements OnInit {
  searchText:string = "";
  searchResults:ScryfallCardModel[] = [];

  getManaCostList = ScryfallCardModel.getManaCostList;

  @Input() createModalOpen:boolean = false;
  @Output() createModalOpenChange = new EventEmitter<boolean>();
  @Input() selectedCard:CardModel = new CardModel();
  @Output() selectedCardChange = new EventEmitter<CardModel>();
  @Output() dialogClosed = new EventEmitter<boolean>();
  imageShowModal:boolean = false;
  searchControl:FormControl;
  imageShowUrl:string = "";
  foilOptions:any = [
    { key: "nonfoil",value: 'Non Foil' },
    { key: "foil",value: 'Foil' },
    { key: "edged",value: 'Edged' }
  ];

  showAddPage:boolean = false;

  constructor(private scryfallService:ScryfallService) { 
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

  addItem(card:ScryfallCardModel){
    this.showAddPage = true;
    this.selectedCard = {
      name: card.name,
      scryfallID: card.id,
      foil: 'nonfoil',
      quantity: 1,
      id: -1
    };
  }

  closeModal(closeValue:boolean){
    this.createModalOpen = false;
    this.dialogClosed.emit(closeValue);
    this.showAddPage = false;
  }
}
