using BCH.Comex.Common.ExceptionHandling.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BCH.Comex.Common.Test
{
    
    
    /// <summary>
    ///This is a test class for ExceptionPolicyElementCollectionTest and is intended
    ///to contain all ExceptionPolicyElementCollectionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ExceptionPolicyElementCollectionTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddTest()
        {
            ExceptionPolicyElementCollection target = new ExceptionPolicyElementCollection();
            ExceptionPolicyElement policy = new ExceptionPolicyElement("PolíticaReemplazar");
            target.Add(policy);
            Assert.IsTrue(target.Count > 0);
        }

        /// <summary>
        ///A test for Clear
        ///</summary>
        [TestMethod()]
        public void ClearTest()
        {
            ExceptionPolicyElementCollection target = new ExceptionPolicyElementCollection();
            ExceptionPolicyElement policy = new ExceptionPolicyElement("PolíticaReemplazar");
            target.Add(policy);
            target.Clear();
            Assert.IsTrue(target.Count == 0);
        }

        /// <summary>
        ///A test for IndexOf
        ///</summary>
        [TestMethod()]
        public void IndexOfTest()
        {
            ExceptionPolicyElementCollection target = new ExceptionPolicyElementCollection();
            ExceptionPolicyElement policy = new ExceptionPolicyElement("PolíticaReemplazar");
            target.Add(policy);
            int expected = 0; 
            int actual;
            actual = target.IndexOf(policy);
            Assert.AreEqual(expected, actual);
            
        }

        /// <summary>
        ///A test for Remove
        ///</summary>
        [TestMethod()]
        public void RemoveWithNameTest()
        {
            ExceptionPolicyElementCollection target = new ExceptionPolicyElementCollection(); 
            string name = "Policy";
            ExceptionPolicyElement policy = new ExceptionPolicyElement(name);
            target.Add(policy);
            Assert.IsTrue(target.Count == 1);
            target.Remove(name);
            Assert.IsTrue(target.Count == 0);
            
        }

        /// <summary>
        ///A test for Remove
        ///</summary>
        [TestMethod()]
        public void RemoveWithPolicyTest()
        {
            ExceptionPolicyElementCollection target = new ExceptionPolicyElementCollection();
            ExceptionPolicyElement policy = new ExceptionPolicyElement("PolicyRemove");
            target.Add(policy);
            Assert.IsTrue(target.Count == 1);
            target.Remove(policy);
            Assert.IsTrue(target.Count == 0);
        }

        /// <summary>
        ///A test for RemoveAt
        ///</summary>
        [TestMethod()]
        public void RemoveAtTest()
        {
            ExceptionPolicyElementCollection target = new ExceptionPolicyElementCollection(); 
            ExceptionPolicyElement policyX = new ExceptionPolicyElement("PolicyX");
            ExceptionPolicyElement policyY = new ExceptionPolicyElement("PolicyY");
            target.Add(policyX);
            target.Add(policyY);
            target.RemoveAt(1);
            Assert.IsTrue(target.Count == 1);
            Assert.AreEqual(-1, target.IndexOf(policyY)); //No existe el elemento
            target.RemoveAt(0);
            Assert.IsTrue(target.Count == 0);
            Assert.AreEqual(-1, target.IndexOf(policyX)); //No existe el elemento
        }
    }
}
