using LibData.Entities;
using LibData;
using Microsoft.EntityFrameworkCore;

namespace WebShopAPI.Services
{
    public static class SeederDB
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppEFContext>();
                context.Database.Migrate();

                if (!context.Categories.Any())
                {
                    Dictionary<string, string> categories = new Dictionary<string, string> {
                        { "Ноутбуки", "laptop.jpg" },
                        { "Монітори", "monotor.jpg"},
                        { "Одяг", "clothing.jpg" }
                    };
                    foreach (var item in categories)
                    {
                        CategoryEntity cat = new CategoryEntity
                        {
                            Name = item.Key,
                            Image = item.Value,
                            DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc)
                        };
                        context.Categories.Add(cat);
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
