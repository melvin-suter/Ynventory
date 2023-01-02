export interface ImportTaskModel {
    fileName?:string;
    taskState?:"Pending"|"Successfull"|"Failed";
    taskType?:"DelverCSV";
    fileData?:string;
    collectionId:number;
    collectionItemId:number;
    errors?:{id:number,error:string,errorData:string}[];
    createdAt?:Date;
    finishedAt?:Date;
}