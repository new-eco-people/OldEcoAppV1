import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicIdeasComponent } from './public-ideas.component';

describe('PublicIdeasComponent', () => {
  let component: PublicIdeasComponent;
  let fixture: ComponentFixture<PublicIdeasComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PublicIdeasComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PublicIdeasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
