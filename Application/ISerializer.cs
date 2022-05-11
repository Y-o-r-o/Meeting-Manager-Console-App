namespace Application
{
    public interface ISerializer<T>
    {
        public void Serialize(T data);

        public  T? Deserialize();
    }
}