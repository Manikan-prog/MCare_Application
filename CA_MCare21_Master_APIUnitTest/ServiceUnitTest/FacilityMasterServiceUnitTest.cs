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
    public class FacilityMasterServiceUnitTest
    {
        Mock<IHttpContextAccessor> httpContextProcessor;
        Mock<ICacheService> cacheService;
        Mock<IMapper> mapper;
        MalaysiaDbContext malaysiaDbContext;
        public FacilityMasterServiceUnitTest()
        {
            httpContextProcessor = new Mock<IHttpContextAccessor>();
            cacheService = new Mock<ICacheService>();
            mapper = new Mock<IMapper>();
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "facilityMasterMalaysiaDatabase")
                            .Options;
            malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
        }

        #region Facility Master CRUD Operation Unit Test Cases
        #region Add Facility Master Unit Test Cases
        [Fact]
        public void Test_AddFacilityMaster_Returns_IsErrorFalseResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            FacilityMasterRequest requestData = new FacilityMasterRequest { Id = 1, FacilityCode = "KLAN", Description = "Test Facility", PrincipalCurrencyId = 1, ForeignCurrencyId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            CaFacilityMaster respData = new CaFacilityMaster { Id = 1, FacilityCode = "KLAN", Description = "Test Facility", PrincipalCurrencyId = 1, ForeignCurrencyId = 1 };
            mapper.Setup(x => x.Map<FacilityMasterRequest, CaFacilityMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaFacilityMasters.Add(new CaFacilityMaster { Id = 2, FacilityCode = "KLAN", Description = "Test Facility 2", PrincipalCurrencyId = 1, ForeignCurrencyId = 1 });
            malaysiaDbContext.SaveChangesAsync();

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var facilityMasterService = new FacilityMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = facilityMasterService.AddFacilityMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_AddFacilityMaster_Returns_IsErrorTrueResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            FacilityMasterRequest requestData = null;
            CaFacilityMaster respData = new CaFacilityMaster { Id = 1, FacilityCode = "KLAN", Description = "Test Facility", PrincipalCurrencyId = 1, ForeignCurrencyId = 1 };
            mapper.Setup(x => x.Map<FacilityMasterRequest, CaFacilityMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaFacilityMasters.Add(new CaFacilityMaster { Id = 2, FacilityCode = "KLAN", Description = "Test Facility 2", PrincipalCurrencyId = 1, ForeignCurrencyId = 1 });
            malaysiaDbContext.SaveChangesAsync();

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var facilityMasterService = new FacilityMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = facilityMasterService.AddFacilityMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Update Facility Master Unit Test Cases
        [Fact]
        public void Test_UpdateFacilityMaster_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaFacilityMasters.Add(new CaFacilityMaster { Id = 1, FacilityCode = "KLAN", Description = "Test Facility", PrincipalCurrencyId = 1, ForeignCurrencyId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            FacilityMasterRequest requestData = new FacilityMasterRequest { Id = 1, FacilityCode = "KLAN", Description = "Test Facility", PrincipalCurrencyId = 1, ForeignCurrencyId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null }; 
            CaFacilityMaster respData = new CaFacilityMaster { Id = 1, FacilityCode = "KLAN", Description = "Test Facility", PrincipalCurrencyId = 1, ForeignCurrencyId = 1 };
            mapper.Setup(x => x.Map<FacilityMasterRequest, CaFacilityMaster>(It.IsAny<FacilityMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var facilityMasterService = new FacilityMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = facilityMasterService.UpdateFacilityMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_UpdateFacilityMaster_Returns_IsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaFacilityMasters.Add(new CaFacilityMaster { Id = 1, FacilityCode = "KLAN", Description = "Test Facility", PrincipalCurrencyId = 1, ForeignCurrencyId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            FacilityMasterRequest requestData = null;
            CaFacilityMaster respData = new CaFacilityMaster { Id = 1, FacilityCode = "KLAN", Description = "Test Facility", PrincipalCurrencyId = 1, ForeignCurrencyId = 1 };
            mapper.Setup(x => x.Map<FacilityMasterRequest, CaFacilityMaster>(It.IsAny<FacilityMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var facilityMasterService = new FacilityMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = facilityMasterService.UpdateFacilityMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Delete Facility Master Unit Test Cases
        [Fact]
        public void Test_DeleteFacilityMaster_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaFacilityMasters.Add(new CaFacilityMaster { Id = 1, FacilityCode = "KLAN", Description = "Test Facility", PrincipalCurrencyId = 1, ForeignCurrencyId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            FacilityMasterRequest requestData = new FacilityMasterRequest { Id = 1, FacilityCode = "KLAN", Description = "Test Facility", PrincipalCurrencyId = 1, ForeignCurrencyId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            CaFacilityMaster respData = new CaFacilityMaster { Id = 1, FacilityCode = "KLAN", Description = "Test Facility", PrincipalCurrencyId = 1, ForeignCurrencyId = 1 ,IsDeleted=true};
            mapper.Setup(x => x.Map<FacilityMasterRequest, CaFacilityMaster>(It.IsAny<FacilityMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var facilityMasterService = new FacilityMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = facilityMasterService.DeleteFacilityMaster(requestData.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_DeleteFacilityMaster_Returns_IsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaFacilityMasters.Add(new CaFacilityMaster { Id = 1, FacilityCode = "KLAN", Description = "Test Facility", PrincipalCurrencyId = 1, ForeignCurrencyId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

             FacilityMasterRequest requestData = new FacilityMasterRequest { Id = 1, FacilityCode = "KLAN", Description = "Test Facility", PrincipalCurrencyId = 1, ForeignCurrencyId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            CaFacilityMaster respData = new CaFacilityMaster { Id = 1, FacilityCode = "KLAN", Description = "Test Facility", PrincipalCurrencyId = 1, ForeignCurrencyId = 1 };
            mapper.Setup(x => x.Map<FacilityMasterRequest, CaFacilityMaster>(It.IsAny<FacilityMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var facilityMasterService = new FacilityMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = facilityMasterService.DeleteFacilityMaster(requestData.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get Facility Master By Id Unit Test Case
        [Fact]
        public void Test_GetFacilityMasterById_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaFacilityMasters.Add(new CaFacilityMaster { Id = 1, FacilityCode = "KLAN", Description = "Test Facility", PrincipalCurrencyId = 1, ForeignCurrencyId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new FacilityMasterResponse { Id = 1, FacilityCode = "KLAN", Description = "Test Facility", ForeignCurrencyId = 1, PrincipalCurrencyId = 1 };
            mapper.Setup(x => x.Map<CaFacilityMaster, FacilityMasterResponse>(It.IsAny<CaFacilityMaster>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)
            //Arrange
            var facilityMasterService = new FacilityMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = facilityMasterService.GetFacilityMasterById(mapperResp.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get Facility Master Unit Test Case
        [Fact]
        public void Test_GetFacilityMaster_Returns_DataCount()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaFacilityMasters.Add(new CaFacilityMaster { Id = 1, FacilityCode = "KLAN", Description = "Test Facility", PrincipalCurrencyId = 1, ForeignCurrencyId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<FacilityMasterResponse>() { new FacilityMasterResponse { Id = 1, FacilityCode = "KLAN", Description = "Test Facility", ForeignCurrencyId = 1, PrincipalCurrencyId = 1 } };
            mapper.Setup(x => x.Map<List<FacilityMasterResponse>>(It.IsAny<List<CaFacilityMaster>>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)  
            //Arrange
            var facilityMasterService = new FacilityMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = facilityMasterService.GetFacilityMaster().Result.ToList();
            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, Convert.ToInt32(result.Count()));
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #endregion
    }
}
