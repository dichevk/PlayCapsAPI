using PlayCapsViewer.Data;
using PlayCapsViewer.Models;
using static Azure.Core.HttpHeader;

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
                            Description = "Pikachu - the exclusive",
                            StartDate = new DateTime(1903,1,1),
                            EndDate = new DateTime(1905,1,1),
                            PlayCapsCategory= new List<PlayCapsCategory>()
                            {
                                new PlayCapsCategory { Category = new Category() { Name = "Electric"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review
                                {
                                    Title="Pikachu",Text = "Pickahu is a mammal-like creatures that have short, yellow fur with brown stripes on their backs, black-tipped ears", Rating = 5,
                                    Reviewer = new Reviewer()
                                    {
                                        FirstName = "Reviewer1", LastName = "LastNameOfReviewer", Country = new Country()
                                        {
                                             Name = "Kanto"
                                        }
                                    }
                                },
                                new Review { Title="Pikachu", Text = "Pickachu is the best at killing rocks", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" , Country = new Country() { Name = "Kanto" }} },
                                new Review { Title="Pikachu",Text = "Pickchu, pickachu, pikachu", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor",Country = new Country()
                            {
                                Name = "Kanto"
                            } } },
                            },
                            Rarity = Data.Enums.PlayCapRarity.Exclusive,
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
                            Description = "Squirtle is one of the three starter Pokémon of Kanto available at the beginning of Pokémon Red, Green, Blue, FireRed, and LeafGreen.",
                            StartDate = new DateTime(1903,1,1),
                            PlayCapsCategory = new List<PlayCapsCategory>()
                            {
                                new PlayCapsCategory { Category = new Category() { Name = "Water"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title= "Squirtle", Text = "squirtle is the best pokemon, because it is one of the three starter Pokémon of Kanto available at the beginning of Pokémon Red, Green, Blue, FireRed, and LeafGreen", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Reviewer1", LastName = "LastNameOfReviewer",Country = new Country()
                            {
                                Name = "Kanto"
                            } } },
                                new Review { Title= "Squirtle",Text = "Squirtle is the best a killing rocks", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones", Country = new Country()
                            {
                                Name = "Kanto"
                            } } },
                                new Review { Title= "Squirtle", Text = "squirtle, squirtle, squirtle", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor",Country = new Country()
                            {
                                Name = "Kanto"
                            } } },
                            },
                            Rarity = Data.Enums.PlayCapRarity.Rare,
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
                            Description = "Venasaur is a monster from the deep moss",
                            StartDate = new DateTime(1903,1,1),
                            EndDate = new DateTime(1906,1,1),
                            PlayCapsCategory = new List<PlayCapsCategory>()
                            {
                                new PlayCapsCategory { Category = new Category() { Name = "Leaf"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Veasaur",Text = "Venusaur is the game mascot of Pokémon Green and its remake Pokémon LeafGreen, appearing on the box art of both games.", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Reviewer1", LastName = "LastNameOfReviewer",Country = new Country()
                            {
                                Name = "Kanto"
                            } } },
                                new Review { Title="Veasaur",Text = "Venasuar is the best a killing rocks", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones",Country = new Country()
                            {
                                Name = "Kanto"
                            } } },
                                new Review { Title="Veasaur",Text = "Venasuar, Venasuar, Venasuar", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor",Country = new Country()
                            {
                                Name = "Halflife"
                            } } },
                            },
                            Rarity = Data.Enums.PlayCapRarity.Legendary,
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
