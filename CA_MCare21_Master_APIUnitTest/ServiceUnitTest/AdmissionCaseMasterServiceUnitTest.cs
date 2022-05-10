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
    public class AdmissionCaseMasterServiceUnitTest
    {
        Mock<IHttpContextAccessor> httpContextProcessor;
        Mock<ICacheService> cacheService;
        Mock<IMapper> mapper;
        MalaysiaDbContext malaysiaDbContext;
        public AdmissionCaseMasterServiceUnitTest()
        {
            httpContextProcessor = new Mock<IHttpContextAccessor>();
            cacheService = new Mock<ICacheService>();
            mapper = new Mock<IMapper>();
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "AdmissionCaseMasterMalaysiaDatabase")
                            .Options;
            malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
        }

        #region Admission Case Master CRUD Operation Unit Test Cases
        #region Add Admission Case Master Unit Test Cases
        [Fact]
        public void Test_AddAdmissionCaseMaster_Returns_IsErrorFalseResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            AdmissionCasesMasterRequest requestData = new AdmissionCasesMasterRequest { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1", EstimatedCostFrom = 64320.00, EstimatedCostTo = 4324264564.00, DepositCollected = 500.12, FacilityCode = "KLAN" };
            CaAdmissionCasesMaster respData = new CaAdmissionCasesMaster { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1" };
            mapper.Setup(x => x.Map<AdmissionCasesMasterRequest, CaAdmissionCasesMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaAdmissionCasesMasters.Add(new CaAdmissionCasesMaster() { Id = 2, Code = "INA2", Description = "INPATIENT ADMISSION 2" });
            malaysiaDbContext.SaveChangesAsync();
            
            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var admissionCaseMasterService = new AdmissionCaseMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);            
            //Act
            var result = admissionCaseMasterService.AddAdmissionCasesMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_AddAdmissionCaseMaster_Returns_IsErrorTrueResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            //AdmissionCasesMasterRequest requestData = new AdmissionCasesMasterRequest { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1", EstimatedCostFrom = 64320.00, EstimatedCostTo = 4324264564.00, DepositCollected = 500.12, FacilityCode = "KLAN" };
            AdmissionCasesMasterRequest requestData = null;
            CaAdmissionCasesMaster respData = new CaAdmissionCasesMaster { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1" };
            mapper.Setup(x => x.Map<AdmissionCasesMasterRequest, CaAdmissionCasesMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaAdmissionCasesMasters.Add(new CaAdmissionCasesMaster() { Id = 2, Code = "INA2", Description = "INPATIENT ADMISSION 2" });
            malaysiaDbContext.SaveChangesAsync();

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var admissionCaseMasterService = new AdmissionCaseMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = admissionCaseMasterService.AddAdmissionCasesMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Update Admission Case Master Unit Test Cases
        [Fact]
        public void Test_UpdateAdmissionCaseMaster_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaAdmissionCasesMasters.Add(new CaAdmissionCasesMaster { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1", EstimatedCostFrom = 64320.00m, EstimatedCostTo = 4324264564.00m, DepositCollected = 500.12m, FacilityCode = "KLAN", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            AdmissionCasesMasterRequest requestData = new AdmissionCasesMasterRequest { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1", EstimatedCostFrom = 64320.00, EstimatedCostTo = 4324264564.00, DepositCollected = 500.12, FacilityCode = "KLAN" };
            CaAdmissionCasesMaster respData = new CaAdmissionCasesMaster { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1" };
            mapper.Setup(x => x.Map<AdmissionCasesMasterRequest, CaAdmissionCasesMaster>(It.IsAny<AdmissionCasesMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var admissionCaseMasterService = new AdmissionCaseMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);           
            //Act
            var result = admissionCaseMasterService.UpdateAdmissionCasesMaster(requestData).Result;
           //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_UpdateAdmissionCaseMaster_Returns_IsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaAdmissionCasesMasters.Add(new CaAdmissionCasesMaster { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1", EstimatedCostFrom = 64320.00m, EstimatedCostTo = 4324264564.00m, DepositCollected = 500.12m, FacilityCode = "KLAN", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            // AdmissionCasesMasterRequest requestData = new AdmissionCasesMasterRequest { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1", EstimatedCostFrom = 64320.00, EstimatedCostTo = 4324264564.00, DepositCollected = 500.12, FacilityCode = "KLAN" };
            AdmissionCasesMasterRequest requestData = null;
            CaAdmissionCasesMaster respData = new CaAdmissionCasesMaster { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1" };
            mapper.Setup(x => x.Map<AdmissionCasesMasterRequest, CaAdmissionCasesMaster>(It.IsAny<AdmissionCasesMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var admissionCaseMasterService = new AdmissionCaseMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = admissionCaseMasterService.UpdateAdmissionCasesMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Delete Admission Case Master Unit Test Cases
        [Fact]
        public void Test_DeleteAdmissionCasesMaster_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaAdmissionCasesMasters.Add(new CaAdmissionCasesMaster { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1", EstimatedCostFrom = 64320.00m, EstimatedCostTo = 4324264564.00m, DepositCollected = 500.12m, FacilityCode = "KLAN", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            AdmissionCasesMasterRequest requestData = new AdmissionCasesMasterRequest { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1", EstimatedCostFrom = 64320.00, EstimatedCostTo = 4324264564.00, DepositCollected = 500.12, FacilityCode = "KLAN" };
            CaAdmissionCasesMaster respData = new CaAdmissionCasesMaster { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1", IsDeleted=true };
            mapper.Setup(x => x.Map<AdmissionCasesMasterRequest, CaAdmissionCasesMaster>(It.IsAny<AdmissionCasesMasterRequest>())).Returns(respData);
            
            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var admissionCaseMasterService = new AdmissionCaseMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = admissionCaseMasterService.DeleteAdmissionCasesMaster(requestData.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_DeleteCauseOfDeathMaster_Returns_IsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaAdmissionCasesMasters.Add(new CaAdmissionCasesMaster { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1", EstimatedCostFrom = 64320.00m, EstimatedCostTo = 4324264564.00m, DepositCollected = 500.12m, FacilityCode = "KLAN", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            AdmissionCasesMasterRequest requestData = new AdmissionCasesMasterRequest { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1", EstimatedCostFrom = 64320.00, EstimatedCostTo = 4324264564.00, DepositCollected = 500.12, FacilityCode = "KLAN" };
            CaAdmissionCasesMaster respData = new CaAdmissionCasesMaster { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1" };
            mapper.Setup(x => x.Map<AdmissionCasesMasterRequest, CaAdmissionCasesMaster>(It.IsAny<AdmissionCasesMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var admissionCaseMasterService = new AdmissionCaseMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = admissionCaseMasterService.DeleteAdmissionCasesMaster(requestData.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get Admission Case Master Unit Test Case
        [Fact]
        public void Test_GetAdmissionCaseMaster_Returns_DataCount()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaAdmissionCasesMasters.Add(new CaAdmissionCasesMaster { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1", EstimatedCostFrom = 64320.00m, EstimatedCostTo = 4324264564.00m, DepositCollected = 500.12m, FacilityCode = "KLAN", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<AdmissionCasesMasterResponse>() { new AdmissionCasesMasterResponse { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1", EstimatedCostFrom = 64320.00m, EstimatedCostTo = 4324264564.00m, DepositCollected = 500.12m } };
            mapper.Setup(x => x.Map<List<AdmissionCasesMasterResponse>>(It.IsAny<List<CaAdmissionCasesMaster>>())).Returns(mapperResp);
            
            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var admissionCaseMasterService = new AdmissionCaseMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = admissionCaseMasterService.GetAdmissionCaseMaster().Result.ToList();
            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, Convert.ToInt32(result.Count()));
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get Admission Case Master By Id Unit Test Case
        [Fact]
        public void Test_GetAdmissionCaseMasterById_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaAdmissionCasesMasters.Add(new CaAdmissionCasesMaster { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1", EstimatedCostFrom = 64320.00m, EstimatedCostTo = 4324264564.00m, DepositCollected = 500.12m, FacilityCode = "KLAN", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new AdmissionCasesMasterResponse { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1", EstimatedCostFrom = 64320.00m, EstimatedCostTo = 4324264564.00m, DepositCollected = 500.12m };
            mapper.Setup(x => x.Map<CaAdmissionCasesMaster, AdmissionCasesMasterResponse>(It.IsAny<CaAdmissionCasesMaster>())).Returns(mapperResp);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var admissionCaseMasterService = new AdmissionCaseMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = admissionCaseMasterService.GetAdmissionCaseMasterById(mapperResp.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #endregion
    }
}
