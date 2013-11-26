using NUnit.Framework;
using Sporthub.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sporthub.Tests
{
    [TestFixture]
    public class ModelTests
    {
        public const double longitude = 6.8697;
        public const double latitude = 45.9231;

        [Test]
        public void Can_create_a_resort()
        {
            var sut = new Resort();
            sut.Name = "Chamonix";

            Assert.IsNotNull(sut);
            Assert.AreEqual("Chamonix", sut.Name);
        }

        [Test]
        public void Can_specify_a_resort_location()
        {
            var sut = new Resort();
            sut.Location = DbGeography.FromText(string.Format("POINT ({0} {1})", longitude, latitude));

            Assert.AreEqual(longitude, sut.Location.Longitude);
            Assert.AreEqual(latitude, sut.Location.Latitude);
        }

        [Test]
        public void Can_create_a_country()
        {
            var sut = new Country();
            sut.Name = "France";

            Assert.IsNotNull(sut);
            Assert.AreEqual("France", sut.Name);
        }

        [Test]
        public void Can_create_a_continent()
        {
            var sut = new Continent();
            sut.Name = "Europe";

            Assert.IsNotNull(sut);
            Assert.AreEqual("Europe", sut.Name);
        }

        [Test]
        public void Can_add_a_resort_to_a_country()
        {
            var sut = new Country();
            var resort = new Resort();
            sut.Resorts.Add(resort);

            Assert.AreEqual(1, sut.Resorts.Count);
        }

        [Test]
        public void Can_add_a_country_to_a_continent()
        {
            var sut = new Continent();
            var country = new Country();
            sut.Countries.Add(country);

            Assert.AreEqual(1, sut.Countries.Count);
        }

        [Test]
        public void Can_create_a_resortarea()
        {
            var sut = new ResortArea();
            sut.Name = "Les Trois Vallees";

            Assert.IsNotNull(sut);
            Assert.AreEqual("Les Trois Vallees", sut.Name);
        }

        [Test]
        public void Can_add_a_resort_to_a_resortarea()
        {
            var sut = new ResortArea();
            var resort = new Resort();
            sut.Resorts.Add(resort);

            Assert.AreEqual(1, sut.Resorts.Count);
        }

        [Test]
        public void Can_create_a_mountainrange()
        {
            var sut = new MountainRange();
            sut.Name = "The Alps";

            Assert.IsNotNull(sut);
            Assert.AreEqual("The Alps", sut.Name);
        }

        [Test]
        public void Can_add_a_resort_to_a_mountainrange()
        {
            var sut = new MountainRange();
            var resort = new Resort();
            sut.Resorts.Add(resort);

            Assert.AreEqual(1, sut.Resorts.Count);
        }




    }
}
