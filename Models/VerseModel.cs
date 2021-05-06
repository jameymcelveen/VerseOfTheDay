using System;

namespace VerseOfTheDay.Models
{
    public class VerseModel
    {
        public class Verse
        {
            public string Id { get; set; }
            public string VerseText { get; set; }
            public string ReferenceText { get; set; }
            public string ImageLink { get; set; }
            public bool Favorite { get; set; }
            public DateTime VerseDate { get; set; }
        }
    }
}