using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BlogPostAPI.Models.EntityFramework
{
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            for (int i = 0; i < 10; i++)
            {
                User user = new User();
                user.Firstname = FakeData.NameData.GetFirstName();
                user.Lastname = FakeData.NameData.GetSurname();
                user.Username = user.Firstname + "_" + user.Lastname;
                user.CreatedDate = FakeData.DateTimeData.GetDatetime();
                user.Email = FakeData.NetworkData.GetEmail();
                user.IsActive = FakeData.BooleanData.GetBoolean();
                context.Users.Add(user);
            }

            List<Category> categories = new List<Category>
            {
                new Category() { CategoryName = "Sport" },
                new Category() { CategoryName = "Fashion" },
                new Category() { CategoryName = "Nature" },
                new Category() { CategoryName = "Politics" },
                new Category() { CategoryName = "Science" },
                new Category() { CategoryName = "Art" },
                new Category() { CategoryName = "Music" }
            };

            foreach (var category in categories)
            {
                context.Categories.Add(category);
            }
            context.SaveChanges();

            List<Category> allCategories = context.Categories.ToList();
            List<User> allUsers = context.Users.ToList();
            int count = allCategories.Count();
            Random rndm = new Random();

            foreach (var user in allUsers)
            {
                for (int i = 0; i < FakeData.NumberData.GetNumber(1, 5); i++)
                {
                    int random = rndm.Next(1, count);
                    Post post = new Post
                    {
                        PostTitle = FakeData.NameData.GetCompanyName(),
                        Id = user.Id,
                        IsDeleted = FakeData.BooleanData.GetBoolean(),
                        PostText = "One morning, when Gregor Samsa woke from troubled dreams, he found himself transformed in his bed into a horrible vermin. He lay on his armour - like back, and if he lifted his head a little he could see his brown belly, slightly domed and divided by arches into stiff sections.The bedding was hardly able to cover it and seemed ready to slide off any moment. His many legs, pitifully thin compared with the size of the rest of him, waved about helplessly as he looked. 'What's happened to me?' he thought. It wasn't a dream. His room, a proper human room although a little too small, lay peacefully between its four familiar walls. A collection of textile samples lay spread out on the table - Samsa was a travelling salesman - and above it there hung a picture that he had recently cut out of an illustrated magazine and housed in a nice, gilded frame. It showed a lady fitted out with a fur hat and fur boa who sat upright, raising a heavy fur muff that covered the whole of her lower arm towards the viewer. Gregor then turned to look out the window at the dull weather. ",
                        CreatedDate = FakeData.DateTimeData.GetDatetime(),
                        CategoryId = random
                    };
                    context.Posts.Add(post);
                }
            }
            context.SaveChanges();

            Comment comment = new Comment()
            {
                CommentText = "comment",
                PostId = 1,
                UserId = 1,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };
            context.Comments.Add(comment);
            context.SaveChanges();
        }
    }
}