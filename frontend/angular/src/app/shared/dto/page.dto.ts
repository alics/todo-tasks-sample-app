export type PageAction = 'list' | 'create' | 'edit' | 'detail';

export type PageSection = 'tasks' | 'not-found';

export interface PageOptions {
  description: string;
  section: PageSection[];
  action?: PageAction[];
  identifier?: 'id';
}
