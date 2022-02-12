import { EventEmitter, Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { Message } from '../models/message';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  messageReceived = new EventEmitter<Message>();
  connectionEstablished = new EventEmitter<Boolean>();

  private connectionIsEstablished = false;
  private _hubConnection!: HubConnection;
  public connectionId : string = '';

  constructor() {
    this.createConnection();
    this.registerOnServerEvents();
    this.startConnection();
  }

  sendMessage(message: Message) {
    this._hubConnection.invoke('NewMessage', message, this.connectionId);
  }

  private createConnection() {
    this._hubConnection = new HubConnectionBuilder()
      .withUrl("http://localhost:5080/" + 'MessageHub')
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
}
