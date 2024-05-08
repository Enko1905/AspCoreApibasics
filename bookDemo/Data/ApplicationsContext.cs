using bookDemo.Models;
namespace bookDemo.Data

{
    public static class ApplicationsContext
    {
        public static List<Book> books { get; set; }

        static ApplicationsContext()
        {
            books = new List<Book>()
            {
                new Book(){Id=1,Title="Karagöz ve Hacivat",Price=75 },
                new Book(){Id=2,Title="Mesnevi",Price=150},
                new Book(){Id=3,Title="Dede Korkut",Price=120},
            };
        }
    }


}

