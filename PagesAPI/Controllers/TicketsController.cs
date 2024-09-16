using Microsoft.AspNetCore.Mvc;
using ParcialAPI.DAL.Entities;
using Newtonsoft.Json;
using System.Net.Http;

namespace PagesAPI.Controllers
{
    public class TicketsController : Controller
    {

        private readonly IHttpClientFactory _HttpClient;
        private readonly IConfiguration _Configuration;

        public TicketsController(IHttpClientFactory HttpClient, IConfiguration Configuration)
        {
            _HttpClient = HttpClient;
            _Configuration = Configuration;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {

            return View();

        }


        [HttpPost]
        public async Task<ActionResult> Verificar()
        {
            var id = Request.Form["id"].ToString();
            var entranceGate = Request.Form["entranceGate"].ToString();
            var url = String.Format("https://localhost:7173/api/Tickets/Get/{0}", id);
            var json = await _HttpClient.CreateClient().GetStringAsync(url);
            Ticket? tickets = JsonConvert.DeserializeObject<Ticket>(json);



            if (tickets != null)
            {
                if (tickets.isUsed == false)
                {
                    tickets.entranceGate = entranceGate;
                    url = String.Format("https://localhost:7173/api/Tickets/Put/{0}", id);
                    await _HttpClient.CreateClient().PutAsJsonAsync(url, tickets);
                    ViewData["Message"] = "Boleta Válida, puede ingresar al concierto";
                    return View();

                }
                else
                {
                    ViewData["Message"] =String.Format("Boleta ya usada, Entrada: {0}  Fecha: {1}",tickets.entranceGate, tickets.useDate);
                    
                    return View();
                }



            }
            ViewData["Message"] = "Boleta Invalida";
            return View();



        }

        






    }


    
}
