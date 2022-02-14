import { Component, NgZone, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Message, UserMessage } from 'src/app/models/message';
import { ChatService } from 'src/app/services/chat.service';
import { SecurityService } from 'src/app/services/Security/security.service';

import { map, mapTo, pluck } from "rxjs/operators";

@Component({
  selector: 'app-financial-chat',
  templateUrl: './financial-chat.component.html',
  styleUrls: ['./financial-chat.component.scss']
})
export class FinancialChatComponent implements OnInit, OnDestroy {

  title = 'ClientApp';
  txtMessage: string = '';
  uniqueID: string = new Date().getTime().toString();
  messages = new Array<Message>();
  message = new Message();
  userName: string = '';

  userMessagesList: UserMessage[] | null = [];

  //  userSubscription: Subscription | undefined;

  //  userSubscription: Subscription | undefined;
  messagesSubsription!: Subscription;

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
      this.chatService.sendMessage(this.message)
        .then(() => {
          if (!this.message.messageIncome.startsWith('/')) {
            let userMessage = new UserMessage();
            userMessage.message = this.txtMessage;
            userMessage.type = "sent";
            userMessage.userName = this.securityService.obtenerUsuario().userName;
            this.chatService.saveMessage(userMessage);
            this.txtMessage = '';
          }
        })
        .catch(err => {
          alert('Error :' + err);
          this.txtMessage = '';
        });
    }

  }
  private subscribeToEvents(): void {

    this.chatService.messageReceived.subscribe((message: Message) => {
      this._ngZone.run(() => {
        if (message.clientUniqueId !== this.uniqueID) {
          message.type = "received";
          this.messages.push(message);
          //console.log('received', message);
          let userMessage = new UserMessage();
          userMessage.message = message.messageIncome;
          userMessage.type = "received";
          userMessage.userName = this.securityService.obtenerUsuario().userName;

          this.chatService.saveMessage(userMessage);
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

    this.chatService.getMessage(this.userName);
    this.messagesSubsription = this.chatService
      .obtainMessagesList()
      .subscribe((mess: any[]) => {
        this.userMessagesList = mess;
        this.userMessagesList.forEach(ms => {
          var temp = new Message();
          temp = { clientUniqueId: "", messageIncome: ms.message, type: ms.type, date: ms.createdDate, user: ms.userName };
          this.messages.push(temp);
        });
      });
  }
  ngOnDestroy() {
    this.messagesSubsription.unsubscribe();
  }
  closeSession() {
    this.securityService.closeSesion();
  }
}
