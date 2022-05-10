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
   public class DepartmentMasterServiceUnitTest
    {
        Mock<IHttpContextAccessor> httpContextProcessor;
        Mock<ICacheService> cacheService;
        Mock<IMapper> mapper;
        MalaysiaDbContext malaysiaDbContext;
        public DepartmentMasterServiceUnitTest()
        {
            httpContextProcessor = new Mock<IHttpContextAccessor>();
            cacheService = new Mock<ICacheService>();
            mapper = new Mock<IMapper>();
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "DepartmentMasterMalaysiaDatabase")
                            .Options;
            malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
        }

        #region DepartmentMaster service Unit test cases for GetDepartmentMaster
        [Fact]
        public void Test_GetDepartmentMaster_ReturnsDataCount()
        {
         
            var httpContextProcessor = new Mock<IHttpContextAccessor>();
            var cacheService = new Mock<ICacheService>();
            var mapper = new Mock<IMapper>();
            malaysiaDbContext.CaDepartmentMasters.Add(new CaDepartmentMaster { Id = 1, DepartmentCode = "ER", Description = "EMERGENCY ROOM", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN", AddInfo = null });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<DepartmentMasterReponse>() { new DepartmentMasterReponse { Id = 1, DepartmentCode="abc"} };
            mapper.Setup(x => x.Map<IEnumerable<DepartmentMasterReponse>>(It.IsAny<List<CaDepartmentMaster>>())).Returns(mapperResp);
            //Execute method of SUT (UserMasterRepository)  
            var departmentMasterService = new DepartmentMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = departmentMasterService.GetDepartmentMaster().Result.ToList();
            Assert.NotNull(result);
            Assert.Equal(1, Convert.ToInt32(result.Count()));
            malaysiaDbContext.Database.EnsureDeleted();

        }
        #endregion

        #region DepartmentMaster service Unit test cases for GetDepartmentMasterById

        [Fact]
        public void Test_GetDepartmentMasterById_ReturnsErrorFalseResponse()
        {
            malaysiaDbContext.CaDepartmentMasters.Add(new CaDepartmentMaster { Id = 1, Description = "test", CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new DepartmentMasterReponse() { Id = 1, Description = "test" };
            mapper.Setup(x => x.Map<CaDepartmentMaster, DepartmentMasterReponse>(It.IsAny<CaDepartmentMaster>())).Returns(mapperResp);
            var departmentMasterService = new DepartmentMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = departmentMasterService.GetDepartmentMasterById(mapperResp.Id).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region DepartmentMaster service Unit test cases for AddDepartmentMaster
        [Fact]
        public void Test_AddDepartmentMaster_ReturnsErrorFalseResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            DepartmentMasterRequest departmentMasterRequest = new DepartmentMasterRequest() { Id = 1, Description = "test", CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, IsDeleted = false, FacilityCode = "KLAN" };
            CaDepartmentMaster mapperResp = new CaDepartmentMaster() { Id = 1, Description = "Test" };
            mapper.Setup(x => x.Map<DepartmentMasterRequest, CaDepartmentMaster>(departmentMasterRequest)).Returns(mapperResp);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaDepartmentMasters.Add(new CaDepartmentMaster() { Id = 2, Description = "Test" });
            malaysiaDbContext.SaveChangesAsync();
            var departmentMasterService = new DepartmentMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = departmentMasterService.AddDepartmentMaster(departmentMasterRequest).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_AddDepartmentMaster_Returns_ReturnsErrorTrueResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            DepartmentMasterRequest requestData = null;
            CaDepartmentMaster respData = new CaDepartmentMaster { Id = 1, Description = "deparmentmaster" };
            mapper.Setup(x => x.Map<DepartmentMasterRequest, CaDepartmentMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaDepartmentMasters.Add(new CaDepartmentMaster() { Id = 2, Description = "INPATIENT ADMISSION 2" });
            malaysiaDbContext.SaveChangesAsync();

            var departmentMasterService = new DepartmentMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);

            var result = departmentMasterService.AddDepartmentMaster(requestData).Result;

            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region DepartmentMaster service Unit test cases for DeleteDepartmentMaster

        [Fact]
        public void Test_DeleteDepartmentMaster_ReturnsErrorFalseResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaDepartmentMasters.Add(new CaDepartmentMaster { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            DepartmentMasterRequest deliveryMethodMasterRequest = new DepartmentMasterRequest() { Id = 1, Description = "Test",  FacilityCode = "Test" };
            CaDepartmentMaster mapperResp = new CaDepartmentMaster() { Id = 1, Description = "Test", IsDeleted = true };
            mapper.Setup(x => x.Map<DepartmentMasterRequest, CaDepartmentMaster>(It.IsAny<DepartmentMasterRequest>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var departmentMasterService = new DepartmentMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = departmentMasterService.DeleteDepartmentMaster(deliveryMethodMasterRequest.Id).Result;

            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_DeleteDepartmentMaster_ReturnsErrorTrueResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaDepartmentMasters.Add(new CaDepartmentMaster { Id = 1, Description = "Test",  IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            DepartmentMasterRequest deliveryMethodMasterRequest = new DepartmentMasterRequest() { Id = 1, Description = "Test", FacilityCode = "Test" };
            CaDepartmentMaster mapperResp = new CaDepartmentMaster() { Id = 1, Description = "Test" };
            mapper.Setup(x => x.Map<DepartmentMasterRequest, CaDepartmentMaster>(It.IsAny<DepartmentMasterRequest>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var departmentMasterService = new DepartmentMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = departmentMasterService.DeleteDepartmentMaster(deliveryMethodMasterRequest.Id).Result;

            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region DepartmentMaster service Unit test cases for UpdateDepartmentMaster
        [Fact]
        public void Test_UpdateDepartmentMaster_ReturnsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaDepartmentMasters.Add(new CaDepartmentMaster { Id = 1, Description = "Test",  IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            DepartmentMasterRequest departmentMasterRequest = new DepartmentMasterRequest() { Id = 1, Description = "Test1", FacilityCode = "Test" };
            CaDepartmentMaster mapperResp = new CaDepartmentMaster() { Id = 1, Description = "Test1",  FacilityCode = "Test" };
            mapper.Setup(x => x.Map<DepartmentMasterRequest, CaDepartmentMaster>(It.IsAny<DepartmentMasterRequest>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)
            var departmentMasterService = new DepartmentMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = departmentMasterService.UpdateDepartmentMaster(departmentMasterRequest).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_UpdateDepartmentMaster_Returns_IsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaDepartmentMasters.Add(new CaDepartmentMaster { Id = 1,  Description = "INPATIENT ADMISSION 1", FacilityCode = "KLAN", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            DepartmentMasterRequest requestData = null;
            CaDepartmentMaster respData = new CaDepartmentMaster { Id = 1,Description = "deparmentmaster" };
            mapper.Setup(x => x.Map<DepartmentMasterRequest, CaDepartmentMaster>(It.IsAny<DepartmentMasterRequest>())).Returns(respData);
            var departmentMasterService = new DepartmentMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = departmentMasterService.UpdateDepartmentMaster(requestData).Result;
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

    }
}
