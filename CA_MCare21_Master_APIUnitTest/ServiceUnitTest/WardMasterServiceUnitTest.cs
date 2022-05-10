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
    public class WardMasterServiceUnitTest
    {
        Mock<IHttpContextAccessor> httpContextProcessor;
        Mock<ICacheService> cacheService;
        Mock<IMapper> mapper;
        MalaysiaDbContext malaysiaDbContext;
        public WardMasterServiceUnitTest()
        {
            httpContextProcessor = new Mock<IHttpContextAccessor>();
            cacheService = new Mock<ICacheService>();
            mapper = new Mock<IMapper>();
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "wardMasterMalaysiaDatabase")
                            .Options;
            malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
        }

        #region Ward Master CRUD Operation Unit Test Cases
        #region Add Ward Master Unit Test Cases
        [Fact]
        public void Test_AddWardMaster_Returns_IsErrorFalseResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            WardMasterRequest requestData = new WardMasterRequest { Id = 1, WardCode = "TestWard", WardName = "TestWard", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, DepartmentId = 21, FacilityCode = "KLAN" };
            CaWardMaster respData = new CaWardMaster { Id = 1, WardCode = "TestWard", WardName = "TestWard" };
            mapper.Setup(x => x.Map<WardMasterRequest, CaWardMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaWardMasters.Add(new CaWardMaster { Id = 2, WardCode = "TestWard2", WardName = "TestWard2" });
            malaysiaDbContext.SaveChangesAsync();

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var wardMasterService = new WardMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = wardMasterService.AddWardMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_AddWardMaster_Returns_IsErrorTrueResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            WardMasterRequest requestData = null;
            CaWardMaster respData = new CaWardMaster { Id = 1, WardCode = "TestWard", WardName = "TestWard" };
            mapper.Setup(x => x.Map<WardMasterRequest, CaWardMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaWardMasters.Add(new CaWardMaster { Id = 2, WardCode = "TestWard2", WardName = "TestWard2" });
            malaysiaDbContext.SaveChangesAsync();

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var wardMasterService = new WardMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = wardMasterService.AddWardMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Update Ward Master Unit Test Cases
        [Fact]
        public void Test_UpdateWardMaster_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaWardMasters.Add(new CaWardMaster { Id = 1, WardCode = "TestWard", WardName = "TestWard", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, DepartmentId = 21, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            WardMasterRequest requestData = new WardMasterRequest { Id = 1, WardCode = "TestWard", WardName = "TestWard", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, DepartmentId = 21, FacilityCode = "KLAN" };
            CaWardMaster respData = new CaWardMaster { Id = 1, WardCode = "TestWard", WardName = "TestWard" };
            mapper.Setup(x => x.Map<WardMasterRequest, CaWardMaster>(It.IsAny<WardMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var wardMasterService = new WardMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = wardMasterService.UpdateWardMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_UpdateWardMaster_Returns_IsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaWardMasters.Add(new CaWardMaster { Id = 1, WardCode = "TestWard", WardName = "TestWard", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, DepartmentId = 21, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            WardMasterRequest requestData = null;
            CaWardMaster respData = new CaWardMaster { Id = 1, WardCode = "TestWard", WardName = "TestWard" };
            mapper.Setup(x => x.Map<WardMasterRequest, CaWardMaster>(It.IsAny<WardMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var wardMasterService = new WardMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = wardMasterService.UpdateWardMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Delete Ward Master Unit Test Cases
        [Fact]
        public void Test_DeleteWardMaster_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaWardMasters.Add(new CaWardMaster { Id = 1, WardCode = "TestWard", WardName = "TestWard", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, DepartmentId = 21, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            WardMasterRequest requestData = new WardMasterRequest { Id = 1, WardCode = "TestWard", WardName = "TestWard", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, DepartmentId = 21, FacilityCode = "KLAN" };
            CaWardMaster respData = new CaWardMaster { Id = 1, WardCode = "TestWard", WardName = "TestWard", IsDeleted = true };
            mapper.Setup(x => x.Map<WardMasterRequest, CaWardMaster>(It.IsAny<WardMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var wardMasterService = new WardMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = wardMasterService.DeleteWardMaster(requestData.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_DeleteWardMaster_Returns_IsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaWardMasters.Add(new CaWardMaster { Id = 1, WardCode = "TestWard", WardName = "TestWard", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, DepartmentId = 21, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            WardMasterRequest requestData = new WardMasterRequest { Id = 1, WardCode = "TestWard", WardName = "TestWard", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, DepartmentId = 21, FacilityCode = "KLAN" };
            CaWardMaster respData = new CaWardMaster { Id = 1, WardCode = "TestWard", WardName = "TestWard" };
            mapper.Setup(x => x.Map<WardMasterRequest, CaWardMaster>(It.IsAny<WardMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var wardMasterService = new WardMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = wardMasterService.DeleteWardMaster(requestData.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get Ward Master By Id Unit Test Case
        [Fact]
        public void Test_GetWardMasterById_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaWardMasters.Add(new CaWardMaster { Id = 1, WardCode = "TestWard", WardName = "TestWard", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, DepartmentId = 21, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new WardMasterResponse { Id = 1, WardCode = "TestWard", WardName = "TestWard", DepartmentId = 21, FacilityCode = "KLAN" };
            mapper.Setup(x => x.Map<CaWardMaster, WardMasterResponse>(It.IsAny<CaWardMaster>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)
            //Arrange
            var wardMasterService = new WardMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = wardMasterService.GetWardMasterById(mapperResp.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get Ward Master Unit Test Case
        [Fact]
        public void Test_GetWardMaster_Returns_DataCount()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaWardMasters.Add(new CaWardMaster { Id = 1, WardCode = "TestWard", WardName = "TestWard", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, DepartmentId = 21, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<WardMasterResponse>() { new WardMasterResponse { Id = 1, WardCode = "TestWard", WardName = "TestWard", DepartmentId = 21, FacilityCode = "KLAN" } };
            mapper.Setup(x => x.Map<List<WardMasterResponse>>(It.IsAny<List<CaWardMaster>>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)  
            //Arrange
            var wardMasterService = new WardMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = wardMasterService.GetWardMaster().Result.ToList();
            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, Convert.ToInt32(result.Count()));
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #endregion
    }
}
