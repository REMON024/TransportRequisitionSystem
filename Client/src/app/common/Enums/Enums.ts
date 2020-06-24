export enum ErrorCode {
    INTERNAL_SERVER_ERROR = 500,
    BAD_REQUEST = 400,
    NOT_FOUND = 404,
    UNAUTHENTICATE = 401,
    FORBIDDEN = 403,
    TOKEN_EXPIRED = 402,
    SESSION_EXPIRED = 419,
    MISSING_TOKEN = 417,
    INVALID_TOKEN_FORMAT = 406
}


export enum UserStatus {
    Active = 1,
    Inactive = 2,
    Block = 4,
    // Delete = 9
}

export enum AccessRights {
    Admin = 1,
    User = 2
}

export enum Action {
    Insert = 1,
    Update,
    Delete,
    View,
    Other
}
export enum RequisitionStatus {
    Submitted = 1,
    Approved = 2,
    Rejected = 3,
    Closed = 4,
    Cancel=5
}
