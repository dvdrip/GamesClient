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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GamesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
