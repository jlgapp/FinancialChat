export class Message {
  clientUniqueId: string = "";
  type: string = "";
  messageIncome: string = "";
  date!: Date;
  user: string = "";
}

export class UserMessage {
  userName: string = "";
  message: string = "";
  type: string = "";
  createdDate! : Date;
}
