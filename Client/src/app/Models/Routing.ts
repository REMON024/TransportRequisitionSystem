export class Routing implements ChildRouting {
    Title: string;
    Url: string;
    Icon: string;
    AccessRights: string[];
    Child: ChildRouting[];
}

export class ChildRouting {
    Title: string;
    Url: string;
    Icon: string;
    AccessRights: string[];
}