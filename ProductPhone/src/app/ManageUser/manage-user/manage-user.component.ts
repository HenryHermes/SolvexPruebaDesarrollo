import { Component } from '@angular/core';
import { NavMenuComponent } from '../../NavMenu/nav-menu/nav-menu.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApisService } from '../../Services/apis.service';
import { Usuarios } from '../../Interfaces/usuarios';
import { ActionResponse } from '../../Interfaces/action-response';

@Component({
  selector: 'app-manage-user',
  standalone: true,
  imports: [NavMenuComponent,CommonModule,FormsModule],
  templateUrl: './manage-user.component.html',
  styleUrl: './manage-user.component.css'
})
export class ManageUserComponent {
  constructor(private Api: ApisService){}
  Ejecucion = ''
  Operation: number = 1
  User: Usuarios = {iD_Ususario:0,username:'',nombre:'',email:'',contrase:'',fecha_Ultima_Mod:'2001-01-01',iD_Rol:0,rol:''}

  CrearUser(Form: any){
      
      this.User.username = Form.username
      this.User.nombre=Form.nombre
      this.User.email = Form.email
      this.User.contrase=Form.contrase
      this.User.iD_Rol=Form.iD_Rol
      this.Api.InsertUser(this.User).subscribe((Result:ActionResponse)=>{
        console.log(Result.message)
        if(Result.success == true){
          this.Ejecucion='Se ha realizado exitosamente'
        }else{
          this.Ejecucion=''
        }
      })
  }
  
  UpdateUser(Form: any){
      
      this.User.iD_Ususario=Form.iD_Ususario
      this.User.username = Form.username
      this.User.nombre=Form.nombre
      this.User.email = Form.email
      this.User.contrase=Form.contrase
      this.User.iD_Rol=Form.iD_Rol
      this.Api.UpdateUser(this.User).subscribe((Result:ActionResponse)=>{
        if(Result.success == true){
          this.Ejecucion='Se ha realizado exitosamente'
        }else{
          this.Ejecucion=''
        }
      })
  }
}
