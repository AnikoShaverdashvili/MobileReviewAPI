using MobileReviewAPI.Data;
using MobileReviewAPI.Models;

namespace MobileReviewAPI
{
    public class Seed
    {
        private readonly MobileReviewDbContext dataContext;

        public Seed(MobileReviewDbContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task SeedDataContext()
        {
            if (!dataContext.MobileOwners.Any())
            {
                var mobileOwners = new List<MobileOwner>()
                {
                    new MobileOwner()
                    {
                        Mobile = new Mobile()
                        {
                            Name = "iPhone",
                            ReleaseDate = new DateTime(2020, 10, 23),
                            MobileCategories = new List<MobileCategory>()
                            {
                                new MobileCategory { Category = new Category() { Name = "Smartphone" } }
                            },
                            Reviews = new List<Review>()
                            {
                                new Review
                                {
                                    Title = "iPhone 12",
                                    Text = "iPhone 12 is the best mobile, because it has a great camera",
                                    Rating = 5,
                                    Reviewer = new Reviewer() { FirstName = "Teddy", LastName = "Smith" }
                                },
                                new Review
                                {
                                    Title = "iPhone 12",
                                    Text = "iPhone 12 is the best mobile for gaming",
                                    Rating = 5,
                                    Reviewer = new Reviewer() { FirstName = "Taylor", LastName = "Jones" }
                                },
                                new Review
                                {
                                    Title = "iPhone 12",
                                    Text = "iPhone 12 is too expensive",
                                    Rating = 2,
                                    Reviewer = new Reviewer() { FirstName = "Jessica", LastName = "McGregor" }
                                },
                            }
                        },
                        Owner = new Owner()
                        {
                            FirstName = "John",
                            LastName = "Doe",
                            Country = new Country()
                            {
                                Name = "United States"
                            }
                        }
                    },
                    new MobileOwner()
                    {
                        Mobile = new Mobile()
                        {
                            Name = "Samsung Galaxy S21",
                            ReleaseDate = new DateTime(2021, 1, 29),
                            MobileCategories = new List<MobileCategory>()
                            {
                                new MobileCategory { Category = new Category() { Name = "Smartphone" } }
                            },
                            Reviews = new List<Review>()
                            {
                                new Review
                                {
                                    Title = "Samsung Galaxy S21",
                                    Text = "Samsung Galaxy S21 is the best mobile, because it has a great battery life",
                                    Rating = 5,
                                    Reviewer = new Reviewer() { FirstName = "Teddy", LastName = "Smith" }
                                },
                                new Review
                                {
                                    Title = "Samsung Galaxy S21",
                                    Text = "Samsung Galaxy S21 is the best mobile for multitasking",
                                    Rating = 5,
                                    Reviewer = new Reviewer() { FirstName = "Taylor", LastName = "Jones" }
                                },
                                new Review
                                {
                                    Title = "Samsung Galaxy S21",
                                    Text = "Samsung Galaxy S21 is too bulky",
                                    Rating = 2,
                                    Reviewer = new Reviewer() { FirstName = "Jessica", LastName = "McGregor" }
                                },
                            }
                        },
                        Owner = new Owner()
                        {
                            FirstName = "Jane",
                            LastName = "Doe",
                            Country = new Country()
                            {
                                Name = "United States"
                            }
                        }
                    }
                };
                dataContext.MobileOwners.AddRange(mobileOwners);
                await dataContext.SaveChangesAsync();
            }
        }
    }
}