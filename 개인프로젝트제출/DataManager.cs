using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace 개인프로젝트제출
{
    class DataManager
    {
        public static List<Movie> Movies = new List<Movie>();
        public static List<User> Users = new List<User>();

        static DataManager()
        {
            Load();
        }

        private static void Load()
        {
            try
            {
                string moviesOutput = File.ReadAllText(@"./Movies.xml");
                XElement moviesXElement = XElement.Parse(moviesOutput);
                Movies = (from item in moviesXElement.Descendants("movies")
                         select new Movie()
                         {
                             Code = item.Element("code").Value,
                             Name = item.Element("name").Value,
                             Director = item.Element("director").Value,
                             BorrowedAt = DateTime.Parse(item.Element("borrowedAt").Value),
                             IsBorrowed = item.Element("isBorrowed").Value != "0" ? true : false,
                             UserId = int.Parse(item.Element("userId").Value),
                             UserName = item.Element("userName").Value
                         }).ToList<Movie>();

                string usersOutput = File.ReadAllText(@"./Users.xml");
                XElement usersXElement = XElement.Parse(usersOutput);
                Users = (from item in usersXElement.Descendants("user")
                         select new User()
                         {
                             Id = int.Parse(item.Element("id").Value),
                             Name = item.Element("name").Value
                         }).ToList<User>();
            }
            catch (FileLoadException ex)
            {
                Save();
            }
        }

        public static void Save()
        {
            string moviesOutput = "";
            moviesOutput += "<movies>\n";
            foreach (var item in Movies)
            {
                moviesOutput += "<movie>\n";
                moviesOutput += "  <code>" + item.Code + "</code>\n";
                moviesOutput += "  <name>" + item.Name + "</name>\n";
                moviesOutput += "  <director>" + item.Director + "</director>\n";
                moviesOutput += "  <borrowedAt>" + item.BorrowedAt.ToLongDateString() + "</borrowedAt>\n";
                moviesOutput += "  <isBorrowed>" + (item.IsBorrowed ? 1 : 0) + "</isBorrowed>\n";
                moviesOutput += "  <userId>" + item.UserId + "</userId>\n";
                moviesOutput += "  <userName>" + item.UserName + "</userName>\n";
                moviesOutput += "</book>\n";
            }
            moviesOutput += "</movies>\n";

            string usersOutput = "";
            usersOutput += "<users>\n";
            foreach (var item in Users)
            {
                usersOutput += "<user>\n";
                usersOutput += "  <id>" + item.Id + "</id>\n";
                usersOutput += "  <name>" + item.Name + "</name>\n";
                usersOutput += "</user>\n";
            }
            usersOutput += "</users>\n";

            File.WriteAllText(@"./Books.xml", moviesOutput);
            File.WriteAllText(@"./Users.xml", usersOutput);
        }
    }
}
