using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoAPITest.Data
{
    public class PageInfoModel
    {
        public string page { get; set; }
        public string per_page { get; set; }
        public string total { get; set; }
        public string total_pages { get; set; }
    }
}
