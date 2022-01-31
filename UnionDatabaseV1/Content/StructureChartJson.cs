using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnionDatabaseV1.Content
{
    public class StructureChartJson
    {
        public string name { get; set; }
        public string title { get; set; }
        public StructureChartJson[] children { get; set; }
    }
}