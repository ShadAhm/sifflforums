import { DateAgoPipe } from './date-ago.pipe';

describe('DateAgoPipe', () => {
  const pipe = new DateAgoPipe();

  it('shows "Just now" for recent dates', () => {
    expect(pipe.transform(new Date())).toBe('Just now');
  });

  it('uses the singular form for a single unit', () => {
    const oneHourAgo = new Date(Date.now() - 3600 * 1000);
    expect(pipe.transform(oneHourAgo)).toBe('1 hour ago');
  });

  it('uses the plural form for multiple units', () => {
    const threeDaysAgo = new Date(Date.now() - 3 * 86400 * 1000);
    expect(pipe.transform(threeDaysAgo)).toBe('3 days ago');
  });
});
