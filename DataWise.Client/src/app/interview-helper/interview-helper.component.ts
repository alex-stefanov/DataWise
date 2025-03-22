import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ChatUIModule, UserModel } from '@syncfusion/ej2-angular-interactive-chat';
import { environment } from '../../environments/environment';

interface StartSessionDto {
  UserId: string;
  Category: string;
  Difficulty: string;
}

interface AnswerDto {
  SessionId: string;
  UserAnswer: string;
}

interface HintDto {
  SessionId: string;
}

interface ChatMessage {
  id: string;
  chatSessionId: string;
  sender: number;
  content: string;
  createdAt: Date;
}

interface StoredUser {
  id: string;
  firstName: string;
  lastName: string;
}

@Component({
  selector: 'interview-helper',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, ChatUIModule, CommonModule, HttpClientModule],
  templateUrl: './interview-helper.component.html',
  styleUrls: ['./interview-helper.component.css']
})
export class InterviewHelperComponent implements OnInit {
  public currentUserModel: UserModel = { user: '', id: '' };
  public systemUserModel: UserModel = { user: 'System', id: 'system' };

  public messages: ChatMessage[] = [];
  public userInput: string = '';
  public sessionId: string = '';

  public isLoggedIn = false;
  public user: StoredUser | null = null;

  private baseUrl = 'https://localhost:7085/api/interview';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    const storedUserId = sessionStorage.getItem('userId');
  
    if (storedUserId) {
      this.fetchUserProfile(storedUserId);
    } else {
      console.warn('No stored user ID found.');
      this.isLoggedIn = false;
    }
  }

  startSession(): void {
    const sessionData: StartSessionDto = {
      UserId: this.currentUserModel.id!,
      Category: 'SQL',
      Difficulty: 'Medium'
    };

    this.http.post<string>(`${this.baseUrl}/start`, sessionData, { responseType: 'text' as 'json' }).subscribe({
      next: (sessionId) => {
        this.sessionId = sessionId;
        this.getChatHistory();
      },
      error: (err) => {
        console.error('Error starting session:', err);
      }
    });    
  }

  sendAnswer(): void {
    const trimmedInput = this.userInput.trim();
    if (!trimmedInput) return;

    const payload: AnswerDto = {
      SessionId: this.sessionId,
      UserAnswer: trimmedInput
    };

    this.userInput = '';

    this.http.post<string>(`${this.baseUrl}/answer`, payload, { responseType: 'text' as 'json' }).subscribe({
      next: (sessionId) => {
        this.sessionId = sessionId;
        this.getChatHistory();
      },
      error: (err) => {
        console.error('Error sending answer:', err);
      }
    });
  }

  getHint(): void {
    const payload: HintDto = { SessionId: this.sessionId };

    this.http.post<string>(`${this.baseUrl}/hint`, payload, { responseType: 'text' as 'json' }).subscribe({
      next: (sessionId) => {
        this.sessionId = sessionId;
        this.getChatHistory();
      },
      error: (err) => {
        console.error('Error fetching hint:', err);
      }
    });
  }

  getChatHistory(): void {
    this.http.get<ChatMessage[]>(`${this.baseUrl}/history/${this.sessionId}`).subscribe({
      next: (msgs) => {
        this.messages = msgs;
      },
      error: (err) => {
        console.error('Error fetching chat history:', err);
      }
    });
  }

  getUserModel(sender: number): UserModel {
    if (sender === 0) {
      return this.currentUserModel;
    }
    if (sender === 1) {
      return this.systemUserModel;
    }
    return { user: 'Interviewer', id: 'interviewer' };
  }

  fetchUserProfile(userId: string): void {  
    const payload = { userId: userId };
    const url = `${environment.apiUrl}/api/user/profile`;
  
    this.http.post<any>(url, payload).subscribe(
      data => {
        this.user = {
          id: data.email,
          firstName: data.firstName,
          lastName: data.lastName,
        };
  
        if (this.user) {
          const displayName = `${this.user.firstName} ${this.user.lastName}`;
          this.currentUserModel = { user: displayName, id: this.user.id };
          this.isLoggedIn = true;
          this.startSession();
        }
      },
      error => {
        console.error('Error fetching profile', error);
      }
    );
  }

  onMessageSend(event: any): void {
    const messageText = event.message?.text?.trim();
    if (messageText) {
      this.userInput = messageText; 
      this.sendAnswer(); 
    }
  }
}
