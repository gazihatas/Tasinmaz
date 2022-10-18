import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TasinmazComponent } from './tasinmaz.component';

describe('TasinmazComponent', () => {
  let component: TasinmazComponent;
  let fixture: ComponentFixture<TasinmazComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TasinmazComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TasinmazComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
