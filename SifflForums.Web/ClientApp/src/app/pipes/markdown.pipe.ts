import { Pipe, PipeTransform } from '@angular/core';
import { MarkdownService } from '../util-services/markdown.service';

@Pipe({
  name: 'markdown',
  standalone: false
})
export class MarkdownPipe implements PipeTransform {
  constructor(private markdownService: MarkdownService) { }

  transform(value: string): string {
    return this.markdownService.render(value);
  }
}
