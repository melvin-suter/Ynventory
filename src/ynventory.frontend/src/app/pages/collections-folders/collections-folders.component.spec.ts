import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CollectionsFoldersComponent } from './collections-folders.component';

describe('CollectionsFoldersComponent', () => {
  let component: CollectionsFoldersComponent;
  let fixture: ComponentFixture<CollectionsFoldersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CollectionsFoldersComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CollectionsFoldersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
