import { ErrorHandler, Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class GlobalErrorHandler implements ErrorHandler {

    handleError(error: any): void {
        if (error instanceof HttpErrorResponse) {
            if (error.status === 401) {
                localStorage.clear();
                // window.location.reload();
            }
        }
        console.log(error);
    }

}
