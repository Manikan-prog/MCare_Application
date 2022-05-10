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
    public class ReligionMasterServiceUnitTest
    {
        Mock<IHttpContextAccessor> httpContextProcessor;
        Mock<ICacheService> cacheService;
        Mock<IMapper> mapper;
        MalaysiaDbContext malaysiaDbContext;
        public ReligionMasterServiceUnitTest()
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

        #region Religion Master CRUD Operation Unit Test Cases
        #region Add Religion Master Unit Test Cases
        [Fact]
        public void Test_AddReligionMaster_ReturnsErrorFalseResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            ReligionMasterRequest religionMasterRequest = new ReligionMasterRequest() { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" };
            CaReligionMaster mapperResp = new CaReligionMaster() { Id = 1, Description = "test" };
            mapper.Setup(x => x.Map<ReligionMasterRequest, CaReligionMaster>(religionMasterRequest)).Returns(mapperResp);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaReligionMasters.Add(new CaReligionMaster() { Id = 2, Description = "test" });
            malaysiaDbContext.SaveChangesAsync();
            var religionMasterService = new ReligionMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = religionMasterService.AddReligionMaster(religionMasterRequest).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_AddReligionMaster_ReturnsErrorTrueResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            //ReligionMasterRequest requestData = new ReligionMasterRequest { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" };
            ReligionMasterRequest requestData = null;
            CaReligionMaster respData = new CaReligionMaster { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" };
            mapper.Setup(x => x.Map<ReligionMasterRequest, CaReligionMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaReligionMasters.Add(new CaReligionMaster() { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChangesAsync();

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var religionMasterService = new ReligionMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = religionMasterService.AddReligionMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Update Religion Master Unit Test Cases
        [Fact]
        public void Test_UpdateReligionMaster_ReturnsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaReligionMasters.Add(new CaReligionMaster { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            ReligionMasterRequest religionMasterInput = new ReligionMasterRequest() { Id = 1, Description = "Test1", FacilityCode = "Test" };
            CaReligionMaster mapperResp = new CaReligionMaster() { Id = 1, Description = "Test1", FacilityCode = "Test" };
            mapper.Setup(x => x.Map<ReligionMasterRequest, CaReligionMaster>(It.IsAny<ReligionMasterRequest>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)
            var religionMasterService = new ReligionMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = religionMasterService.UpdateReligionMaster(religionMasterInput).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_UpdateReligionMaster_ReturnsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaReligionMasters.Add(new CaReligionMaster { Id = 1, Description = "Test", Code = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            // ReligionMasterRequest requestData = new ReligionMasterRequest { Id = 1, Description = "Test1", Code = "Test", FacilityCode = "Test" };
            ReligionMasterRequest requestData = null;
            CaReligionMaster respData = new CaReligionMaster { Id = 1, Description = "Test1", Code = "Test", FacilityCode = "Test" };
            mapper.Setup(x => x.Map<ReligionMasterRequest, CaReligionMaster>(It.IsAny<ReligionMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var religionMasterService = new ReligionMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = religionMasterService.UpdateReligionMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Delete Religion Master Unit Test Cases
        [Fact]
        public void Test_DeleteReligionMaster_ReturnsErrorFalseResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaReligionMasters.Add(new CaReligionMaster { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            ReligionMasterRequest religionMasterInput = new ReligionMasterRequest() { Id = 1, Description = "Test", FacilityCode = "Test" };
            CaReligionMaster mapperResp = new CaReligionMaster() { Id = 1, Description = "Test", IsDeleted = true };
            mapper.Setup(x => x.Map<ReligionMasterRequest, CaReligionMaster>(It.IsAny<ReligionMasterRequest>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var religionMasterService = new ReligionMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = religionMasterService.DeleteReligionMaster(religionMasterInput.Id).Result;

            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_DeleteReligionMaster_ReturnsErrorTrueResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaReligionMasters.Add(new CaReligionMaster { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            ReligionMasterRequest religionMasterInput = new ReligionMasterRequest() { Id = 1, Description = "Test", FacilityCode = "Test" };
            CaReligionMaster mapperResp = new CaReligionMaster() { Id = 1, Description = "Test" };
            mapper.Setup(x => x.Map<ReligionMasterRequest, CaReligionMaster>(It.IsAny<ReligionMasterRequest>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var religionMasterService = new ReligionMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = religionMasterService.DeleteReligionMaster(religionMasterInput.Id).Result;

            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get Religion Master By Id Unit Test Case
        [Fact]
        public void Test_GetReligionMasterById_ReturnsErrorFalseResponse()
        {
            malaysiaDbContext.CaReligionMasters.Add(new CaReligionMaster { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new ReligionMasterResponse() { Id = 1, Description = "test" };
            mapper.Setup(x => x.Map<CaReligionMaster, ReligionMasterResponse>(It.IsAny<CaReligionMaster>())).Returns(mapperResp);
            var religionMasterService = new ReligionMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = religionMasterService.GetReligionMasterById(mapperResp.Id).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get Religion Master Unit Test Case
        [Fact]
        public void Test_GetReligionMaster_ReturnsDataCount()
        {

            var httpContextProcessor = new Mock<IHttpContextAccessor>();
            var cacheService = new Mock<ICacheService>();
            var mapper = new Mock<IMapper>();
            malaysiaDbContext.CaReligionMasters.Add(new CaReligionMaster { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, Code = "Testing", Smrpcode = "TST", Smrpdescription = "Testings", FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<ReligionMasterResponse>() { new ReligionMasterResponse { Id = 1, Description = "test" } };
            mapper.Setup(x => x.Map<IEnumerable<ReligionMasterResponse>>(It.IsAny<List<CaReligionMaster>>())).Returns(mapperResp);
            var religionMasterService = new ReligionMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var religionMasters = religionMasterService.GetReligionMaster().Result.ToList();
            Assert.NotNull(religionMasters);
            Assert.Equal(1, Convert.ToInt32(religionMasters.Count()));
        }
        #endregion
        #endregion

    }
}
