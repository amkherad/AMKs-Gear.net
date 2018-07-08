using System;
using System.Collections.Generic;
using System.Diagnostics;
using AMKsGear.Core.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AMKsGear.MSTests.Core.Collections
{
    [TestClass]
    public class KeyValuesCollectionTest
    {
        [TestMethod]
        public void NameStringCollectionTestEmpty()
        {
            var col = new NameStringCollection();

            foreach (KeyValuePair<string, string> row in col)
            {
                Trace.WriteLine($"{row.Key}:{row.Value}");
            }

            GC.KeepAlive(col);
        }
        
        [TestMethod]
        public void NameStringCollectionTest6Items()
        {
            var col = new NameStringCollection();
            
            col.Add("key1", "value1");
            col.Add("key1", "value2");
            col.Add("key1", "value3");
            col.Add("key2", "value1");
            col.Add("key3", "value1");
            col.Add("key3", "value2");

            var linearValues = new List<string>();
            foreach (KeyValuePair<string, string> row in col)
            {
                Trace.WriteLine($"{row.Key}:{row.Value}");
                linearValues.Add($"{row.Key}:{row.Value}");
            }
            
            Assert.AreEqual(linearValues[0], "key1:value1");
            Assert.AreEqual(linearValues[1], "key1:value2");
            Assert.AreEqual(linearValues[2], "key1:value3");
            Assert.AreEqual(linearValues[3], "key2:value1");
            Assert.AreEqual(linearValues[4], "key3:value1");
            Assert.AreEqual(linearValues[5], "key3:value2");
        }
    }
}