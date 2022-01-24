import {Component} from '@angular/core';
import {LangChangeEvent, TranslateService} from "@ngx-translate/core";
import {DateAdapter} from "@angular/material/core";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';

  constructor(translate: TranslateService, private adapter: DateAdapter<any>) {
    // this language will be used as a fallback when a translation isn't found in the current language
    translate.setDefaultLang('en');

    // the lang to use, if the lang isn't available, it will use the current loader to get them
    translate.use('es');

    translate.addLangs(['en', 'es']);

    translate.onLangChange
      .subscribe(({lang}: LangChangeEvent) => this.adapter.setLocale(lang))
  }
}
