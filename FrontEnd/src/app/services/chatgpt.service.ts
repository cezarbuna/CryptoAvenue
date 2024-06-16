import { Injectable } from '@angular/core';
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class ChatgptService {


  private apiUrl = 'https://api.openai.com/v1/chat/completions';

  constructor(private http: HttpClient) { }

  getChatResponse(prompt: string): Observable<any> {
    const headers = {
    };

    const body = {
      prompt: prompt,
      max_tokens: 150
    };

    return this.http.post<any>(this.apiUrl, body, { headers });
  }
}
