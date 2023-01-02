export interface ImportTaskModel {
    fileName?:string;
    taskState?:"Pending"|"Successfull"|"Failed";
    taskType?:"DelverCSV";
    fileData?:string;
    collectionId:number;
    collectionItemId:number;
    createdAt?:Date;
    finishedAt?:Date;
}