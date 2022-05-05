namespace Application
{
    public interface ISerializer<T>
    {
        public void serialize(T data);

        public  T? deserialize();
    }
}