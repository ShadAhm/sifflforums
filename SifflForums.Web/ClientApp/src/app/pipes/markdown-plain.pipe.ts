import { Pipe, PipeTransform } from '@angular/core';
import { MarkdownService } from '../util-services/markdown.service';

@Pipe({
  name: 'markdownPlain',
  standalone: false
})
export class MarkdownPlainPipe implements PipeTransform {
  constructor(private markdownService: MarkdownService) { }

  transform(value: string): string {
    return this.markdownService.toPlainText(value);
  }
}
