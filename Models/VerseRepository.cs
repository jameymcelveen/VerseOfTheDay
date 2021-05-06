using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Mime;
using Microsoft.EntityFrameworkCore;

namespace VerseOfTheDay.Models
{
    public interface IVerseRepository
    {
        public List<VerseModel.Verse> GetVerses(DateTime date, int count);
        public List<VerseModel.Verse> GetFavoriteVerses(DateTime date, int count);
    }
    public class VerseRepository : IVerseRepository
    {
        public VerseRepository()
        {
        }
        public List<VerseModel.Verse> GetVerses(DateTime date, int count)
        {
            var result = new List<VerseModel.Verse>
            {
                new VerseModel.Verse() {Id = Guid.NewGuid().ToString(), VerseText = "Hello"}
            };
            return result;
        }
        public List<VerseModel.Verse> GetFavoriteVerses(DateTime date, int count)
        {
            throw new NotImplementedException();
        }
    }
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { }
        public DbSet<VerseModel.Verse> Verses { get; set; }
    }
}