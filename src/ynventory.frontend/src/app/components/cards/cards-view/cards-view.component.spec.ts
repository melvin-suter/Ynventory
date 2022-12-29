import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardsViewComponent } from './cards-view.component';

describe('CardsViewComponent', () => {
  let component: CardsViewComponent;
  let fixture: ComponentFixture<CardsViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CardsViewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CardsViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
