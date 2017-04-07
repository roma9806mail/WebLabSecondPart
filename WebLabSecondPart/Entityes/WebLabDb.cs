using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebLabSecondPart.Entityes
{
    public class WebLabDb : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}