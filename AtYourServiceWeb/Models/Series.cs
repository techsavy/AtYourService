using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtYourService.Web.Models
{
    public class Series<TKey, TValue>
    {
        public IEnumerable<TKey> Keys { get; set; }

        public IEnumerable<TValue> Values { get; set; }
    }
}