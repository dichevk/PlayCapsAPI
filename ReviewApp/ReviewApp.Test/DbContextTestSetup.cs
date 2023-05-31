using Microsoft.EntityFrameworkCore;
using PlayCapsViewer.Data;
using PlayCapsViewer.Data.Enums;

namespace ReviewApp.Test
{
    public class DbContextTestSetup
    {
        public async Task<DataContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.PlayCaps.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.PlayCaps.Add(
                    new PlayCap()
                    {
                        Id = i,
                        Name = "Pikachu Tazo",
                        StartDate = new DateTime(1903, 1, 1),
                        PlayCapsCategory = new List<PlayCapsCategory>()
                            {
                                new PlayCapsCategory { Category = new Category() {Id = i, Name = "Electric"}}
                            },
                        Reviews = new List<Review>()
                            {
                                new Review { Title="Pikachu Tazo",Text = "Pickahu is the best pokemon, because it is electric", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title="Pikachu Tazo", Text = "Pickachu is the best a killing rocks", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title="Pikachu Tazo",Text = "Pickchu, pickachu, pikachu", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            },
                        Rarity = PlayCapRarity.Legendary,
                        Description = "Powerful Tazo",
                        EndDate = new DateTime(9999, 1, 1),
                    });
                    databaseContext.Add(
                        new Country
                        {
                            Id = i,
                            Name = "Azuka",
                            Players = new List<Player>()
                            {
                               new Player()
                               {
                                   Id=i,
                                   FirstName="rain",
                                   LastName="tt",
                                   Country=new Country(),
                                   Gym = "Pikachu's gym"
                               }
                            }
                        });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }
    }
}
