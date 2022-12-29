import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FoldersMigrationComponent } from './folders-migration.component';

describe('FoldersMigrationComponent', () => {
  let component: FoldersMigrationComponent;
  let fixture: ComponentFixture<FoldersMigrationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FoldersMigrationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FoldersMigrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
