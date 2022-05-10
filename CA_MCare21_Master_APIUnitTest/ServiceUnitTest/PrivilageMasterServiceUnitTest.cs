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
    public class PrivilageMasterServiceUnitTest
    {
        Mock<IHttpContextAccessor> httpContextProcessor;
        Mock<ICacheService> cacheService;
        Mock<IMapper> mapper;
        MalaysiaDbContext malaysiaDbContext;
        public PrivilageMasterServiceUnitTest()
        {
            httpContextProcessor = new Mock<IHttpContextAccessor>();
            cacheService = new Mock<ICacheService>();
            mapper = new Mock<IMapper>();
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "privilageMasterMalaysiaDatabase")
                            .Options;
            malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
        }

        #region Privilage Master CRUD Operation Unit Test Cases
        #region Add Privilage Master Unit Test Cases
        [Fact]
        public void Test_AddPrivilageMaster_Returns_IsErrorFalseResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            PrivilageMasterRequest requestData = new PrivilageMasterRequest { Id = 1, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1, IsDiscountPolicy = true, PolicyId = 1, DisplayRemarksInBill = false, DisplayDiscountInBill = true, Remarks = "DISCOUNT FOR REGISTRATION FEE, MO AND PHARMACY", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            CaPrivilageMaster respData = new CaPrivilageMaster { Id = 1, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1 };
            mapper.Setup(x => x.Map<PrivilageMasterRequest, CaPrivilageMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaPrivilageMasters.Add(new CaPrivilageMaster { Id = 2, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1 });
            malaysiaDbContext.SaveChangesAsync();

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var privilageMasterService = new PrivilageMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = privilageMasterService.AddPrivilageMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_AddPrivilageMaster_Returns_IsErrorTrueResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            PrivilageMasterRequest requestData = null;
            CaPrivilageMaster respData = new CaPrivilageMaster { Id = 1, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1 };
            mapper.Setup(x => x.Map<PrivilageMasterRequest, CaPrivilageMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaPrivilageMasters.Add(new CaPrivilageMaster { Id = 2, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1 });
            malaysiaDbContext.SaveChangesAsync();

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var privilageMasterService = new PrivilageMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = privilageMasterService.AddPrivilageMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Update Privilage Master Unit Test Cases
        [Fact]
        public void Test_UpdatePrivilageMaster_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaPrivilageMasters.Add(new CaPrivilageMaster { Id = 1, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1, IsDiscountPolicy = true, PolicyId = 1, DisplayRemarksInBill = false, DisplayDiscountInBill = true, Remarks = "DISCOUNT FOR REGISTRATION FEE, MO AND PHARMACY", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            PrivilageMasterRequest requestData = new PrivilageMasterRequest { Id = 1, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1, IsDiscountPolicy = true, PolicyId = 1, DisplayRemarksInBill = false, DisplayDiscountInBill = true, Remarks = "DISCOUNT FOR REGISTRATION FEE, MO AND PHARMACY", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            CaPrivilageMaster respData = new CaPrivilageMaster { Id = 1, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1 };
            mapper.Setup(x => x.Map<PrivilageMasterRequest, CaPrivilageMaster>(It.IsAny<PrivilageMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var privilageMasterService = new PrivilageMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = privilageMasterService.UpdatePrivilageMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_UpdatePrivilageMaster_Returns_IsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaPrivilageMasters.Add(new CaPrivilageMaster { Id = 1, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1, IsDiscountPolicy = true, PolicyId = 1, DisplayRemarksInBill = false, DisplayDiscountInBill = true, Remarks = "DISCOUNT FOR REGISTRATION FEE, MO AND PHARMACY", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            PrivilageMasterRequest requestData = null;
            CaPrivilageMaster respData = new CaPrivilageMaster { Id = 1, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1 };
            mapper.Setup(x => x.Map<PrivilageMasterRequest, CaPrivilageMaster>(It.IsAny<PrivilageMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var privilageMasterService = new PrivilageMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = privilageMasterService.UpdatePrivilageMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Delete Privilage Master Unit Test Cases
        [Fact]
        public void Test_DeletePrivilageMaster_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaPrivilageMasters.Add(new CaPrivilageMaster { Id = 1, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1, IsDiscountPolicy = true, PolicyId = 1, DisplayRemarksInBill = false, DisplayDiscountInBill = true, Remarks = "DISCOUNT FOR REGISTRATION FEE, MO AND PHARMACY", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            PrivilageMasterRequest requestData = new PrivilageMasterRequest { Id = 1, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1, IsDiscountPolicy = true, PolicyId = 1, DisplayRemarksInBill = false, DisplayDiscountInBill = true, Remarks = "DISCOUNT FOR REGISTRATION FEE, MO AND PHARMACY", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            CaPrivilageMaster respData = new CaPrivilageMaster { Id = 1, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1 ,IsDeleted=true};
            mapper.Setup(x => x.Map<PrivilageMasterRequest, CaPrivilageMaster>(It.IsAny<PrivilageMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var privilageMasterService = new PrivilageMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = privilageMasterService.DeletePrivilageMaster(requestData.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_DeletePrivilageMaster_Returns_IsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaPrivilageMasters.Add(new CaPrivilageMaster { Id = 1, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1, IsDiscountPolicy = true, PolicyId = 1, DisplayRemarksInBill = false, DisplayDiscountInBill = true, Remarks = "DISCOUNT FOR REGISTRATION FEE, MO AND PHARMACY", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            PrivilageMasterRequest requestData = new PrivilageMasterRequest { Id = 1, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1, IsDiscountPolicy = true, PolicyId = 1, DisplayRemarksInBill = false, DisplayDiscountInBill = true, Remarks = "DISCOUNT FOR REGISTRATION FEE, MO AND PHARMACY", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            CaPrivilageMaster respData = new CaPrivilageMaster { Id = 1, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1 };
            mapper.Setup(x => x.Map<PrivilageMasterRequest, CaPrivilageMaster>(It.IsAny<PrivilageMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var privilageMasterService = new PrivilageMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = privilageMasterService.DeletePrivilageMaster(requestData.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get Privilage Master By Id Unit Test Case
        [Fact]
        public void Test_GetPrivilageMasterById_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaPrivilageMasters.Add(new CaPrivilageMaster { Id = 1, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1, IsDiscountPolicy = true, PolicyId = 1, DisplayRemarksInBill = false, DisplayDiscountInBill = true, Remarks = "DISCOUNT FOR REGISTRATION FEE, MO AND PHARMACY", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new PrivilageMasterResponse { Id = 1, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1, IsDiscountPolicy = true, PolicyId = 1, FacilityCode = "KLAN" };
            mapper.Setup(x => x.Map<CaPrivilageMaster, PrivilageMasterResponse>(It.IsAny<CaPrivilageMaster>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)
            //Arrange
            var privilageMasterService = new PrivilageMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = privilageMasterService.GetPrivilageMasterById(mapperResp.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get Privilage Master Unit Test Case
        [Fact]
        public void Test_GetPrivilageMaster_Returns_DataCount()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaPrivilageMasters.Add(new CaPrivilageMaster { Id = 1, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1, IsDiscountPolicy = true, PolicyId = 1, DisplayRemarksInBill = false, DisplayDiscountInBill = true, Remarks = "DISCOUNT FOR REGISTRATION FEE, MO AND PHARMACY", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<PrivilageMasterResponse>() { new PrivilageMasterResponse { Id = 1, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1, IsDiscountPolicy = true, PolicyId = 1, FacilityCode = "KLAN" } };
            mapper.Setup(x => x.Map<List<PrivilageMasterResponse>>(It.IsAny<List<CaPrivilageMaster>>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)  
            //Arrange
            var privilageMasterService = new PrivilageMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = privilageMasterService.GetPrivilageMaster().Result.ToList();
            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, Convert.ToInt32(result.Count()));
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #endregion
    }
}
