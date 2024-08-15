import { ChangeDetectionStrategy, Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card'; // Importing Angular Material's Card module
import { MatIconModule } from '@angular/material/icon'; // Importing Angular Material's Icon module
import { TranslateModule } from '@ngx-translate/core'; // Importing ngx-translate for internationalization support

@Component({
  standalone: true, // This component does not have any dependencies on parent components
  selector: 'app-not-found', // Selector used to identify this component in HTML
  templateUrl: './not-found.component.html', // Template file for component's view
  styleUrl: './not-found.component.scss', // Stylesheet file for component's styles
  changeDetection: ChangeDetectionStrategy.OnPush, // Optimizing change detection strategy for better performance
  imports: [MatCardModule, MatIconModule, TranslateModule], // Importing necessary Angular Material and ngx-translate modules
})
export class NotFoundComponent {}
