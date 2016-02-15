using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using UniversityData;
using System.ServiceModel;

namespace UniversityService
{
    public class UniversityDataService : IUniversityDataService
    {
        IEnumerable<Contracts.UniversityData> IUniversityDataService.GetAll()
        {
            IEnumerable<Contracts.UniversityData> detail = new List<Contracts.UniversityData>();

            try
            {
                IUniversityDataRepository repository = new UniversityDataRepository();
                detail = repository.GetAllData();
            }
            catch
            {
                throw new FaultException<Contracts.UniversityServiceFault>(new Contracts.UniversityServiceFault { Message = "communication error" }, "error accessing data");
            }
            return detail;

        }

        IEnumerable<Contracts.UniversityData> IUniversityDataService.GetByName(string namePart)        
        {
            IEnumerable<Contracts.UniversityData> detail = new List<Contracts.UniversityData>();

            if (String.IsNullOrEmpty(namePart) || String.IsNullOrWhiteSpace(namePart))
            {
                throw new FaultException<Contracts.UniversityServiceFault>(new Contracts.UniversityServiceFault { Message = "invalid argument" }, "null or empty search argument passed");
            }

            try
            {
                IUniversityDataRepository repository = new UniversityDataRepository();
                detail = repository.GetDataByName(namePart.Trim());
            }
            catch
            {
                throw new FaultException<Contracts.UniversityServiceFault>(new Contracts.UniversityServiceFault { Message = "communication error" }, "error accessing data");
            }
            return detail;
        }

        IEnumerable<Contracts.UniversityData> IUniversityDataService.GetByState(string stAbbr)
        {
            IEnumerable<Contracts.UniversityData> detail = new List<Contracts.UniversityData>();

            if (String.IsNullOrEmpty(stAbbr) || String.IsNullOrWhiteSpace(stAbbr))
            {
                throw new FaultException<Contracts.UniversityServiceFault>(new Contracts.UniversityServiceFault { Message = "invalid argument" }, "null or empty search argument passed");
            }
            
            if (stAbbr.Trim().Length != 2)
            {
                throw new FaultException<Contracts.UniversityServiceFault>(new Contracts.UniversityServiceFault { Message = "invalid argument" }, "state must be the two-character abbreviation");
            }

            try
            {
                IUniversityDataRepository repository = new UniversityDataRepository();
                detail = repository.GetDataByState(stAbbr);
            }
            catch
            {
                throw new FaultException<Contracts.UniversityServiceFault>(new Contracts.UniversityServiceFault { Message = "communication error" }, "error accessing data");
            }
            return detail;
        }
    }
}
