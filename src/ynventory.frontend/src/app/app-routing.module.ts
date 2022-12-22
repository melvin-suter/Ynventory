import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppLayoutComponent } from './layout/app-layout/app-layout.component';
import { BareComponent } from './layout/bare/bare.component';
import { CardsComponent } from './pages/cards/cards.component';
import { CollectionsComponent } from './pages/collections/collections.component';
import { DeckComponent } from './pages/deck/deck.component';
import { DecksComponent } from './pages/decks/decks.component';
import { FoldersComponent } from './pages/folders/folders.component';

const routes: Routes = [
  { path: '', component: AppLayoutComponent, children: [
    {path: 'cards', component: CardsComponent},
    {path: 'collections/:id', component: FoldersComponent},
    {path: 'collections/:colid/:id', component: CardsComponent},
    {path: 'collections', component: CollectionsComponent},
    {path: 'decks/:id', component: DeckComponent},
    {path: 'decks', component: DecksComponent},
    {path: '', component: CollectionsComponent}
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
