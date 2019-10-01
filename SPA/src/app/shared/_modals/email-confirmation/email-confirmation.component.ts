import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { AuthService } from '../../_services/auth.service';
import { ToasterService } from '../../_services/toaster.service';
import { NgForm } from '@angular/forms';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-email-confirmation',
  templateUrl: './email-confirmation.component.html',
  styleUrls: ['./email-confirmation.component.css']
})
export class EmailConfirmationComponent implements OnInit {

  searching = false;

  constructor(
    public dialogRef: MatDialogRef<EmailConfirmationComponent>,
    // @Inject(MAT_DIALOG_DATA) public data: DialogData
    private authService: AuthService,
    private toasterService: ToasterService
    ) { }

  ngOnInit() {
  }

  cancelForget(): void {
    this.dialogRef.close();
  }

  ResendEmailConfirmation(f: NgForm) {
    this.searching = true;
    this.authService.rquestEmailConfirmationLink(f.value.email)

    .pipe(finalize(() => {
      f.resetForm();
      this.dialogRef.close();
      this.searching = false;
    }))

    .subscribe(() => {
      this.toasterService.success(`An email confirmation link has been sent to ${f.value.email}`);
    });
  }

}
