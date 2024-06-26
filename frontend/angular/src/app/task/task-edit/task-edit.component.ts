import { CdkPortal, PortalModule } from '@angular/cdk/portal';
import { CommonModule } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { ChangeDetectionStrategy, Component, DestroyRef, OnDestroy, OnInit, ViewChild, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormBuilder, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ActivatedRoute, ParamMap, Router, RouterLink } from '@angular/router';
import { LocalizeRouterModule, LocalizeRouterService } from '@gilsdav/ngx-translate-router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { Observable, delay, first, map, switchMap } from 'rxjs';
import { DataSource } from 'src/app/shared/classes/data-source';
import { ROUTE_DEFINITION } from 'src/app/shared/constants/route-definition.constant';
import { TaskDeleteDirective } from 'src/app/shared/directives/task-delete.directive';
import { TaskStatus } from 'src/app/shared/dto/task-status';
import { TaskDto } from 'src/app/shared/dto/task.tdo';
import { CanComponentDeactivate } from 'src/app/shared/guards/can-deactivate-guard.service';
import { ApiService } from 'src/app/shared/services/api.service';

import { CustomConfirmDialog, CustomConfirmDialogService } from 'src/app/shared/services/custom-confirm-dialog.service';

@Component({
  standalone: true,
  selector: 'app-task-edit',
  templateUrl: './task-edit.component.html',
  styleUrl: './task-edit.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [
    PortalModule,
    MatIconModule,
    MatButtonModule,
    MatCardModule,
    MatInputModule,
    MatTooltipModule,
    CommonModule,
    ReactiveFormsModule,
    TranslateModule,
    RouterLink,
    MatSelectModule,
    LocalizeRouterModule,
    TaskDeleteDirective,
    MatDatepickerModule,
  ],
})
export class TaskEditComponent implements OnInit, OnDestroy, CanComponentDeactivate {
  @ViewChild(CdkPortal, { static: true }) public portalContent!: CdkPortal;
  private destroyRef = inject(DestroyRef);

  taskStatus = TaskStatus;
  taskStatusOptions = Object.keys(TaskStatus).slice(3, 6);

  private defaultDto: TaskDto = { title: '' };
  public dataSource = new DataSource<TaskDto>(this.defaultDto);
  public readonly ROUTE_DEFINITION = ROUTE_DEFINITION;

  public form = this.fb.group({
    title: new FormControl<string>('', { nonNullable: true, validators: [Validators.required, Validators.min(3)] }),
    status: new FormControl<TaskStatus>(0, { nonNullable: true }),
    deadline: new FormControl<Date>(new Date(), { nonNullable: true, validators: [Validators.required] }),
  });

  constructor(
    private apiService: ApiService,
    private route: ActivatedRoute,
    private translate: TranslateService,
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private lr: LocalizeRouterService,
    private confirm: CustomConfirmDialogService,
    private router: Router,
  ) {}

  public canDeactivate(): boolean | Observable<boolean> {
    return this.form.pristine || this.confirm.openCustomConfirmDialog(CustomConfirmDialog.UnsavedWork);
  }

  public ngOnInit(): void {
    this.route.paramMap
      .pipe(
        delay(500),
        getParamIdCustom(),
        switchMap((params) => {
          const id = params!;
          console.log(id);
          return this.apiService.get(id); // Ensuring id is treated as string
        }),
        takeUntilDestroyed(this.destroyRef),
      )
      .subscribe({
        next: (task) => {
          this.dataSource.setData(task);
          this.form.patchValue(task);
        },
        error: (err) => {
          if (err instanceof HttpErrorResponse && err.status >= 400 && err.status < 500) {
            this.dataSource.setData(this.defaultDto);
          } else {
            const error = this.translate.instant('error.unexpected-exception');
            this.dataSource.setError(error);
          }
        },
      });
  }

  public ngOnDestroy(): void {
    this.portalContent?.detach();
  }

  public onSubmit(): void {
    this.apiService
      .update(this.dataSource.data().id, this.form.value as TaskDto)
      .pipe(first())
      .subscribe({
        next: (task) => {
          this.dataSource.setData(task as TaskDto);
          this.form.reset(task as TaskDto);
          this.snackBar.open(this.translate.instant('response.update.success'), this.translate.instant('uni.close'));
          const translatedRoute = this.lr.translateRoute(`/`);
          this.router.navigate([translatedRoute]);
        },
        error: () => {
          this.snackBar.open(this.translate.instant('response.update.failed'), this.translate.instant('uni.close'));
        },
      });
  }

  public onReset(event: Event): void {
    event.preventDefault();
    this.form.reset(this.dataSource.data());
  }

  public onDeleted(): void {
    const translatedRoute = this.lr.translateRoute(`/`);
    this.router.navigate([translatedRoute]);
  }
}

export function getParamIdCustom() {
  return map((params: ParamMap) => {
    const id = params.get('id');
    return id ? id : '';
  });
}
