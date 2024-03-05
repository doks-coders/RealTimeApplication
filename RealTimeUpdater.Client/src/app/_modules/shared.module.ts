import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { DataTablesModule } from 'angular-datatables';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinner, NgxSpinnerModule } from 'ngx-spinner';
import { BaseChartDirective } from 'ng2-charts';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    TabsModule.forRoot(),
    BaseChartDirective,
    DataTablesModule,
    ModalModule.forRoot(),
    ToastrModule.forRoot({positionClass:"toast-bottom-right"}),
    NgxSpinnerModule.forRoot({type:"ball-scale-multiple"})
  ],
  exports:[
    BaseChartDirective,
    TabsModule,
    DataTablesModule,
    ModalModule,
    ToastrModule,
    NgxSpinnerModule
  ]
})
export class SharedModule { }
