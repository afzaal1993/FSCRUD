import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { BatchService } from '../services/batch.service';
import { Batches } from '../models/batch.model';

@Component({
  selector: 'app-edit-batch',
  templateUrl: './edit-batch.component.html',
  styleUrls: ['./edit-batch.component.css'],
})
export class EditBatchComponent implements OnInit, OnDestroy {
  id: string | null = null;
  paramsSubscribtion?: Subscription;
  editSubscription?: Subscription;
  batch?: Batches;

  constructor(
    private route: ActivatedRoute,
    private batchService: BatchService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.paramsSubscribtion = this.route.paramMap.subscribe({
      next: (resp) => {
        this.id = resp.get('id');

        if (this.id) {
          this.batchService.getBatchById(this.id).subscribe({
            next: (response) => {
              console.log(response);
              if (response.status) {
                this.batch = response.data as Batches;
              }
            },
          });
        }
      },
    });
  }

  editBatch(id: string) {
    this.editSubscription = this.batchService.editBatch(id, this.batch).subscribe({
      next: (response) => {
        if (response.status) {
          this.router.navigateByUrl('/admin/batches');
        }
      }
    })
  }

  Cancel() {
    this.router.navigateByUrl('/admin/batches');
  }

  ngOnDestroy(): void {
    this.paramsSubscribtion?.unsubscribe();
  }
}
