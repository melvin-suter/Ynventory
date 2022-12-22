import { Component, OnInit } from '@angular/core';
import { CollectionModel } from 'src/app/models/collection.model';
import { CollectionService } from 'src/app/services/collection.service';

@Component({
  selector: 'app-collections',
  templateUrl: './collections.component.html',
  styleUrls: ['./collections.component.scss']
})
export class CollectionsComponent implements OnInit {

  collections:CollectionModel[];

  constructor(private collectionService:CollectionService) { 
    this.collections = collectionService.getCollections();
  }


  ngOnInit(): void {
  }

}
