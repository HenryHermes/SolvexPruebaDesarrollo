import { Component,Input } from '@angular/core';
import { Usuarios } from '../../Interfaces/usuarios';

@Component({
  selector: 'app-user-items',
  standalone: true,
  imports: [],
  templateUrl: './user-items.component.html',
  styleUrl: './user-items.component.css'
})
export class UserItemsComponent {
  @Input() User!: Usuarios;

}
