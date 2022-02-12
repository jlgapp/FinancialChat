import { Component, NgZone, OnInit } from '@angular/core';
import { Message } from 'src/app/models/message';
import { ChatService } from 'src/app/services/chat.service';
import { SecurityService } from 'src/app/services/Security/security.service';

@Component({
  selector: 'app-financial-chat',
  templateUrl: './financial-chat.component.html',
  styleUrls: ['./financial-chat.component.scss']
})
export class FinancialChatComponent implements OnInit {

  title = 'ClientApp';
  txtMessage: string = '';
  uniqueID: string = new Date().getTime().toString();
  messages = new Array<Message>();
  message = new Message();

  constructor(
    private chatService: ChatService,
    private _ngZone: NgZone,
    private securityService: SecurityService
  ) {
    this.subscribeToEvents();
  }
  sendMessage(): void {
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
  }

  ngOnInit(): void {
  }
  closeSession(){
    this.securityService.closeSesion();
  }
}
