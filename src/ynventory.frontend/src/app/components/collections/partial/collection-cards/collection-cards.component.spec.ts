import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CollectionCardsComponent } from './collection-cards.component';

describe('CollectionCardsComponent', () => {
  let component: CollectionCardsComponent;
  let fixture: ComponentFixture<CollectionCardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CollectionCardsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CollectionCardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
