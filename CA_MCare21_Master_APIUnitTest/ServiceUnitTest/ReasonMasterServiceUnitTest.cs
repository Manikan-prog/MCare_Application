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
  public class ReasonMasterServiceUnitTest
    {
        Mock<IHttpContextAccessor> httpContextProcessor;
        Mock<ICacheService> cacheService;
        Mock<IMapper> mapper;
        MalaysiaDbContext malaysiaDbContext;
        public ReasonMasterServiceUnitTest()
        {
            httpContextProcessor = new Mock<IHttpContextAccessor>();
            cacheService = new Mock<ICacheService>();
            mapper = new Mock<IMapper>();
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "ReasonMasterMalaysiaDatabase")
                            .Options;
            malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
        }

        #region Reason Master service Unit test cases for GetReasonMaster
        [Fact]
        public void Test_GetReasonMaster_ReturnsDataCount()
        {
          
            var httpContextProcessor = new Mock<IHttpContextAccessor>();
            var cacheService = new Mock<ICacheService>();
            var mapper = new Mock<IMapper>();
            malaysiaDbContext.CaReasonMasters.Add(new CaReasonMaster
            {
                Id = 1,
                ReasonCode = "test",
                ReasonName = "test",
                IsDeleted = false,
                IsAdmission = true,
                IsOe = true,
                IsRegestration = true,
                IsBilling = true,
                IsPaymentReceipt = true,
                IsRefund = true,
                IsPaymentVoucher = true,
                IsContract = true,
                IsAccReceipt = true,
                IsDiscount = true,
                IsDischarge = true,
                IsBedTransfer = true,
                IsPrescription = true,
                IsIndentRequisition = true,
                IsTakeNoteRecording = true,
                IsPatientUnlocked = true,
                IsMedicalLegalLockdown = true,
                IsMedicalLegalUnlock = true,
                IsCancelPrintMedicalRecordQueue = true,
                IsContactPriceOverriden = true,
                IsRejectedPendingApprovalRx = true,
                IsDoNotMerge = true,
                IsMergeMedicalRecord = true,
                IsPregnancyStatusUpdate = true,
                IsRejectedPendingApprovalServices = true,
                IsChangePrescription = true,
                CreatedDate = DateTime.Now.Date,
                UpdatedBy = null,
                UpdatedDate = null,
                FacilityCode = "KLAN"
            });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<ReasonMasterResponse>() { new ReasonMasterResponse { Id = 1, ReasonCode="abc" } };
            mapper.Setup(x => x.Map<IEnumerable<ReasonMasterResponse>>(It.IsAny<List<CaReasonMaster>>())).Returns(mapperResp);
            var reasonMasterService = new ReasonMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = reasonMasterService.GetReasonMaster().Result.ToList();
            Assert.NotNull(result);
            Assert.Equal(1, Convert.ToInt32(result.Count()));
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Reason Master service Unit test cases for GetReasonMasterById
        [Fact]
        public void Test_GetReasonMasterById_ReturnsErrorFalseResponse()
        {
            malaysiaDbContext.CaReasonMasters.Add(new CaReasonMaster { Id = 1, IsAdmission = true, IsBilling = true, IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new ReasonMasterResponse() { Id = 1, ReasonCode="abc" };
            mapper.Setup(x => x.Map<CaReasonMaster, ReasonMasterResponse>(It.IsAny<CaReasonMaster>())).Returns(mapperResp);
            var reasonMasterService = new ReasonMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = reasonMasterService.GetReasonMasterById(mapperResp.Id).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Reason Master service Unit test cases for AddReasonMaster
        [Fact]
        public void Test_AddReasonMaster_ReturnsErrorFalseResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            ReasonMasterRequest reasonMasterRequest = new ReasonMasterRequest() { Id = 1, IsAdmission=true, IsBilling=true, IsDeleted = false, FacilityCode = "KLAN" };
            CaReasonMaster mapperResp = new CaReasonMaster() { Id = 1, ReasonCode = "abc" };
            mapper.Setup(x => x.Map<ReasonMasterRequest, CaReasonMaster>(reasonMasterRequest)).Returns(mapperResp);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaReasonMasters.Add(new CaReasonMaster() { Id = 2, IsAdmission = true, IsBilling = true });
            malaysiaDbContext.SaveChangesAsync();
            var reasonMasterService = new ReasonMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = reasonMasterService.AddReasonMaster(reasonMasterRequest).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_AddReasonMaster_Returns_ReturnsErrorTrueResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            ReasonMasterRequest requestData = null;
            CaReasonMaster respData = new CaReasonMaster { Id = 1, IsAdmission = true };
            mapper.Setup(x => x.Map<ReasonMasterRequest, CaReasonMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaReasonMasters.Add(new CaReasonMaster() { Id = 2, IsAdmission = true });
            malaysiaDbContext.SaveChangesAsync();
            var reasonMasterService = new ReasonMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = reasonMasterService.AddReasonMaster(requestData).Result;
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Reason Master service Unit test cases for DeleteReasonMaster
        [Fact]
        public void Test_DeleteReasonMaster_ReturnsErrorFalseResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaReasonMasters.Add(new CaReasonMaster { Id = 1, IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            ReasonMasterRequest reasonMasterRequest = new ReasonMasterRequest() { Id = 1, FacilityCode = "Test" };
            CaReasonMaster mapperResp = new CaReasonMaster() { Id = 1, IsDeleted = true };
            mapper.Setup(x => x.Map<ReasonMasterRequest, CaReasonMaster>(It.IsAny<ReasonMasterRequest>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var reasonMasterService = new ReasonMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = reasonMasterService.DeleteReasonMaster(reasonMasterRequest.Id).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_DeleteReasonMaster_ReturnsErrorTrueResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaReasonMasters.Add(new CaReasonMaster { Id = 1, IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            ReasonMasterRequest reasonMasterRequest = new ReasonMasterRequest() { Id = 1, FacilityCode = "Test" };
            CaReasonMaster mapperResp = new CaReasonMaster() { Id = 1, IsAppointment=true, IsAccReceipt=true };
            mapper.Setup(x => x.Map<ReasonMasterRequest, CaReasonMaster>(It.IsAny<ReasonMasterRequest>())).Returns(mapperResp);
            var reasonMasterService = new ReasonMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = reasonMasterService.DeleteReasonMaster(reasonMasterRequest.Id).Result;
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Reason Master service Unit test cases for UpdateReasonMaster
        [Fact]
        public void Test_UpdateReasonMaster_ReturnsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaReasonMasters.Add(new CaReasonMaster { Id = 1, IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            ReasonMasterRequest reasonMasterRequest = new ReasonMasterRequest() { Id = 1, FacilityCode = "Test" };
            CaReasonMaster mapperResp = new CaReasonMaster() { Id = 1, FacilityCode = "Test" };
            mapper.Setup(x => x.Map<ReasonMasterRequest, CaReasonMaster>(It.IsAny<ReasonMasterRequest>())).Returns(mapperResp);
            var reasonMasterService = new ReasonMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = reasonMasterService.UpdateReasonMaster(reasonMasterRequest).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_UpdateReasonMaster_Returns_IsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaReasonMasters.Add(new CaReasonMaster { Id = 1, FacilityCode = "KLAN", IsDeleted = false });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            ReasonMasterRequest requestData = null;
            CaReasonMaster respData = new CaReasonMaster { Id = 1, IsAdmission=true};
            mapper.Setup(x => x.Map<ReasonMasterRequest, CaReasonMaster>(It.IsAny<ReasonMasterRequest>())).Returns(respData);
            var reasonMasterService = new ReasonMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = reasonMasterService.UpdateReasonMaster(requestData).Result;
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion
    }
}
