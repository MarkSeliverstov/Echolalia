using SQLite;
using System;

namespace Echolalia.Models
{
    public class Word
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Original { get; set; }
        public string Translation { get; set; }
        public bool IsAddedByUser { get; set; }
        public LearningProgress Progress { get; set; }
        public DateTime LastPracticed { get; set; }
        public bool IsFavorite { get; set; }
    }

    public enum LearningProgress
    {
        unknown,
        inProcess,
        learned
    }
}