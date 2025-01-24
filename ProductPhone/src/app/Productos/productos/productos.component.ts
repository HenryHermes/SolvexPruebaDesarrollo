import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ProductItemComponent } from '../../ProductItem/product-item/product-item.component';
import { ApisService } from '../../Services/apis.service';
import { FormTraerProducto } from '../../Interfaces/form-traer-producto';
import { Productos } from '../../Interfaces/productos';
import { GetProductResponse } from '../../Interfaces/get-product-response';
import { NgxPaginationModule } from 'ngx-pagination';
import { ProductoCategoria } from '../../Interfaces/producto-categoria';
import { GetResponseProductoCategoria } from '../../Interfaces/get-response-producto-categoria';
import { ProductoCategoriaItemsComponent } from '../../ProductoCategoriaItems/producto-categoria-items/producto-categoria-items.component';
import { ActionResponse } from '../../Interfaces/action-response';
import { NavMenuComponent } from '../../NavMenu/nav-menu/nav-menu.component';

@Component({
  selector: 'app-productos',
  standalone: true,
  imports: [FormsModule,ProductItemComponent,CommonModule,NgxPaginationModule,ProductoCategoriaItemsComponent,NavMenuComponent],
  templateUrl: './productos.component.html',
  styleUrl: './productos.component.css'
})
export class ProductosComponent {
  constructor (private Api: ApisService){}
  role : string = '' + localStorage.getItem('rol')

  PaginaActual:number = 1;
  PaginaActualPC:number = 1;
  inputCategoria: string = '';
  inputDeleteIDProducto: number = 0;
  inputDeleteIDColor: number = 0;

  inicialForm : FormTraerProducto = {iD_Producto:0,nombre:'',iD_Color:0,color:'',precio1:0,precio2:999999,fecha_Ultima_Mod_1:'2000-01-01',fecha_Ultima_Mod_2:'2000-01-01',cantidad1:0,cantidad2:999999}
  inicialFormCategorias: string = ''

  ngOnInit(): void {
    this.traerProducto(this.inicialForm)
    this.traerProductoCategoria(this.inicialFormCategorias)
    console.log(this.role)
  }

  ListaProductos: Productos[] = []
  ListaProductosCategoria: ProductoCategoria[] = []

  traerProducto(Form : FormTraerProducto){
    
    Form.fecha_Ultima_Mod_1 = '2000-01-01'
    Form.fecha_Ultima_Mod_2 = '2000-01-01'
    
    this.Api.TraerProductos(Form).subscribe((Result : GetProductResponse) => {
      this.ListaProductos = Result.result
      console.log(this.ListaProductos)
    })
  }

  traerProductoCategoria(Form : string){
    
    this.Api.TraerProductosCategoria(Form).subscribe((Result : GetResponseProductoCategoria) => {
      this.ListaProductosCategoria = Result.result      
      console.log(this.ListaProductosCategoria)
    })
  }

  DeleteProducto(IDProducto : number, IDColor : number){
    
    this.Api.DeleteProductos(IDProducto,IDColor).subscribe((Result : ActionResponse) => {
           
      console.log(Result.message)
    })
  }

}
