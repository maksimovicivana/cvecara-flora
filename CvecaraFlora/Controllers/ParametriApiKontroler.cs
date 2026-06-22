using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CvecaraFlora.Controllers
{
    [Route("api/parametri")]
    [ApiController]
    public class ParametriApiKontroler : ControllerBase
    {
        [HttpGet]
        public IActionResult VratiParametre()
        {
            string putanja = Path.Combine(Directory.GetCurrentDirectory(), "parametri.json");
            string json = System.IO.File.ReadAllText(putanja);

            JsonDocument dokument = JsonDocument.Parse(json);
            int brojDanaZaHitno = dokument.RootElement.GetProperty("BrojDanaZaHitno").GetInt32();

            return Ok(new
            {
                BrojDanaZaHitno = brojDanaZaHitno
            });
        }
    }
}