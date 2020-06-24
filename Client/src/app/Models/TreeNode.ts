export class TodoItemNode {
    children: TodoItemNode[];
    item: string;
    Title: string;
    itemId: number;
  }
  
  /** Flat to-do item node with expandable and level information */
  export class TodoItemFlatNode {
    item: string;
    itemId: number;
    Title: string;
    level: number;
    expandable: boolean;
  }