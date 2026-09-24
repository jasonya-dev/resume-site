var builder = WebApplication.CreateBuilder(args);

// Allow the static frontend (served from a different origin/port, e.g. Live Server or file://)
// to call this API from the browser.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors();

app.MapGet("/", () => "ContactApi is running.");

app.MapPost("/api/contact", async (ContactMessage message) =>
{
    if (string.IsNullOrWhiteSpace(message.Name) ||
        string.IsNullOrWhiteSpace(message.Email) ||
        string.IsNullOrWhiteSpace(message.Message))
    {
        return Results.BadRequest(new { error = "Name, email, and message are all required." });
    }

    // Simplest possible storage: append to a local log file so you can see submissions
    // land while you're developing. Swap this for an email send (e.g. via SendGrid/SMTP)
    // or a database write when you're ready to go further.
    var line = $"{DateTime.UtcNow:u} | {message.Name} <{message.Email}> | {message.Message.Replace('\n', ' ')}{Environment.NewLine}";
    await File.AppendAllTextAsync("contact-submissions.log", line);

    return Results.Ok(new { status = "received" });
});

app.Run();

record ContactMessage(string Name, string Email, string Message);
