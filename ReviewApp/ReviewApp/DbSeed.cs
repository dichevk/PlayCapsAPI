using PlayCapsViewer.Data;
using PlayCapsViewer.Models;

namespace PlayCapsViewer
{
    public class DbSeed
    {
        private readonly DataContext dataContext;
        public DbSeed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.PlayCapsPlayers.Any())
            {
                var pokemonOwners = new List<PlayCapsPlayer>()
                {
                    new PlayCapsPlayer()
                    {
                        PlayCap = new PlayCap()
                        {
                            Name = "Pikachu",
                            StartDate = new DateTime(1903,1,1),
                            EndDate = new DateTime(1905,1,1),
                            PlayCapsCategory= new List<PlayCapsCategory>()
                            {
                                new PlayCapsCategory { Category = new Category() { Name = "Electric"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Pikachu",Text = "Pickahu is the best pokemon, because it is electric", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title="Pikachu", Text = "Pickachu is the best a killing rocks", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title="Pikachu",Text = "Pickchu, pickachu, pikachu", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Player = new Player()
                        {
                            FirstName = "Jack",
                            LastName = "London",
                            Gym = "Brocks Gym",
                            Country = new Country()
                            {
                                Name = "Kanto"
                            }
                        }
                    },
                    new PlayCapsPlayer()
                    {
                        PlayCap = new PlayCap()
                        {
                            Name = "Squirtle",
                            StartDate = new DateTime(1903,1,1),
                            PlayCapsCategory = new List<PlayCapsCategory>()
                            {
                                new PlayCapsCategory { Category = new Category() { Name = "Water"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title= "Squirtle", Text = "squirtle is the best pokemon, because it is electric", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title= "Squirtle",Text = "Squirtle is the best a killing rocks", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title= "Squirtle", Text = "squirtle, squirtle, squirtle", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Player = new Player()
                        {
                            FirstName = "Harry",
                            LastName = "Potter",
                            Gym = "Mistys Gym",
                            Country = new Country()
                            {
                                Name = "Saffron City"
                            }
                        }
                    },
                     new PlayCapsPlayer()
                    {
                        PlayCap = new PlayCap()
                        {
                            Name = "Venasuar",
                            StartDate = new DateTime(1903,1,1),
                            EndDate = new DateTime(1906,1,1),
                            PlayCapsCategory = new List<PlayCapsCategory>()
                            {
                                new PlayCapsCategory { Category = new Category() { Name = "Leaf"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Veasaur",Text = "Venasuar is the best pokemon, because it is electric", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title="Veasaur",Text = "Venasuar is the best a killing rocks", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title="Veasaur",Text = "Venasuar, Venasuar, Venasuar", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Player = new Player()
                        {
                            FirstName = "Ash",
                            LastName = "Ketchum",
                            Gym = "Ashs Gym",
                            Country = new Country()
                            {
                                Name = "Millet Town"
                            }
                        }
                    }
                };
                dataContext.PlayCapsPlayers.AddRange(pokemonOwners);
                dataContext.SaveChanges();
            }
        }
    }
}
