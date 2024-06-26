import { Directive, HostListener, input, output } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TranslateService } from '@ngx-translate/core';
import { filter, first, switchMap } from 'rxjs';
import { ApiService } from '../services/api.service';
import { CustomConfirmDialog, CustomConfirmDialogService } from '../services/custom-confirm-dialog.service';

@Directive({
  selector: '[appTaskDelete]',
  standalone: true,
})
export class TaskDeleteDirective {
  public id = input.required<string>({ alias: 'appTaskDelete' });
  public deleted = output<string>();

  constructor(
    private apiService: ApiService,
    private confirm: CustomConfirmDialogService,
    private snackBar: MatSnackBar,
    private translate: TranslateService,
  ) {}

  @HostListener('click')
  public onClick(): void {
    this.confirm
      .openCustomConfirmDialog(CustomConfirmDialog.Delete)
      .pipe(
        first(),
        filter((res) => !!res),
        switchMap(() => this.apiService.remove(this.id())),
      )
      .subscribe({
        next: () => {
          this.deleted.emit(this.id());
          this.snackBar.open(this.translate.instant('response.delete.success'), this.translate.instant('uni.close'));
        },
        error: () => {
          this.snackBar.open(this.translate.instant('response.delete.failed'), this.translate.instant('uni.close'));
        },
      });
  }
}
