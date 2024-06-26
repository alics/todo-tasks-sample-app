// import { animate, state, style, transition, trigger } from '@angular/animations';
import { CdkPortal, PortalModule } from '@angular/cdk/portal';
import { CommonModule } from '@angular/common';
import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  DestroyRef,
  OnDestroy,
  OnInit,
  ViewChild,
  effect,
  inject,
  signal,
} from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatSortModule, Sort } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ActivatedRoute, Params, Router, RouterLink } from '@angular/router';
import { LocalizeRouterModule } from '@gilsdav/ngx-translate-router';
import { TranslateModule } from '@ngx-translate/core';
import { combineLatest, debounceTime } from 'rxjs';
import { ROUTE_DEFINITION } from 'src/app/shared/constants/route-definition.constant';
import { TaskDeleteDirective } from 'src/app/shared/directives/task-delete.directive';
import { TaskStatus } from 'src/app/shared/dto/task-status';
import { TaskDto } from 'src/app/shared/dto/task.tdo';
import { getParamPage } from 'src/app/shared/rxjs/get-param-page';
import { getParamQuery } from 'src/app/shared/rxjs/get-param-query';
import { getParamSort } from 'src/app/shared/rxjs/get-param-sort';
import { ApiService } from 'src/app/shared/services/api.service';

@Component({
  standalone: true,
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrl: './task-list.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [
    FormsModule,
    LocalizeRouterModule,
    MatButtonModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatMenuModule,
    MatPaginatorModule,
    MatSortModule,
    MatTableModule,
    MatTooltipModule,
    RouterLink,
    TranslateModule,
    PortalModule,
    CommonModule,
    TaskDeleteDirective,
  ],
})
export class TaskListComponent implements OnInit, OnDestroy {
  @ViewChild(CdkPortal, { static: true }) public portalContent!: CdkPortal;

  taskStatus = TaskStatus;

  public readonly ROUTE_DEFINITION = ROUTE_DEFINITION;
  public readonly displayedColumns: string[] = ['title', 'deadline', 'status', 'actions'];
  public readonly pageSizeOptions = [5, 10, 25, 100];
  public data = signal<TaskDto[]>([]);
  public totalCount = signal(0);

  public query = signal('');
  public pageSize = signal(5);
  public pageIndex = signal(1);
  public sortBy = signal<keyof TaskDto>('id');
  public sortDirection = signal<'asc' | 'desc'>('asc');

  public expandedElement: TaskDto | null = null;
  private destroyRef = inject(DestroyRef);

  constructor(
    private apiService: ApiService,
    private cdr: ChangeDetectorRef,
    private router: Router,
    private route: ActivatedRoute,
  ) {
    effect(() => {
      if (this.query() != '') {
        this.apiService
          .findByTitle(this.query())
          .pipe(takeUntilDestroyed(this.destroyRef))
          .subscribe((tasks) => {
            this.data.set(tasks.items);
            this.totalCount.set(tasks.totalItems);
          });
      } else {
        this.apiService
          .getAll()
          .pipe(takeUntilDestroyed(this.destroyRef))
          .subscribe((tasks) => {
            this.data.set(tasks.items);
            this.totalCount.set(tasks.totalItems);
          });
      }
    });
  }

  public ngOnDestroy(): void {
    this.portalContent?.detach();
  }

  public ngOnInit(): void {
    combineLatest([
      this.route.queryParamMap.pipe(getParamPage()),
      this.route.queryParamMap.pipe(getParamQuery()),
      this.route.queryParamMap.pipe(getParamSort()),
    ])
      .pipe(debounceTime(1000), takeUntilDestroyed(this.destroyRef))
      .subscribe(([page, query, sort]) => {
        this.query.set(query);
        this.pageIndex.set(page.pageIndex || 1);
        this.pageSize.set(page.pageSize || 5);
        this.sortDirection.set(sort.sortDirection || 'asc');
      });
  }

  public onSortChange(event: Sort): void {
    this.setFiltersToRoute({
      sortBy: event.active,
      sortDirection: event.direction,
      pageIndex: null,
    });
  }

  public onPageChange(event: PageEvent): void {
    let pageIndex = null;
    if (event.pageSize === this.pageSize()) {
      pageIndex = event.pageIndex + 1 > 1 ? event.pageIndex + 1 : null;
    }
    this.setFiltersToRoute({
      pageIndex,
      pageSize: event.pageSize,
    });
  }

  public onQueryChange(event: Event): void {
    const query = (event.target as HTMLInputElement).value;
    this.setFiltersToRoute({
      query: query ? encodeURIComponent(query) : null,
      pageIndex: null,
    });
  }

  public onQueryRemove(): void {
    this.setFiltersToRoute({
      query: null,
      pageIndex: null,
    });
  }

  public onClear(): void {
    this.setFiltersToRoute({
      query: null,
      pageIndex: null,
      pageSize: null,
      sortBy: null,
      sortDirection: null,
    });
  }

  public onDeleted(id: number): void {
    this.data.update((value) => value.filter((i) => i.id !== id));
  }

  public onExpand(event: Event, element: TaskDto): void {
    this.expandedElement = this.expandedElement === element ? null : element;
    this.cdr.markForCheck();
    event.stopPropagation();
  }

  private setFiltersToRoute(queryParams?: Params | null): void {
    this.router.navigate([], {
      queryParams,
      queryParamsHandling: 'merge',
      replaceUrl: true,
    });
  }
}
