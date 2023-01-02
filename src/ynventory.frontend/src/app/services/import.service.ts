import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ImportTaskModel } from '../models/import-task.model';

@Injectable({
  providedIn: 'root'
})
export class ImportService {

  constructor(private http: HttpClient) { }

  /****************
   *  Collections
   ****************/

  getTasks():Observable<ImportTaskModel[]>{
    return this.http.get<ImportTaskModel[]>('/api/import');
  }

  createTasks(data:ImportTaskModel):Observable<any>{
    console.log(data);
    return this.http.post<ImportTaskModel[]>('/api/import',data);
  }
}
