import { Component,Input } from '@angular/core';
import { Productos } from '../../Interfaces/productos';
import { ApisService } from '../../Services/apis.service';

@Component({
  selector: 'app-product-item',
  standalone: true,
  imports: [],
  templateUrl: './product-item.component.html',
  styleUrl: './product-item.component.css'
})
export class ProductItemComponent {
  @Input() Product!: Productos;

  

  

}
