using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubClient
{
    //help class which will be parsed through the pages;
    public class Story
    {
        public int page { get; set; }
        public int id1 { get; set; }
        public int id2 { get; set; }
        public int id3 { get; set; }
        public int id4 { get; set; }
        public int id5 { get; set; }
        public int id6 { get; set; }
        public int id7 { get; set; }
        public int id8 { get; set; }
        public int id9 { get; set; }
        public int id10 { get; set; }
    }
    public class ReposHelper
    {
        public string username;
        public string reposname;
    }
}
