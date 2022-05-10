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
    public class RaceMasterServiceUnitTest
    {
        Mock<IHttpContextAccessor> httpContextProcessor;
        Mock<ICacheService> cacheService;
        Mock<IMapper> mapper;
        MalaysiaDbContext malaysiaDbContext;
        public RaceMasterServiceUnitTest()
        {
            httpContextProcessor = new Mock<IHttpContextAccessor>();
            cacheService = new Mock<ICacheService>();
            mapper = new Mock<IMapper>();
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "MalaysiaDatabase")
                            .Options;
            malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
        }

        #region Race Master CRUD Operation Unit Test Cases
        #region Add Race Master Unit Test Cases
        [Fact]
        public void Test_AddRaceMaster_ReturnsErrorFalseResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            RaceMasterRequest raceMasterRequest = new RaceMasterRequest() { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" };
            CaRaceMaster mapperResp = new CaRaceMaster() { Id = 1, Description = "test" };
            mapper.Setup(x => x.Map<RaceMasterRequest, CaRaceMaster>(raceMasterRequest)).Returns(mapperResp);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaRaceMasters.Add(new CaRaceMaster() { Id = 2, Description = "test" });
            malaysiaDbContext.SaveChangesAsync();
            var raceMasterService = new RaceMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = raceMasterService.AddRaceMaster(raceMasterRequest).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_AddRaceMaster_ReturnsErrorTrueResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            //RaceMasterRequest requestData = new RaceMasterRequest { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" };
            RaceMasterRequest requestData = null;
            CaRaceMaster respData = new CaRaceMaster { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" };
            mapper.Setup(x => x.Map<RaceMasterRequest, CaRaceMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaRaceMasters.Add(new CaRaceMaster() { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChangesAsync();

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var raceMasterService = new RaceMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = raceMasterService.AddRaceMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Update Race Master Unit Test Cases
        [Fact]
        public void Test_UpdateRaceMaster_ReturnsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaRaceMasters.Add(new CaRaceMaster { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            RaceMasterRequest raceMasterInput = new RaceMasterRequest() { Id = 1, Description = "Test1", FacilityCode = "Test" };
            CaRaceMaster mapperResp = new CaRaceMaster() { Id = 1, Description = "Test1", FacilityCode = "Test" };
            mapper.Setup(x => x.Map<RaceMasterRequest, CaRaceMaster>(It.IsAny<RaceMasterRequest>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)
            var raceMasterService = new RaceMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = raceMasterService.UpdateRaceMaster(raceMasterInput).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_UpdateRaceMaster_ReturnsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaRaceMasters.Add(new CaRaceMaster { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            // RaceMasterRequest requestData = new RaceMasterRequest { Id = 1, Description = "Test1", FacilityCode = "Test"};
            RaceMasterRequest requestData = null;
            CaRaceMaster respData = new CaRaceMaster { Id = 1, Description = "Test1", FacilityCode = "Test" };
            mapper.Setup(x => x.Map<RaceMasterRequest, CaRaceMaster>(It.IsAny<RaceMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var raceMasterService = new RaceMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = raceMasterService.UpdateRaceMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Delete Race Master Unit Test Cases
        [Fact]
        public void Test_DeleteRaceMaster_ReturnsErrorFalseResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaRaceMasters.Add(new CaRaceMaster { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            RaceMasterRequest raceMasterInput = new RaceMasterRequest() { Id = 1, Description = "Test", FacilityCode = "Test" };
            CaRaceMaster mapperResp = new CaRaceMaster() { Id = 1, Description = "Test", IsDeleted = true };
            mapper.Setup(x => x.Map<RaceMasterRequest, CaRaceMaster>(It.IsAny<RaceMasterRequest>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var raceMasterService = new RaceMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = raceMasterService.DeleteRaceMaster(raceMasterInput.Id).Result;

            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_DeleteRaceMaster_ReturnsErrorTrueResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaRaceMasters.Add(new CaRaceMaster { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            RaceMasterRequest raceMasterInput = new RaceMasterRequest() { Id = 1, Description = "Test", FacilityCode = "Test" };
            CaRaceMaster mapperResp = new CaRaceMaster() { Id = 1, Description = "Test" };
            mapper.Setup(x => x.Map<RaceMasterRequest, CaRaceMaster>(It.IsAny<RaceMasterRequest>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var raceMasterService = new RaceMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = raceMasterService.DeleteRaceMaster(raceMasterInput.Id).Result;

            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get Race Master By Id Unit Test Case
        [Fact]
        public void Test_GetRaceMasterById_ReturnsErrorFalseResponse()
        {
            malaysiaDbContext.CaRaceMasters.Add(new CaRaceMaster { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new RaceMasterResponse() { Id = 1, Description = "test" };
            mapper.Setup(x => x.Map<CaRaceMaster, RaceMasterResponse>(It.IsAny<CaRaceMaster>())).Returns(mapperResp);
            var raceMasterService = new RaceMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = raceMasterService.GetRaceMasterById(mapperResp.Id).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get Race Master Unit Test Case
        [Fact]
        public void Test_GetRaceMaster_ReturnsDataCount()
        {

            var httpContextProcessor = new Mock<IHttpContextAccessor>();
            var cacheService = new Mock<ICacheService>();
            var mapper = new Mock<IMapper>();
            malaysiaDbContext.CaRaceMasters.Add(new CaRaceMaster { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, Code = "Testing", Smrpcode = "TST", Smrpdescription = "Testings", FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<RaceMasterResponse>() { new RaceMasterResponse { Id = 1, Description = "test" } };
            mapper.Setup(x => x.Map<IEnumerable<RaceMasterResponse>>(It.IsAny<List<CaRaceMaster>>())).Returns(mapperResp);
            var raceMasterService = new RaceMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var raceMasters = raceMasterService.GetRaceMaster().Result.ToList();
            Assert.NotNull(raceMasters);
            Assert.Equal(1, Convert.ToInt32(raceMasters.Count()));
        }
        #endregion
        #endregion
    }
}
