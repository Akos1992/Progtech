using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;

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
        private String baseDough;
        private List<String> ingredientsList = new List<string>();

        public String BaseDough
        {
            get { return baseDough; }
        }

        public IReadOnlyList<String> IngredientsList { 
            get { return ingredientsList.AsReadOnly(); } 
        }


        public Pizza(string baseDough)
        {
            ingredients = new StringBuilder();
            ingredients.Append(baseDough+"\n");
            this.baseDough = baseDough;
        }

        public void AddIngredient(string ingredient)
        {
            ingredients.Append(ingredient + "\n");
            ingredientsList.Add(ingredient);
        }

        public override string ToString()
        {
            return ingredients.ToString();
        }
    }
}
