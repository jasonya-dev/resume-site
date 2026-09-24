// Point this at wherever the ContactApi project ends up running.
// dotnet run defaults to something like https://localhost:5001 or http://localhost:5000 —
// check your terminal output when you run the backend and update this if it differs.
const CONTACT_API_URL = "http://localhost:5000/api/contact";

const form = document.getElementById("contact-form");
const status = document.getElementById("form-status");

form.addEventListener("submit", async (event) => {
  event.preventDefault();

  const payload = {
    name: form.name.value.trim(),
    email: form.email.value.trim(),
    message: form.message.value.trim(),
  };

  if (!payload.name || !payload.email || !payload.message) {
    setStatus("Please fill in every field.", "error");
    return;
  }

  setStatus("Sending…", null);

  try {
    const response = await fetch(CONTACT_API_URL, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(payload),
    });

    if (!response.ok) throw new Error(`Server responded ${response.status}`);

    setStatus("Message sent — thanks for reaching out.", "ok");
    form.reset();
  } catch (err) {
    console.error(err);
    setStatus(
      "Couldn't reach the server. Is the ContactApi project running?",
      "error"
    );
  }
});

function setStatus(text, state) {
  status.textContent = text;
  if (state) {
    status.setAttribute("data-state", state);
  } else {
    status.removeAttribute("data-state");
  }
}
