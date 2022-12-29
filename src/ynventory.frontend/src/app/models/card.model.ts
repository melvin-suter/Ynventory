export class CardModel {
    id:number = -1;
    name:string = "";
    scryfallID:string = "";
    quantity:number = 0;
    foil:string = "";

    public static getImageUrl(card:CardModel, type:"large"|"small"|"normal"|"border_crop"|"art_crop" = "large"):string{
        return "https://cards.scryfall.io/" + type + "/front/" + card.scryfallID.substring(0,1) + "/" + card.scryfallID.substring(1,2)+ "/" + card.scryfallID + ".jpg";
    }
}