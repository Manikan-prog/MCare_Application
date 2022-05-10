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
    public class MaritalStatusServiceUnitTest
    {
        Mock<IHttpContextAccessor> httpContextProcessor;
        Mock<ICacheService> cacheService;
        Mock<IMapper> mapper;
        MalaysiaDbContext malaysiaDbContext;
        public MaritalStatusServiceUnitTest()
        {
            httpContextProcessor = new Mock<IHttpContextAccessor>();
            cacheService = new Mock<ICacheService>();
            mapper = new Mock<IMapper>();
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: " MaritalStatusMalaysiaDatabase")
                            .Options;
            malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
        }

        #region Marital Status Master service Unit test cases for GetMaritalStatusMaster
        [Fact]
         public void Test_GetMaritalStatusMaster_ReturnsDataCount()
        {
       
            var httpContextProcessor = new Mock<IHttpContextAccessor>();
            var cacheService = new Mock<ICacheService>();
            var mapper = new Mock<IMapper>();
        
            malaysiaDbContext.CaMaritalStatusMasters.Add(new CaMaritalStatusMaster { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<MaritalStatusMasterResponse>() { new MaritalStatusMasterResponse { Id = 1, Description = "test" } };
            mapper.Setup(x => x.Map<IEnumerable<MaritalStatusMasterResponse>>(It.IsAny<List<CaMaritalStatusMaster>>())).Returns(mapperResp);
            var maritalStatusService = new MaritalStatusService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = maritalStatusService.GetMaritalStatusMaster().Result.ToList();
            Assert.NotNull(result);
            Assert.Equal(1, Convert.ToInt32(result.Count()));
            malaysiaDbContext.Database.EnsureDeleted();

        }
        #endregion

        #region Marital Status Master service Unit test cases for GetMaritalStatusMasterById
        [Fact]
        public void Test_GetMaritalStatusMasterById_ReturnsErrorFalseResponse()
        {
            malaysiaDbContext.CaMaritalStatusMasters.Add(new CaMaritalStatusMaster { Id = 1, Description = "test", CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new MaritalStatusMasterResponse() { Id = 1, Description = "test" };
            mapper.Setup(x => x.Map<CaMaritalStatusMaster, MaritalStatusMasterResponse>(It.IsAny<CaMaritalStatusMaster>())).Returns(mapperResp);
            var maritalStatusService = new MaritalStatusService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = maritalStatusService.GetMaritalStatusMasterById(mapperResp.Id).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Marital Status Master service Unit test cases for AddMaritalStatusMaster
        [Fact]
        public void Test_AddMaritalStatusMaster_ReturnsErrorFalseResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            MaritalStatusMasterRequest maritalStatusMasterRequest = new MaritalStatusMasterRequest() { Id = 1, Description = "test", CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, IsDeleted = false, FacilityCode = "KLAN" };
            CaMaritalStatusMaster mapperResp = new CaMaritalStatusMaster() { Id = 1, Description = "Test" };
            mapper.Setup(x => x.Map<MaritalStatusMasterRequest, CaMaritalStatusMaster>(maritalStatusMasterRequest)).Returns(mapperResp);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaMaritalStatusMasters.Add(new CaMaritalStatusMaster() { Id = 2, Description = "Test" });
            malaysiaDbContext.SaveChangesAsync();
            var maritalStatusService = new MaritalStatusService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = maritalStatusService.AddMaritalStatusMaster(maritalStatusMasterRequest).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_AddMaritalStatusMaster_Returns_ReturnsErrorTrueResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            MaritalStatusMasterRequest requestData = null;
            CaMaritalStatusMaster respData = new CaMaritalStatusMaster { Id = 1, Description = "deparmentmaster" };
            mapper.Setup(x => x.Map<MaritalStatusMasterRequest, CaMaritalStatusMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaMaritalStatusMasters.Add(new CaMaritalStatusMaster() { Id = 2, Description = "deparmentmaster2" });
            malaysiaDbContext.SaveChangesAsync();

            var maritalStatusService = new MaritalStatusService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);

            var result = maritalStatusService.AddMaritalStatusMaster(requestData).Result;

            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Marital Status Master service Unit test cases for UpdateMaritalStatusMaster
        [Fact]
        public void Test_UpdateMaritalStatusMaster_ReturnsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaMaritalStatusMasters.Add(new CaMaritalStatusMaster { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            MaritalStatusMasterRequest maritalStatusMasterRequest = new MaritalStatusMasterRequest() { Id = 1, Description = "Test1", FacilityCode = "Test" };
            CaMaritalStatusMaster mapperResp = new CaMaritalStatusMaster() { Id = 1, Description = "Test1", FacilityCode = "Test" };
            mapper.Setup(x => x.Map<MaritalStatusMasterRequest, CaMaritalStatusMaster>(It.IsAny<MaritalStatusMasterRequest>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)
            var maritalStatusService = new MaritalStatusService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = maritalStatusService.UpdateMaritalStatusMaster(maritalStatusMasterRequest).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_UpdateMaritalStatusMaster_Returns_IsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaMaritalStatusMasters.Add(new CaMaritalStatusMaster { Id = 1, Description = "INPATIENT ADMISSION 1", FacilityCode = "KLAN", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            MaritalStatusMasterRequest requestData = null;
            CaMaritalStatusMaster respData = new CaMaritalStatusMaster { Id = 1, Description = "deparmentmaster" };
            mapper.Setup(x => x.Map<MaritalStatusMasterRequest, CaMaritalStatusMaster>(It.IsAny<MaritalStatusMasterRequest>())).Returns(respData);
            var maritalStatusService = new MaritalStatusService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = maritalStatusService.UpdateMaritalStatusMaster(requestData).Result;
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Marital Status Master service Unit test cases for DeleteMaritalStatusMaster
        [Fact]
        public void Test_DeleteMaritalStatusMaster_ReturnsErrorFalseResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaMaritalStatusMasters.Add(new CaMaritalStatusMaster { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            MaritalStatusMasterRequest maritalStatusMasterRequest = new MaritalStatusMasterRequest() { Id = 1, Description = "Test", FacilityCode = "Test" };
            CaMaritalStatusMaster mapperResp = new CaMaritalStatusMaster() { Id = 1, Description = "Test", IsDeleted = true };
            mapper.Setup(x => x.Map<MaritalStatusMasterRequest, CaMaritalStatusMaster>(It.IsAny<MaritalStatusMasterRequest>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var maritalStatusService = new MaritalStatusService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = maritalStatusService.DeleteMaritalStatusMaster(maritalStatusMasterRequest.Id).Result;

            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_DeleteMaritalStatusMaster_ReturnsErrorTrueResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaMaritalStatusMasters.Add(new CaMaritalStatusMaster { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            MaritalStatusMasterRequest maritalStatusMasterRequest = new MaritalStatusMasterRequest() { Id = 1, Description = "Test", FacilityCode = "Test" };
            CaMaritalStatusMaster mapperResp = new CaMaritalStatusMaster() { Id = 1, Description = "Test" };
            mapper.Setup(x => x.Map<MaritalStatusMasterRequest, CaMaritalStatusMaster>(It.IsAny<MaritalStatusMasterRequest>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var maritalStatusService = new MaritalStatusService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = maritalStatusService.DeleteMaritalStatusMaster(maritalStatusMasterRequest.Id).Result;

            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion
      
    
    }
}
