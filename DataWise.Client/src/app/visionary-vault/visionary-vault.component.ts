import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-visionary-vault',
  standalone: true,
  imports: [HttpClientModule, FormsModule, CommonModule],
  templateUrl: './visionary-vault.component.html',
  styleUrls: ['./visionary-vault.component.css']
})
export class VisionaryVaultComponent {
  file: File | null = null;
  columns: string[] = [];
  selectedCategory: string = '';
  selectedValue: string = '';
  selectedAggregation: number = 1; 
  selectedChartType: number = 1; 
  chartTitle: string = 'My Chart';
  chartImageUrl: string = '';

  constructor(private http: HttpClient) {}

  onFileSelected(event: any): void {
    if (event.target.files && event.target.files.length > 0) {
      this.file = event.target.files[0];
      this.convertFileToBase64(this.file!).then(base64 => {
        localStorage.setItem('uploadedFile', base64);
      });

      const formData = new FormData();
      formData.append('file', this.file!);
      const url = `${environment.apiUrl}/api/chart/columns`;
      this.http.post<any>(url, formData)
        .subscribe(response => {
          this.columns = response.columns;
        }, error => {
          console.error('Error fetching columns', error);
        });
    }
  }

  convertFileToBase64(file: File): Promise<string> {
    return new Promise<string>((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => resolve(reader.result as string);
      reader.onerror = error => reject(error);
    });
  }

  generateChart(): void {
    const base64File = localStorage.getItem('uploadedFile');
    if (!base64File) {
      alert('File not found in localStorage.');
      return;
    }

    const blob = this.base64ToBlob(base64File);
    const formData = new FormData();
    formData.append('file', blob, this.file?.name || 'uploaded.csv');

    formData.append('categoryColumn', this.selectedCategory);
    formData.append('valueColumn', this.selectedValue);
    formData.append('aggregation', this.selectedAggregation.toString());
    formData.append('chartType', this.selectedChartType.toString());
    formData.append('title', this.chartTitle);

    const url = `${environment.apiUrl}/api/chart/generate`;
    this.http.post(url, formData, { responseType: 'blob' })
      .subscribe(response => {
        this.chartImageUrl = URL.createObjectURL(response);
      }, error => {
        console.error('Error generating chart', error);
      });
  }

  base64ToBlob(base64: string): Blob {
    const parts = base64.split(',');
    const byteString = atob(parts[1]);
    const mimeString = parts[0].split(':')[1].split(';')[0];
    const ab = new ArrayBuffer(byteString.length);
    const ia = new Uint8Array(ab);
    for(let i = 0; i < byteString.length; i++){
      ia[i] = byteString.charCodeAt(i);
    }
    return new Blob([ab], { type: mimeString });
  }
}