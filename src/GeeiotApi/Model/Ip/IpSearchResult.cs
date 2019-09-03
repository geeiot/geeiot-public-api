using GeeiotApi.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeeiotApi.Model.Ip
{
    public class IpSearchResult : ResultModel
    {
        public string Ip { get; set; }

        public string Local { get; set; }

        public long Time { get; set; }
    }
}
