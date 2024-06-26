import { marker as _ } from '@biesbjerg/ngx-translate-extract-marker';
import { RouteDefinitionDto } from '../dto/route.dto';

export const ROUTES_I18N: RouteDefinitionDto<{
  route: string;
}> = {
  APP: {
    TASKS: {
      route: _('routes.app.tasks'),
    },
    NOT_FOUND: {
      route: _('routes.app.not-found'),
    },
  },
  TASKS: {
    CREATE: {
      route: _('routes.tasks.create'),
    },
    EDIT: {
      route: _('routes.tasks.edit'),
    },
  },
};
