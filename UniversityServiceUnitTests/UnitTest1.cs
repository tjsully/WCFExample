using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Contracts;
using UniversityData;
using UniversityService;

namespace UniversityServiceUnitTests
{
    [TestClass]
    public class UniversityServiceTests
    {
        [TestMethod]
        public void GetByName_ValidParameter_DataReturned()
        {
            //arrange
            IUniversityDataService service = new UniversityDataService(new FakeUniversityDataRepository());

            //act
            IEnumerable<Contracts.UniversityData> detail = service.GetByName("Technical");

            //assert
            Assert.IsTrue(detail.ToList().Count() == 2);
        }
        [TestMethod]
        public void GetByState_ValidParameter_DataReturned()
        {
            //arrange
            IUniversityDataService service = new UniversityDataService(new FakeUniversityDataRepository());

            //act
            IEnumerable<Contracts.UniversityData> detail = service.GetByState("WI");

            //assert
            Assert.IsTrue(detail.ToList().Count() == 2);
        }

        [TestMethod]
        public void GetAll_ValidParameter_DataReturned()
        {
            //arrange
            IUniversityDataService service = new UniversityDataService(new FakeUniversityDataRepository());

            //act
            IEnumerable<Contracts.UniversityData> detail = service.GetAll();

            //assert
            Assert.IsTrue(detail.ToList().Count() == 2);
        }
    }

    internal class FakeUniversityDataRepository : IUniversityDataRepository
    {
        IEnumerable<Contracts.UniversityData> IUniversityDataRepository.GetAllData()
        {
            return GetFakeData();
        }

        IEnumerable<Contracts.UniversityData> IUniversityDataRepository.GetDataByName(string namePart)
        {
            return GetFakeData();
        }

        IEnumerable<Contracts.UniversityData> IUniversityDataRepository.GetDataByState(string state)
        {
            return GetFakeData();
        }

        IEnumerable<Contracts.UniversityData> GetFakeData()
        {
            IList<Contracts.UniversityData> result = new List<Contracts.UniversityData>();
            result.Add(new Contracts.UniversityData
            {
                ADDR = "address1",
                CITY = "city1",
                INSTNM = "institution1",
                STABBR = "WI",
                UNITID = 1,
                WEBADDR = "www.institution1.com",
                ZIP = "55555"
            });
            result.Add(new Contracts.UniversityData
            {
                ADDR = "address2",
                CITY = "city2",
                INSTNM = "institution2",
                STABBR = "WI",
                UNITID = 1,
                WEBADDR = "www.institution2.com",
                ZIP = "55555"
            });

            return (IEnumerable<Contracts.UniversityData>)result;
        }

    }
}
