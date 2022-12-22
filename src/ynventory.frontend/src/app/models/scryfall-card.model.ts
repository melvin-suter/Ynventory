export class ScryfallCardModel {
    border_color:string = "";
    id:string = "";
    cmc:string = "";
    image_uris?:{small:string,large:string};
    colors?:string[];
    mana_cost:string = "";
    name:string = "";
    oracle_text:string = "";

    static getManaCostList(input:any):string[]{
        if(input.mana_cost == undefined || input.mana_cost.length <= 0){
            return [];
        }
        let sanitizedString = input.mana_cost.split("}{").join(",");
        sanitizedString = sanitizedString.replace(new RegExp("{"),"").replace(new RegExp("}"),"");
        return sanitizedString.split(",");
    }
}