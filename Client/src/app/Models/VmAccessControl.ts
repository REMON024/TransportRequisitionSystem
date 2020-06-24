export class Action {
    ActionID: number;
    ActionName: string;
    Title: string;
}


export class Controller {
    ControllerID: number;
    ControllerName: string;
    Title: string;
    Actions: Action[];
}