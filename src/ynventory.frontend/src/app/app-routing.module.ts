import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppLayoutComponent } from './layout/app-layout/app-layout.component';
import { BareComponent } from './layout/bare/bare.component';
import { CardsComponent } from './pages/cards/cards.component';
import { CollectionsFoldersComponent } from './pages/collections-folders/collections-folders.component';
import { CollectionsComponent } from './pages/collections/collections.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { DeckComponent } from './pages/deck/deck.component';
import { DecksComponent } from './pages/decks/decks.component';

const routes: Routes = [
  { path: '', component: AppLayoutComponent, children: [
    {path: 'cards', component: CardsComponent},
    {path: 'collections/:colid/folders', component: CollectionsFoldersComponent},
    {path: 'collections/:colid/folders/:id', component: CardsComponent},
    {path: 'collections', component: CollectionsComponent},
    {path: 'decks/:id', component: DeckComponent},
    {path: 'decks', component: DecksComponent},
    {path: 'home', component: DashboardComponent},
    {path: '', redirectTo: 'home', pathMatch: 'full'}
  ]},
  { path: '', component: BareComponent, children: [
    {path: 'login', component: CardsComponent},
  ]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
