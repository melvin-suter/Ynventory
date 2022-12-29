import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CollectionModel } from 'src/app/models/collection.model';
import { CollectionService } from 'src/app/services/collection.service';


@Component({
  selector: 'app-collections-view',
  templateUrl: './collections-view.component.html',
  styleUrls: ['./collections-view.component.scss']
})
export class CollectionsViewComponent implements OnInit {

  collection?:CollectionModel;

  constructor(private collectionService:CollectionService,private route: ActivatedRoute) { 
    this.route.params.subscribe(params => {
      this.collection = collectionService.getCollection(params['colid']);
    });

  }

  ngOnInit(): void {
  }


}
