import { Component, OnDestroy } from '@angular/core';
import { AddBatchRequest } from '../models/add-batch-request.model';
import { BatchService } from '../services/batch.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-batch',
  templateUrl: './add-batch.component.html',
  styleUrls: ['./add-batch.component.css'],
})
export class AddBatchComponent implements OnDestroy {
  model: AddBatchRequest;
  private addBatchSubscription?: Subscription;

  constructor(private batchService: BatchService, private router: Router) {
    this.model = {
      batchName: '',
    };
  }

  onFormSubmit() {
    this.addBatchSubscription = this.batchService
      .addBatch(this.model)
      .subscribe({
        next: (response) => {
          if (response.status) {
            this.router.navigateByUrl('/admin/batches');
          }
        },
        error: (error) => {
          console.log(error);
        },
      });
  }

  ngOnDestroy(): void {
    this.addBatchSubscription?.unsubscribe();
  }
}
