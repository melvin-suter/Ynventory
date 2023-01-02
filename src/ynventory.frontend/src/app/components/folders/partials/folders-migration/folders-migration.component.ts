import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TreeNode } from 'primeng/api';
import { BehaviorSubject, Observable, switchMap } from 'rxjs';
import { CardModel } from 'src/app/models/card.model';
import { CollectionModel } from 'src/app/models/collection.model';
import { CollectionItemModel } from 'src/app/models/collectionItem.model';
import { CollectionService } from 'src/app/services/collection.service';

@Component({
  selector: 'app-folders-migration',
  templateUrl: './folders-migration.component.html',
  styleUrls: ['./folders-migration.component.scss']
})
export class FoldersMigrationComponent {

  // Collection
  collectionItem?:CollectionItemModel;
  collectionId:number = -1;
  collectionItemId:number = -1;

  // Left Table
  to_collectionId:number = -1;
  to_collectionItemId:number = -1;
  to_cardsObservable$?:Observable<CollectionItemModel[]>;
  to_refresh$ = new BehaviorSubject<number>(1);
  to_cards:CardModel[] = [];
  to_cardsSelected:CardModel[] = [];

  // Right Table
  from_cardsObservable$?:Observable<CollectionItemModel[]>;
  from_refresh$ = new BehaviorSubject<number>(1);
  from_cards:CardModel[] = [];
  from_cardsSelected:CardModel[] = [];
 
  // Misc
  folderSelected:boolean = false;
  quantityMove:string = "X";
  public get quantityMoveValid():boolean {
    if(!isNaN(parseInt(this.quantityMove)) && isFinite(Number(this.quantityMove))) {
      return false;
    }

    return this.quantityMove.toUpperCase() === "X" ? false : true;
  }
  public get quantityMoveSanitized():string {
    return this.quantityMove.toUpperCase() === "X" ? "X" : String(parseInt(this.quantityMove)) == "NaN" ? "X" : String(parseInt(this.quantityMove));
  }

  // Tree
  treeData:TreeNode[] = [];
  selectedItem:TreeNode = {};



  constructor(private collectionService: CollectionService, private route: ActivatedRoute) {
    this.route.params.subscribe(params => {
      // Collection Data
      this.collectionId = params['colid'];
      this.collectionItemId = params['id'];

      collectionService.getCollectionItem(this.collectionId, this.collectionItemId).subscribe( (data:CollectionItemModel) => {
        this.collectionItem = data;
      });

      // Load Data Right Size
      this.from_cardsObservable$ = this.from_refresh$.pipe(switchMap((_:any) => {
        return this.collectionService.getCollectionItemCards(this.collectionId, this.collectionItemId);
      }));

      this.from_cardsObservable$.subscribe( (data:CollectionItemModel[]) => {
        this.from_cardsSelected = [];
        this.from_cards = data;
      });

    });


    // Load Tree Data
    this.collectionService.getCollections().subscribe( (collections:CollectionModel[]) => {
      collections.forEach( (collection:CollectionModel) => {
        collectionService.getCollectionItems(collection.id!).subscribe( (collectionItems:CollectionItemModel[]) => {
          let treeChildren:TreeNode[] = [];

          collectionItems.forEach( (item:CollectionItemModel) => {
            treeChildren.push({
              label: item.name,
              data: item,
              expandedIcon: "pi pi-folder-open",
              collapsedIcon: "pi pi-folder"
            })
          });

          this.treeData.push({
            label: collection.name,
            data: collection,
            expandedIcon: "pi pi-box",
            collapsedIcon: "pi pi-box",
            children: treeChildren
          })
        });

      });
    });
  }

  nodeSelected(event:any){
    this.folderSelected = false;

    // Load Data Left Size
    let node:TreeNode = event.node;
    if( node.children == undefined || node.children!.length == 0){ // Is a folder
      let item:CollectionItemModel = node.data;
      let collection:CollectionModel = node.parent?.data;

      this.to_collectionId = Number(collection!.id);
      this.to_collectionItemId = item.id!;

      this.loadLeftData();
    }
  }

  loadLeftData(){
    this.collectionService.getCollectionItemCards(this.to_collectionId, this.to_collectionItemId ).subscribe( (cards:CardModel[]) => {
      this.to_cardsSelected = [];
      this.to_cards = cards;
      this.folderSelected = true;
    });
  }

  moveForward(){
    let toColItem = {collectionId: this.to_collectionId, collectionItemId: this.to_collectionItemId};
    let fromColItem = { collectionId: this.collectionId, collectionItemId: this.collectionItemId };
    this.from_cardsSelected.forEach( (card:CardModel) => {
      this.collectionService.moveCollectionItemCard(card, fromColItem, toColItem, () => {
        // Reload Lists if call succeeded/failed
        this.loadLeftData();
        this.from_refresh$.next(2);
      });

    });
  }

  moveBack(){
    let toColItem = {collectionId: this.to_collectionId, collectionItemId: this.to_collectionItemId};
    let fromColItem = { collectionId: this.collectionId, collectionItemId: this.collectionItemId };
    this.from_cardsSelected.forEach( (card:CardModel) => {
      this.collectionService.moveCollectionItemCard(card, toColItem, fromColItem, () => {
        // Reload Lists if call succeeded/failed
        this.loadLeftData();
        this.from_refresh$.next(2);
      });

    });
  }


}
