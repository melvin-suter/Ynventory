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
  left_cardsObservable$?:Observable<CollectionItemModel[]>;
  left_refresh$ = new BehaviorSubject<number>(1);
  left_cards:CardModel[] = [];
  left_cardsSelected:CardModel[] = [];

  // Right Table
  right_cardsObservable$?:Observable<CollectionItemModel[]>;
  right_refresh$ = new BehaviorSubject<number>(1);
  right_cards:CardModel[] = [];
  right_cardsSelected:CardModel[] = [];
 

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
      this.right_cardsObservable$ = this.right_refresh$.pipe(switchMap((_:any) => {
        return this.collectionService.getCollectionItemCards(this.collectionId, this.collectionItemId);
      }));

      this.right_cardsObservable$.subscribe( (data:CollectionItemModel[]) => {
        this.right_cardsSelected = [];
        this.right_cards = data;
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
    // Load Data Left Size
    let node:TreeNode = event.node;
    if( node.children == undefined || node.children!.length == 0){ // Is a folder
      let item:CollectionItemModel = node.data;
      let collection:CollectionModel = node.parent?.data;

      this.collectionService.getCollectionItemCards(collection.id!, item.id!).subscribe( (cards:CardModel[]) => {
        this.left_cardsSelected = [];
        this.left_cards = cards;
      });

    }
  }

}
