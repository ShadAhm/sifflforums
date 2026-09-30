import { Injectable } from '@angular/core';
import MarkdownIt from 'markdown-it';

// Renders user-written Markdown (comments, submissions) to HTML.
// Raw HTML is escaped and unsafe link protocols are rejected by markdown-it;
// Angular's [innerHTML] sanitizer is a second layer on top of that.
@Injectable({
  providedIn: 'root'
})
export class MarkdownService {
  private readonly md = new MarkdownIt({ html: false, linkify: true, breaks: true, typographer: false })
    .disable(['heading', 'lheading', 'image', 'table', 'hr']);

  constructor() {

    const defaultLinkOpen = this.md.renderer.rules.link_open
      ?? ((tokens, idx, options, env, self) => self.renderToken(tokens, idx, options));

    this.md.renderer.rules.link_open = (tokens, idx, options, env, self) => {
      tokens[idx].attrSet('target', '_blank');
      tokens[idx].attrSet('rel', 'noopener noreferrer nofollow');
      return defaultLinkOpen(tokens, idx, options, env, self);
    };
  }

  render(text: string): string {
    return this.md.render(text ?? '');
  }

  toPlainText(text: string): string {
    const html = this.render(text);
    const doc = new DOMParser().parseFromString(html, 'text/html');
    return (doc.body.textContent ?? '').replace(/\s+/g, ' ').trim();
  }
}
