using SQLite;

namespace Echolalia.Models
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Original { get; set; }
        public string Translation { get; set; }
        public LearningProgress progress { get; set; }
    }

    public enum LearningProgress
    {
        unknown,
        inProcess,
        learned
    }
}