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
    public class ModeOfArrivalMasterServiceUnitTest
    {
        Mock<IHttpContextAccessor> httpContextProcessor;
        Mock<ICacheService> cacheService;
        Mock<IMapper> mapper;
        MalaysiaDbContext malaysiaDbContext;
        public ModeOfArrivalMasterServiceUnitTest()
        {
            httpContextProcessor = new Mock<IHttpContextAccessor>();
            cacheService = new Mock<ICacheService>();
            mapper = new Mock<IMapper>();
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "ModeOfArrivalMasterMalaysiaDatabase")
                            .Options;
            malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
        }

        #region Mode Of Arrival Master service Unit test cases for GetModeOfArrivalMaster
        [Fact]
        public void Test_GetModeOfArrivalMaster_ReturnsDataCount()
        {
      
            var httpContextProcessor = new Mock<IHttpContextAccessor>();
       
            var cacheService = new Mock<ICacheService>();
            var mapper = new Mock<IMapper>();
            malaysiaDbContext.CaModeOfArrivalMasters.Add(new CaModeOfArrivalMaster { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<ModeOfArrivalMasterResponse>() { new ModeOfArrivalMasterResponse { Id = 1, Description = "test"} };
            mapper.Setup(x => x.Map<IEnumerable<ModeOfArrivalMasterResponse>>(It.IsAny<List<CaModeOfArrivalMaster>>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var modeOfArrivalMasterService = new ModeOfArrivalMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = modeOfArrivalMasterService.GetModeOfArrivalMaster().Result.ToList();
            Assert.NotNull(result);
            Assert.Equal(1, Convert.ToInt32(result.Count()));
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Mode Of Arrival Master service Unit test cases for GetModeOfArrivalMasterById
        [Fact]
        public void Test_GetModeOfArrivalMasterById_ReturnsErrorFalseResponse()
        {
            malaysiaDbContext.CaModeOfArrivalMasters.Add(new CaModeOfArrivalMaster { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new ModeOfArrivalMasterResponse() { Id = 1, Description = "test" };
            mapper.Setup(x => x.Map<CaModeOfArrivalMaster, ModeOfArrivalMasterResponse>(It.IsAny<CaModeOfArrivalMaster>())).Returns(mapperResp);
            var modeOfArrivalMasterService = new ModeOfArrivalMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = modeOfArrivalMasterService.GetModeOfArrivalMasterById(mapperResp.Id).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Mode Of Arrival Master service Unit test cases for AddModeOfArrivalMaster
        [Fact]
        public void Test_AddModeOfArrivalMaster_ReturnsErrorFalseResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            ModeOfArrivalMasterRequest modeOfArrivalMasterRequest = new ModeOfArrivalMasterRequest() { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" };
            CaModeOfArrivalMaster mapperResp = new CaModeOfArrivalMaster() { Id = 1, Description = "Test" };
            mapper.Setup(x => x.Map<ModeOfArrivalMasterRequest, CaModeOfArrivalMaster>(modeOfArrivalMasterRequest)).Returns(mapperResp);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaModeOfArrivalMasters.Add(new CaModeOfArrivalMaster() { Id = 2, Description = "Test" });
            malaysiaDbContext.SaveChangesAsync();
            var modeOfArrivalMasterService = new ModeOfArrivalMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = modeOfArrivalMasterService.AddModeOfArrivalMaster(modeOfArrivalMasterRequest).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_AddModeOfArrivalMaster_Returns_ReturnsErrorTrueResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            ModeOfArrivalMasterRequest requestData = null;
            CaModeOfArrivalMaster respData = new CaModeOfArrivalMaster { Id = 1, Description = "AddModeOfArrivalMaster" };
            mapper.Setup(x => x.Map<ModeOfArrivalMasterRequest, CaModeOfArrivalMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaModeOfArrivalMasters.Add(new CaModeOfArrivalMaster() { Id = 2, Description = "AddModeOfArrivalMaster 2" });
            malaysiaDbContext.SaveChangesAsync();

            var modeOfArrivalMasterService = new ModeOfArrivalMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);

            var result = modeOfArrivalMasterService.AddModeOfArrivalMaster(requestData).Result;

            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Mode Of Arrival Master service Unit test cases for DeleteModeOfArrivalMaster
        [Fact]
        public void Test_DeleteModeOfArrivalMaster_ReturnsErrorFalseResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaModeOfArrivalMasters.Add(new CaModeOfArrivalMaster { Id = 1, Description = "Test", IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            ModeOfArrivalMasterRequest modeOfArrivalMasterRequest = new ModeOfArrivalMasterRequest() { Id = 1, Description = "Test", FacilityCode = "Test" };
            CaModeOfArrivalMaster mapperResp = new CaModeOfArrivalMaster() { Id = 1, Description = "Test", IsDeleted = true };
            mapper.Setup(x => x.Map<ModeOfArrivalMasterRequest, CaModeOfArrivalMaster>(It.IsAny<ModeOfArrivalMasterRequest>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var modeOfArrivalMasterService = new ModeOfArrivalMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = modeOfArrivalMasterService.DeleteModeOfArrivalMaster(modeOfArrivalMasterRequest.Id).Result;

            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_DeleteModeOfArrivalMaster_ReturnsErrorTrueResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaModeOfArrivalMasters.Add(new CaModeOfArrivalMaster { Id = 1, Description = "Test", IsDeleted = false,  FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            ModeOfArrivalMasterRequest modeOfArrivalMasterRequest = new ModeOfArrivalMasterRequest() { Id = 1, Description = "Test", FacilityCode = "Test" };
            CaModeOfArrivalMaster mapperResp = new CaModeOfArrivalMaster() { Id = 1, Description = "Test" };
            mapper.Setup(x => x.Map<ModeOfArrivalMasterRequest, CaModeOfArrivalMaster>(It.IsAny<ModeOfArrivalMasterRequest>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var modeOfArrivalMasterService = new ModeOfArrivalMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = modeOfArrivalMasterService.DeleteModeOfArrivalMaster(modeOfArrivalMasterRequest.Id).Result;

            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Mode Of Arrival Master service Unit test cases for UpdateModeOfArrivalMaster
        [Fact]
        public void Test_UpdateModeOfArrivalMaster_ReturnsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaModeOfArrivalMasters.Add(new CaModeOfArrivalMaster { Id = 1, Description = "Test", IsDeleted = false,  FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            ModeOfArrivalMasterRequest modeOfArrivalMasterRequest = new ModeOfArrivalMasterRequest() { Id = 1, Description = "Test1", FacilityCode = "Test" };
            CaModeOfArrivalMaster mapperResp = new CaModeOfArrivalMaster() { Id = 1, Description = "Test1", FacilityCode = "Test" };
            mapper.Setup(x => x.Map<ModeOfArrivalMasterRequest, CaModeOfArrivalMaster>(It.IsAny<ModeOfArrivalMasterRequest>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)
            var modeOfArrivalMasterService = new ModeOfArrivalMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = modeOfArrivalMasterService.UpdateModeOfArrivalMaster(modeOfArrivalMasterRequest).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_UpdateModeOfArrivalMaster_Returns_IsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaModeOfArrivalMasters.Add(new CaModeOfArrivalMaster { Id = 1, Description = "INPATIENT ADMISSION 1", FacilityCode = "KLAN", IsDeleted = false });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            ModeOfArrivalMasterRequest requestData = null;
            CaModeOfArrivalMaster respData = new CaModeOfArrivalMaster { Id = 1, Description = "AddModeOfArrivalMaster" };
            mapper.Setup(x => x.Map<ModeOfArrivalMasterRequest, CaModeOfArrivalMaster>(It.IsAny<ModeOfArrivalMasterRequest>())).Returns(respData);
            var modeOfArrivalMasterService = new ModeOfArrivalMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = modeOfArrivalMasterService.UpdateModeOfArrivalMaster(requestData).Result;
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

    }
}
