using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    [DataContract]
    public class UniversityData
    {
        [DataMember]
        public double UNITID { get; set; }
        [DataMember]
        public string INSTNM { get; set; }
        [DataMember]
        public string ADDR { get; set; }
        [DataMember]
        public string CITY { get; set; }
        [DataMember]
        public string STABBR { get; set; }
        [DataMember]
        public string ZIP { get; set; }
        [DataMember]
        public string WEBADDR { get; set; }
    }
}
