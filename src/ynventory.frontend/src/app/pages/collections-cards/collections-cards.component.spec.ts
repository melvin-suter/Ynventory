import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CollectionsCardsComponent } from './collections-cards.component';

describe('CollectionsCardsComponent', () => {
  let component: CollectionsCardsComponent;
  let fixture: ComponentFixture<CollectionsCardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CollectionsCardsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CollectionsCardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
