
import { AccessRightService } from 'src/app/api-services/access-right.service';

import { VMAccessRightForDropDwon } from 'src/app/Models/VMAccessRightForDropDwon';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FlatTreeControl } from '@angular/cdk/tree';
import { MatTreeFlattener, MatTreeFlatDataSource } from '@angular/material';
import { SelectionModel } from '@angular/cdk/collections';
import { Controller, Action } from 'src/app/Models/VmAccessControl';
import { TodoItemFlatNode, TodoItemNode } from 'src/app/Models/TreeNode';
import { ChecklistDatabase } from '../access.service';
import { AccessRight } from 'src/app/Models/AccessRight';
import { NotificationService } from 'src/app/services/notification.service';


@Component({
  selector: 'app-access-right-list',
  templateUrl: './access-right-list.component.html',
  styleUrls: ['./access-right-list.component.scss']
})

export class AccessRightListComponent implements OnInit {

  @Input() public IsUpdate: boolean = true;
  @Input() public ButtonText: string = 'Create Access Right';
  @Input() public CancelButtonUrl: string = '/';
  @Output() public AfterSubmit: EventEmitter<AccessRight> = new EventEmitter();
  @Input() public RoleName: string = '';

  flatNodeMap = new Map<TodoItemFlatNode, TodoItemNode>();

  /** Map from nested node to flattened node. This helps us to keep the same object for selection */
  nestedNodeMap = new Map<TodoItemNode, TodoItemFlatNode>();

  /** A selected parent node to be inserted */
  selectedParent: TodoItemFlatNode | null = null;

  /** The new item's name */
  newItemName = '';

  treeControl: FlatTreeControl<TodoItemFlatNode>;

  treeFlattener: MatTreeFlattener<TodoItemNode, TodoItemFlatNode>;

  dataSource: MatTreeFlatDataSource<TodoItemNode, TodoItemFlatNode>;

  /** The selection for checklist */
  checklistSelection = null;

  public lstAccessRight:VMAccessRightForDropDwon[]=[];
  
  constructor(
    private accessRightService: AccessRightService,
    private database: ChecklistDatabase,
    private notificationService: NotificationService
  ) {
    this.treeFlattener = new MatTreeFlattener(this.transformer, this.getLevel,
      this.isExpandable, this.getChildren);
    this.treeControl = new FlatTreeControl<TodoItemFlatNode>(this.getLevel, this.isExpandable);
    this.dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

    database.dataChange.subscribe(data => {
      this.dataSource.data = data;
    });

    if (this.IsUpdate) {
      database.selectDataChange.subscribe(response => {
        this.checklistSelection = new SelectionModel<TodoItemFlatNode>(true, this.GetSelectedNode(response));
      });
    } else {
      this.checklistSelection = new SelectionModel<TodoItemFlatNode>(true);
    }
    
  }

  ngOnInit() {
   this.getAllAccessRight()
   

    
  }

  getAllAccessRight(){

    this.accessRightService.GetAllRoleName().subscribe((res:any) => {
      this.lstAccessRight=res;
      console.log(this.lstAccessRight);

    })
  }

  getLevel = (node: TodoItemFlatNode) => node.level;

  isExpandable = (node: TodoItemFlatNode) => node.expandable;

  getChildren = (node: TodoItemNode): TodoItemNode[] => node.children;

  hasChild = (_: number, _nodeData: TodoItemFlatNode) => _nodeData.expandable;

  hasNoContent = (_: number, _nodeData: TodoItemFlatNode) => _nodeData.item === '';

  /**
   * Transformer to convert nested node to flat node. Record the nodes in maps for later use.
   */
  transformer = (node: TodoItemNode, level: number) => {
    const existingNode = this.nestedNodeMap.get(node);
    const flatNode = existingNode && existingNode.item === node.item
      ? existingNode
      : new TodoItemFlatNode();
    flatNode.item = node.item;
    flatNode.level = level;
    flatNode.Title = node.Title;
    flatNode.expandable = !!node.children;
    this.flatNodeMap.set(flatNode, node);
    this.nestedNodeMap.set(node, flatNode);
    return flatNode;
  }

  /** Whether all the descendants of the node are selected. */
  descendantsAllSelected(node: TodoItemFlatNode): boolean {
    const descendants = this.treeControl.getDescendants(node);
    const descAllSelected = descendants.every(child =>
      this.checklistSelection.isSelected(child)
    );
    return descAllSelected;
  }

  /** Whether part of the descendants are selected */
  descendantsPartiallySelected(node: TodoItemFlatNode): boolean {
    const descendants = this.treeControl.getDescendants(node);
    const result = descendants.some(child => this.checklistSelection.isSelected(child));
    return result && !this.descendantsAllSelected(node);
  }

  /** Toggle the to-do item selection. Select/deselect all the descendants node */
  todoItemSelectionToggle(node: TodoItemFlatNode): void {
    this.checklistSelection.toggle(node);
    const descendants = this.treeControl.getDescendants(node);
    this.checklistSelection.isSelected(node)
      ? this.checklistSelection.select(...descendants)
      : this.checklistSelection.deselect(...descendants);

    // Force update for the parent
    descendants.every(child =>
      this.checklistSelection.isSelected(child)
    );
    this.checkAllParentsSelection(node);
  }

