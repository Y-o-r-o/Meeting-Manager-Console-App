namespace Application.Models
{
    public class Type
    {
        public Types Value { get; set; }

        public Type() { }
        public Type(string type)
        {
            if (type.All(char.IsDigit))
                throw new ArgumentException("Type cant be number.");
            var enumIsParsed = Enum.TryParse(type, true, out Types parsed);
            if (!enumIsParsed)
                throw new ArgumentException("Given string didn't matched any existing type.");
            Value = parsed;
        }

        public static implicit operator string(Type type) => type.Value.ToString();

        public override string ToString() => Value.ToString();

    }

    public enum Types
    {
        Live,
        InPerson
    }
}