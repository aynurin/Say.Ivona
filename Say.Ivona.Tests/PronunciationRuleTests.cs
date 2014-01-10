using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using Say.Ivona.Model;

namespace Say.Ivona.Tests
{
    [TestFixture]
    internal class PronunciationRuleTests : IvonaApiTestBase
    {
        [TestFixtureTearDown]
        public void DeleteAllPronunciationRules()
        {
            IvonaRestApi api = Api();
            PronunciationRule[] rules = api.ListPronunciationRules("en");
            foreach (PronunciationRule r in rules)
                api.DeletePronunciationRule(r);
        }

        [Test]
        public void AddPronunciationRule()
        {
            DeleteAllPronunciationRules();

            IvonaRestApi api = Api();

            var newRule = new PronunciationRule {LangId = "en", Stat = 2, Key = "WSG", Value = "Water Savings Groupa"};
            Assert.IsTrue(api.AddPronunciationRule(newRule));

            PronunciationRule[] rules = Api().ListPronunciationRules(newRule.LangId);
            Assert.IsNotNull(rules);
            Trace.WriteLine("AddPronunciationRule: Rules received: " + rules.Length);
            Assert.IsTrue(rules.Length > 0);
        }

        [Test]
        public void DeletePronunciationRule()
        {
            DeleteAllPronunciationRules();

            IvonaRestApi api = Api();
            var newRule = new PronunciationRule {LangId = "en", Stat = 2, Key = "WSGs", Value = "Water Savings Groupas"};
            Assert.IsTrue(api.AddPronunciationRule(newRule));

            PronunciationRule[] rules = api.ListPronunciationRules(newRule.LangId);
            Assert.IsNotNull(rules);
            int count = rules.Length;
            Trace.WriteLine("AddPronunciationRule: Rules received: " + count);
            Assert.IsTrue(count > 0);

            DeleteAllPronunciationRules();

            rules = api.ListPronunciationRules(newRule.LangId);
            Assert.IsNotNull(rules);
            Assert.AreEqual(0, rules.Length);
        }

        [Test]
        public void ListPronunciationRules()
        {
            PronunciationRule[] rules = Api().ListPronunciationRules("en");
            Assert.IsNotNull(rules);
            Trace.WriteLine("ListPronunciationRules: Rules received: " + rules.Length);
        }

        [Test]
        public void ModifyPronunciationRule()
        {
            DeleteAllPronunciationRules();

            IvonaRestApi api = Api();
            var newRule = new PronunciationRule {LangId = "en", Stat = 2, Key = "WSGs", Value = "Water Savings Groupas"};
            Assert.IsTrue(api.AddPronunciationRule(newRule));

            PronunciationRule[] rules = api.ListPronunciationRules(newRule.LangId);
            Assert.IsNotNull(rules);
            int count = rules.Length;
            Trace.WriteLine("AddPronunciationRule: Rules received: " + count);
            Assert.IsTrue(count > 0);

            PronunciationRule rule = rules.First(r => r.Key == "WSGs");
            rule.Value = "Water Savings Groups";
            Assert.IsTrue(api.ModifyPronunciationRule(rule));

            rules = api.ListPronunciationRules(newRule.LangId);
            Assert.IsNotNull(rules);
            Assert.AreEqual(count, rules.Length);
            Assert.AreEqual(rule.Value, rules.First(r => r.Key == "WSGs").Value);
        }
    }
}