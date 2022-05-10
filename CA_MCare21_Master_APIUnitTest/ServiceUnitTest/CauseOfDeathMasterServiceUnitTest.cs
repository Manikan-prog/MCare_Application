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
    public class CauseOfDeathMasterServiceUnitTest
    {
        Mock<IHttpContextAccessor> httpContextProcessor;
        Mock<ICacheService> cacheService;
        Mock<IMapper> mapper;
        MalaysiaDbContext malaysiaDbContext;
        public CauseOfDeathMasterServiceUnitTest()
        {
            httpContextProcessor = new Mock<IHttpContextAccessor>();
            cacheService = new Mock<ICacheService>();
            mapper = new Mock<IMapper>();

            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "CauseOfDeathMasterMalaysiaDatabase")
                            .Options;
            malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
        }

        #region CauseOfDeathMaster service Unit test cases for GetCauseOfDeathMaster
        [Fact]
        public void Test_GetCauseOfDeathMaster_ReturnsDataCount()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaCauseOfDeathMasters.Add(new CaCauseOfDeathMaster { Id = 1, Description = "Test", Code = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<CauseOfDeathMasterResponse>() { new CauseOfDeathMasterResponse { Id = 1, Description = "test" } };
            mapper.Setup(x => x.Map<IEnumerable<CauseOfDeathMasterResponse>>(It.IsAny<List<CaCauseOfDeathMaster>>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var causeOfDeathMasterService = new CauseOfDeathMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = causeOfDeathMasterService.GetCauseOfDeathMaster().Result.ToList();

            Assert.NotNull(result);
            Assert.Equal(1, Convert.ToInt32(result.Count()));
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region CauseOfDeathMaster service Unit test cases for GetCauseOfDeathMasterById
        [Fact]
        public void Test_GetCauseOfDeathMasterById_ReturnsErrorFalseResponse()
        {
            malaysiaDbContext.CaCauseOfDeathMasters.Add(new CaCauseOfDeathMaster { Id = 1, Description = "Test", Code = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new CauseOfDeathMasterResponse() { Id = 1, Description = "test" };
            mapper.Setup(x => x.Map<CaCauseOfDeathMaster, CauseOfDeathMasterResponse>(It.IsAny<CaCauseOfDeathMaster>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var causeOfDeathMasterService = new CauseOfDeathMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = causeOfDeathMasterService.GetCauseOfDeathMasterById(mapperResp.Id).Result;

            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region CauseOfDeathMaster service Unit test cases for DeleteCauseOfDeathMaster
        [Fact]
        public void Test_DeleteCauseOfDeathMaster_ReturnsErrorFalseResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaCauseOfDeathMasters.Add(new CaCauseOfDeathMaster { Id = 1, Description = "Test", Code = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            CauseOfDeathMasterRequest causeOfDeathMasterInput = new CauseOfDeathMasterRequest() { Id = 1, Description = "Test", Code = "Test", FacilityCode = "Test" };
            CaCauseOfDeathMaster mapperResp = new CaCauseOfDeathMaster() { Id = 1, Description = "Test", Code = "Test", FacilityCode = "Test", IsDeleted =true};
            mapper.Setup(x => x.Map<CauseOfDeathMasterRequest, CaCauseOfDeathMaster>(It.IsAny<CauseOfDeathMasterRequest>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var causeOfDeathMasterService = new CauseOfDeathMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = causeOfDeathMasterService.DeleteCauseOfDeathMaster(causeOfDeathMasterInput.Id).Result;

            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_DeleteCauseOfDeathMaster_ReturnsErrorTrueResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaCauseOfDeathMasters.Add(new CaCauseOfDeathMaster { Id = 1, Description = "Test", Code = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            CauseOfDeathMasterRequest causeOfDeathMasterInput = new CauseOfDeathMasterRequest() { Id = 1, Description = "Test", Code = "Test", FacilityCode = "Test" };
            CaCauseOfDeathMaster mapperResp = new CaCauseOfDeathMaster() { Id = 1, Description = "Test", Code = "Test", FacilityCode = "Test" };
            mapper.Setup(x => x.Map<CauseOfDeathMasterRequest, CaCauseOfDeathMaster>(It.IsAny<CauseOfDeathMasterRequest>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var causeOfDeathMasterService = new CauseOfDeathMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = causeOfDeathMasterService.DeleteCauseOfDeathMaster(causeOfDeathMasterInput.Id).Result;

            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region CauseOfDeathMaster service Unit test cases for AddGetCauseOfDeathMaster
        [Fact]
        public void Test_AddGetCauseOfDeathMaster_ReturnsErrorFalseResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            CauseOfDeathMasterRequest causeOfDeathMasterInput = new CauseOfDeathMasterRequest() { Id = 1, Description = "Test", Code = "Test", FacilityCode = "Test" };
            CaCauseOfDeathMaster mapperResp = new CaCauseOfDeathMaster() { Id = 1, Description = "Test" };
            mapper.Setup(x => x.Map<CauseOfDeathMasterRequest, CaCauseOfDeathMaster>(causeOfDeathMasterInput)).Returns(mapperResp);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaCauseOfDeathMasters.Add(new CaCauseOfDeathMaster() { Id = 2, Description = "Test" });
            malaysiaDbContext.SaveChangesAsync();

            //Execute method of SUT (CoreMasterRepository)
            var causeOfDeathMasterService = new CauseOfDeathMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act

            var result = causeOfDeathMasterService.AddCauseOfDeathMaster(causeOfDeathMasterInput).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_AddCauseOfDeathMaster_Returns_ReturnsErrorTrueResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            CauseOfDeathMasterRequest requestData = null;
            CaCauseOfDeathMaster respData = new CaCauseOfDeathMaster { Id = 1, Code = "INA1", Description = "CauseOfDeathMaster" };
            mapper.Setup(x => x.Map<CauseOfDeathMasterRequest, CaCauseOfDeathMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaCauseOfDeathMasters.Add(new CaCauseOfDeathMaster() { Id = 2, Code = "INA2", Description = "CauseOfDeathMaster1" });
            malaysiaDbContext.SaveChangesAsync();

            var causeOfDeathMasterService = new CauseOfDeathMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
         
            var result = causeOfDeathMasterService.AddCauseOfDeathMaster(requestData).Result;
        
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region CauseOfDeathMaster service Unit test cases for UpdateCauseOfDeathMaster
        [Fact]
        public void Test_UpdateCauseOfDeathMaster_ReturnsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaCauseOfDeathMasters.Add(new CaCauseOfDeathMaster { Id = 1, Description = "Test", Code = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            CauseOfDeathMasterRequest causeOfDeathMasterInput = new CauseOfDeathMasterRequest() { Id = 1, Description = "Test1", Code = "Test", FacilityCode = "Test" };
            CaCauseOfDeathMaster mapperResp = new CaCauseOfDeathMaster() { Id = 1, Description = "Test1", Code = "Test", FacilityCode = "Test" };
            mapper.Setup(x => x.Map<CauseOfDeathMasterRequest, CaCauseOfDeathMaster>(It.IsAny<CauseOfDeathMasterRequest>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)
            var causeOfDeathMasterService = new CauseOfDeathMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = causeOfDeathMasterService.UpdateCauseOfDeathMaster(causeOfDeathMasterInput).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_UpdateCauseOfDeathMaster_Returns_IsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaCauseOfDeathMasters.Add(new CaCauseOfDeathMaster { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1", FacilityCode = "KLAN", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            CauseOfDeathMasterRequest requestData = null;
            CaCauseOfDeathMaster respData = new CaCauseOfDeathMaster { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1" };
            mapper.Setup(x => x.Map<CauseOfDeathMasterRequest, CaCauseOfDeathMaster>(It.IsAny<CauseOfDeathMasterRequest>())).Returns(respData);
            var causeOfDeathMasterService = new CauseOfDeathMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = causeOfDeathMasterService.UpdateCauseOfDeathMaster(requestData).Result;
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion
    }
}
