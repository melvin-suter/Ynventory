import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardsTableComponent } from './cards-table.component';

describe('CardsTableComponent', () => {
  let component: CardsTableComponent;
  let fixture: ComponentFixture<CardsTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CardsTableComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CardsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
