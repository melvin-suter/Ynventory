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
import { BareComponent } from './components/layout/bare/bare.component';
import { AppLayoutComponent } from './components/layout/app-layout/app-layout.component';
 

/******************
 *    Partials
import { NavbarItemComponent } from './components/partials/navbar-item/navbar-item.component';
 ******************/
import { NavbarComponent } from './components/partials/navbar/navbar.component';
import { NavbarItemComponent } from './components/partials/navbar-item/navbar-item.component';
import { ImageViewerComponent } from './components/partials/image-viewer/image-viewer.component';
import { CardSearchComponent } from './components/partials/card-search/card-search.component';


/******************
 *     Pages
 ******************/
import { DashboardComponent } from './components/pages/dashboard/dashboard.component';
import { LoginComponent } from './components/pages/login/login.component';

import { CollectionsListComponent } from './components/collections/collections-list/collections-list.component';
import { CollectionsViewComponent } from './components/collections/collections-view/collections-view.component';
import { CollectionCardsComponent } from './components/collections/partial/collection-cards/collection-cards.component';
import { CollectionFoldersComponent } from './components/collections/partial/collection-folders/collection-folders.component';

import { DecksListComponent } from './components/decks/decks-list/decks-list.component';
import { DecksViewComponent } from './components/decks/decks-view/decks-view.component';

import { FoldersViewComponent } from './components/folders/folders-view/folders-view.component';
import { FoldersMigrationComponent } from './components/folders/partials/folders-migration/folders-migration.component';
import { FoldersCardsListComponent } from './components/folders/partials/folders-cards-list/folders-cards-list.component';

 

/******************
   *   Prime
 ******************/
import { SidebarModule } from 'primeng/sidebar';
import { ButtonModule } from 'primeng/button';
import { RippleModule } from 'primeng/ripple';
import { TableModule } from 'primeng/table';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { CardModule } from 'primeng/card';
import { ChartModule } from 'primeng/chart';
import { TabViewModule } from 'primeng/tabview';
import { DropdownModule } from 'primeng/dropdown';
import { CheckboxModule } from 'primeng/checkbox';
import { CardsListComponent } from './components/cards/cards-list/cards-list.component';
import { CardsViewComponent } from './components/cards/cards-view/cards-view.component';
import { CardsTableComponent } from './components/partials/cards-table/cards-table.component';
import { GridComponent } from './components/partials/grid/grid.component';
import { AccordionModule } from 'primeng/accordion';
import { SliderModule } from 'primeng/slider';
import { OverlayPanelModule } from 'primeng/overlaypanel';


@NgModule({
  declarations: [
    AppComponent,

    // Layouts
    AppLayoutComponent,
    BareComponent,

    // Partials
    NavbarComponent,
    ImageViewerComponent,
    NavbarItemComponent,
    
    // Pages
    DashboardComponent,
    CardSearchComponent,
    LoginComponent,
    DecksListComponent,
    DecksViewComponent,
    CollectionsListComponent,
    CollectionsViewComponent,
    CollectionCardsComponent,
    CollectionFoldersComponent,
    FoldersViewComponent,
    CardsListComponent,
    CardsViewComponent,
    CardsTableComponent,
    GridComponent,
    FoldersMigrationComponent,
    FoldersCardsListComponent,


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
    ChartModule,
    TabViewModule,
    DropdownModule,
    CheckboxModule,
    AccordionModule,
    SliderModule,
    OverlayPanelModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
