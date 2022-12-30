export class ScryfallCardModel {
    border_color:string = "";
    id:string = "";
    cmc:string = "";
    image_uris?:{small:string,large:string};
    colors?:string[];
    mana_cost:string = "";
    name:string = "";
    oracle_text:string = "";
    prices?:{usd:string};
    legalities?:{
        alchemy:"not_legal" | "legal"
        commander: "not_legal" | "legal"
        historic: "not_legal" | "legal"
        legacy: "not_legal" | "legal"
        modern: "not_legal" | "legal"
        standard: "not_legal" | "legal"
        pioneer: "not_legal" | "legal"
    };
    rarity?:"common" |"uncommon" |"rare" |"special" |"mythic" | "bonus";

    static getManaCostList(input:any):string[]{
        if(input.mana_cost == undefined || input.mana_cost.length <= 0){
            return [];
        }
        let sanitizedString = input.mana_cost.split("}{").join(",");
        sanitizedString = sanitizedString.replace(new RegExp("{"),"").replace(new RegExp("}"),"");
        return sanitizedString.split(",");
    }
}