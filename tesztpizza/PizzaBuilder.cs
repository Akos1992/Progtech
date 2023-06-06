using System.Text;

namespace PizzaBuilderApp
{
    public class PizzaBuilder
    {
        private Pizza pizza;

        public PizzaBuilder()
        {
            // Kezdetben egy alap pizza tésztát használunk
            pizza = new Pizza("Alap tészta");
        }

        public void AddTomatoSauce()
        {
            pizza.AddIngredient("Pizzaszósz");
        }

        public void AddSalami()
        {
            pizza.AddIngredient("Szalámi");
        }

        public void AddPepperoni()
        {
            pizza.AddIngredient("Pepperóni");
        }

        public void AddCheese()
        {
            pizza.AddIngredient("Sajt");
        }

        public Pizza GetPizza()
        {
            return pizza;
        }
    }

    public class Pizza
    {
        private StringBuilder ingredients;

        public Pizza(string baseDough)
        {
            ingredients = new StringBuilder();
            ingredients.AppendLine(baseDough);
        }

        public void AddIngredient(string ingredient)
        {
            ingredients.AppendLine(ingredient);
        }

        public override string ToString()
        {
            return ingredients.ToString();
        }
    }
}
