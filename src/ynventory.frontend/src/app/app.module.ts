import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { CardsComponent } from './pages/cards/cards.component';
import { HttpClientModule } from  '@angular/common/http';
import { BareComponent } from './layout/bare/bare.component';
import { RouterModule } from '@angular/router';
import { AppLayoutComponent } from './layout/app-layout/app-layout.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NavbarComponent } from './partial/navbar/navbar.component';
import { CollectionsComponent } from './pages/collections/collections.component';
import { FoldersComponent } from './pages/folders/folders.component';
import { ModalComponent } from './partial/modal/modal.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DecksComponent } from './pages/decks/decks.component';
import { DeckComponent } from './pages/deck/deck.component';
import { CardListComponent } from './partial/card-list/card-list.component';
import { ImageViewerComponent } from './partial/image-viewer/image-viewer.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CardsComponent,
    BareComponent,
    AppLayoutComponent,
    NavbarComponent,
    CollectionsComponent,
    FoldersComponent,
    ModalComponent,
    DecksComponent,
    DeckComponent,
    CardListComponent,
    ImageViewerComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    RouterModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
