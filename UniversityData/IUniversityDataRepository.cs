using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace UniversityData
{
    public interface IUniversityDataRepository
    {
        IEnumerable<Contracts.UniversityData> GetAllData();
        IEnumerable<Contracts.UniversityData> GetDataByName(string namePart);
        IEnumerable<Contracts.UniversityData> GetDataByState(string state);
    }
}
