import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DecksViewComponent } from './decks-view.component';

describe('DecksViewComponent', () => {
  let component: DecksViewComponent;
  let fixture: ComponentFixture<DecksViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DecksViewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DecksViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
