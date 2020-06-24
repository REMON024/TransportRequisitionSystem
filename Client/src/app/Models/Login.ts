export class VmLogin {
    public Username: string;
    public Password: string;

    constructor(username : string, password : string) {
        this.Username = username;
        this.Password = password;
    }
}

export class ChangePassword {
    public OldPassword: string;
    public NewPassword: string;
    public ConfirmPassword: string;
}

export class ResetPassword {
    public Username: string;
    public NewPassword: string;
}
