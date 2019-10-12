import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicShareIdeaComponent } from './public-share-idea.component';

describe('PublicShareIdeaComponent', () => {
  let component: PublicShareIdeaComponent;
  let fixture: ComponentFixture<PublicShareIdeaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PublicShareIdeaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PublicShareIdeaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
