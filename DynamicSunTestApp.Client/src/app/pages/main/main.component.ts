import { Component } from '@angular/core';
import {RouterLink, RouterOutlet} from "@angular/router";
import {CommonModule} from "@angular/common";

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [RouterOutlet, RouterLink, CommonModule],
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss'
})
export class MainComponent {

}
