import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PropertiesForRentComponent } from './properties-for-rent.component';

describe('PropertiesForRentComponent', () => {
  let component: PropertiesForRentComponent;
  let fixture: ComponentFixture<PropertiesForRentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PropertiesForRentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PropertiesForRentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
