import { NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Console } from 'console';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NgFor],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  http = inject(HttpClient);
  title = 'client';
  name = 'krishna'; 
  users : any;

  ngOnInit(): void {
    this.http.get('http://localhost:5207/api/Users').subscribe({
      next : response => this.users = response,
      error : error => console.log(error),
      complete : () => console.log("Requets Completed")  
    })
  }
  //constructor(private httpclient : HttpClient){}
}
