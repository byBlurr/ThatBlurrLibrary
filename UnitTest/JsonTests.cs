using Blurr.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;

namespace UnitTest
{
    /// <summary>
    /// Tests for JSON methods
    /// </summary>
    [TestClass]
    public class JsonTests
    {
        /// <summary>
        /// Test converting object to json string and back
        /// </summary>
        [TestMethod]
        public void SerializationTest()
        {
            Animal pet = new Animal() { Name = "Gary", AnimalType = AnimalTypes.GOOSE, Age = 10 };
            string json = Serialization.ToJson(pet);
            Animal converted = Serialization.FromJson<Animal>(json);

            Assert.AreEqual<Animal>(pet, converted);
            Assert.AreEqual(pet.Age, converted.Age);
        }
    }
}

public class Animal
{
    public string Name { get; set; }
    public AnimalTypes AnimalType { get; set; }

    [Range(0, 20)]
    public int Age { get; set; }

    public override bool Equals(object obj)
    {
        return obj is Animal animal &&
               Name == animal.Name &&
               AnimalType == animal.AnimalType &&
               Age == animal.Age;
    }
}

public enum AnimalTypes
{
    DOG, CAT, HORSE, RAT, LIZARD, GOOSE
}
