import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditBatchComponent } from './edit-batch.component';

describe('EditBatchComponent', () => {
  let component: EditBatchComponent;
  let fixture: ComponentFixture<EditBatchComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [EditBatchComponent]
    });
    fixture = TestBed.createComponent(EditBatchComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
