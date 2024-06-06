import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class OpenAiService {
  private apiKey = 'sk-proj-P5QyRSd3M39b3tn39shiT3BlbkFJHrCUm0T8kjq7XLk8sQDO';
  private apiUrl = 'https://api.openai.com/v1/chat/completions';

  constructor(private http: HttpClient) {}

  getChatResponse(prompt: string): Observable<string> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.apiKey}`,
      'Content-Type': 'application/json'
    });

    const body = {
      model: 'gpt-3.5-turbo',
      messages: [{ role: 'user', content: prompt }],
      temperature: 0.7
    };

    return this.http.post<any>(this.apiUrl, body, { headers }).pipe(
      map(response => response.choices[0].message.content)
    );
  }
}
