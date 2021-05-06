using System.Collections.Generic;

namespace VerseOfTheDay.Models
{
    public class VersesModel
    {
        public List<VerseModel.Verse> Verses { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public bool HasMorePages { get; set; }
    }
}