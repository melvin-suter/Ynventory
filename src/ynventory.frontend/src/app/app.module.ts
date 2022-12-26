import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from  '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


/******************
 *   Layouts
 ******************/
import { BareComponent } from './layout/bare/bare.component';
import { AppLayoutComponent } from './layout/app-layout/app-layout.component';
 

/******************
 *    Partials
 ******************/
import { NavbarComponent } from './partial/navbar/navbar.component';
import { ModalComponent } from './partial/modal/modal.component';
import { CardListComponent } from './partial/card-list/card-list.component';
import { ImageViewerComponent } from './partial/image-viewer/image-viewer.component';
import { NavbarItemComponent } from './partial/navbar-item/navbar-item.component';


/******************
 *     Pages
 ******************/
import { HomeComponent } from './pages/home/home.component';
import { CardsComponent } from './pages/cards/cards.component';
import { CollectionsComponent } from './pages/collections/collections.component'
import { DecksComponent } from './pages/decks/decks.component';
import { DeckComponent } from './pages/deck/deck.component';
import { CollectionsFoldersComponent } from './pages/collections-folders/collections-folders.component';
import { CollectionsCardsComponent } from './pages/collections-cards/collections-cards.component';

 

/******************
   *   Prime
 ******************/
import {SidebarModule} from 'primeng/sidebar';
import {ButtonModule} from 'primeng/button';
import {RippleModule} from 'primeng/ripple';
import {TableModule } from 'primeng/table';
import {DialogModule } from 'primeng/dialog';
import {InputTextModule } from 'primeng/inputtext';
import { CardModule } from 'primeng/card';
import { ChartModule } from 'primeng/chart';
import { DashboardComponent } from './pages/dashboard/dashboard.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CardsComponent,

    // Layouts
    AppLayoutComponent,
    BareComponent,

    // Partials
    NavbarComponent,
    ModalComponent,
    CardListComponent,
    ImageViewerComponent,
    NavbarItemComponent,
    
    // Pages
    CollectionsComponent,
    DecksComponent,
    DeckComponent,
    CollectionsFoldersComponent,
    CollectionsCardsComponent,
    DashboardComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    
    // Prime
    SidebarModule,
    ButtonModule,
    RippleModule,
    TableModule,
    DialogModule,
    InputTextModule,
    CardModule,
    ChartModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
