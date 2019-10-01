import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { NgForm } from '@angular/forms';
import { AuthService } from '../../_services/auth.service';
import { ToasterService } from '../../_services/toaster.service';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-forgotten-password',
  templateUrl: './forgotten-password.component.html',
  styleUrls: ['./forgotten-password.component.css']
})
export class ForgottenPasswordComponent implements OnInit {

  searching = false;

  constructor(
    public dialogRef: MatDialogRef<ForgottenPasswordComponent>,
    // @Inject(MAT_DIALOG_DATA) public data: DialogData
    private authService: AuthService,
    private toasterService: ToasterService
  ) { }

  ngOnInit() {
  }

  cancelForget(): void {
    this.dialogRef.close();
  }

  sendResetPasswordLink(f: NgForm) {
    this.searching = true;
    this.authService.sendResetPasswordLink(f.value.email)

    .pipe(finalize(() => {
      f.resetForm();
      this.dialogRef.close();
      this.searching = false;
    }))

    .subscribe((result: any) => {
      this.toasterService.success(`A password reset link has been sent to ${result.email}`);
    });
  }

}
