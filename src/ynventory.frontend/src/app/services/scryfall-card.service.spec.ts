import { TestBed } from '@angular/core/testing';

import { ScryfallCardService } from './scryfall-card.service';

describe('ScryfallCardService', () => {
  let service: ScryfallCardService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ScryfallCardService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
