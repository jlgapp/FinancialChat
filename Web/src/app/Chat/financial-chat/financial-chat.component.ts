import { Component, NgZone, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
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
  userName: string = '';

//  userSubscription: Subscription | undefined;

  constructor(
    private chatService: ChatService,
    private _ngZone: NgZone,
    private securityService: SecurityService
  ) {
    this.subscribeToEvents();
  }
  sendMessage(): void {
    //console.log('subs',this.userSubscription);
    if (this.txtMessage) {
      this.message = new Message();
      this.message.clientUniqueId = this.uniqueID;
      this.message.type = "sent";
      this.message.messageIncome = this.txtMessage;
      this.message.date = new Date();
      this.message.user = this.securityService.obtenerUsuario().userName
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
    /*this.userSubscription = this.securityService.seguridadCambio.subscribe(
      (status) => {
        this.userName = status;
        this.message.user = status;
        console.log('status user', this.userName);
      }
    );*/
    this.userName = this.securityService.obtenerUsuario().userName;
  }
  closeSession(){
    this.securityService.closeSesion();
  }
}
