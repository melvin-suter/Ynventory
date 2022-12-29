import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CollectionFoldersComponent } from './collection-folders.component';

describe('CollectionFoldersComponent', () => {
  let component: CollectionFoldersComponent;
  let fixture: ComponentFixture<CollectionFoldersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CollectionFoldersComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CollectionFoldersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
