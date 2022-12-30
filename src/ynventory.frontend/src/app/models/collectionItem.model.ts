export interface CollectionItemModel {
    name?:string;
    description?:string;
    cardCount?:number;
    id?:number;
    type?:"Folder"|"Deck";
}