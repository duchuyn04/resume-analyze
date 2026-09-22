const server = Bun.serve({
  port: 4325,
  fetch(req) {
    const file = Bun.file('docs/workflow/diagrams/cv-analysis-erd.html');
    return new Response(file, {
      headers: {
        'Content-Type': 'text/html; charset=utf-8',
        'Cache-Control': 'no-cache'
      }
    });
  }
});

console.log(`[ERD Server Ready] Serving at http://127.0.0.1:4325/`);
