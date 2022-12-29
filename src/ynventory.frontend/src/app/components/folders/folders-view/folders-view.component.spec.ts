import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FoldersViewComponent } from './folders-view.component';

describe('FoldersViewComponent', () => {
  let component: FoldersViewComponent;
  let fixture: ComponentFixture<FoldersViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FoldersViewComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FoldersViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
