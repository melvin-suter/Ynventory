import { TestBed } from '@angular/core/testing';

import { ScryfallService } from './scryfall.service';

describe('ScryfallService', () => {
  let service: ScryfallService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ScryfallService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
