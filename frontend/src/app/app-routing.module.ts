import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BatchListComponent } from './features/batches/batch-list/batch-list.component';
import { AddBatchComponent } from './features/batches/add-batch/add-batch.component';
import { EditBatchComponent } from './features/batches/edit-batch/edit-batch.component';
import { LoginComponent } from './features/auth/login/login.component';
import { authGuard } from './features/auth/guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: LoginComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'admin/batches',
    component: BatchListComponent,
    canActivate: [authGuard],
  },
  {
    path: 'admin/batches/add',
    component: AddBatchComponent,
    canActivate: [authGuard],
  },
  {
    path: 'admin/batches/:id',
    component: EditBatchComponent,
    canActivate: [authGuard],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
