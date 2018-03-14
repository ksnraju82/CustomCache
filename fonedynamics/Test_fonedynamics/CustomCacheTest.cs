using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using fonedynamics;


namespace Test_fonedynamics
{
    [TestClass]
    public class CustomCacheTest
    {

        private static CustomCache<int, string> _cache;

        [TestMethod]
        public void CustomCache_TryGetValue_Test()
        {
            //Arrange
            _cache = new CustomCache<int, string>(3);
            string strOutValue;
            _cache.AddOrUpdate(1, "test1");
            _cache.AddOrUpdate(2, "test2");
            _cache.AddOrUpdate(3, "test3");

            //Act
            bool actual = _cache.TryGetValue(3, out strOutValue);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void CustomCache_GetMaxLength_Test()
        {
            //Arrange
            _cache = new CustomCache<int, string>(3);
            string strOutValue;
            _cache.AddOrUpdate(1, "test1");
            _cache.AddOrUpdate(2, "test2");
            _cache.AddOrUpdate(3, "test3");
            _cache.AddOrUpdate(4, "test4");

            //Act
            bool actual = _cache.TryGetValue(4, out strOutValue);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void CustomCache_AddItemToCache_Test()
        {
            //Arrange
            _cache = new CustomCache<int, string>(1);
            string strOutValue;
            _cache.AddOrUpdate(1, "test1");
            
            //Act
            bool actual = _cache.TryGetValue(1, out strOutValue);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void CustomCache_UpdateItemToCache_Test()
        {
            //Arrange
            _cache = new CustomCache<int, string>(3);
            string strOutValue;
            _cache.AddOrUpdate(1, "test1");
            _cache.AddOrUpdate(2, "test2");
            _cache.AddOrUpdate(3, "test3");

            //Act
            _cache.AddOrUpdate(2, "updatetest2");
            bool actual = _cache.TryGetValue(2, out strOutValue);
            //Assert
            Assert.AreEqual("updatetest2", strOutValue);
        }

        [TestMethod]
        public void CustomCache_removeleastrecentlyused_Test()
        {
            //Arrange
            _cache = new CustomCache<int, string>(3);
            string strOutValue;
            _cache.AddOrUpdate(1, "test1");
            _cache.AddOrUpdate(2, "test2");
            _cache.AddOrUpdate(3, "test3");
            _cache.AddOrUpdate(4, "test4");

            //Act
            bool actual = _cache.TryGetValue(1, out strOutValue);

            //Assert
            Assert.AreEqual(false, actual);

        }
        

        [TestMethod]
        public void CustomCache_removeitem_Test()
        {
            //Arrange
            _cache = new CustomCache<int, string>(5);
            string strOutValue;
            _cache.AddOrUpdate(1, "test1");
            _cache.AddOrUpdate(2, "test2");
            _cache.AddOrUpdate(3, "test3");
            _cache.AddOrUpdate(4, "test4");
            _cache.AddOrUpdate(5, "test5");

            //Act
            bool actual = _cache.Remove(2);
            actual = _cache.TryGetValue(2, out strOutValue);

            //Assert
            Assert.AreEqual(false, actual);

        }
    }
}
