import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { Chart, ChartOptions } from 'chart.js/auto';
import { BehaviorSubject, Observable } from 'rxjs';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, AfterViewInit {
  constructor(private authentication: AuthenticationService) { }
  sum: number = 0;
  @ViewChild('myChart') myChart?: ElementRef;
  private chart?: Chart<'bar'>;
  pieChartLegend = true
  pieChartPlugins = []

  public pieChartOptions: ChartOptions<'line'> = {
    responsive: false,
  };

  ngOnInit(): void {

    this.authentication.currentUser$.subscribe({
      next: (e) => {
        if (e) {
          let array: number[] = JSON.parse(e)
          this.sum = array.reduce((acc,curr)=>acc+curr);
          this.updateChart(array);
        }
      }
    })

  }
  ngAfterViewInit(): void {
    if (!this.myChart) return;
    this.chart = new Chart(this.myChart.nativeElement, {
      type: "bar",
      data: {
        labels: ['Data 1', 'Data 2', 'Data 3','Data 4','Data 5'],
        datasets: [
          { data: [10, 10, 10, 10, 10],backgroundColor:["gray","lightgray","gray","lightgray","gray"],label:"Test results"},
        ]
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        animation: {
          duration: 1000, // 1 second animation
          easing: 'easeInOutQuad' // Smooth transition with acceleration and deceleration
        },
      }
    });

  }


  updateChart(array: number[]) {
    // Simulate fetching new data

    if (this.chart && array) {
      let data = this.chart.data.datasets[0].data;
      //the specific index which is 2

      data[0] = Number(array[0])
      data[1] = Number(array[1])
      data[2] = Number(array[2])
      data[3] = Number(array[3])
      data[4] = Number(array[4])
      console.log(data);
      this.chart.data.datasets[0].data = data;
      this.chart.update();
    }
    // Update the entire chart

  }
}
