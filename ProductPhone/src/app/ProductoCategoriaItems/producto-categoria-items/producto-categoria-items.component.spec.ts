import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductoCategoriaItemsComponent } from './producto-categoria-items.component';

describe('ProductoCategoriaItemsComponent', () => {
  let component: ProductoCategoriaItemsComponent;
  let fixture: ComponentFixture<ProductoCategoriaItemsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProductoCategoriaItemsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductoCategoriaItemsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
