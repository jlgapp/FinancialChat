import { Component, NgZone } from '@angular/core';
import { ChatService } from './services/chat.service';
import { Message } from './models/message';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'ClientApp';

  constructor(

  ) {
    //this.subscribeToEvents();
  }
  /*sendMessage(): void {
    if (this.txtMessage) {
      this.message = new Message();
      this.message.clientUniqueId = this.uniqueID;
      this.message.type = "sent";
      this.message.messageIncome = this.txtMessage;
      this.message.date = new Date();
      this.message.user = 'Pepe'
      this.messages.push(this.message);
      this.chatService.sendMessage(this.message);
      this.txtMessage = '';

    }
  }
  private subscribeToEvents(): void {

    this.chatService.messageReceived.subscribe((message: Message) => {
      this._ngZone.run(() => {
        if (message.clientUniqueId !== this.uniqueID) {
          message.type = "received";
          this.messages.push(message);
          //console.log('received', message);
        }
        if (message.clientUniqueId === this.uniqueID && message.type === "BotResponse") {
          message.type = "received";
          this.messages.push(message);
          //console.log('received', message);
        }

      });
    });
  }*/
}
