export class ApiCommonMessage {
    UserName: string;
    SessionId: string;
    FormName: string;
    ModuleName: string;
    Content: string;

    constructor(userName: string, SessionId: string, content: any) {
        this.UserName = userName;
        this.SessionId = SessionId;
        this.Content = JSON.stringify(content);
    }
}
