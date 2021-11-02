using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Strategy.Duck;

namespace UnitTestProject
{
    [TestClass]
    public class StrategyDuckTest
    {
        #region TestMallardDuck
        [TestMethod]
        public void TestMallardDuck()
        {
            Duck Mallard = new MallardDuck();
            Assert.AreEqual("Quack", Mallard.PerformQuack());
            Assert.AreEqual("I'm flying!!", Mallard.PerformFly());
        }
        #endregion//TestMallardDuck

        #region TestRubberDuck
        [TestMethod]
        public void TestRubberDuck()
        {
            Duck RubberDuck = new RubberDuck();
            Assert.AreEqual("Squeak", RubberDuck.PerformQuack());
            Assert.AreEqual("I can't fly.", RubberDuck.PerformFly());
        }
        #endregion//TestRubberDuck

        #region TestModelDuck
        [TestMethod]
        public void TestModelDuck()
        {
            Duck ModelDuck = new ModelDuck();
            Assert.AreEqual("Quack", ModelDuck.PerformQuack());
            Assert.AreEqual("I can't fly.", ModelDuck.PerformFly());

            ModelDuck.FlyBehavoir = new FlyRocketPowered();
            Assert.AreEqual("I'm flying with a rocket!", ModelDuck.FlyBehavoir.Fly());
            Assert.AreEqual("I'm flying with a rocket!", ModelDuck.PerformFly());

            ModelDuck.QuackBehavior = new MuteQuack();
            Assert.AreEqual("<<silence>>", ModelDuck.QuackBehavior.Quacking());
            Assert.AreEqual("<<silence>>", ModelDuck.PerformQuack());
        }
        #endregion//TestModelDuck
    }
}
