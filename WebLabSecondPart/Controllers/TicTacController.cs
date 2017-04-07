using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLabSecondPart.Models;

namespace WebLabSecondPart.Controllers
{
    public class TicTacController : Controller
    {
        static string message;

        public ActionResult Index()
        {
            return View(new TicTacModel
            {
                Message = message,
                GameBoard = Game.GameBoard
            });
        }

        public ActionResult StartNewGame()
        {
            message = "";
            Game.Iniziallize();
            return RedirectToAction("Index");
        }


        public ActionResult Turn(string id)
        {
            string[] rowAndCol = id.Split('_');
            int row = int.Parse(rowAndCol[0]);
            int col = int.Parse(rowAndCol[1]);
            message = Game.GamerTurn(row, col);
            if (message == "Try one more time")
            {
                return RedirectToAction("Index");
            }

            if (Game.GameEnd('X'))
            {
                message = "You win!!!";
                return RedirectToAction("Index");
            }

            Game.PCTurn();
            if (Game.GameEnd('0'))
            {
                message = "Pc win :(";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}