using GamesClient.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GamesClient.Controllers
{
    public class GamesController : Controller
    {
        private readonly GamesAPIClient _apiClient;

        public GamesController(GamesAPIClient apiClient)
        {
            if (apiClient == null)
            {
                throw new ArgumentNullException(nameof(apiClient), "The 'apiClient' parameter cannot be null.");
            }

            _apiClient = apiClient;
        }

        // GET: GamesController
        public async Task<ActionResult> Index()
        {
            var games = await _apiClient.GamesAllAsync();
            return View(games);
        }

        // GET: GamesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var game = await _apiClient.GamesGETAsync(id);
            return View(game);
        }

        // GET: GamesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GamesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Game game)
        {
            try
            {
                // Check if _apiClient is null before proceeding
                if (_apiClient == null)
                {
                    // Log or handle the situation where _apiClient is unexpectedly null
                    return View("Error: GamesAPIClient is not initialized.");
                }

                if (ModelState.IsValid)
                {
                    // Use the API client to create the game
                    await _apiClient.GamesPOSTAsync(game);

                    return RedirectToAction(nameof(Index));
                }

                // If ModelState is not valid, return to the create view with validation errors
                return View(game);
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error, show an error message)
                ModelState.AddModelError(string.Empty, "An error occurred while creating the game. Error: " + ex.Message);
                return View(game);
            }
        }


        // GET: GamesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var game = await _apiClient.GamesGETAsync(id);

                if (game == null)
                {
                    return NotFound();
                }

                return View(game);
            }
            catch (Exception ex)
            {
                return View("Error: " + ex.Message);
            }
        }

        // POST: GamesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Game game)
        {
            if (id != game.GameID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _apiClient.GamesPUTAsync(id, game);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the game. Error: " + ex.Message);
                }
            }

            return View(game);
        }

        // GET: GamesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GamesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
