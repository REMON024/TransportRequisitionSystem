import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { TodoItemNode } from "src/app/Models/TreeNode";
import { Controller, Action } from "src/app/Models/VmAccessControl";

@Injectable({
    providedIn: 'root'
  })
  export class ChecklistDatabase {
    dataChange = new BehaviorSubject<TodoItemNode[]>([]);
    selectDataChange = new BehaviorSubject<Controller[]>([]);
  
    get data(): TodoItemNode[] { return this.dataChange.value; }
  
    constructor() {
      this.initialize();
    }

    initialize() {
    }

    public initializeData(lstAccess: Controller[]) {
      const data = this.buildTree(lstAccess, 0);
      this.dataChange.next(data);
    }

    /**
     * Build the file structure tree. The `value` is the Json object, or a sub-tree of a Json object.
     * The return value is the list of `TodoItemNode`.
     */
    buildTree(obj: Controller[], level: number): TodoItemNode[] {
      return obj.reduce<TodoItemNode[]>((accumulator, key) => {
        const node = new TodoItemNode();
        node.item = key.ControllerName;
        node.Title = key.Title;
        node.itemId = key.ControllerID
        
        if(key.Actions.length > 0){
          node.children = this.buildChildTree(key.Actions, level + 1);
        }else{
          node.children = null;
        }
        return accumulator.concat(node);
      }, []);
    }
  
  
    buildChildTree(obj: Action[], level: number) : TodoItemNode[] {
      return obj.reduce<TodoItemNode[]>((accumulator, key) => {
        const node = new TodoItemNode();
        node.item = key.ActionName;
        node.Title = key.Title;
        node.itemId = key.ActionID;   
        node.children = null;
  
        return accumulator.concat(node);
      }, []);
    }
  
    /** Add an item to to-do list */
    insertItem(parent: TodoItemNode, name: string) {
      if (parent.children) {
        parent.children.push({item: name} as TodoItemNode);
        this.dataChange.next(this.data);
      }
    }
  
    updateItem(node: TodoItemNode, name: string) {
      node.item = name;
      this.dataChange.next(this.data);
    }
  }