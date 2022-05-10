using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CA_MCare21_MasterAPI.Data;
using CA_MCare21_MasterAPI.Entities;
using AutoMapper;
using CA_MCare21_MasterAPI.Services;
using CA_MCare21_MasterAPI.Models;
using CA_MCare21_MasterAPI.Repositories;
using Microsoft.AspNetCore.Http;
using CA_MCare21_CrossCuttingConcern.Caching;
using Moq;
using Microsoft.EntityFrameworkCore;

namespace CA_MCare21_MasterAPIUnitTest.ServiceUnitTest
{
    public class CountryMasterServiceUnitTest
    {
        Mock<IHttpContextAccessor> httpContextProcessor;
        Mock<ICacheService> cacheService;
        Mock<IMapper> mapper;
        MalaysiaDbContext malaysiaDbContext;
        public CountryMasterServiceUnitTest()
        {
            httpContextProcessor = new Mock<IHttpContextAccessor>();
            cacheService = new Mock<ICacheService>();
            mapper = new Mock<IMapper>();
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "countryMasterMalaysiaDatabase")
                            .Options;
            malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
        }

        #region Country Master CRUD Operation Unit Test Cases
        #region Add Country Master Unit Test Cases
        [Fact]
        public void Test_AddCountry_Returns_IsErrorFalseResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            CountryMasterRequest requestData = new CountryMasterRequest { Id = 1, Description = "TestCountry", Nationality = "TestCountry", NationalityRate = 1, CountryCode = "TST", CountryCodeNo = 1, Smrpcode = "TS", Smrpdescription = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            CaCountryMaster respData = new CaCountryMaster { Id = 1, Description = "TestCountry", Nationality = "TestCountry" };
            mapper.Setup(x => x.Map<CountryMasterRequest, CaCountryMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaCountryMasters.Add(new CaCountryMaster() { Id = 2, Description = "India", Nationality = "India" });
            malaysiaDbContext.SaveChangesAsync();

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var countryMasterService = new CountryMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = countryMasterService.AddCountry(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_AddCountry_Returns_IsErrorTrueResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            CountryMasterRequest requestData = null;
            CaCountryMaster respData = new CaCountryMaster { Id = 1, Description = "TestCountry", Nationality = "TestCountry" };
            mapper.Setup(x => x.Map<CountryMasterRequest, CaCountryMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaCountryMasters.Add(new CaCountryMaster() { Id = 2, Description = "India", Nationality = "India" });
            malaysiaDbContext.SaveChangesAsync();

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var countryMasterService = new CountryMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = countryMasterService.AddCountry(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Update Country Master Unit Test Cases
        [Fact]
        public void Test_UpdateCountry_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaCountryMasters.Add(new CaCountryMaster { Id = 1, Description = "TestCountry", Nationality = "TestCountry", NationalityRate = 1, CountryCode = "TST", CountryCodeNo = 1, Smrpcode = "TS", Smrpdescription = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            CountryMasterRequest requestData = new CountryMasterRequest { Id = 1, Description = "TestCountry", Nationality = "TestCountry", NationalityRate = 1, CountryCode = "TST", CountryCodeNo = 1, Smrpcode = "TS", Smrpdescription = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            CaCountryMaster respData = new CaCountryMaster { Id = 1, Description = "TestCountry", Nationality = "TestCountry" };
            mapper.Setup(x => x.Map<CountryMasterRequest, CaCountryMaster>(It.IsAny<CountryMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var countryMasterService = new CountryMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = countryMasterService.UpdateCountry(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_UpdateCountry_Returns_IsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaCountryMasters.Add(new CaCountryMaster { Id = 1, Description = "TestCountry", Nationality = "TestCountry", NationalityRate = 1, CountryCode = "TST", CountryCodeNo = 1, Smrpcode = "TS", Smrpdescription = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            CountryMasterRequest requestData = null;
            CaCountryMaster respData = new CaCountryMaster { Id = 1, Description = "TestCountry", Nationality = "TestCountry" };
            mapper.Setup(x => x.Map<CountryMasterRequest, CaCountryMaster>(It.IsAny<CountryMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var countryMasterService = new CountryMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = countryMasterService.UpdateCountry(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Delete Country Master Unit Test Cases
        [Fact]
        public void Test_DeleteCountry_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaCountryMasters.Add(new CaCountryMaster { Id = 1, Description = "TestCountry", Nationality = "TestCountry", NationalityRate = 1, CountryCode = "TST", CountryCodeNo = 1, Smrpcode = "TS", Smrpdescription = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            CountryMasterRequest requestData = new CountryMasterRequest { Id = 1, Description = "TestCountry", Nationality = "TestCountry", NationalityRate = 1, CountryCode = "TST", CountryCodeNo = 1, Smrpcode = "TS", Smrpdescription = "Test", FacilityCode = "KLAN" };
            CaCountryMaster respData = new CaCountryMaster { Id = 1, Description = "TestCountry", Nationality = "TestCountry", IsDeleted = true };
            mapper.Setup(x => x.Map<CountryMasterRequest, CaCountryMaster>(It.IsAny<CountryMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var countryMasterService = new CountryMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = countryMasterService.DeleteCountry(requestData.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_DeleteCountry_Returns_IsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaCountryMasters.Add(new CaCountryMaster { Id = 1, Description = "TestCountry", Nationality = "TestCountry", NationalityRate = 1, CountryCode = "TST", CountryCodeNo = 1, Smrpcode = "TS", Smrpdescription = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            CountryMasterRequest requestData = new CountryMasterRequest { Id = 1, Description = "TestCountry", Nationality = "TestCountry", NationalityRate = 1, CountryCode = "TST", CountryCodeNo = 1, Smrpcode = "TS", Smrpdescription = "Test", FacilityCode = "KLAN" };
            CaCountryMaster respData = new CaCountryMaster { Id = 1, Description = "TestCountry", Nationality = "TestCountry" };
            mapper.Setup(x => x.Map<CountryMasterRequest, CaCountryMaster>(It.IsAny<CountryMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var countryMasterService = new CountryMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = countryMasterService.DeleteCountry(requestData.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get Country Master By Id Unit Test Case
        [Fact]
        public void Test_GetCountryById_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaCountryMasters.Add(new CaCountryMaster { Id = 1, Description = "TestCountry", Nationality = "TestCountry", NationalityRate = 1, CountryCode = "TST", CountryCodeNo = 1, Smrpcode = "TS", Smrpdescription = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new CountryMasterResponse { Id = 1, Description = "TestCountry", Nationality = "TestCountry", NationalityRate = 1, CountryCode = "TST", CountryCodeNo = 1, Smrpcode = "TS", Smrpdescription = "Test", FacilityCode = "KLAN" };
            mapper.Setup(x => x.Map<CaCountryMaster, CountryMasterResponse>(It.IsAny<CaCountryMaster>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)
            //Arrange
            var countryMasterService = new CountryMasterService (httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = countryMasterService.GetCountryById(mapperResp.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get Country Master Unit Test Case
        [Fact]
        public void Test_GetCountryMaster_Returns_DataCount()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaCountryMasters.Add(new CaCountryMaster { Id = 1, Description = "TestCountry", Nationality = "TestCountry", NationalityRate = 1, CountryCode = "TST", CountryCodeNo = 1, Smrpcode = "TS", Smrpdescription = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<CountryMasterResponse>() { new CountryMasterResponse { Id = 1, Description = "TestCountry", Nationality = "TestCountry", NationalityRate = 1, CountryCode = "TST", CountryCodeNo = 1, Smrpcode = "TS", Smrpdescription = "Test", FacilityCode = "KLAN" } };
            mapper.Setup(x => x.Map<List<CountryMasterResponse>>(It.IsAny<List<CaCountryMaster>>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)  
            //Arrange
            var countryMasterService = new CountryMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = countryMasterService.GetCountryMaster().Result.ToList();
            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, Convert.ToInt32(result.Count()));
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #endregion
    }
}
