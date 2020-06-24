export interface SessionLog {
    Id: number;
    SessionId: string;
    UserId: string;
    LoginDate: Date;
    LogoutDate: Date | null;
    IsLoggedIn: boolean;
    Ipaddress: string;
    Device: string;
    OS: string;
    Browser: string;
}