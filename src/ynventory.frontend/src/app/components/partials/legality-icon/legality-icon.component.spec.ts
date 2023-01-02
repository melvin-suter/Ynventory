import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LegalityIconComponent } from './legality-icon.component';

describe('LegalityIconComponent', () => {
  let component: LegalityIconComponent;
  let fixture: ComponentFixture<LegalityIconComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LegalityIconComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LegalityIconComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
