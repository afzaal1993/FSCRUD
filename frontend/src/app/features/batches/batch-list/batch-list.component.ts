import { Component, OnDestroy, OnInit } from '@angular/core';
import { BatchService } from '../services/batch.service';
import { Batches } from '../models/batch.model';
import { Observable, Subscription } from 'rxjs';
import { genericResult } from 'src/app/core/models/generic-result.model';

@Component({
  selector: 'app-batch-list',
  templateUrl: './batch-list.component.html',
  styleUrls: ['./batch-list.component.css'],
})
export class BatchListComponent implements OnInit, OnDestroy {
  genericResult$?: Observable<genericResult>;
  batchesArray?: Batches[];
  deleteSubscription$?: Subscription;

  constructor(private batchService: BatchService) {}
    

  ngOnInit(): void {
    this.getAllBatches();
  }

  getAllBatches() {
    this.genericResult$ = this.batchService.getAllBatches();
  }

  convertToBatchesArray(data?: object) {
    this.batchesArray = data as Batches[];
    console.log(this.batchesArray);
    return this.batchesArray;
  }

  deleteBatch(id: string) {
    if (confirm("Are you sure you want to delete this batch?")) {
      this.deleteSubscription$ = this.batchService.deleteBatch(id).subscribe({
        next: (response) => {
          if (response.status) {
            this.getAllBatches();
          }
        },
        error: (err) => {
          console.log(err);
        }
      })
    }
  }

  ngOnDestroy(): void {
    this.deleteSubscription$?.unsubscribe();
  }
}
