/**
 * Directive for handling task deletion functionality on click event.
 * This directive opens a confirmation dialog, deletes the task if confirmed,
 * and displays appropriate success or failure messages using MatSnackBar.
 */
import { Directive, HostListener, input, output } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TranslateService } from '@ngx-translate/core';
import { filter, first, switchMap } from 'rxjs/operators';
import { ApiService } from '../services/api.service';
import { CustomConfirmDialog, CustomConfirmDialogService } from '../services/custom-confirm-dialog.service';

/**
 * Directive to handle task deletion functionality on click event.
 */
@Directive({
  selector: '[appTaskDelete]', // Selector for using this directive in HTML templates
  standalone: true, // Indicates that this directive does not have any dependencies on parent components
})
export class TaskDeleteDirective {
  /**
   * Input property to receive the ID of the task to be deleted.
   */
  public id = input.required<string>({ alias: 'appTaskDelete' });

  /**
   * Output property to emit an event when a task is successfully deleted.
   */
  public deleted = output<string>();

  /**
   * Constructor for TaskDeleteDirective.
   * @param apiService Instance of ApiService for making API requests.
   * @param confirm Instance of CustomConfirmDialogService for displaying custom confirmation dialogs.
   * @param snackBar Instance of MatSnackBar for displaying feedback messages.
   * @param translate Instance of TranslateService for internationalization support.
   */
  constructor(
    private apiService: ApiService,
    private confirm: CustomConfirmDialogService,
    private snackBar: MatSnackBar,
    private translate: TranslateService,
  ) {}

  /**
   * Host listener for handling click events on the element where the directive is applied.
   * Opens a confirmation dialog, deletes the task if confirmed, and displays appropriate messages.
   */
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
