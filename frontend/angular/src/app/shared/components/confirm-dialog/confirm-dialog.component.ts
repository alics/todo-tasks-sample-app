import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { TranslateModule } from '@ngx-translate/core';

/**
 * Interface representing the structure of data expected by the ConfirmDialogComponent.
 */
export interface ConfirmDialogData {
  title: string; // Title of the confirmation dialog
  content: string; // Content/message of the confirmation dialog
}

/**
 * Angular component for displaying a confirmation dialog using Angular Material's MatDialog.
 */
@Component({
  standalone: true, // Indicates that this component does not rely on any parent components
  selector: 'app-confirm-dialog', // Selector used to identify this component in HTML templates
  templateUrl: './confirm-dialog.component.html', // Template file for component's view
  styleUrl: './confirm-dialog.component.scss', // Stylesheet file for component's styles
  changeDetection: ChangeDetectionStrategy.OnPush, // Optimizing change detection strategy for better performance
  imports: [MatDialogModule, MatButtonModule, TranslateModule], // Importing necessary Angular Material and ngx-translate modules
})
export class ConfirmDialogComponent {
  /**
   * Constructor for ConfirmDialogComponent.
   * @param data Injected data containing title and content for the confirmation dialog.
   * @param dialogRef Reference to the MatDialogRef instance for this dialog component.
   */
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: ConfirmDialogData,
    public dialogRef: MatDialogRef<ConfirmDialogComponent>,
  ) {}
}
