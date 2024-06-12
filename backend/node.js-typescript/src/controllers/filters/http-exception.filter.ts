import {
    ExceptionFilter,
    Catch,
    ArgumentsHost,
    HttpException,
    HttpStatus,
    Logger,
} from '@nestjs/common';
import { Request, Response } from 'express';
import { ApplicationException } from 'src/infrastructure/core/exceptions/application.execption';

/**
 * Global HTTP exception filter.
 */
@Catch()
export class HttpExceptionFilter implements ExceptionFilter {
    private readonly logger = new Logger(HttpExceptionFilter.name);

    /**
     * Handles exceptions and sends appropriate HTTP responses.
     * 
     * @param exception - The exception to be caught.
     * @param host - The arguments host.
     */
    catch(exception: unknown, host: ArgumentsHost) {
        const ctx = host.switchToHttp();
        const response = ctx.getResponse<Response>();
        const request = ctx.getRequest<Request>();
        const status = exception instanceof ApplicationException
            ? exception.statusCode
            : HttpStatus.INTERNAL_SERVER_ERROR;

        const exceptionResponse = exception instanceof HttpException
            ? exception.getResponse()
            : {
                success: false,
                errorMessage: (exception as Error).message,
                innerMessage: exception instanceof ApplicationException
                    ? exception.message
                    : null,
            };

        this.logger.error(
            (exception as Error).message,
            (exception as Error).stack,
        );

        response.status(status).json(exceptionResponse);
    }
}
