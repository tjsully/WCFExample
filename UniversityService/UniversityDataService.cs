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
        private readonly IUniversityDataRepository _repository;
        
        public UniversityDataService(IUniversityDataRepository repository)
        {
            _repository = repository;
        }

        IEnumerable<Contracts.UniversityData> IUniversityDataService.GetAll()
        {
            IEnumerable<Contracts.UniversityData> detail = new List<Contracts.UniversityData>();

            try
            {
                //IUniversityDataRepository repository = new UniversityDataRepository();
                detail = _repository.GetAllData();
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
                //IUniversityDataRepository repository = new UniversityDataRepository();
                detail = _repository.GetDataByName(namePart.Trim());
            }
            catch (Exception e)
            {
                throw new FaultException<Contracts.UniversityServiceFault>(new Contracts.UniversityServiceFault { Message = "communication error" }, e.Message);
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
                //IUniversityDataRepository repository = new UniversityDataRepository();
                detail = _repository.GetDataByState(stAbbr);
            }
            catch (Exception e)
            {
                throw new FaultException<Contracts.UniversityServiceFault>(new Contracts.UniversityServiceFault { Message = "communication error" }, e.Message);
            }
            return detail;
        }
    }
}
