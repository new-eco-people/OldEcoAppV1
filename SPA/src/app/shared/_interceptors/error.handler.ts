import { ErrorHandler, Inject } from '@angular/core';
import { ToasterService } from '../_services/toaster.service';

export class AppErrorHandler implements ErrorHandler {

    constructor(@Inject(ToasterService) private toastService: ToasterService) {
    // constructor() {

    }

    handleError(error: string): void {
        console.log(error);
        // const result = error.replace(new RegExp('%n%', 'g'), '\n');
        //console.log(error);
        this.toastService.error(error);
    }

}
