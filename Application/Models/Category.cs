using System.ComponentModel;

namespace Application.Models
{
    public class Category
    {
        public Categories Value { get; }

        public Category()
        {
            Value = default(Categories);
        }

        public Category(string category)
        {
            if (category.All(char.IsDigit))
                throw new ArgumentException("Category cant be number.");
            var enumIsParsed = Enum.TryParse(category, true, out Categories parsed);
            if (!enumIsParsed)
                throw new ArgumentException("Given string didn't matched any existing category.");
            Value = parsed;
        }

        public static implicit operator string(Category category) => category.Value.ToString();

        public override string ToString() => Value.ToString();

    }

    public enum Categories
    {
        CodeMonkey,
        Hub,
        Short,
        TeamBuilding
    }
}