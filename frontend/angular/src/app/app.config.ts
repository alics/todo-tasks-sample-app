import { CommonModule, DatePipe, registerLocaleData } from '@angular/common';
import localeCs from '@angular/common/locales/cs';
import {
  ApplicationConfig,
  ErrorHandler,
  importProvidersFrom,
  provideExperimentalZonelessChangeDetection,
} from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { provideRouter, withViewTransitions } from '@angular/router';

import { MatDialogModule } from '@angular/material/dialog';
import { MatPaginatorIntl } from '@angular/material/paginator';
import { MAT_SNACK_BAR_DEFAULT_OPTIONS, MatSnackBarModule } from '@angular/material/snack-bar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { provideAppVersion } from 'ngx-app-version';
import { provideFixedFooter } from 'ngx-fixed-footer';
import { provideTranslateVersion } from 'ngx-translate-version';
// import { VERSION } from '../environments/version';
import { provideNativeDateAdapter } from '@angular/material/core';
import { routes } from './app.routes';
import { CustomErrorHandlerService } from './shared/services/custom-error-handler.service';

import { MatPaginationIntlService } from './shared/services/mat-paginator-intl.service';

registerLocaleData(localeCs, 'de-DE');

export const appConfig: ApplicationConfig = {
  providers: [
    provideExperimentalZonelessChangeDetection(),
    provideRouter(routes, withViewTransitions()),
    provideAppVersion({
      version: 'VERSION.version',
    }),
    provideTranslateVersion(routes, {
      version: 'VERSION.version',
    }),
    provideFixedFooter({
      containerSelector: '.permanent-main',
      cssAttribute: 'margin',
    }),
    provideNativeDateAdapter(),
    importProvidersFrom(BrowserModule, BrowserAnimationsModule, MatSnackBarModule, MatDialogModule, CommonModule),
    { provide: MAT_SNACK_BAR_DEFAULT_OPTIONS, useValue: { verticalPosition: 'top', horizontalPosition: 'right' } },
    { provide: ErrorHandler, useClass: CustomErrorHandlerService },

    {
      provide: MatPaginatorIntl,
      useClass: MatPaginationIntlService,
    },
    DatePipe,
  ],
};
