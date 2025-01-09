using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDB_CLI_Tool.Models
{
    public class Movie
    {
        public int Id { get; set; }                   
        public string Title { get; set; }            
        public string Overview { get; set; }          
        public string release_date {  get; set; }
        public string poster_path { get; set; }       
        public double vote_average { get; set; }       
        public List<int> genre_ids { get; set; }
        public List<string> GenreNames { get; set; }
    }

}
