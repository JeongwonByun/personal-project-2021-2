using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 개인프로젝트제출
{
    class Movie
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool IsBorrowed { get; set; }
        public DateTime BorrowedAt { get; set; }
    }
}
