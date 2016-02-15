using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;

namespace UniversityData
{
    public class UniversityDataRepository : IUniversityDataRepository
    {
        
        IEnumerable<Contracts.UniversityData> IUniversityDataRepository.GetAllData()
        {
            using (UniversityDataEntities context = new UniversityDataEntities())
            {
                IList<Contracts.UniversityData> details = new List<Contracts.UniversityData>();
                IEnumerable<UniversityDetail> fullDetails = context.UniversityDetails;
                foreach (UniversityDetail item in fullDetails)
                {
                    details.Add(new Contracts.UniversityData
                    {
                        UNITID = item.UNITID.Value,
                        INSTNM = item.INSTNM,
                        ADDR = item.ADDR,
                        CITY = item.CITY,
                        STABBR = item.STABBR,
                        ZIP = item.ZIP,
                        WEBADDR = item.WEBADDR
                    });
                }

                return details;
            }
        }

        IEnumerable<Contracts.UniversityData> IUniversityDataRepository.GetDataByName(string namePart)
        {
            using (UniversityDataEntities context = new UniversityDataEntities())
            {
                IList<Contracts.UniversityData> details = new List<Contracts.UniversityData>();
                IEnumerable<UniversityDetail> fullDetails = context.UniversityDetails.Where(n => n.INSTNM.Contains(namePart)).ToList();
                foreach (UniversityDetail item in fullDetails)
                {
                    details.Add(new Contracts.UniversityData
                    {
                        UNITID = item.UNITID.Value,
                        INSTNM = item.INSTNM,
                        ADDR = item.ADDR,
                        CITY = item.CITY,
                        STABBR = item.STABBR,
                        ZIP = item.ZIP,
                        WEBADDR = item.WEBADDR
                    });
                }

                return details;
            }
        }

        IEnumerable<Contracts.UniversityData> IUniversityDataRepository.GetDataByState(string state)
        {
            using (UniversityDataEntities context = new UniversityDataEntities())
            {
                IList<Contracts.UniversityData> details = new List<Contracts.UniversityData>();
                IEnumerable<UniversityDetail> fullDetails = context.UniversityDetails.Where(n => n.STABBR.Equals(state)).ToList();
                foreach (UniversityDetail item in fullDetails)
                {
                    details.Add(new Contracts.UniversityData
                    {
                        UNITID = item.UNITID.Value,
                        INSTNM = item.INSTNM,
                        ADDR = item.ADDR,
                        CITY = item.CITY,
                        STABBR = item.STABBR,
                        ZIP = item.ZIP,
                        WEBADDR = item.WEBADDR
                    });
                }

                return details;
            }
        }
    }
}
