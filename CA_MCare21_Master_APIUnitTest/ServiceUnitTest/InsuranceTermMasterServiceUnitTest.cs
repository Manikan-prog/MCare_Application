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
    public class InsuranceTermMasterServiceUnitTest
    {
        Mock<IHttpContextAccessor> httpContextProcessor;
        Mock<ICacheService> cacheService;
        Mock<IMapper> mapper;
        MalaysiaDbContext malaysiaDbContext;
        public InsuranceTermMasterServiceUnitTest()
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

        #region Insurance Term Master CRUD Operation Unit Test Cases
        #region Add Insurance Term Master Unit Test Cases
        [Fact]
        public void Test_AddInsuranceTermMaster_ReturnsErrorFalseResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            InsuranceTermMasterRequest insuranceTermMasterRequest = new InsuranceTermMasterRequest() { Id = 1, InsuranceTerm = "test", IsDeleted = false, FacilityCode = "KLAN" };
            CaInsuranceTermMaster mapperResp = new CaInsuranceTermMaster() { Id = 1, InsuranceTerm = "test" };
            mapper.Setup(x => x.Map<InsuranceTermMasterRequest, CaInsuranceTermMaster>(insuranceTermMasterRequest)).Returns(mapperResp);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaInsuranceTermMasters.Add(new CaInsuranceTermMaster() { Id = 2, InsuranceTerm = "test" });
            malaysiaDbContext.SaveChangesAsync();
            var insuranceTermMasterService = new InsuranceTermMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = insuranceTermMasterService.AddInsuranceTermMaster(insuranceTermMasterRequest).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_AddInsuranceTermMaster_ReturnsErrorTrueResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            //InsuranceTermMasterRequest requestData = new InsuranceTermMasterRequest { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" };
            InsuranceTermMasterRequest requestData = null;
            CaInsuranceTermMaster respData = new CaInsuranceTermMaster { Id = 1, InsuranceTerm = "test", IsDeleted = false, FacilityCode = "KLAN" };
            mapper.Setup(x => x.Map<InsuranceTermMasterRequest, CaInsuranceTermMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaInsuranceTermMasters.Add(new CaInsuranceTermMaster() { Id = 1, InsuranceTerm = "test", IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChangesAsync();

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var insuranceTermMasterService = new InsuranceTermMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = insuranceTermMasterService.AddInsuranceTermMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Update Insurance Term Master Unit Test Cases
        [Fact]
        public void Test_UpdateInsuranceTermMaster_ReturnsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaInsuranceTermMasters.Add(new CaInsuranceTermMaster { Id = 1, InsuranceTerm = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            InsuranceTermMasterRequest insuranceTermMasterInput = new InsuranceTermMasterRequest() { Id = 1, InsuranceTerm = "Test1", FacilityCode = "Test" };
            CaInsuranceTermMaster mapperResp = new CaInsuranceTermMaster() { Id = 1, InsuranceTerm = "Test1", FacilityCode = "Test" };
            mapper.Setup(x => x.Map<InsuranceTermMasterRequest, CaInsuranceTermMaster>(It.IsAny<InsuranceTermMasterRequest>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)
            var insuranceTermMasterService = new InsuranceTermMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = insuranceTermMasterService.UpdateInsuranceTermMaster(insuranceTermMasterInput).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_UpdateInsuranceTermMaster_ReturnsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaInsuranceTermMasters.Add(new CaInsuranceTermMaster { Id = 1, InsuranceTerm = "Test1", FacilityCode = "Test" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            // InsuranceTermMasterRequest requestData = new InsuranceTermMasterRequest { Id = 1, InsuranceTerm = "Test1", FacilityCode = "Test" };
            InsuranceTermMasterRequest requestData = null;
            CaInsuranceTermMaster respData = new CaInsuranceTermMaster { Id = 1, InsuranceTerm = "Test1", FacilityCode = "Test" };
            mapper.Setup(x => x.Map<InsuranceTermMasterRequest, CaInsuranceTermMaster>(It.IsAny<InsuranceTermMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var insuranceTermMasterService = new InsuranceTermMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = insuranceTermMasterService.UpdateInsuranceTermMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Delete Insurance Term Master Unit Test Cases
        [Fact]
        public void Test_DeleteInsuranceTermMaster_ReturnsErrorFalseResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaInsuranceTermMasters.Add(new CaInsuranceTermMaster { Id = 1, InsuranceTerm = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            InsuranceTermMasterRequest insuranceTermMasterInput = new InsuranceTermMasterRequest() { Id = 1, InsuranceTerm = "Test", FacilityCode = "Test" };
            CaInsuranceTermMaster mapperResp = new CaInsuranceTermMaster() { Id = 1, InsuranceTerm = "Test", IsDeleted = true };
            mapper.Setup(x => x.Map<InsuranceTermMasterRequest, CaInsuranceTermMaster>(It.IsAny<InsuranceTermMasterRequest>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var insuranceTermMasterService = new InsuranceTermMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = insuranceTermMasterService.DeleteInsuranceTermMaster(insuranceTermMasterInput.Id).Result;

            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_DeleteInsuranceTermMaster_ReturnsErrorTrueResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaInsuranceTermMasters.Add(new CaInsuranceTermMaster { Id = 1, InsuranceTerm = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            InsuranceTermMasterRequest insuranceTermMasterInput = new InsuranceTermMasterRequest() { Id = 1, InsuranceTerm = "Test", FacilityCode = "Test" };
            CaInsuranceTermMaster mapperResp = new CaInsuranceTermMaster() { Id = 1, InsuranceTerm = "Test" };
            mapper.Setup(x => x.Map<InsuranceTermMasterRequest, CaInsuranceTermMaster>(It.IsAny<InsuranceTermMasterRequest>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var insuranceTermMasterService = new InsuranceTermMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = insuranceTermMasterService.DeleteInsuranceTermMaster(insuranceTermMasterInput.Id).Result;

            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get Insurance Term Master By Id Unit Test Case
        [Fact]
        public void Test_GetInsuranceTermMasterById_ReturnsErrorFalseResponse()
        {
            malaysiaDbContext.CaInsuranceTermMasters.Add(new CaInsuranceTermMaster { Id = 1, InsuranceTerm = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new InsuranceTermMasterResponse() { Id = 1, InsuranceTerm = "test" };
            mapper.Setup(x => x.Map<CaInsuranceTermMaster, InsuranceTermMasterResponse>(It.IsAny<CaInsuranceTermMaster>())).Returns(mapperResp);
            var insuranceTermMasterService = new InsuranceTermMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = insuranceTermMasterService.GetInsuranceTermMasterById(mapperResp.Id).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get Insurance Term Master Unit Test Case
        [Fact]
        public void Test_GetInsuranceTermMaster_ReturnsDataCount()
        {

            var httpContextProcessor = new Mock<IHttpContextAccessor>();
            var cacheService = new Mock<ICacheService>();
            var mapper = new Mock<IMapper>();
            malaysiaDbContext.CaInsuranceTermMasters.Add(new CaInsuranceTermMaster { Id = 1, InsuranceTerm = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<InsuranceTermMasterResponse>() { new InsuranceTermMasterResponse { Id = 1, InsuranceTerm = "test" } };
            mapper.Setup(x => x.Map<IEnumerable<InsuranceTermMasterResponse>>(It.IsAny<List<CaInsuranceTermMaster>>())).Returns(mapperResp);
            var insuranceTermMasterService = new InsuranceTermMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var insuranceTermMasters = insuranceTermMasterService.GetInsuranceTermMaster().Result.ToList();
            Assert.NotNull(insuranceTermMasters);
            Assert.Equal(1, Convert.ToInt32(insuranceTermMasters.Count()));
        }
        #endregion
        #endregion

    }
}
