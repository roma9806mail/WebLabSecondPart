using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLabSecondPart.Models
{
    public class TicTacModel
    {
        public char[,] GameBoard { get; set; }

        public string Message { get; set; }
    }
}