import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './nav-menu.component.html',
  styleUrl: './nav-menu.component.css'
})
export class NavMenuComponent {
  constructor( private router: Router ) { }

  role : string = '' + localStorage.getItem('rol')

  ChangePageUsuario(){
    this.router.navigate(['../User'])
  }

  ChangePageProducto(){
    this.router.navigate(['../Productos'])
  }

  ChangePageCrearProducto(){
    this.router.navigate(['../ManageProduct'])
  }

  ChangePageCrearUsuario(){
    
    this.router.navigate(['../ManageUser'])
    
  }
}
