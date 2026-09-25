import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { ForumsectionsService } from '../../services/forumsections.service';
import { ForumSection } from '../../models/forums';

// Admin-only: the API rejects non-admins with 403
@Component({
  selector: 'app-forumsection-create',
  standalone: false,
  changeDetection: ChangeDetectionStrategy.Eager,
  templateUrl: './forumsection-create.component.html'
})
export class ForumsectionCreateComponent {
  isSubmitting: boolean;
  errorMessage: string;

  form = new FormGroup({
    name: new FormControl('', [Validators.required, Validators.maxLength(100)]),
    description: new FormControl('', Validators.maxLength(500))
  });

  constructor(private router: Router, private forumSectionsService: ForumsectionsService) { }

  onSubmit(): void {
    if (!this.form.valid) {
      this.errorMessage = 'Name is required (max 100 characters); description is limited to 500 characters.';
      return;
    }

    const input = new ForumSection();
    input.name = this.form.value.name;
    input.description = this.form.value.description;

    this.isSubmitting = true;
    this.errorMessage = null;
    this.forumSectionsService.create(input).subscribe({
      next: (response: ForumSection) => this.router.navigateByUrl(`/submissions/${response.id}`),
      error: (error: HttpErrorResponse) => {
        this.errorMessage = this.getErrorMessage(error);
        this.isSubmitting = false;
      }
    });
  }

  private getErrorMessage(error: HttpErrorResponse): string {
    switch (error.status) {
      case 0: return 'Unable to reach the server.';
      case 401: return 'Please log in again.';
      case 403: return 'Only administrators can create forum sections.';
      case 400: return 'Please check the name and description.';
      default: return 'Could not create the forum section.';
    }
  }
}
