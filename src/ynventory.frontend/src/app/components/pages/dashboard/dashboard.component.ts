import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  data:any;
  constructor() { }

  chartOptions:any = {
    plugins: {
      legend: {
        position: 'bottom'
      }
    }
  }

  ngOnInit(): void {
    this.data = {
      labels: ['White','Red','Green', 'Blue', 'Black'],
      datasets: [
        {
          data: [300, 50, 100, 400, 500],
          backgroundColor: [
            "rgb(248,231,185)",
            "rgb(211,32,42)",
            "rgb(0,115,62)",
            "rgb(14,104,171)",
            "rgb(21,11,0)"
          ],
          hoverBackgroundColor: [
            "rgb(248,231,185)",
            "rgb(211,32,42)",
            "rgb(0,115,62)",
            "rgb(14,104,171)",
            "rgb(21,11,0)"
          ]
        }
      ]
    };
  }

}
