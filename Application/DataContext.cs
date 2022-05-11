using Application.Models;

namespace Application
{
    public class DataContext
    {
        private ISerializer<DataContext> _serializer;

        public List<Meeting> Meetings { get; set; } = new();

        public DataContext(ISerializer<DataContext> serializer)
        {
            _serializer = serializer;
        }

        public void SaveChanges()
        {
            _serializer.Serialize(this);
        }

    }
}