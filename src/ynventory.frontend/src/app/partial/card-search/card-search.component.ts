import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { debounceTime } from 'rxjs';
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
  imageShowModal:boolean = false;
  searchControl:FormControl;
  imageShowUrl:string = "";

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
}
