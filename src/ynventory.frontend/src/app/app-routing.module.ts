import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';
import { AppLayoutComponent } from './components/layout/app-layout/app-layout.component';
import { BareComponent } from './components/layout/bare/bare.component';
import { DashboardComponent } from './components/pages/dashboard/dashboard.component';
import { DecksListComponent } from './components/decks/decks-list/decks-list.component';
import { DecksViewComponent } from './components/decks/decks-view/decks-view.component';
import { LoginComponent } from './components/pages/login/login.component';
import { CollectionsListComponent } from './components/collections/collections-list/collections-list.component';
import { CollectionsViewComponent } from './components/collections/collections-view/collections-view.component';
import { FoldersViewComponent } from './components/folders/folders-view/folders-view.component';
import { CardsViewComponent } from './components/cards/cards-view/cards-view.component';
import { CardsListComponent } from './components/cards/cards-list/cards-list.component';
import { TasksListComponent } from './components/tasks/tasks-list/tasks-list.component';

const routes: Routes = [
  { path: '', component: AppLayoutComponent, canActivate: [AuthGuard], children: [
    {path: 'cards', component: CardsListComponent},
    {path: 'card/:cardid', component: CardsViewComponent},
    {path: 'collections/:colid', component: CollectionsViewComponent},
    {path: 'collections/:colid/folders/:id', component: FoldersViewComponent},
    {path: 'collections', component: CollectionsListComponent},
    {path: 'decks/:id', component: DecksViewComponent},
    {path: 'decks', component: DecksListComponent},
    {path: 'home', component: DashboardComponent},
    {path: 'tasks', component: TasksListComponent},
    {path: '', redirectTo: 'home', pathMatch: 'full'}
  ]},
  { path: '', component: BareComponent, children: [
    {path: 'login', component: LoginComponent},
  ]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
