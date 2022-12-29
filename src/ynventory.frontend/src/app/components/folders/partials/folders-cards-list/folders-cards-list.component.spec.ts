import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FoldersCardsListComponent } from './folders-cards-list.component';

describe('FoldersCardsListComponent', () => {
  let component: FoldersCardsListComponent;
  let fixture: ComponentFixture<FoldersCardsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FoldersCardsListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FoldersCardsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
