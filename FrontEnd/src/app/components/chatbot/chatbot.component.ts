import { Component, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { OpenAiService } from "../../services/open-ai.service";
import { PortfolioService } from "../../services/portfolio.service";
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import { NgIf } from "@angular/common";
import { ButtonModule } from "primeng/button";
import { PanelModule } from "primeng/panel";
import { InputTextareaModule } from 'primeng/inputtextarea';
import {InputTextModule} from "primeng/inputtext";

@Component({
  selector: 'app-chatbot',
  standalone: true,
  imports: [
    FormsModule,
    NgIf,
    ButtonModule,
    PanelModule,
    InputTextareaModule,
    ReactiveFormsModule,
    InputTextModule
  ],
  templateUrl: './chatbot.component.html',
  styleUrls: ['./chatbot.component.css']
})
export class ChatbotComponent implements OnInit {
  userQuestion: string = '';
  chatResponse: string = '';
  portfolioInfo: string = '';
  userId: string | null = null;

  questionForm: FormGroup =  new FormGroup({
    question: new FormControl('', [Validators.required]),
  });


  constructor(private router: Router,
              private openAiService: OpenAiService,
              private portfolioService: PortfolioService) {}

  ngOnInit(): void {
    if (!localStorage.getItem('userId')) {
      this.router.navigate(['home']);
    } else {
      this.userId = localStorage.getItem('userId');
      this.getPortfolio();

    }
  }

  sendQuestion() {
    if(this.questionForm.get('question')?.value != ''){
      const prompt = `User's portfolio info: ${this.portfolioInfo}\nUser's question: ${this.questionForm.get('question')?.value}`;
      this.openAiService.getChatResponse(prompt).subscribe(response => {
        this.chatResponse = response;
      });
    }
  }

  getPortfolio(): void {
    if (this.userId != null) {
      this.portfolioService.getPortfolioInfo(this.userId).subscribe(res => {
        this.portfolioInfo = res;
      });
      console.log(this.portfolioInfo);
    }
  }
}
