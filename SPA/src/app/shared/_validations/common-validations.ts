import { ServiceActionConstant } from 'src/app/core/state-management/services-state/service-action-constant';
import { AbstractControl, ValidationErrors, FormGroup } from '@angular/forms';
import { Injectable } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { debounceTime, distinctUntilChanged, finalize } from 'rxjs/operators';
import { IAppState } from 'src/app/core/state-management/store';
import { NgRedux } from '@angular-redux/store';

@Injectable()
export class CommonValidations {

    constructor(private authService: AuthService, private redux: NgRedux<IAppState>) {
    }

    confirmWith = (controlName: string) => {
        return (c: AbstractControl): ValidationErrors | null => {
            if (
                c.parent === undefined ||
                !c.parent.get(controlName).value ||
                !c.value
            ) { return {invalidConfirmWith: true}; }

            const value: string = c.value;
            const valueToConfirm: string = c.parent.get(controlName).value;

            if (value === valueToConfirm) { return null; }

            return {invalidConfirmWith: true};
        };
    }

    shouldBeUnique(c: AbstractControl): Promise<ValidationErrors | null> {
        return new Promise((resolve, reject) => {

            this.redux.dispatch({ type: ServiceActionConstant.UPDATE_CHECKING_AVALIABILITY_LOADER, state: true });

            c.valueChanges.pipe(
                debounceTime(500), distinctUntilChanged())
                .subscribe(() => {

                this.authService.available(c.value)
                .pipe(finalize(() => this.redux.dispatch({ type: ServiceActionConstant.UPDATE_CHECKING_AVALIABILITY_LOADER, state: false })))
                .subscribe(x => {
                    if (!x) {resolve({ unique: true }); } else {resolve(null); }
                });

            });
        });
    }

    // return (c: AbstractControl): ValidationErrors | null {

    //     // if (!moment(c.value, moment.ISO_8601, true).isValid()) {
    //     // return {invalidDateFormat: true};
    //     // }

    //     // return null;
    // }

    // static notFuture(c: AbstractControl): ValidationErrors | null {

    //     if (moment().isBefore(moment(c.value, moment.ISO_8601, true))) {
    //         return {invalidDateLogic: true};
    //     }

    //     return null;
    // }

    // static compareDates(g: AbstractControl): ValidationErrors | null {
    //     if (
    //         g.parent === undefined ||
    //         !moment(g.parent.get('startDate').value, moment.ISO_8601, true).isValid() ||
    //         !moment(g.parent.get('endDate').value, moment.ISO_8601, true).isValid()
    //         ) { return null; }

    //     const startDate = g.parent.get('startDate');
    //     const endDate = g.parent.get('endDate');

    //     if (moment(endDate.value).isBefore(moment(startDate.value))) {
    //         return {invalidStartEnd: true};
    //      }
    //      return null;
    // }

    // static hackFix(c: AbstractControl): {state, value?} {

    //     if (typeof c.value !== 'object' || !['year', 'month', 'day'].every(p => p in c.value)) {
    //         return { state: false};
    //      }

    //     //  const value = DateValidator.rM(c.value);
    //      const value = c.value;
    //      return {state: true, value};
    // }

    // static rM(c) { // rM = Reduce Month
    //     return { year: c.year, month: (c.month - 1), day: c.day };
    // }

    // static aM(c) { // aM = Add Month
    //     return { year: c.year, month: (c.month + 1), day: c.day };
    // }

    // // static dateDiffs
}
