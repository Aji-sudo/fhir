using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/fhir")]
public class FhirController : ControllerBase
{
    // GET: api/fhir/Patient/{id}
    [HttpGet("Patient/{id}")]
    public IActionResult GetPatient(string id)
    {
        // Mock a Patient resource for demonstration
        var patient = new Patient
        {
            Id = id,
            Name = new List<HumanName>
            {
                new HumanName
                {
                    Given = new[] { "lizzie" },
                    Family = "Doe"
                }
            },
            BirthDate = "1990-01-01",
        };

        // Serialize Patient resource to JSON and return it
        return Ok(patient.ToJson());
    }

    // POST: api/fhir/Patient
    //[HttpPost("Patient")]
    //public IActionResult CreatePatient([FromBody] Patient patient)
    //{
    //    if (patient == null)
    //    {
    //        return BadRequest(new { message = "Invalid Patient resource." });
    //    }

    //    // Assign a unique ID to the patient and mock saving to a database
    //    patient.Id = Guid.NewGuid().ToString();

    //    // Return the created Patient with a URI for its resource
    //    return Created($"api/fhir/Patient/{patient.Id}", patient.ToJson());
    //}

    //[HttpPost("Patient")]
    //public IActionResult CreatePatient([FromBody] string jsonBody)
    //{
    //    var parser = new FhirJsonParser();
    //    var patient = parser.Parse<Patient>(jsonBody);  // Parse from raw JSON string
    //    patient.Id = Guid.NewGuid().ToString();  // Simulate saving the Patient and generating a new ID

    //    return Created($"api/fhir/Patient/{patient.Id}", patient.ToJson());  // Serialize Patient object back to JSON
    //}

    [HttpPost("Patient")]
    public IActionResult CreatePatient([FromBody] string jsonBody)
    {
        try
        {
            // Parse the JSON string into a Patient object using FhirJsonParser
            var parser = new FhirJsonParser();
            var patient = parser.Parse<Patient>(jsonBody);

            if (patient == null)
            {
                return BadRequest(new { message = "Invalid patient data" });
            }

            // Generate a new ID for the patient
            patient.Id = Guid.NewGuid().ToString();

            // Return the created patient
            return Created($"api/fhir/Patient/{patient.Id}", patient);
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during parsing
            return BadRequest(new { message = ex.Message });
        }
    }


    [HttpPut("Patient/{id}")]
    public IActionResult UpdatePatient(string id, [FromBody] string jsonBody)
    {
        try
        {
            // Parse the JSON string into a Patient object using FhirJsonParser
            var parser = new FhirJsonParser();
            var patient = parser.Parse<Patient>(jsonBody);

            if (patient == null)
            {
                return BadRequest(new { message = "enter details correctly" });
            }

            // Generate a new ID for the patient
            //patient.Id = Guid.NewGuid().ToString();

            // Return the created patient
            return Ok(patient.ToJson());
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during parsing
            return BadRequest(new { message = ex.Message });
        }
    }




    // PUT: api/fhir/Patient/{id}
    //[HttpPut("Patient/{id}")]
    //public IActionResult UpdatePatient(string id, [FromBody] Patient patient)
    //{
    //    if (patient == null)
    //    {
    //        return BadRequest(new { message = "Invalid Patient resource." });
    //    }

    //    // Mock update logic: Update patient with the given ID
    //    patient.Id = id;

    //    // Return the updated Patient
    //    return Ok(patient.ToJson());
    //}

    //[HttpPut("Patient/{id}")]
    //public IActionResult UpdatePatient(string id, [FromBody] Patient jsonBody)
    //{
    //    try
    //    {
    //        // Check if the incoming body is valid
    //        if (jsonBody == null)
    //        {
    //            return BadRequest(new { message = "Invalid or missing Patient data." });
    //        }

    //        // Check if the patient ID in the URL matches the ID in the request body (optional, but good practice)
    //        if (id != jsonBody.Id)
    //        {
    //            return BadRequest(new { message = "Patient ID in the URL does not match the body." });
    //        }

    //        // Find the patient in the database (assuming you're using a repository or database)
    //        var existingPatient = _patientRepository.GetPatientById(id);
    //        if (existingPatient == null)
    //        {
    //            return NotFound(new { message = "Patient not found." });
    //        }

    //        // Update the patient fields (you can update any fields you want here)
    //        existingPatient.Name = jsonBody.Name;
    //        existingPatient.Gender = jsonBody.Gender;
    //        existingPatient.BirthDate = jsonBody.BirthDate;

    //        // Save the updated patient (you can save to a database or repository)
    //        _patientRepository.SavePatient(existingPatient);

    //        // Return a response with the updated patient
    //        return Ok(existingPatient);  // Return the updated patient data in the response
    //    }
    //    catch (Exception ex)
    //    {
    //        // Catch any exceptions and return a bad request with the error message
    //        return BadRequest(new { message = $"Error: {ex.Message}" });
    //    }
    //}


    // DELETE: api/fhir/Patient/{id}
    [HttpDelete("Patient/{id}")]
    public IActionResult DeletePatient(string id)
    {
        // Mock deletion logic: Assume the patient is deleted
        return NoContent();
    }
}
