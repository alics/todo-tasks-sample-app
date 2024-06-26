import { Routes } from '@angular/router';
import { ROUTE_DEFINITION } from './shared/constants/route-definition.constant';
import { CanDeactivateGuardService } from './shared/guards/can-deactivate-guard.service';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./task/task-list/task-list.component').then((m) => m.TaskListComponent),
  },
  {
    path: ROUTE_DEFINITION.TASKS.CREATE,
    canDeactivate: [CanDeactivateGuardService],
    loadComponent: () => import('./task/task-create/task-create.component').then((m) => m.TaskCreateComponent),
  },
  {
    path: `:id/${ROUTE_DEFINITION.TASKS.EDIT}`,
    canDeactivate: [CanDeactivateGuardService],
    loadComponent: () => import('./task/task-edit/task-edit.component').then((m) => m.TaskEditComponent),
  },
  {
    path: '**',
    loadComponent: () => import('./not-found/not-found.component').then((m) => m.NotFoundComponent),
  },
];
