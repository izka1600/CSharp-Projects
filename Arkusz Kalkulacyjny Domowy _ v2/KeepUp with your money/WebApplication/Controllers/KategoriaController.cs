﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthDatabase.Entities;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class KategoriaController : Controller
    {
		private readonly IAntiforgery _antiforgery;

		private readonly IArkuszService _arkuszService;
		private readonly UserManager<AppUser> _userManager;

		public KategoriaController(
			IArkuszService arkuszService,
			UserManager<AppUser> userManager)
		{
			_arkuszService = arkuszService;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
		{
			var currentUser = await _userManager.GetUserAsync(User);
			if (currentUser == null)
			{
				return Challenge();
			}

			return View();
		}

		[Route("/WebApiKategoria/WylistujKategorie")]
		public async Task<IActionResult> ListCategories()
		{

			var currentUser = await _userManager.GetUserAsync(User);
			if (currentUser == null)
			{
				return RedirectToAction("ListCategories");
			}

			ICollection<KategoriaViewModel> currentKategoriaItems = await _arkuszService.Get_Kategorie();
			var model = new ListKategoriaViewModel()
			{
				Items = currentKategoriaItems
			};
			return View(model);

		}
	}
}