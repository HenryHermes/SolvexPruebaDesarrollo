import { Component } from '@angular/core';
import { NavMenuComponent } from '../../NavMenu/nav-menu/nav-menu.component';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Productos } from '../../Interfaces/productos';
import { ApisService } from '../../Services/apis.service';
import { ActionResponse } from '../../Interfaces/action-response';

@Component({
  selector: 'app-manage-product',
  standalone: true,
  imports: [NavMenuComponent,FormsModule,CommonModule],
  templateUrl: './manage-product.component.html',
  styleUrl: './manage-product.component.css'
})
export class ManageProductComponent {
  constructor(private Api: ApisService){}
  Ejecucion = ''
  Operation: number = 1
  Operation2: number = 1
  producto : Productos = {iD_Producto:0,nombre:'',iD_Color:0,color:'',precio:0,fecha_Ultima_Mod:'2000-01-01',cantidad:0,imagen:null}

  ngOnInit(): void {
    
    console.log(this.Operation)
  }

  create(Form: any){
    
    this.Api.InsertProductos(Form.nombre).subscribe((Result:ActionResponse)=>{
      if(Result.success == true){
        this.Ejecucion='Se ha realizado exitosamente'
      }else{
        this.Ejecucion=''
      }
    })
  }

  Update(Form: any){
    
    this.Api.UpdateProductos(Form.iD_Producto,Form.nombre).subscribe((Result:ActionResponse)=>{
      if(Result.success == true){
        this.Ejecucion='Se ha realizado exitosamente'
      }else{
        this.Ejecucion=''
      }
    })
  }

  CrearColor(Form: any){
    
    this.producto.iD_Producto = Form.iD_Producto
    this.producto.iD_Color=Form.iD_Color
    this.producto.precio = Form.precio
    this.producto.cantidad=Form.cantidad
    this.Api.InsertProductosColor(this.producto).subscribe((Result:ActionResponse)=>{
      if(Result.success == true){
        this.Ejecucion='Se ha realizado exitosamente'
      }else{
        this.Ejecucion=''
      }
    })
  }

  UpdateColor(Form: any){
    
    this.producto.iD_Producto = Form.iD_Producto
    this.producto.iD_Color=Form.iD_Color
    this.producto.precio = Form.precio
    this.producto.cantidad=Form.cantidad
    this.Api.UpdateProductosColor(this.producto).subscribe((Result:ActionResponse)=>{
      if(Result.success == true){
        this.Ejecucion='Se ha realizado exitosamente'
      }else{
        this.Ejecucion=''
      }
    })
  }



}
