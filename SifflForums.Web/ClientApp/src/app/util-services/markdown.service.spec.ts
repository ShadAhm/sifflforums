import { MarkdownService } from './markdown.service';

describe('MarkdownService', () => {
  const service = new MarkdownService();

  it('renders bold and italic', () => {
    expect(service.render('**b** *i* _j_')).toBe('<p><strong>b</strong> <em>i</em> <em>j</em></p>\n');
  });

  it('renders strikethrough', () => {
    expect(service.render('~~s~~')).toContain('<s>s</s>');
  });

  it('escapes raw HTML', () => {
    const html = service.render('<b>raw</b> <script>alert(1)</script>');
    expect(html).not.toContain('<b>');
    expect(html).not.toContain('<script>');
    expect(html).toContain('&lt;b&gt;raw&lt;/b&gt;');
  });

  it('does not render javascript: links', () => {
    expect(service.render('[x](javascript:alert(1))')).not.toContain('<a');
  });

  it('opens links in a new tab without passing the opener', () => {
    const html = service.render('[x](https://example.com)');
    expect(html).toContain('href="https://example.com"');
    expect(html).toContain('target="_blank"');
    expect(html).toContain('rel="noopener noreferrer nofollow"');
  });

  it('turns single newlines into line breaks', () => {
    expect(service.render('a\nb')).toContain('a<br>\nb');
  });

  it('renders multi-line blockquotes', () => {
    expect(service.render('> one\n> two')).toBe('<blockquote>\n<p>one<br>\ntwo</p>\n</blockquote>\n');
  });

  it('does not render headings or images', () => {
    expect(service.render('# heading')).not.toContain('<h1');
    expect(service.render('![alt](https://example.com/x.png)')).not.toContain('<img');
  });

  it('handles null input', () => {
    expect(service.render(null)).toBe('');
  });

  it('strips syntax for plain text', () => {
    expect(service.toPlainText('**hi** _there_\n\n- item')).toBe('hi there item');
  });
});
