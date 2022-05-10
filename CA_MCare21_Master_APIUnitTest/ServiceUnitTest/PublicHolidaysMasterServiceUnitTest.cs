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
  public  class PublicHolidaysMasterServiceUnitTest
    {
        Mock<IHttpContextAccessor> httpContextProcessor;
        Mock<ICacheService> cacheService;
        Mock<IMapper> mapper;
        MalaysiaDbContext malaysiaDbContext;
        public PublicHolidaysMasterServiceUnitTest()
        {
            httpContextProcessor = new Mock<IHttpContextAccessor>();
            cacheService = new Mock<ICacheService>();
            mapper = new Mock<IMapper>();
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "PublicHolidaysMasterMalaysiaDatabase")
                            .Options;
            malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
        }

        #region Public Holidays Master service Unit test cases for GetPublicHolidaysMaster
        [Fact]
        public void Test_GetPublicHolidaysMaster_ReturnsDataCount()
        {
           
            var httpContextProcessor = new Mock<IHttpContextAccessor>();
            var cacheService = new Mock<ICacheService>();
            var mapper = new Mock<IMapper>();
            malaysiaDbContext.CaPublicHolidaysMasters.Add(new CaPublicHolidaysMaster { Id = 1, Reason = "fever", HoliDayName = "holi", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<PublicHolidaysMasterResponse>() { new PublicHolidaysMasterResponse { Id = 1, Reason = "fever"} };
            mapper.Setup(x => x.Map<IEnumerable<PublicHolidaysMasterResponse>>(It.IsAny<List<CaPublicHolidaysMaster>>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var publicHolidaysMasterService = new PublicHolidaysMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = publicHolidaysMasterService.GetPublicHolidaysMaster().Result.ToList();
            Assert.NotNull(result);
            Assert.Equal(1, Convert.ToInt32(result.Count()));
            malaysiaDbContext.Database.EnsureDeleted();

        }
        #endregion

        #region Public Holidays Master service Unit test cases for GetPublicHolidaysMasterById
        [Fact]
        public void Test_GetPublicHolidaysMasterById_ReturnsErrorFalseResponse()
        {
            malaysiaDbContext.CaPublicHolidaysMasters.Add(new CaPublicHolidaysMaster { Id = 1, Reason = "fever", IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new PublicHolidaysMasterResponse() { Id = 1, Reason = "fever" };
            mapper.Setup(x => x.Map<CaPublicHolidaysMaster, PublicHolidaysMasterResponse>(It.IsAny<CaPublicHolidaysMaster>())).Returns(mapperResp);
            var publicHolidaysMasterService = new PublicHolidaysMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = publicHolidaysMasterService.GetPublicHolidaysMasterById(mapperResp.Id).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Public Holidays Master service Unit test cases for AddPublicHolidaysMaster
        [Fact]
        public void Test_AddPublicHolidaysMaster_ReturnsErrorFalseResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            PublicHolidaysMasterRequest publicHolidaysMasterRequest = new PublicHolidaysMasterRequest() { Id = 1, Reason = "fever", IsDeleted = false, FacilityCode = "KLAN" };
            CaPublicHolidaysMaster mapperResp = new CaPublicHolidaysMaster() { Id = 1, Reason = "fever" };
            mapper.Setup(x => x.Map<PublicHolidaysMasterRequest, CaPublicHolidaysMaster>(publicHolidaysMasterRequest)).Returns(mapperResp);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaPublicHolidaysMasters.Add(new CaPublicHolidaysMaster() { Id = 2, Reason = "fever" });
            malaysiaDbContext.SaveChangesAsync();
            var publicHolidaysMasterService = new PublicHolidaysMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = publicHolidaysMasterService.AddPublicHolidaysMaster(publicHolidaysMasterRequest).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_AddPublicHolidaysMaster_Returns_ReturnsErrorTrueResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            PublicHolidaysMasterRequest requestData = null;
            CaPublicHolidaysMaster respData = new CaPublicHolidaysMaster { Id = 1, HoliDayName = "sunday" };
            mapper.Setup(x => x.Map<PublicHolidaysMasterRequest, CaPublicHolidaysMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaPublicHolidaysMasters.Add(new CaPublicHolidaysMaster() { Id = 2, HoliDayName = "monday" });
            malaysiaDbContext.SaveChangesAsync();
            var publicHolidaysMasterService = new PublicHolidaysMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = publicHolidaysMasterService.AddPublicHolidaysMaster(requestData).Result;
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Public Holidays Master service Unit test cases for DeletePublicHolidaysMaster

        [Fact]
        public void Test_DeletePublicHolidaysMaster_ReturnsErrorFalseResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaPublicHolidaysMasters.Add(new CaPublicHolidaysMaster { Id = 1, IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            PublicHolidaysMasterRequest publicHolidaysMasterRequest = new PublicHolidaysMasterRequest() { Id = 1, FacilityCode = "Test" };
            CaPublicHolidaysMaster mapperResp = new CaPublicHolidaysMaster() { Id = 1,  IsDeleted = true };
            mapper.Setup(x => x.Map<PublicHolidaysMasterRequest, CaPublicHolidaysMaster>(It.IsAny<PublicHolidaysMasterRequest>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var publicHolidaysMasterService = new PublicHolidaysMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = publicHolidaysMasterService.DeletePublicHolidaysMaster(publicHolidaysMasterRequest.Id).Result;

            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_DeletePublicHolidaysMaster_ReturnsErrorTrueResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaOccupationMasters.Add(new CaOccupationMaster { Id = 1, Description = "Test", IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            PublicHolidaysMasterRequest publicHolidaysMasterRequest = new PublicHolidaysMasterRequest() { Id = 1,FacilityCode = "Test" };
            CaOccupationMaster mapperResp = new CaOccupationMaster() { Id = 1, Description = "Test" };
            mapper.Setup(x => x.Map<PublicHolidaysMasterRequest, CaOccupationMaster>(It.IsAny<PublicHolidaysMasterRequest>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var publicHolidaysMasterService = new PublicHolidaysMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = publicHolidaysMasterService.DeletePublicHolidaysMaster(publicHolidaysMasterRequest.Id).Result;

            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Public Holidays Master service Unit test cases for UpdatePublicHolidaysMaster
        [Fact]
        public void Test_UpdatePublicHolidaysMaster_ReturnsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaPublicHolidaysMasters.Add(new CaPublicHolidaysMaster { Id = 1,  IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            PublicHolidaysMasterRequest publicHolidaysMasterRequest = new PublicHolidaysMasterRequest() { Id = 1, FacilityCode = "Test" };
            CaPublicHolidaysMaster mapperResp = new CaPublicHolidaysMaster() { Id = 1, FacilityCode = "Test" };
            mapper.Setup(x => x.Map<PublicHolidaysMasterRequest, CaPublicHolidaysMaster>(It.IsAny<PublicHolidaysMasterRequest>())).Returns(mapperResp);
            var publicHolidaysMasterService = new PublicHolidaysMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = publicHolidaysMasterService.UpdatePublicHolidaysMaster(publicHolidaysMasterRequest).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_UpdatePublicHolidaysMaster_Returns_IsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaPublicHolidaysMasters.Add(new CaPublicHolidaysMaster { Id = 1, HoliDayName = "sunday", FacilityCode = "KLAN", IsDeleted = false });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            PublicHolidaysMasterRequest requestData = null;
            CaPublicHolidaysMaster respData = new CaPublicHolidaysMaster { Id = 1, HoliDayName = "sunday" };
            mapper.Setup(x => x.Map<PublicHolidaysMasterRequest, CaPublicHolidaysMaster>(It.IsAny<PublicHolidaysMasterRequest>())).Returns(respData);
            var publicHolidaysMasterService = new PublicHolidaysMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = publicHolidaysMasterService.UpdatePublicHolidaysMaster(requestData).Result;
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion
    }
}
