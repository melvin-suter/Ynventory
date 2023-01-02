export class CardModel {
    id?:number = -1;
    name?:string = "";
    cardMetadataId?:string = "";
    quantity?:number = 0;
    cardFinish?:"NonFoil"|"Foil"|"Edged";
    isCommander?:boolean;
    metadata?: {
        id:string
        name:string
        lang:string
        layout:string
        imageUrlSmall:string
        imageUrlLarge:string
        type:string
        manaCost:string
        oracleText:string
        power:number
        toughness: number
        manaCostTotal: number
        colors: string[]
        colorIdentity: string[]
        keywords: string[],
        legalities:any
    };

    public static getImageUrl(card:CardModel, type:"large"|"small"|"normal"|"border_crop"|"art_crop" = "large"):string{
        return "https://cards.scryfall.io/" + type + "/front/" + card.cardMetadataId!.substring(0,1) + "/" + card.cardMetadataId!.substring(1,2)+ "/" + card.cardMetadataId! + ".jpg";
    }
}