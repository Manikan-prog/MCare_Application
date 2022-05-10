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
    public class RoomMasterServiceUnitTest
    {
        Mock<IHttpContextAccessor> httpContextProcessor;
        Mock<ICacheService> cacheService;
        Mock<IMapper> mapper;
        MalaysiaDbContext malaysiaDbContext;
        public RoomMasterServiceUnitTest()
        {
            httpContextProcessor = new Mock<IHttpContextAccessor>();
            cacheService = new Mock<ICacheService>();
            mapper = new Mock<IMapper>();
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "RoomMasterMalaysiaDatabase")
                            .Options;
            malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
        }

        #region Room Master service Unit test cases for GetRoomMaster
        [Fact]
        public void Test_GetRoomMaster_ReturnsDataCount()
        {
         
            var httpContextProcessor = new Mock<IHttpContextAccessor>();
            var cacheService = new Mock<ICacheService>();
            var mapper = new Mock<IMapper>();
            malaysiaDbContext.CaRoomMasters.Add(new CaRoomMaster { Id = 1, Description = "Test", RoomCode = "10", WardId = 1, GenderId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<RoomMasterResponse>() { new RoomMasterResponse { Id = 1, Description = "test"} };
            mapper.Setup(x => x.Map<IEnumerable<RoomMasterResponse>>(It.IsAny<List<CaRoomMaster>>())).Returns(mapperResp);
            var roomMasterService = new RoomMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var roomMasters = roomMasterService.GetRoomMaster().Result.ToList();
            Assert.NotNull(roomMasters);
            Assert.Equal(1, Convert.ToInt32(roomMasters.Count()));

        }
        #endregion

        #region Room Master service Unit test cases for GetRoomMasterById
        [Fact]
        public void Test_GetRoomMasterById_ReturnsErrorFalseResponse()
        {
            malaysiaDbContext.CaRoomMasters.Add(new CaRoomMaster { Id = 1, Description="test", IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new RoomMasterResponse() { Id = 1, Description="test"};
            mapper.Setup(x => x.Map<CaRoomMaster, RoomMasterResponse>(It.IsAny<CaRoomMaster>())).Returns(mapperResp);
            var roomMasterService = new RoomMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = roomMasterService.GetRoomMasterById(mapperResp.Id).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Room Master service Unit test cases for AddRoomMaster
        [Fact]
        public void Test_AddRoomMaster_ReturnsErrorFalseResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            RoomMasterRequest roomMasterRequest = new RoomMasterRequest() { Id = 1, Description="test", IsDeleted = false, FacilityCode = "KLAN" };
            CaRoomMaster mapperResp = new CaRoomMaster() { Id = 1, Description="test" };
            mapper.Setup(x => x.Map<RoomMasterRequest, CaRoomMaster>(roomMasterRequest)).Returns(mapperResp);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaRoomMasters.Add(new CaRoomMaster() { Id = 2, Description="test" });
            malaysiaDbContext.SaveChangesAsync();
            var roomMasterService = new RoomMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = roomMasterService.AddRoomMaster(roomMasterRequest).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_AddRoomMaster_Returns_ReturnsErrorTrueResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            RoomMasterRequest requestData = null;
            CaRoomMaster respData = new CaRoomMaster { Id = 1, Description = "RoomMaster" };
            mapper.Setup(x => x.Map<RoomMasterRequest, CaRoomMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaRoomMasters.Add(new CaRoomMaster() { Id = 2, Description = "RoomMaster 2" });
            malaysiaDbContext.SaveChangesAsync();
            var roomMasterService = new RoomMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = roomMasterService.AddRoomMaster(requestData).Result;
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Room Master service Unit test cases for DeleteRoomMaster
        [Fact]
        public void Test_DeleteRoomMaster_ReturnsErrorFalseResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaRoomMasters.Add(new CaRoomMaster { Id = 1, IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            RoomMasterRequest roomMasterRequest = new RoomMasterRequest() { Id = 1, FacilityCode = "Test" };
            CaRoomMaster mapperResp = new CaRoomMaster() { Id = 1, IsDeleted = true };
            mapper.Setup(x => x.Map<RoomMasterRequest, CaRoomMaster>(It.IsAny<RoomMasterRequest>())).Returns(mapperResp);
         
            var roomMasterService = new RoomMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = roomMasterService.DeleteRoomMaster(roomMasterRequest.Id).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_DeleteRoomMaster_ReturnsErrorTrueResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaRoomMasters.Add(new CaRoomMaster { Id = 1, IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            RoomMasterRequest roomMasterRequest = new RoomMasterRequest() { Id = 1, FacilityCode = "Test" };
            CaRoomMaster mapperResp = new CaRoomMaster() { Id = 1, Description="test" };
            mapper.Setup(x => x.Map<RoomMasterRequest, CaRoomMaster>(It.IsAny<RoomMasterRequest>())).Returns(mapperResp);
            var roomMasterService = new RoomMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = roomMasterService.DeleteRoomMaster(roomMasterRequest.Id).Result;
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Room Master service Unit test cases for UpdateRoomMaster
        [Fact]
        public void Test_UpdateRoomMaster_ReturnsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaRoomMasters.Add(new CaRoomMaster { Id = 1, IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            RoomMasterRequest roomMasterRequest = new RoomMasterRequest() { Id = 1, FacilityCode = "Test" };
            CaRoomMaster mapperResp = new CaRoomMaster() { Id = 1, FacilityCode = "Test" };
            mapper.Setup(x => x.Map<RoomMasterRequest, CaRoomMaster>(It.IsAny<RoomMasterRequest>())).Returns(mapperResp);
            var roomMasterService = new RoomMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = roomMasterService.UpdateRoomMaster(roomMasterRequest).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_UpdateRoomMaster_Returns_IsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaRoomMasters.Add(new CaRoomMaster { Id = 1, FacilityCode = "KLAN", IsDeleted = false });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            RoomMasterRequest requestData = null;
            CaRoomMaster respData = new CaRoomMaster { Id = 1, Description= "RoomMaster" };
            mapper.Setup(x => x.Map<RoomMasterRequest, CaRoomMaster>(It.IsAny<RoomMasterRequest>())).Returns(respData);
            var roomMasterService = new RoomMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = roomMasterService.UpdateRoomMaster(requestData).Result;
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion
    }
}
