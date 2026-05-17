const complaintForm = document.getElementById("complaintForm");
const copyEmailButton = document.getElementById("copyEmailButton");
const formNote = document.getElementById("formNote");
const destinationEmail = "devpratapsingh211@gmail.com";

function updateNote(message, isSuccess = true) {
  formNote.textContent = message;
  formNote.style.color = isSuccess ? "#245f2f" : "#8a2e23";
}

complaintForm.addEventListener("submit", (event) => {
  event.preventDefault();

  const formData = new FormData(complaintForm);
  const name = formData.get("name")?.toString().trim() || "";
  const phone = formData.get("phone")?.toString().trim() || "";
  const location = formData.get("location")?.toString().trim() || "";
  const subject = formData.get("subject")?.toString().trim() || "";
  const message = formData.get("message")?.toString().trim() || "";

  const emailSubject = encodeURIComponent(`Complaint: ${subject}`);
  const emailBody = encodeURIComponent(
    [
      `Name: ${name}`,
      `Mobile Number: ${phone}`,
      `Village / City: ${location}`,
      "",
      "Complaint Details:",
      message,
    ].join("\n")
  );

  window.location.href = `mailto:${destinationEmail}?subject=${emailSubject}&body=${emailBody}`;
  updateNote("Your mail app has been opened with the complaint details.");
});

copyEmailButton.addEventListener("click", async () => {
  try {
    await navigator.clipboard.writeText(destinationEmail);
    updateNote(`Email copied: ${destinationEmail}`);
  } catch (error) {
    updateNote(`Copy failed. Please use this email: ${destinationEmail}`, false);
  }
});
