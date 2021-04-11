using Games.Core.Entities.Categories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Games.Infrastructure.Repositories.MsSqlServer.Seeds
{
    public static class CategorySeed
    {
        public static void Run(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new List<Category> {
                    new Category(1, "Strategiczna"),
                    new Category(2, "Sportowa"),
                    new Category(3, "Wyścigówa"),
                    new Category(4, "Wojenna"),
                    new Category(5, "FPS"),
                    new Category(6, "MMORPG"),
                    new Category(7, "Horror"),
                    new Category(8, "Przygodowa"),
                    new Category(9, "Strzelanka")
                });
        }
    }
}
