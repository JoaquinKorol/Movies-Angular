using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDB_CLI_Tool.Models
{
    public class MovieResponse
    {
        [JsonProperty("results")]
        public List<Movie> Result { get; set; }
        public int total_pages { get; set; }
    }
}
