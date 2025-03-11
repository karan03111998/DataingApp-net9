import { NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit{
  http = inject(HttpClient)
  title = 'Dating App';
  users : any;
  ngOnInit(): void {
    this.http.get('http://localhost:5100/api/user').subscribe({
      next: response => this.users = response,
      error : error => console.log(error),
      complete : () => console.log('Requested completed')
    })
  }

}
