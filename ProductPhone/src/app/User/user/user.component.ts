import { Component } from '@angular/core';
import { NavMenuComponent } from '../../NavMenu/nav-menu/nav-menu.component';
import { Usuarios } from '../../Interfaces/usuarios';
import { ApisService } from '../../Services/apis.service';
import { FormTraerUsuarios } from '../../Interfaces/form-traer-usuarios';
import { GetUsuariosResponse } from '../../Interfaces/get-usuarios-response';
import { UserItemsComponent } from '../../UserItems/user-items/user-items.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActionResponse } from '../../Interfaces/action-response';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [NavMenuComponent,FormsModule,UserItemsComponent,NgxPaginationModule,CommonModule],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})
export class UserComponent {
  constructor (private Api: ApisService){}
  
  InicialForm: FormTraerUsuarios = {iD_Ususario:0,username:'',nombre:'',email:'',iD_Rol:0,rol:''}
  
  ListaUsuarios: Usuarios[] = []
  PaginaActual:number = 1;
  UserID:number=0

  ngOnInit(): void {
    this.traerUsuarios(this.InicialForm)
  }

  traerUsuarios(Form : FormTraerUsuarios){
    this.Api.TraerUsuarios(Form).subscribe((Result : GetUsuariosResponse) => {
      this.ListaUsuarios = Result.result
      console.log(this.ListaUsuarios)
    })
  }

  DeleteUser(ID : number){
      
      this.Api.DeleteUser(ID).subscribe((Result : ActionResponse) => {
             
        console.log(Result.message)
      })
  }



}
