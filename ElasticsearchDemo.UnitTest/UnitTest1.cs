using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nest;

namespace ElasticsearchDemo.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private readonly ElasticClient esClient = new ElasticClient(ESSetting.ConnectionSettings);

        [TestMethod]
        public void SimpleQueryTest()
        {
            var rd = new System.Random();

            var tq = new TermQuery()
            {
                Field = "eventid",
                Value = rd.Next(1, 1000)
            };

            var sr = new SearchRequest("meetup", "events")
            {
                Query = tq
            };

            var result = esClient.Search<MeetupEvents>(sr);
            var items = result.Documents.ToList();
            Assert.IsTrue(items.Any());
        }

        [TestMethod]
        public void RegexpQueryTest()
        {
            var rq = new RegexpQuery()
            {
                Field = "eventname",
                Value = @"Test_[0-9]\d{5}(?!\d)"
            };

            var sr = new SearchRequest();
            sr.Query = rq;

            var result = esClient.Search<MeetupEvents>(sr);
            var items = result.Documents.ToList();
            Assert.IsTrue(items.Any());
        }

    }
}
