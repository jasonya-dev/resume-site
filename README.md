# Jason Nya — Resume Site

A responsive static resume page (HTML/CSS/JS) with a small C# backend that receives
submissions from the contact form.

```
resume-site/
├── frontend/
│   ├── index.html
│   ├── css/style.css
│   └── js/script.js
└── backend/
    └── ContactApi/
        ├── ContactApi.csproj
        └── Program.cs
```

## Requirements

- [VS Code](https://code.visualstudio.com/)
- [.NET 8 SDK](https://dotnet.microsoft.com/download) (for the backend)
- The **Live Server** VS Code extension (easiest way to preview the frontend), or any
  static file server

## Run the backend (C#)

```bash
cd backend/ContactApi
dotnet run
```

The terminal will print the URL it's listening on, usually something like
`http://localhost:5000`. If it's different, update `CONTACT_API_URL` at the top of
`frontend/js/script.js` to match.

Submitted messages are appended to `backend/ContactApi/contact-submissions.log` so you
can see them land while testing. Swap that line in `Program.cs` for an email send
(SendGrid, SMTP, etc.) once you're ready to make it real.

## Run the frontend (HTML/CSS/JS)

Right-click `frontend/index.html` in VS Code and choose **Open with Live Server**, or
run:

```bash
cd frontend
python3 -m http.server 5500
```

then open `http://localhost:5500` in your browser.

## Editing your info

Everything on the page — name, experience, skills, education, contact details — lives
directly in `frontend/index.html`, so you can update it there any time without touching
the CSS or backend.

## Mobile / desktop

The layout is responsive by default: it's a single column on phones and widens to a
two-column layout for skills, education, and the contact section on larger screens
(see the `@media` rule near the bottom of `style.css`). Test it in VS Code with the
browser's device toolbar, or just resize the window.

## Deploying later

- **Frontend**: any static host works (GitHub Pages, Netlify, Vercel, Azure Static
  Web Apps).
- **Backend**: any host that runs ASP.NET Core (Azure App Service, Render, a VPS).
  Once deployed, update `CONTACT_API_URL` in `script.js` to the live backend URL.
