import { Component, Input } from '@angular/core';
import { ProductoCategoria } from '../../Interfaces/producto-categoria';

@Component({
  selector: 'app-producto-categoria-items',
  standalone: true,
  imports: [],
  templateUrl: './producto-categoria-items.component.html',
  styleUrl: './producto-categoria-items.component.css'
})
export class ProductoCategoriaItemsComponent {
  @Input() ProductCategoria!: ProductoCategoria;

}
