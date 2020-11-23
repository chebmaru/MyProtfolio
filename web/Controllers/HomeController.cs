using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using web.ViewModels;

namespace web.Controllers
{
    public class HomeController : Controller
    {
        public IUnitOfWork<Owner> _owner { get; }
        public IUnitOfWork<PortfolioItem> _portfolio { get; }

        public HomeController(IUnitOfWork<Owner> owner,
                              IUnitOfWork<PortfolioItem> portfolio )
        {
            _owner = owner;
            _portfolio = portfolio;
        }
        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                Owner = _owner.Entity.GetAll().First(),
                PortfolioItems = _portfolio.Entity.GetAll().ToList()
            };
            return View(homeViewModel);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
