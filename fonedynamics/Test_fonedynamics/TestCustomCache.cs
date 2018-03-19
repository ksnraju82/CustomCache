using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CacheLibrary;


namespace CacheLibrary.Tests
{
    [TestClass]
    public class TestCustomCache
    {
        CustomCache<int, string> _cache = new CustomCache<int, string>(3);

        [TestMethod]
        public void CustomCache_ItemsShouldBeAddedToCache()
        {
            string strOutValue;

            _cache.AddOrUpdate(1, "test1");
            _cache.AddOrUpdate(2, "test2");
            _cache.AddOrUpdate(3, "test3");

            //Act
            bool actual = _cache.TryGetValue(1, out strOutValue);

            //Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void CustomCache_ItemShouldBeUpdateInCache()
        {
            string strOutValue;
            //Act
            _cache.AddOrUpdate(2, "updatetest2");
            bool actual = _cache.TryGetValue(2, out strOutValue);
            //Assert
            Assert.AreEqual("updatetest2", strOutValue);
        }

        [TestMethod]
        public void CustomCache_removeleastrecentlyRetrieveditem()
        {
            string strOutValue;

            _cache.AddOrUpdate(1, "test1");
            _cache.AddOrUpdate(2, "test2");
            _cache.AddOrUpdate(3, "test3");

            //Act
            bool initial = _cache.TryGetValue(2, out strOutValue);
            _cache.AddOrUpdate(4, "test4");
            bool actual = _cache.TryGetValue(2, out strOutValue);

            //Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void CustomCache_removeleastrecentlyUpdateditem()
        {
            string strOutValue;
            //Act
            _cache.AddOrUpdate(1, "test1");
            _cache.AddOrUpdate(2, "test2");
            _cache.AddOrUpdate(3, "test3");
            _cache.AddOrUpdate(3, "updatetest3");

            _cache.AddOrUpdate(4, "test4");
            bool actual = _cache.TryGetValue(3, out strOutValue);

            //Assert
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void CustomCache_removeleastrecentlyAddeditem()
        {
            //Arrange
            string strOutValue;
            _cache.AddOrUpdate(3, "test3");
            _cache.AddOrUpdate(2, "test2");
            _cache.AddOrUpdate(1, "test1");
            _cache.AddOrUpdate(4, "test4");

            //Act
            bool actual = _cache.TryGetValue(1, out strOutValue);

            //Assert
            Assert.AreEqual(false, actual);
        }


        [TestMethod]
        public void CustomCache_removeitem()
        {
            //Arrange
            string strOutValue;

            //Act            
            bool actual = _cache.TryGetValue(2, out strOutValue);

            //Assert
            Assert.AreEqual(false, actual);

        }

        [TestMethod]
        public void CustomCache_GetValueByKey()
        {
            //Arrange
            string strOutValue;
            _cache.AddOrUpdate(1, "test1");
            _cache.AddOrUpdate(2, "test2");
            _cache.AddOrUpdate(3, "test3");

            //Act
            bool actual = _cache.TryGetValue(3, out strOutValue);

            //Assert
            Assert.AreEqual(true, actual);
        }
    }
}
