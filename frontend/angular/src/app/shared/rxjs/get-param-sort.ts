import { ParamMap } from '@angular/router';
import { OperatorFunction, map } from 'rxjs';
import { TaskDto } from '../dto/task.tdo';

interface GetParamSort {
  sortBy: keyof TaskDto;
  sortDirection: 'asc' | 'desc';
}

export const getParamSort = (): OperatorFunction<ParamMap, GetParamSort> => {
  return (input$) => {
    return input$.pipe(
      map((params) => {
        return {
          sortBy: (params.get('sortBy') || 'id') as keyof TaskDto,
          sortDirection: (params.get('sortDirection') || 'asc') as 'asc' | 'desc',
        };
      }),
    );
  };
};
