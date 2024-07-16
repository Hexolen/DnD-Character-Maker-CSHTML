using DnD_Character_Maker_CSHTML.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DnD_Character_Maker_CSHTML.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetCharacters()
        {
            List<DNDCharacter> characters = new List<DNDCharacter>();

            using (SqlConnection connection = new SqlConnection("Server=HEXOLEN;Database=DnDCM;User Id=Emre;Password=11;"))
            {
                string sql = "SELECT CharacterID, CharacterName, UserName, ClassLevel, Background, Race  FROM Characters INNER JOIN Users on Users.UserID = Characters.UserID";
                
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int id = (int)reader["CharacterID"];
                        string name = (string)reader["CharacterName"];

                        DNDCharacter dnd = new DNDCharacter(id, name);

                        dnd.creator = (string)reader["UserName"];
                        dnd.classLevel = (string)reader["ClassLevel"];
                        dnd.background = (string)reader["Background"];
                        dnd.race = (string)reader["Race"];

                        characters.Add(dnd);
                    }

                    reader.Close();
                }
            }

            return Json(characters);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}