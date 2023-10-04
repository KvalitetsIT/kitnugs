using KitNugs.Repository;

namespace UnitTest.Repository
{
    public class HelloTableTests {
        [Test]
        public void TestMethod()
        {
            var f = new TestDatabaseFixture().CreateContext();

            f.HelloTable.Add(new HelloTable { Created = DateTimeOffset.Now.ToUniversalTime() });
            f.SaveChanges();

            var count = f.HelloTable.Count();
            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void TestAnotherMethod()
        {
            var f = new TestDatabaseFixture().CreateContext();

            f.HelloTable.Add(new HelloTable { Created = DateTimeOffset.Now.ToUniversalTime() });
            f.SaveChanges();

            var count = f.HelloTable.Count();
            Assert.That(count, Is.EqualTo(1));
        }
    }
}
