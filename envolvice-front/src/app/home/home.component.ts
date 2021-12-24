import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { Employee } from '../shared/models/employee';
import { EmployeesService } from '../shared/services/employees.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
  employees: Employee[] = [];
  isUploading = false;
  fileName: string = '';
  isValidFile: boolean = true;
  error: string = '';
  constructor(private _employeeService: EmployeesService) {}

  onFileChange(event: any) {
    this.error = '';
    const file = event.target.files[0];
    let fileName: string = file.name;
    const fileExtension = fileName.substring(
      fileName.lastIndexOf('.') + 1,
      fileName.length
    );
    if (fileExtension !== 'txt') {
      this.isValidFile = false;
      return;
    }

    this.isUploading = true;
    const payload = new FormData();
    payload.append('file', file, fileName);
    this._employeeService
      .getFilter(payload)
      .pipe(finalize(() => (this.isUploading = false)))
      .subscribe(
        (res) => (this.employees = res),
        (error) => {
          console.log(error);
          this.error = error.error.Message;
        }
      );
  }
}
