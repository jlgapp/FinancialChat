import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Message, UserMessage } from '../models/message';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  messageReceived = new EventEmitter<Message>();
  connectionEstablished = new EventEmitter<Boolean>();

  baseUrl = environment.baseUrl;

  private connectionIsEstablished = false;
  private _hubConnection!: HubConnection;
  public connectionId : string = '';

  listaMessages = new Subject<UserMessage[]>();

  constructor(private http: HttpClient) {
    this.createConnection();
    this.registerOnServerEvents();
    this.startConnection();
  }

  sendMessage(message: Message) : Promise<any> {
    return this._hubConnection
    .invoke('NewMessage', message, this.connectionId)
  }

  private createConnection() {
    this._hubConnection = new HubConnectionBuilder()
      .withUrl(environment.onlineUrl + 'MessageHub')
      .build();
  }

  private startConnection(): void {
    this._hubConnection
      .start()
      .then(() => {
        this.connectionIsEstablished = true;
        console.log('Hub connection started');
        this.connectionEstablished.emit(true);
      }).then(() => this.getConnectionId())
      .catch(err => {
        console.log('Error while establishing connection, retrying...', err);
        setTimeout( () => { this.startConnection(); }, 5000);
      });
  }

  private registerOnServerEvents(): void {
    this._hubConnection.on('MessageReceived', (data: any) => {
      console.log('return',data);
      this.messageReceived.emit(data);
    });
  }

  public getConnectionId = () => {
    this._hubConnection.invoke('GetConnectionId').then(
      (data) => {
        //console.log(data);
          this.connectionId = data;
        }
    );}


  saveMessage(userMessage : UserMessage){
    this.http
    .post<UserMessage>(this.baseUrl + 'v1/chatmessages', userMessage)
    .subscribe((response) => {
      console.log('create respuesta', response);
    },
    (error) => {
      alert("Error saving message " + userMessage.type + " " + error)
    }
    );
  }

  getMessage(user : string) {
    this.http
    .get<UserMessage[]>(this.baseUrl + 'v1/chatmessages/' + user)
    .subscribe((response) => {
      console.log(response);
      this.listaMessages.next(response);
    });
  }

  obtainMessagesList() {
    return this.listaMessages.asObservable();
  }
}
