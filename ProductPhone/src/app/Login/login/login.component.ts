import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { dateTimestampProvider } from 'rxjs/internal/scheduler/dateTimestampProvider';
import { ApisService } from '../../Services/apis.service';
import { LoginData } from '../../Interfaces/login-data';
import { LoginResponse } from '../../Interfaces/login-response';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  constructor(private router: Router, private User: ApisService ) {}

  UserName:string = ''
  Password:string = ''

  ChangePage(rol: number){
    if (rol!=0) {
      if (rol!=3){
        this.router.navigate(['../Productos'])
      }else{
        this.router.navigate(['../ProductosCliente'])
      }
      
    }
    
  }

  GetFormData(Data : LoginData){
    this.UserName=Data.userName
    this.Password=Data.password
    
    this.User.LoginUsers(Data).subscribe((Result : LoginResponse) => {
      this.ChangePage(Result.rol)
      localStorage.setItem('JWTToken', Result.token)
      localStorage.setItem('rol', Result.rol.toFixed())
    })

  }

}
