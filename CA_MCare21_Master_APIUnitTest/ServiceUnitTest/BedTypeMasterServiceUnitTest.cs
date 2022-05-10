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
   public class BedTypeMasterServiceUnitTest
    {
        Mock<IHttpContextAccessor> httpContextProcessor;
        Mock<ICacheService> cacheService;
        Mock<IMapper> mapper;
        MalaysiaDbContext malaysiaDbContext;
        public BedTypeMasterServiceUnitTest()
        {
            httpContextProcessor = new Mock<IHttpContextAccessor>();
            cacheService = new Mock<ICacheService>();
            mapper = new Mock<IMapper>();
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "BedTypeMasterMalaysiaDatabase")
                            .Options;
            malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
        }

        #region Bed Type Master CRUD Operation Unit Test Cases
        #region Add Bed Type Master  Unit Test Cases
        [Fact]
        public void Test_AddBedTypeMaster_Returns_IsErrorFalseResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            BedTypeMasterRequest requestData = new BedTypeMasterRequest { Id = 1, Description = "TestBed", BedTypeCode = "1T", MaximumBed = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            CaBedTypeMaster respData = new CaBedTypeMaster { Id = 1, Description = "TestBed", BedTypeCode = "1T" };
            mapper.Setup(x => x.Map<BedTypeMasterRequest, CaBedTypeMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaBedTypeMasters.Add(new CaBedTypeMaster() { Id = 2, Description = "TestBed2", BedTypeCode = "2T" });
            malaysiaDbContext.SaveChangesAsync();

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var bedTypeMasterService = new BedTypeMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = bedTypeMasterService.AddBedTypeMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_AddBedTypeMaster_Returns_IsErrorTrueResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            //BedTypeMasterRequest requestData = new BedTypeMasterRequest { Id = 1, Description = "", BedTypeCode = "1T", MaximumBed = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            BedTypeMasterRequest requestData = null;
            CaBedTypeMaster respData = new CaBedTypeMaster { Id = 1, Description = "", BedTypeCode = "1T" };
            mapper.Setup(x => x.Map<BedTypeMasterRequest, CaBedTypeMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaBedTypeMasters.Add(new CaBedTypeMaster() { Id = 2, Description = "", BedTypeCode = "2T" });
            malaysiaDbContext.SaveChangesAsync();

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var bedTypeMasterService = new BedTypeMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = bedTypeMasterService.AddBedTypeMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Update Bed Type Master Unit Test Cases
        [Fact]
        public void Test_UpdateBedTypeMaster_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaBedTypeMasters.Add(new CaBedTypeMaster { Id = 1, Description = "TestBed", BedTypeCode = "1T", MaximumBed = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            BedTypeMasterRequest requestData = new BedTypeMasterRequest { Id = 1, Description = "TestBed", BedTypeCode = "1T", MaximumBed = 1, FacilityCode = "KLAN" };
            CaBedTypeMaster respData = new CaBedTypeMaster { Id = 1, Description = "TestBed", BedTypeCode = "1T" };
            mapper.Setup(x => x.Map<BedTypeMasterRequest, CaBedTypeMaster>(It.IsAny<BedTypeMasterRequest>())).Returns(respData);
           
            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var bedTypeMasterService = new BedTypeMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = bedTypeMasterService.UpdateBedTypeMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_UpdateBedTypeMaster_Returns_IsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaBedTypeMasters.Add(new CaBedTypeMaster { Id = 1, Description = "TestBed", BedTypeCode = "1T", MaximumBed = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            //BedTypeMasterRequest requestData = new BedTypeMasterRequest { Id = 1, Description = "TestBed", BedTypeCode = "1T", MaximumBed = 1, FacilityCode = "KLAN" };
            BedTypeMasterRequest requestData = null;
            CaBedTypeMaster respData = new CaBedTypeMaster { Id = 1, Description = "TestBed", BedTypeCode = "1T" };
            mapper.Setup(x => x.Map<BedTypeMasterRequest, CaBedTypeMaster>(It.IsAny<BedTypeMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var bedTypeMasterService = new BedTypeMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = bedTypeMasterService.UpdateBedTypeMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Delete Bed Type Master Unit Test Cases
        [Fact]
        public void Test_DeleteBedTypeMaster_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaBedTypeMasters.Add(new CaBedTypeMaster { Id = 1, Description = "TestBed", BedTypeCode = "1T", MaximumBed = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            BedTypeMasterRequest requestData = new BedTypeMasterRequest { Id = 1, Description = "TestBed", BedTypeCode = "1T", MaximumBed = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            CaBedTypeMaster respData = new CaBedTypeMaster { Id = 1, Description = "TestBed", BedTypeCode = "1T",IsDeleted=true };
            mapper.Setup(x => x.Map<BedTypeMasterRequest, CaBedTypeMaster>(It.IsAny<BedTypeMasterRequest>())).Returns(respData);

            //Execute method of SUT (CoreMasterRepository)  
            //Arrange
            var bedTypeMasterService = new BedTypeMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = bedTypeMasterService.DeleteBedTypeMaster(requestData.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_DeleteBedTypeMaster_Returns_IsErrorTrueResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaBedTypeMasters.Add(new CaBedTypeMaster { Id = 1, Description = "TestBed", BedTypeCode = "1T", MaximumBed = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            BedTypeMasterRequest requestData = new BedTypeMasterRequest { Id = 1, Description = "TestBed", BedTypeCode = "1T", MaximumBed = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            CaBedTypeMaster respData = new CaBedTypeMaster { Id = 1, Description = "TestBed", BedTypeCode = "1T" };
            mapper.Setup(x => x.Map<BedTypeMasterRequest, CaBedTypeMaster>(It.IsAny<BedTypeMasterRequest>())).Returns(respData);

            //Execute method of SUT (CoreMasterRepository)  
            //Arrange
            var bedTypeMasterService = new BedTypeMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = bedTypeMasterService.DeleteBedTypeMaster(requestData.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get Bed Type Master By Id Unit Test Case
        [Fact]
        public void Test_GetBedTypeMasterById_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaBedTypeMasters.Add(new CaBedTypeMaster { Id = 1, Description = "TestBed", BedTypeCode = "1T", MaximumBed = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new BedTypeMasterResponse { Id = 1, Description = "TestBed", BedTypeCode = "1T", MaximumBed = 1, FacilityCode = "KLAN" };
            mapper.Setup(x => x.Map<CaBedTypeMaster, BedTypeMasterResponse>(It.IsAny<CaBedTypeMaster>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)
            //Arrange
            var bedTypeMasterService = new BedTypeMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = bedTypeMasterService.GetBedTypeMasterById(mapperResp.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get Bed Type Master Unit Test Case
        [Fact]
        public void Test_GetBedTypeMaster_Returns_DataCount()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaBedTypeMasters.Add(new CaBedTypeMaster { Id = 1, Description = "TestBed", BedTypeCode = "1T", MaximumBed = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<BedTypeMasterResponse>() { new BedTypeMasterResponse { Id = 1, Description = "TestBed", BedTypeCode = "1T", MaximumBed = 1, FacilityCode = "KLAN" } };
            mapper.Setup(x => x.Map<List<BedTypeMasterResponse>>(It.IsAny<List<CaBedTypeMaster>>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)  
            //Arrange
            var bedTypeMasterService = new BedTypeMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = bedTypeMasterService.GetBedTypeMaster().Result.ToList();
            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, Convert.ToInt32(result.Count()));
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #endregion
    }
}
