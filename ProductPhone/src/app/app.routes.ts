import { RouterModule, Routes } from '@angular/router';

import { NgModule } from '@angular/core';
import { LoginComponent } from './Login/login/login.component';
import { ProductosComponent } from './Productos/productos/productos.component';
import { ProductosClienteComponent } from './ProductosCliente/productos-cliente/productos-cliente.component';
import { UserComponent } from './User/user/user.component';
import { ManageUserComponent } from './ManageUser/manage-user/manage-user.component';
import { ManageProductComponent } from './ManageProduct/manage-product/manage-product.component';

export const routes: Routes = [
    {
        path: '',
        component:LoginComponent
    },
    {
        path: 'Productos',
        component:ProductosComponent
    },
    {
        path:'ProductosCliente',
        component:ProductosClienteComponent
    },
    {
        path:'User',
        component:UserComponent
    },
    {
        path:'ManageUser',
        component:ManageUserComponent
    },
    {
        path:'ManageProduct',
        component:ManageProductComponent
    }
];
