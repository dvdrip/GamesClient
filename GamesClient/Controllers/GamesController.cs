using GamesClient.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GamesClient.Controllers
{
    public class GamesController : Controller
    {
        // GET: GamesController
        public async Task<ActionResult> Index()
        {
            var httpClient = new HttpClient();
            var apiClient = new GamesAPIClient(httpClient);
            var games = await apiClient.GamesAllAsync();
            return View(games);
        }

        // GET: GamesController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var httpClient = new HttpClient();
            var apiClient = new GamesAPIClient(httpClient);
            var game = await apiClient.GamesGETAsync(id);
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
        public ActionResult Create(IFormCollection collection)
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

        // GET: GamesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                // Retrieve the game details by ID
                var httpClient = new HttpClient();
                var apiClient = new GamesAPIClient(httpClient);
                var game = await apiClient.GamesGETAsync(id);

                if (game == null)
                {
                    // Game not found, return appropriate response (e.g., 404 Not Found)
                    return NotFound();
                }

                return View(game);
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error, show an error message)
                return View("Error");
            }
        }

        // POST: GamesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Game game)
        {
            var httpClient = new HttpClient();
            var apiClient = new GamesAPIClient(httpClient);

            if (id != game.GameID)
            {
                // ID mismatch, return appropriate response (e.g., 400 Bad Request)
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Use the API client to update the game
                    await apiClient.GamesPUTAsync(id, game);

                    // Redirect to the index action after successful update
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log the error, show an error message)
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the game.");
                }
            }

            // If ModelState is not valid, return to the edit view with validation errors
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
