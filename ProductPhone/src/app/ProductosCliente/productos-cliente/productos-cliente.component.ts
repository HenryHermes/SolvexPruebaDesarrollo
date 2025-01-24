import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgxPaginationModule } from 'ngx-pagination';
import { ProductItemComponent } from '../../ProductItem/product-item/product-item.component';
import { ApisService } from '../../Services/apis.service';
import { FormTraerProducto } from '../../Interfaces/form-traer-producto';
import { GetProductResponse } from '../../Interfaces/get-product-response';
import { Productos } from '../../Interfaces/productos';

@Component({
  selector: 'app-productos-cliente',
  standalone: true,
  imports: [FormsModule,CommonModule,NgxPaginationModule,ProductItemComponent],
  templateUrl: './productos-cliente.component.html',
  styleUrl: './productos-cliente.component.css'
})
export class ProductosClienteComponent {
  constructor (private Api: ApisService){}
  ListaProductos: Productos[] = []
  PaginaActual:number = 1;
  inicialForm : FormTraerProducto = {iD_Producto:0,nombre:'',iD_Color:0,color:'',precio1:0,precio2:999999,fecha_Ultima_Mod_1:'2000-01-01',fecha_Ultima_Mod_2:'2000-01-01',cantidad1:0,cantidad2:999999}
  ngOnInit(): void {
    this.traerProducto(this.inicialForm)
  }
  traerProducto(Form : FormTraerProducto){
      
      Form.fecha_Ultima_Mod_1 = '2000-01-01'
      Form.fecha_Ultima_Mod_2 = '2000-01-01'
      
      this.Api.TraerProductos(Form).subscribe((Result : GetProductResponse) => {
        this.ListaProductos = Result.result
        console.log(this.ListaProductos)
      })
    }
}
