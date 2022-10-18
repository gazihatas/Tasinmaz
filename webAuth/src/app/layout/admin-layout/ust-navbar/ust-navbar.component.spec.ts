import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UstNavbarComponent } from './ust-navbar.component';

describe('UstNavbarComponent', () => {
  let component: UstNavbarComponent;
  let fixture: ComponentFixture<UstNavbarComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UstNavbarComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UstNavbarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
