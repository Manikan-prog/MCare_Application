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
    public class IdentificationMasterServiceUnitTest
    {
        Mock<IHttpContextAccessor> httpContextProcessor;
        Mock<ICacheService> cacheService;
        Mock<IMapper> mapper;
        MalaysiaDbContext malaysiaDbContext;
        public IdentificationMasterServiceUnitTest()
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

        #region Identification Master CRUD Operation Unit Test Cases
        #region Add Identification Master Unit Test Cases
        [Fact]
        public void Test_AddIdentificationMaster_ReturnsErrorFalseResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            IdentificationMasterRequest identificationMasterRequest = new IdentificationMasterRequest() { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" };
            CaIdentificationMaster mapperResp = new CaIdentificationMaster() { Id = 1, Description = "test" };
            mapper.Setup(x => x.Map<IdentificationMasterRequest, CaIdentificationMaster>(identificationMasterRequest)).Returns(mapperResp);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaIdentificationMasters.Add(new CaIdentificationMaster() { Id = 2, Description = "test" });
            malaysiaDbContext.SaveChangesAsync();
            var identificationMasterService = new IdentificationMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = identificationMasterService.AddIdentificationMaster(identificationMasterRequest).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_AddIdentificationMaster_ReturnsErrorTrueResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            //IdentificationMasterRequest requestData = new IdentificationMasterRequest { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" };
            IdentificationMasterRequest requestData = null;
            CaIdentificationMaster respData = new CaIdentificationMaster { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" };
            mapper.Setup(x => x.Map<IdentificationMasterRequest, CaIdentificationMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaIdentificationMasters.Add(new CaIdentificationMaster() { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChangesAsync();

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var identificationMasterService = new IdentificationMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = identificationMasterService.AddIdentificationMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Update Identification Master Unit Test Cases
        [Fact]
        public void Test_UpdateIdentificationMaster_ReturnsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaIdentificationMasters.Add(new CaIdentificationMaster { Id = 1, Description = "Test", Code = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            IdentificationMasterRequest identificationMasterInput = new IdentificationMasterRequest() { Id = 1, Description = "Test1", Code = "Test", FacilityCode = "Test" };
            CaIdentificationMaster mapperResp = new CaIdentificationMaster() { Id = 1, Description = "Test1", Code = "Test", FacilityCode = "Test" };
            mapper.Setup(x => x.Map<IdentificationMasterRequest, CaIdentificationMaster>(It.IsAny<IdentificationMasterRequest>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)
            var identificationMasterService = new IdentificationMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = identificationMasterService.UpdateIdentificationMaster(identificationMasterInput).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_UpdateIdentificationMaster_ReturnsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaIdentificationMasters.Add(new CaIdentificationMaster { Id = 1, Description = "Test", Code = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            // IdentificationMasterRequest requestData = new IdentificationMasterRequest { Id = 1, Description = "Test1", Code = "Test", FacilityCode = "Test" };
            IdentificationMasterRequest requestData = null;
            CaIdentificationMaster respData = new CaIdentificationMaster { Id = 1, Description = "Test1", Code = "Test", FacilityCode = "Test" };
            mapper.Setup(x => x.Map<IdentificationMasterRequest, CaIdentificationMaster>(It.IsAny<IdentificationMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var identificationMasterService = new IdentificationMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = identificationMasterService.UpdateIdentificationMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Delete Identification Master Unit Test Cases
        [Fact]
        public void Test_DeleteIdentificationMaster_ReturnsErrorFalseResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaIdentificationMasters.Add(new CaIdentificationMaster { Id = 1, Description = "Test", Code = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            IdentificationMasterRequest identificationMasterInput = new IdentificationMasterRequest() { Id = 1, Description = "Test", Code = "Test", FacilityCode = "Test" };
            CaIdentificationMaster mapperResp = new CaIdentificationMaster() { Id = 1, Description = "Test", IsDeleted = true };
            mapper.Setup(x => x.Map<IdentificationMasterRequest, CaIdentificationMaster>(It.IsAny<IdentificationMasterRequest>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var identificationMasterService = new IdentificationMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = identificationMasterService.DeleteIdentificationMaster(identificationMasterInput.Id).Result;

            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_DeleteIdentificationMaster_ReturnsErrorTrueResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaIdentificationMasters.Add(new CaIdentificationMaster { Id = 1, Description = "Test", Code = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            IdentificationMasterRequest identificationMasterInput = new IdentificationMasterRequest() { Id = 1, Description = "Test", Code = "Test", FacilityCode = "Test" };
            CaIdentificationMaster mapperResp = new CaIdentificationMaster() { Id = 1, Description = "Test" };
            mapper.Setup(x => x.Map<IdentificationMasterRequest, CaIdentificationMaster>(It.IsAny<IdentificationMasterRequest>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var identificationMasterService = new IdentificationMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = identificationMasterService.DeleteIdentificationMaster(identificationMasterInput.Id).Result;

            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get Identification Master By Id Unit Test Case
        [Fact]
        public void Test_GetIdentificationMasterById_ReturnsErrorFalseResponse()
        {
            malaysiaDbContext.CaIdentificationMasters.Add(new CaIdentificationMaster { Id = 1, Code = "IT001", Description = "Test", IdentificationFormat = "Alphanumeric", ExpiryDate = true, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, MasterIdentificationTypeId = 2, Smrpcode = "1", Smrpdescription = "Testing", FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new IdentificationMasterResponse() { Id = 1, Description = "test" };
            mapper.Setup(x => x.Map<CaIdentificationMaster, IdentificationMasterResponse>(It.IsAny<CaIdentificationMaster>())).Returns(mapperResp);
            var identificationMasterService = new IdentificationMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = identificationMasterService.GetIdentificationMasterById(mapperResp.Id).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get Identification Master Unit Test Case
        [Fact]
        public void Test_GetIdentificationMaster_ReturnsDataCount()
        {

            var httpContextProcessor = new Mock<IHttpContextAccessor>();
            var cacheService = new Mock<ICacheService>();
            var mapper = new Mock<IMapper>();
            malaysiaDbContext.CaIdentificationMasters.Add(new CaIdentificationMaster { Id = 1, Code = "IT001", Description = "Test", IdentificationFormat = "Alphanumeric", ExpiryDate = true, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, MasterIdentificationTypeId = 2, Smrpcode = "1", Smrpdescription = "Testing", FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<IdentificationMasterResponse>() { new IdentificationMasterResponse { Id = 1, Description = "test" } };
            mapper.Setup(x => x.Map<IEnumerable<IdentificationMasterResponse>>(It.IsAny<List<CaIdentificationMaster>>())).Returns(mapperResp);
            var identificationMasterService = new IdentificationMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var identificationMasters = identificationMasterService.GetIdentificationMaster().Result.ToList();
            Assert.NotNull(identificationMasters);
            Assert.Equal(1, Convert.ToInt32(identificationMasters.Count()));

        }
        #endregion                        
        #endregion

    }
}
