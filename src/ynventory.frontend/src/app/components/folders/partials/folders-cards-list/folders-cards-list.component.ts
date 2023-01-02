import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BehaviorSubject, Observable, switchMap } from 'rxjs';
import { CardModel } from 'src/app/models/card.model';
import { CollectionItemModel } from 'src/app/models/collectionItem.model';


import { CollectionService } from 'src/app/services/collection.service';
import { ScryfallService } from 'src/app/services/scryfall.service';



@Component({
  selector: 'app-folders-cards-list',
  templateUrl: './folders-cards-list.component.html',
  styleUrls: ['./folders-cards-list.component.scss']
})
export class FoldersCardsListComponent  {

  // Observable Handler Shit
  cardsObservable$?:Observable<CollectionItemModel[]>;
  refresh$ = new BehaviorSubject<number>(1);
 
  // Table Data
  collectionItem?:CollectionItemModel;
  cards:CardModel[] = [];
  selectedCards:CardModel[] = [];
  collectionId:number = -1;
  collectionItemId:number = -1;

  // Modal Data
  showAddModal:boolean = false;
  showEditModal:boolean = false;
  showDeleteModal:boolean = false;
  modalData:CardModel = {};
  modalDataDelete:CardModel[] = [];
  modalNewData:CardModel = {};

  constructor(private collectionService: CollectionService,  private route: ActivatedRoute, private scryfallService:ScryfallService) { 
    this.route.params.subscribe(params => {
      this.collectionId = params['colid'];
      this.collectionItemId = params['id'];


      collectionService.getCollectionItem(this.collectionId, this.collectionItemId).subscribe( (data:CollectionItemModel) => {
        this.collectionItem = data;
      });

      this.cardsObservable$ = this.refresh$.pipe(switchMap((_:any) => {
        return this.collectionService.getCollectionItemCards(this.collectionId, this.collectionItemId);
      }));

      this.cardsObservable$.subscribe( (data:CollectionItemModel[]) => {
        this.selectedCards = [];
        this.cards = data;
      });

    });
  }



  openAddModal() {
    this.modalData = {};
    console.log(this.showAddModal);
    this.showAddModal = true;
  }

  openEditModal(){
    this.modalData = JSON.parse(JSON.stringify(this.selectedCards[0]));
    this.showEditModal = true;
  }

  openDeleteModal(){
    this.modalDataDelete = this.selectedCards;
    this.showDeleteModal = true;
  }


  createItem(event:any) {
    this.showAddModal = false;

    if(event){
      this.collectionService.createCollectionItemCard(this.collectionId, this.collectionItemId,this.modalNewData).subscribe(() => {
        this.refresh$.next(2);
        this.modalNewData = {};
      });

    }
  }
 
  saveItem(){
    this.collectionService.updateCollectionItemCard(this.collectionId, this.collectionItemId,this.modalData).subscribe((data:any) => {
      this.refresh$.next(2);
      this.showEditModal = false;
      this.modalData = {};
    });
  }

  deleteItem(){
    this.modalDataDelete.forEach( (item:CardModel) => {

      this.collectionService.deleteCollectionItemCard(this.collectionId, this.collectionItemId, item.id!).subscribe(() => {
        this.refresh$.next(2);
      });

    });

    this.modalDataDelete = [];
    this.showDeleteModal = false;
  }


}
