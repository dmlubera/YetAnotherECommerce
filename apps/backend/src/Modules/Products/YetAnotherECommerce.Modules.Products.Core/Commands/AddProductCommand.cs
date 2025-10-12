using YetAnotherECommerce.Shared.Abstractions.Commands;

namespace YetAnotherECommerce.Modules.Products.Core.Commands
{
    public class AddProductCommand : ICommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        private AddProductCommand() { }

        public AddProductCommand(string name, string description, decimal price, int quantity)
        {
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
        }
    }
}