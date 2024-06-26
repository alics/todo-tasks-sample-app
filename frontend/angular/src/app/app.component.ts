import { Portal, PortalModule } from '@angular/cdk/portal';
import { AsyncPipe } from '@angular/common';
import { ChangeDetectionStrategy, Component, DestroyRef, OnInit, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterLink, RouterOutlet } from '@angular/router';
import { LocalizeRouterModule } from '@gilsdav/ngx-translate-router';
import { TranslateModule } from '@ngx-translate/core';

import { NgxFixedFooterDirective } from 'ngx-fixed-footer';
import { Observable } from 'rxjs';
import { DEFAULT_LANGUAGE } from './shared/constants/language.constant';

import { LanguageService } from './shared/services/language.service';

@Component({
  standalone: true,
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [
    AsyncPipe,
    RouterOutlet,
    RouterLink,
    MatIconModule,
    MatButtonModule,
    TranslateModule,
    MatToolbarModule,
    LocalizeRouterModule,
    PortalModule,
    NgxFixedFooterDirective,
  ],
})
export class AppComponent implements OnInit {
  private destroyRef = inject(DestroyRef);
  public endYear = 2024;
  public breadcrumbsPortal$!: Observable<Portal<unknown>>;
  public lang = DEFAULT_LANGUAGE;

  constructor(private language: LanguageService) {
    this.language.initLang();
  }

  public ngOnInit(): void {
    this.language.language$.pipe(takeUntilDestroyed(this.destroyRef)).subscribe((language) => (this.lang = language));
  }

  public toggleLanguage() {
    this.language.setLang(this.lang === 'en' ? 'de' : 'en');
  }
}
