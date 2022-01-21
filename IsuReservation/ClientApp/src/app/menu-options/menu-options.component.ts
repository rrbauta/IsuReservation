import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-menu-options',
  templateUrl: './menu-options.component.html',
  styleUrls: ['./menu-options.component.css']
})
export class MenuOptionsComponent implements OnInit {

  @Input('title') title: string = '';
  @Input('subtitle') subtitle: string = '';
  @Input('reservation_list') reservation_list: boolean = false;
  @Input('reservation_create') reservation_create: boolean = false;
  @Input('contact_list') contact_list: boolean = false;
  @Input('contact_create') contact_create: boolean = false;

  constructor() {
  }

  ngOnInit(): void {
  }

}
