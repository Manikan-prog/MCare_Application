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
    public class OccupationMasterServiceUnitTest
    {
        Mock<IHttpContextAccessor> httpContextProcessor;
        Mock<ICacheService> cacheService;
        Mock<IMapper> mapper;
        MalaysiaDbContext malaysiaDbContext;
        public OccupationMasterServiceUnitTest()
        {
            httpContextProcessor = new Mock<IHttpContextAccessor>();
            cacheService = new Mock<ICacheService>();
            mapper = new Mock<IMapper>();
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "OccupationMasterMalaysiaDatabase")
                            .Options;
            malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
        }

        #region Occupation Master service Unit test cases for GetOccupationMaster
        [Fact]
        public void Test_GetOccupationMaster_ReturnsDataCount()
        {
            var httpContextProcessor = new Mock<IHttpContextAccessor>();
            var cacheService = new Mock<ICacheService>();
            var mapper = new Mock<IMapper>();
            malaysiaDbContext.CaOccupationMasters.Add(new CaOccupationMaster { Id = 1, Description = "Test", Code = "AB", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<OccupationMasterResponse>() { new OccupationMasterResponse { Id = 1, Description = "test" } };
            mapper.Setup(x => x.Map<IEnumerable<OccupationMasterResponse>>(It.IsAny<List<CaOccupationMaster>>())).Returns(mapperResp);
            var occupationMasterService = new OccupationMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = occupationMasterService.GetOccupationMaster().Result.ToList();
            Assert.NotNull(result);
            Assert.Equal(1, Convert.ToInt32(result.Count()));
            malaysiaDbContext.Database.EnsureDeleted();

        }
        #endregion

        #region Occupation Master service Unit test cases for GetOccupationMasterById
        [Fact]
        public void Test_GetOccupationMasterById_ReturnsErrorFalseResponse()
        {
            malaysiaDbContext.CaOccupationMasters.Add(new CaOccupationMaster { Id = 1, Description = "test", CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "01", IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new OccupationMasterResponse() { Id = 1, Description = "test" };
            mapper.Setup(x => x.Map<CaOccupationMaster, OccupationMasterResponse>(It.IsAny<CaOccupationMaster>())).Returns(mapperResp);
            var occupationMasterService = new OccupationMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = occupationMasterService.GetOccupationMasterById(mapperResp.Id).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Occupation Master service Unit test cases for AddOccupationMaster
        [Fact]
        public void Test_AddOccupationMaster_ReturnsErrorFalseResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            OccupationMasterRequest occupationMasterRequest = new OccupationMasterRequest() { Id = 1, Description = "test", CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "01", IsDeleted = false, FacilityCode = "KLAN" };
            CaOccupationMaster mapperResp = new CaOccupationMaster() { Id = 1, Description = "Test" };
            mapper.Setup(x => x.Map<OccupationMasterRequest, CaOccupationMaster>(occupationMasterRequest)).Returns(mapperResp);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaOccupationMasters.Add(new CaOccupationMaster() { Id = 2, Description = "Test" });
            malaysiaDbContext.SaveChangesAsync();
            var occupationMasterService = new OccupationMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = occupationMasterService.AddOccupationMaster(occupationMasterRequest).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_AddOccupationMaster_Returns_ReturnsErrorTrueResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            OccupationMasterRequest requestData = null;
            CaOccupationMaster respData = new CaOccupationMaster { Id = 1, Description = "OccupationMaster" };
            mapper.Setup(x => x.Map<OccupationMasterRequest, CaOccupationMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaOccupationMasters.Add(new CaOccupationMaster() { Id = 2, Description = "OccupationMaster 2" });
            malaysiaDbContext.SaveChangesAsync();

            var occupationMasterService = new OccupationMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);

            var result = occupationMasterService.AddOccupationMaster(requestData).Result;

            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Occupation Master service Unit test cases for DeleteOccupationMaster
        [Fact]
        public void Test_DeleteOccupationMaster_ReturnsErrorFalseResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaOccupationMasters.Add(new CaOccupationMaster { Id = 1, Description = "Test", IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            OccupationMasterRequest occupationMasterRequest = new OccupationMasterRequest() { Id = 1, Description = "Test", FacilityCode = "Test" };
            CaOccupationMaster mapperResp = new CaOccupationMaster() { Id = 1, Description = "Test", IsDeleted = true };
            mapper.Setup(x => x.Map<OccupationMasterRequest, CaOccupationMaster>(It.IsAny<OccupationMasterRequest>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var occupationMasterService = new OccupationMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = occupationMasterService.DeleteOccupationMaster(occupationMasterRequest.Id).Result;

            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_DeleteOccupationMaster_ReturnsErrorTrueResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaOccupationMasters.Add(new CaOccupationMaster { Id = 1, Description = "Test", IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            OccupationMasterRequest occupationMasterRequest = new OccupationMasterRequest() { Id = 1, Description = "Test", FacilityCode = "Test" };
            CaOccupationMaster mapperResp = new CaOccupationMaster() { Id = 1, Description = "Test" };
            mapper.Setup(x => x.Map<OccupationMasterRequest, CaOccupationMaster>(It.IsAny<OccupationMasterRequest>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var occupationMasterService = new OccupationMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = occupationMasterService.DeleteOccupationMaster(occupationMasterRequest.Id).Result;

            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Occupation Master service Unit test cases for UpdateModeOfArrivalMaster

        [Fact]
        public void Test_UpdateModeOfArrivalMaster_ReturnsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaOccupationMasters.Add(new CaOccupationMaster { Id = 1, Description = "Test", IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            OccupationMasterRequest occupationMasterRequest = new OccupationMasterRequest() { Id = 1, Description = "Test1", FacilityCode = "Test" };
            CaOccupationMaster mapperResp = new CaOccupationMaster() { Id = 1, Description = "Test1", FacilityCode = "Test" };
            mapper.Setup(x => x.Map<OccupationMasterRequest, CaOccupationMaster>(It.IsAny<OccupationMasterRequest>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)
            var occupationMasterService = new OccupationMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = occupationMasterService.UpdateOccupationMaster(occupationMasterRequest).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_UpdateOccupationMaster_Returns_IsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaOccupationMasters.Add(new CaOccupationMaster { Id = 1, Description = "INPATIENT ADMISSION 1", FacilityCode = "KLAN", IsDeleted = false });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            OccupationMasterRequest requestData = null;
            CaOccupationMaster respData = new CaOccupationMaster { Id = 1, Description = "OccupationMaster" };
            mapper.Setup(x => x.Map<OccupationMasterRequest, CaOccupationMaster>(It.IsAny<OccupationMasterRequest>())).Returns(respData);
            var occupationMasterService = new OccupationMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = occupationMasterService.UpdateOccupationMaster(requestData).Result;
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion
    }
}
