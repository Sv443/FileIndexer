
namespace CommonTypes
{
    public class Files : IEntity
    {
        public Files()
        {

        }

        public int ID { get; set; }
        public string Pfad { get; set; }

        public string Content { get; set; }


        

        public bool Equals(Files other)
        {
            return ID == other.ID &&
                    Pfad == other.Pfad &&
                    Content == other.Content;
                    
        }
    }
}
