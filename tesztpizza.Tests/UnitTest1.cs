using PizzaBuilderApp;
using static tesztpizza.Tests.Util;

namespace tesztpizza.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestPizzaBuilder()
        {
            // Arrange
            PizzaBuilder pizzaBuilder = new PizzaBuilder();

            // Act
            pizzaBuilder.AddTomatoSauce();
            pizzaBuilder.AddPepperoni();
            pizzaBuilder.AddCheese();
            Pizza pizza = pizzaBuilder.GetPizza();

            // Assert
            string expected = "Alap tészta\nPizzaszósz\nPepperóni\nSajt\n";
            string actual = pizza.ToString();
            //Assert.IsTrue(actual.Contains(expected));
            Console.WriteLine(StringToHex(expected));
            Console.WriteLine(StringToHex(actual));

            Assert.AreEqual(expected, actual);
        }
    }
}