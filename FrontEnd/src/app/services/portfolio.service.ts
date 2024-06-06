import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PortfolioService {

  constructor(private httpClient: HttpClient) { }

  getPortfolioInfo(userId: string): Observable<string> {
    return this.httpClient.get<string>(`https://localhost:7008/api/Chatbot/get-portfolio-info/${userId}`);
  }
}
