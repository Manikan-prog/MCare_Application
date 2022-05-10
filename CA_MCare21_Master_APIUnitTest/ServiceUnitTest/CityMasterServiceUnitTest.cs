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
    public class CityMasterServiceUnitTest
    {
        Mock<IHttpContextAccessor> httpContextProcessor;
        Mock<ICacheService> cacheService;
        Mock<IMapper> mapper;
        MalaysiaDbContext malaysiaDbContext;
        public CityMasterServiceUnitTest()
        {
            httpContextProcessor = new Mock<IHttpContextAccessor>();
            cacheService = new Mock<ICacheService>();
            mapper = new Mock<IMapper>();
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "cityMasterMalaysiaDatabase")
                            .Options;
            malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
        }

        #region City Master CRUD Operation Unit Test Cases
        #region Add City Master Unit Test Cases
        [Fact]
        public void Test_AddCity_Returns_IsErrorFalseResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            CityMasterRequest requestData = new CityMasterRequest { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI", Smrpcode = "335", Smrpdescription = "Ayer Baloi", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            CaCityMaster respData = new CaCityMaster { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI" };
            mapper.Setup(x => x.Map<CityMasterRequest, CaCityMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaCityMasters.Add(new CaCityMaster() { Id = 2, StateId = 9, CityCode = "TST", Description = "Test City" });
            malaysiaDbContext.SaveChangesAsync();

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var cityMasterService = new CityMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = cityMasterService.AddCity(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_AddCity_Returns_IsErrorTrueResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            //CityMasterRequest requestData = new CityMasterRequest { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI", Smrpcode = "335", Smrpdescription = "Ayer Baloi", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            CityMasterRequest requestData = null;
            CaCityMaster respData = new CaCityMaster { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI" };
            mapper.Setup(x => x.Map<CityMasterRequest, CaCityMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaCityMasters.Add(new CaCityMaster() { Id = 2, StateId = 9, CityCode = "TST", Description = "Test City" });
            malaysiaDbContext.SaveChangesAsync();

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var cityMasterService = new CityMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = cityMasterService.AddCity(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Update City Master Unit Test Cases
        [Fact]
        public void Test_UpdateCity_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaCityMasters.Add(new CaCityMaster { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI", Smrpcode = "335", Smrpdescription = "Ayer Baloi", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            CityMasterRequest requestData = new CityMasterRequest { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI", Smrpcode = "335", Smrpdescription = "Ayer Baloi", FacilityCode = "KLAN" };
            CaCityMaster respData = new CaCityMaster { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI" };
            mapper.Setup(x => x.Map<CityMasterRequest, CaCityMaster>(It.IsAny<CityMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var cityMasterService = new CityMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = cityMasterService.UpdateCity(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_UpdateCity_Returns_IsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaCityMasters.Add(new CaCityMaster { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI", Smrpcode = "335", Smrpdescription = "Ayer Baloi", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            CityMasterRequest requestData = null;
            CaCityMaster respData = new CaCityMaster { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI" };
            mapper.Setup(x => x.Map<CityMasterRequest, CaCityMaster>(It.IsAny<CityMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var cityMasterService = new CityMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = cityMasterService.UpdateCity(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Delete City Master Unit Test Cases
        [Fact]
        public void Test_DeleteCity_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaCityMasters.Add(new CaCityMaster { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI", Smrpcode = "335", Smrpdescription = "Ayer Baloi", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            CityMasterRequest requestData = new CityMasterRequest { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI", Smrpcode = "335", Smrpdescription = "Ayer Baloi", FacilityCode = "KLAN" };
            CaCityMaster respData = new CaCityMaster { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI", IsDeleted = true };
            mapper.Setup(x => x.Map<CityMasterRequest, CaCityMaster>(It.IsAny<CityMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var cityMasterService = new CityMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = cityMasterService.DeleteCity(requestData.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_DeleteCity_Returns_IsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaCityMasters.Add(new CaCityMaster { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI", Smrpcode = "335", Smrpdescription = "Ayer Baloi", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            CityMasterRequest requestData = new CityMasterRequest { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI", Smrpcode = "335", Smrpdescription = "Ayer Baloi", FacilityCode = "KLAN" };
            CaCityMaster respData = new CaCityMaster { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI" };
            mapper.Setup(x => x.Map<CityMasterRequest, CaCityMaster>(It.IsAny<CityMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var cityMasterService = new CityMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = cityMasterService.DeleteCity(requestData.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get City Master By Id Unit Test Case
        [Fact]
        public void Test_GetCityById_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaCityMasters.Add(new CaCityMaster { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI", Smrpcode = "335", Smrpdescription = "Ayer Baloi", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new CityMasterResponse { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI", Smrpcode = "335", Smrpdescription = "Ayer Baloi" };
            mapper.Setup(x => x.Map<CaCityMaster, CityMasterResponse>(It.IsAny<CaCityMaster>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)
            //Arrange
            var cityMasterService = new CityMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = cityMasterService.GetCityById(mapperResp.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get City Master Unit Test Case
        [Fact]
        public void Test_GetCityMaster_Returns_DataCount()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaCityMasters.Add(new CaCityMaster { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI", Smrpcode = "335", Smrpdescription = "Ayer Baloi", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<CityMasterResponse>() { new CityMasterResponse { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI", Smrpcode = "335", Smrpdescription = "Ayer Baloi" } };
            mapper.Setup(x => x.Map<List<CityMasterResponse>>(It.IsAny<List<CaCityMaster>>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)  
            //Arrange
            var cityMasterService = new CityMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = cityMasterService.GetCityMaster().Result.ToList();
            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, Convert.ToInt32(result.Count()));
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #endregion
    }
}
