import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginData } from '../Interfaces/login-data';
import { Observable, from, throwError } from 'rxjs';
import { error } from 'console';
import { Router } from '@angular/router';
import e from 'express';
import { LoginResponse } from '../Interfaces/login-response';
import { catchError } from 'rxjs/operators';
import { GetProductResponse } from '../Interfaces/get-product-response';
import { FormTraerProducto } from '../Interfaces/form-traer-producto';
import { GetResponseProductoCategoria } from '../Interfaces/get-response-producto-categoria';
import { ActionResponse } from '../Interfaces/action-response';
import { Productos } from '../Interfaces/productos';
import { GetUsuariosResponse } from '../Interfaces/get-usuarios-response';
import { FormTraerUsuarios } from '../Interfaces/form-traer-usuarios';
import { Usuarios } from '../Interfaces/usuarios';

@Injectable({
  providedIn: 'root'
})
export class ApisService {
  urlLogin = 'https://localhost:7185/Auth/Login'
  urlTraerProducto = 'https://localhost:7185/Product/TraerProducto'
  urlTraerProductoCategoria= 'https://localhost:7185/Product/TraerProductoCategoria'
  urlDeleteProducto= 'https://localhost:7185/Product/DeleteProducto'
  urlInsertProducto= 'https://localhost:7185/Product/InsertProducto'
  urlUpdateProducto= 'https://localhost:7185/Product/UpdateProducto'
  urlInsertProductoColor= 'https://localhost:7185/Product/InsertProductoColor'
  urlUpdateProductoColor= 'https://localhost:7185/Product/UpdateProductoColor'
  urlTraerUser= 'https://localhost:7185/Ususario/TraerUsuario'
  urlDeleteUser= 'https://localhost:7185/Ususario/DeleteUser'
  urlInsertUser= 'https://localhost:7185/Ususario/InsertarUsuario'
  urlUpdateUser= 'https://localhost:7185/Ususario/UpdateUsuario'


  httpOptions = {
    headers: new HttpHeaders({
      'Authorization': 'Bearer ' + localStorage.getItem('JWTToken')
    })
  };

  constructor(private router: Router , private http: HttpClient) { }

  ErrorDeAcesso : LoginResponse = { rol : 0, token : ""}
  ErrorDePermiso : LoginResponse = { rol : 0, token : "No Tienes Acceso a esta funcion"}

  ErrorTraerProductos : GetProductResponse = {success : false, message : '', result: []}
  ErrorTraerProductoCategoria: GetResponseProductoCategoria ={success:false, message:'', result:[]}
  ErrorTraerUser: GetUsuariosResponse ={success:false, message:'', result:[]}
  ErrorAction : ActionResponse = {success : false, message : '', result: ''}

  LoginUsers(data: LoginData): Observable<LoginResponse>{
    
    return this.http.post<LoginResponse>(this.urlLogin,data).pipe(catchError(this.TranslateError.bind(this)))
  }

  TranslateError(error: HttpErrorResponse) : Observable <LoginResponse>{
    if (error.status == 400 || error.status == 401) {
      return throwError(() => this.ErrorDeAcesso)
    }else{
      return throwError(() => this.ErrorDePermiso)
    }
  }

  ErrorGetPorduct(error: HttpErrorResponse) : Observable <GetProductResponse>{
    if (error.status == 401) {
      this.router.navigate(['..'])
    }
    return throwError(() => this.ErrorTraerProductos)
  }

  ErrorGetPorductCategoria(error: HttpErrorResponse) : Observable <GetResponseProductoCategoria>{
    if (error.status == 401) {
      this.router.navigate(['..'])
    }
    return throwError(() => this.ErrorTraerProductoCategoria)
  }

  ErrorGetUser(error: HttpErrorResponse) : Observable <GetUsuariosResponse>{
    if ( error.status == 401) {
      this.router.navigate(['..'])
    }
    return throwError(() => this.ErrorTraerUser)
  }

  ErrorSentAction(error: HttpErrorResponse) : Observable <ActionResponse>{
    if ( error.status == 401) {
      this.router.navigate(['..'])
    }
    return throwError(() => this.ErrorAction)
  }

  //Productos
  TraerProductos(data: FormTraerProducto): Observable<GetProductResponse>{
    
    return this.http.post<GetProductResponse>(this.urlTraerProducto,data,this.httpOptions).pipe(catchError(this.ErrorGetPorduct.bind(this)))
  }

  TraerProductosCategoria(data: string): Observable<GetResponseProductoCategoria>{
    let dataString = data    
    return this.http.post<GetProductResponse>(this.urlTraerProductoCategoria +'?name='+dataString,data,this.httpOptions).pipe(catchError(this.ErrorGetPorductCategoria.bind(this)))
  }

  DeleteProductos(IDProducto: number,IDColor:number): Observable<ActionResponse>{
    let dataString = IDProducto.toString()   
    let dataString2 = IDColor.toString()   
    return this.http.post<ActionResponse>(this.urlDeleteProducto +'?ID_Producto='+dataString +'&ID_Color='+dataString2,null,this.httpOptions).pipe(catchError(this.ErrorSentAction.bind(this)))
  }

  UpdateProductos(ID: number,Nombre:string): Observable<ActionResponse>{
    let dataString = ID.toString()   
    let dataString2 = Nombre  
    return this.http.post<ActionResponse>(this.urlUpdateProducto +'?ID='+dataString +'&Nombre='+dataString2,null,this.httpOptions).pipe(catchError(this.ErrorSentAction.bind(this)))
  }

  InsertProductos(Nombre:string): Observable<ActionResponse>{
      
    let dataString2 = Nombre  
    return this.http.post<ActionResponse>(this.urlInsertProducto +'?Nombre='+dataString2,null,this.httpOptions).pipe(catchError(this.ErrorSentAction.bind(this)))
  }

  UpdateProductosColor(producto: Productos): Observable<ActionResponse>{
     
    return this.http.post<ActionResponse>(this.urlUpdateProductoColor,producto,this.httpOptions).pipe(catchError(this.ErrorSentAction.bind(this)))
  }

  InsertProductosColor(producto: Productos): Observable<ActionResponse>{
      
    return this.http.post<ActionResponse>(this.urlInsertProductoColor,producto,this.httpOptions).pipe(catchError(this.ErrorSentAction.bind(this)))
  }

  //Usuarios

  TraerUsuarios(data: FormTraerUsuarios): Observable<GetUsuariosResponse>{
    
    return this.http.post<GetUsuariosResponse>(this.urlTraerUser,data,this.httpOptions).pipe(catchError(this.ErrorGetUser.bind(this)))
  }

  InsertUser(ususario: Usuarios): Observable<ActionResponse>{
      
    return this.http.post<ActionResponse>(this.urlInsertUser,ususario,this.httpOptions).pipe(catchError(this.ErrorSentAction.bind(this)))
  }

  UpdateUser(ususario: Usuarios): Observable<ActionResponse>{
      
    return this.http.post<ActionResponse>(this.urlUpdateUser,ususario,this.httpOptions).pipe(catchError(this.ErrorSentAction.bind(this)))
  }

  DeleteUser(ID: number): Observable<ActionResponse>{
    let dataString = ID.toString()
    return this.http.post<ActionResponse>(this.urlDeleteUser +'?ID='+dataString,null,this.httpOptions).pipe(catchError(this.ErrorSentAction.bind(this)))
  }






}
