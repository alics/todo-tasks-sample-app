import { Language, LanguageDto } from '../dto/language.dto';

export const DEFAULT_LANGUAGE: Language = 'en';
export const AVAILABLE_LANGUAGES: Language[] = Object.values(LanguageDto);
export const LOCALES: { [key in LanguageDto]: string } = {
  de: 'de_DE',
  en: 'en_US',
};
