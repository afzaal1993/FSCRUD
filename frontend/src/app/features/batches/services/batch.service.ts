import { Injectable } from '@angular/core';
import { AddBatchRequest } from '../models/add-batch-request.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { genericResult } from '../../../core/models/generic-result.model';
import { environment } from 'src/environments/environment';
import { Batches } from '../models/batch.model';

@Injectable({
  providedIn: 'root',
})
export class BatchService {
  constructor(private http: HttpClient) { }

  addBatch(model: AddBatchRequest): Observable<genericResult> {
    return this.http.post<genericResult>(
      `${environment.apiBaseUrl}/Batch/Create`,
      model
    );
  }

  editBatch(id: string, model?: Batches): Observable<genericResult> {
    return this.http.put<genericResult>(`${environment.apiBaseUrl}/Batch/Update/${id}`, model);
  }

  deleteBatch(id: string): Observable<genericResult> {
    return this.http.delete<genericResult>(`${environment.apiBaseUrl}/Batch/Delete/${id}`);
  }

  getAllBatches(): Observable<genericResult> {
    return this.http.get<genericResult>(
      `${environment.apiBaseUrl}/Batch/GetAll`
    );
  }

  getBatchById(id: string): Observable<genericResult> {
    return this.http.get<genericResult>(
      `${environment.apiBaseUrl}/Batch/GetById/${id}`
    );
  }
}