  /** Toggle a leaf to-do item selection. Check all the parents to see if they changed */
  todoLeafItemSelectionToggle(node: TodoItemFlatNode): void {
    this.checklistSelection.toggle(node);
    console.log(node);
    this.checkAllParentsSelection(node);
  }

  /* Checks all the parents when a leaf node is selected/unselected */
  checkAllParentsSelection(node: TodoItemFlatNode): void {
    let parent: TodoItemFlatNode | null = this.getParentNode(node);
    while (parent) {
      this.checkRootNodeSelection(parent);
      parent = this.getParentNode(parent);
    }
  }

  /** Check root node checked state and change it accordingly */
  checkRootNodeSelection(node: TodoItemFlatNode): void {
    const nodeSelected = this.checklistSelection.isSelected(node);
    const descendants = this.treeControl.getDescendants(node);
    const descAllSelected = descendants.every(child =>
      this.checklistSelection.isSelected(child)
    );
    if (nodeSelected && !descAllSelected) {
      this.checklistSelection.deselect(node);
    } else if (!nodeSelected && descAllSelected) {
      this.checklistSelection.select(node);
    }
  }

  /* Get the parent node of a node */
  getParentNode(node: TodoItemFlatNode): TodoItemFlatNode | null {
    const currentLevel = this.getLevel(node);

    if (currentLevel < 1) {
      return null;
    }
    const startIndex = this.treeControl.dataNodes.indexOf(node) - 1;

    for (let i = startIndex; i >= 0; i--) {
      const currentNode = this.treeControl.dataNodes[i];

      if (this.getLevel(currentNode) < currentLevel) {
        return currentNode;
      }
    }
    return null;
  }

  private ChangeNodeToAction(node: TodoItemFlatNode): Action {
    const action = new Action();
    action.ActionName = node.item;
    action.Title = node.Title;
    action.ActionID = node.itemId
    return action;
  }

  private ChangeNodeToController(node: TodoItemFlatNode): Controller {
    const controller = new Controller();
    controller.ControllerName = node.item;
    controller.Title = node.Title;
    controller.ControllerID = node.itemId;
    controller.Actions = [];
    console.log("node assign", node);
    return controller;
  }

  private FindParentControllerIndex(lstController: Controller[], controllerName: string): number {
    return lstController.findIndex(item => {
      return item.ControllerName === controllerName;
    })
  }

  private FindControllerIndex(lstController: Controller[], controllerName: string): boolean {
    const index = lstController.findIndex(item => {
      return item.ControllerName === controllerName;
    });

    return index === -1 ? false : true;
  }

  private FindControllerActionIndex(lstController: Controller[], actionName: string): boolean {
    const index = lstController.findIndex(item => {
      return this.FindAction(item.Actions, actionName);
    });

    return index === -1 ? false : true;
  }

  private FindAction(lstAction: Action[], actionName): boolean {
    const index = lstAction.findIndex(action => {
      return action.ActionName === actionName;
    });

    return index === -1 ? false : true;
  }

  private changeNodeToAccessRight(nodes: TodoItemFlatNode[]): Controller[] {
    return nodes.reduce<Controller[]>((accumulator, todoItemNode) => {
      console.log("change !!", accumulator, todoItemNode);
      let controller = new Controller();
      let parentNode = this.getParentNode(todoItemNode);

      if (parentNode == null) {
        controller = this.ChangeNodeToController(todoItemNode);
        const index = this.FindParentControllerIndex(accumulator, controller.ControllerName);

        if (index === -1) {
          return accumulator.concat(controller);
        } else {
          return accumulator;
        }
      } else {
        const index = this.FindParentControllerIndex(accumulator, parentNode.item);
        if (index !== -1) {
          const childAction = this.ChangeNodeToAction(todoItemNode);
          accumulator[index].Actions.push(childAction);

          return accumulator;
        } else {
          const parentController = this.ChangeNodeToController(parentNode);
          parentController.Actions.push(this.ChangeNodeToAction(todoItemNode));

          return accumulator.concat(parentController);

        }
      }
    }, []);
  }

  public ButtonClick() {
    if (this.RoleName !== '') {
      const lstController = this.changeNodeToAccessRight(this.checklistSelection.selected);

      if (lstController.length > 0) {
        const accessRight = new AccessRight();
        accessRight.RoleName = this.RoleName;
        accessRight.RightLists = lstController;
        this.AfterSubmit.next(accessRight);
      } else {
        this.notificationService.warn('Please select at least one access control');
      }
    }else {
      this.notificationService.warn('Role name required');
    }
  }

  private GetSelectedNode(lstController: Controller[]): TodoItemFlatNode[] {
    return this.treeControl.dataNodes.filter(node => {
      console.log("node def", node);
      const result = this.FindControllerIndex(lstController, node.item);
      if (result) {
        return true;
      } else {
        const actionResult = this.FindControllerActionIndex(lstController, node.item);
        if (actionResult) {
          return true;
        }
        return false;
      }
    })
  }
  

}
