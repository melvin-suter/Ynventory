import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeckComponent } from './deck.component';

describe('DeckComponent', () => {
  let component: DeckComponent;
  let fixture: ComponentFixture<DeckComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DeckComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeckComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
