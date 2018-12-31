using System;
using System.Collections.Generic;
using System.Linq;
using Nest;

namespace ElasticsearchDemo
{
    public static class Test
    {
        public static void Test1()
        {
            var seed = 1000;
            var datas = new List<MeetupEvents>(seed);
            var rd = new Random();

            for (int i = 0; i < seed; i++)
            {
                var data = new MeetupEvents
                {
                    eventid = i + 1,
                    orignalid = Guid.NewGuid().ToString("N"),
                    eventname = "Test_" + rd.Next(1, 999999).ToString("000000"),
                    description = ""
                };
                datas.Add(data);
            }

            ESProvider es = new ESProvider();
            es.BulkPopulateIndex(datas);
        }

        public static void QueryTest1()
        {
            var client = new ElasticClient(ESSetting.ConnectionSettings);

            SearchRequest sr = new SearchRequest("meetup", "events");

            TermQuery tq = new TermQuery();
            tq.Field = "eventid";
            tq.Value = "100";
            sr.Query = tq;

            var result = client.Search<MeetupEvents>(sr);
            var ddd =  result.Documents.ToList();

        }




    }
}
