using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    [ServiceContract]
    public interface IUniversityDataService
    {
        [OperationContract]
        [FaultContract(typeof(UniversityServiceFault))]
        IEnumerable<UniversityData> GetAll();
        [OperationContract]
        [FaultContract(typeof(UniversityServiceFault))]
        IEnumerable<UniversityData> GetByName(string namePart);
        [OperationContract]
        [FaultContract(typeof(UniversityServiceFault))]
        IEnumerable<UniversityData> GetByState(string stAbbr);
    }
}
