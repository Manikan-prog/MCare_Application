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
    public class ClinicMasterServiceUnitTest
    {
        Mock<IHttpContextAccessor> httpContextProcessor;
        Mock<ICacheService> cacheService;
        Mock<IMapper> mapper;
        MalaysiaDbContext malaysiaDbContext;
        public ClinicMasterServiceUnitTest()
        {
            httpContextProcessor = new Mock<IHttpContextAccessor>();
            cacheService = new Mock<ICacheService>();
            mapper = new Mock<IMapper>();
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "clinicMasterMalaysiaDatabase")
                            .Options;
            malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
        }

        #region Clinic Master CRUD Operation Unit Test Cases
        #region Add Clinic Master Unit Test Cases
        [Fact]
        public void Test_AddClinicMaster_Returns_IsErrorFalseResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            ClinicMasterRequest requestData = new ClinicMasterRequest { Id = 1, ClinicCode = "K0012", ClinicName = "Test Clinic", DoctorName = "Test Doctor", Street1 = "", Street2 = "", Street3 = "", Street4 = "", City = "Test", PostCode = "41200", StateId = 1, PhoneNo = "", FaxNo = "", MobilePhoneNo = "", EmailId = "", IsObsolete = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            CaClinicMaster respData = new CaClinicMaster { Id = 1, ClinicCode = "K0012", ClinicName = "Test Clinic" };
            mapper.Setup(x => x.Map<ClinicMasterRequest, CaClinicMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaClinicMasters.Add(new CaClinicMaster() { Id = 2, ClinicCode = "K0013", ClinicName = "Test Clinic 2" });
            malaysiaDbContext.SaveChangesAsync();

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var clinicMasterService = new ClinicMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = clinicMasterService.AddClinicMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_AddClinicMaster_Returns_IsErrorTrueResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            //CityMasterRequest requestData = new CityMasterRequest { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI", Smrpcode = "335", Smrpdescription = "Ayer Baloi", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            ClinicMasterRequest requestData = null;
            CaClinicMaster respData = new CaClinicMaster { Id = 1, ClinicCode = "K0012", ClinicName = "Test Clinic" };
            mapper.Setup(x => x.Map<ClinicMasterRequest, CaClinicMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaClinicMasters.Add(new CaClinicMaster() { Id = 2, ClinicCode = "K0013", ClinicName = "Test Clinic 2" });
            malaysiaDbContext.SaveChangesAsync();

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var clinicMasterService = new ClinicMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = clinicMasterService.AddClinicMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Update Clinic Master Unit Test Cases
        [Fact]
        public void Test_UpdateClinicMaster_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaClinicMasters.Add(new CaClinicMaster{ Id = 1, ClinicCode = "K0012", ClinicName = "Test Clinic", DoctorName = "Test Doctor", Street1 = "", Street2 = "", Street3 = "", Street4 = "", City = "Test", PostCode = "41200", StateId = 1, PhoneNo = "", FaxNo = "", MobilePhoneNo = "", EmailId = "", IsObsolete = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            ClinicMasterRequest requestData = new ClinicMasterRequest { Id = 1, ClinicCode = "K0012", ClinicName = "Test Clinic", DoctorName = "Test Doctor", Street1 = "", Street2 = "", Street3 = "", Street4 = "", City = "Test", PostCode = "41200", StateId = 1, PhoneNo = "", FaxNo = "", MobilePhoneNo = "", EmailId = "", IsObsolete = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            CaClinicMaster respData = new CaClinicMaster { Id = 1, ClinicCode = "K0012", ClinicName = "Test Clinic" };
            mapper.Setup(x => x.Map<ClinicMasterRequest, CaClinicMaster>(It.IsAny<ClinicMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var clinicMasterService = new ClinicMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = clinicMasterService.UpdateClinicMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_UpdateClinicMaster_Returns_IsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaClinicMasters.Add(new CaClinicMaster { Id = 1, ClinicCode = "K0012", ClinicName = "Test Clinic", DoctorName = "Test Doctor", Street1 = "", Street2 = "", Street3 = "", Street4 = "", City = "Test", PostCode = "41200", StateId = 1, PhoneNo = "", FaxNo = "", MobilePhoneNo = "", EmailId = "", IsObsolete = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            ClinicMasterRequest requestData = null;
            CaClinicMaster respData = new CaClinicMaster { Id = 1, ClinicCode = "K0012", ClinicName = "Test Clinic" };
            mapper.Setup(x => x.Map<ClinicMasterRequest, CaClinicMaster>(It.IsAny<ClinicMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var clinicMasterService = new ClinicMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = clinicMasterService.UpdateClinicMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Delete Clinic Master Unit Test Cases
        [Fact]
        public void Test_DeleteClinicMaster_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaClinicMasters.Add(new CaClinicMaster { Id = 1, ClinicCode = "K0012", ClinicName = "Test Clinic", DoctorName = "Test Doctor", Street1 = "", Street2 = "", Street3 = "", Street4 = "", City = "Test", PostCode = "41200", StateId = 1, PhoneNo = "", FaxNo = "", MobilePhoneNo = "", EmailId = "", IsObsolete = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            ClinicMasterRequest requestData = new ClinicMasterRequest { Id = 1, ClinicCode = "K0012", ClinicName = "Test Clinic", DoctorName = "Test Doctor", Street1 = "", Street2 = "", Street3 = "", Street4 = "", City = "Test", PostCode = "41200", StateId = 1, PhoneNo = "", FaxNo = "", MobilePhoneNo = "", EmailId = "", IsObsolete = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            CaClinicMaster respData = new CaClinicMaster { Id = 1, ClinicCode = "K0012", ClinicName = "Test Clinic", IsDeleted = true };
            mapper.Setup(x => x.Map<ClinicMasterRequest, CaClinicMaster>(It.IsAny<ClinicMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var clinicMasterService = new ClinicMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = clinicMasterService.DeleteClinicMaster(requestData.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_DeleteClinicMaster_Returns_IsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaClinicMasters.Add(new CaClinicMaster { Id = 1, ClinicCode = "K0012", ClinicName = "Test Clinic", DoctorName = "Test Doctor", Street1 = "", Street2 = "", Street3 = "", Street4 = "", City = "Test", PostCode = "41200", StateId = 1, PhoneNo = "", FaxNo = "", MobilePhoneNo = "", EmailId = "", IsObsolete = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            ClinicMasterRequest requestData = new ClinicMasterRequest { Id = 1, ClinicCode = "K0012", ClinicName = "Test Clinic", DoctorName = "Test Doctor", Street1 = "", Street2 = "", Street3 = "", Street4 = "", City = "Test", PostCode = "41200", StateId = 1, PhoneNo = "", FaxNo = "", MobilePhoneNo = "", EmailId = "", IsObsolete = false, FacilityCode = "KLAN" }; ;
            CaClinicMaster respData = new CaClinicMaster { Id = 1, ClinicCode = "K0012", ClinicName = "Test Clinic" };
            mapper.Setup(x => x.Map<ClinicMasterRequest, CaClinicMaster>(It.IsAny<ClinicMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var clinicMasterService = new ClinicMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = clinicMasterService.DeleteClinicMaster(requestData.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get Clinic Master By Id Unit Test Case
        [Fact]
        public void Test_GetClinicMasterById_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaClinicMasters.Add(new CaClinicMaster { Id = 1, ClinicCode = "K0012", ClinicName = "Test Clinic", DoctorName = "Test Doctor", Street1 = "", Street2 = "", Street3 = "", Street4 = "", City = "Test", PostCode = "41200", StateId = 1, PhoneNo = "", FaxNo = "", MobilePhoneNo = "", EmailId = "", IsObsolete = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new ClinicMasterResponse { Id = 1, ClinicCode = "K0012", ClinicName = "Test Clinic" };
            mapper.Setup(x => x.Map<CaClinicMaster, ClinicMasterResponse>(It.IsAny<CaClinicMaster>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)
            //Arrange
            var clinicMasterService = new ClinicMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = clinicMasterService.GetClinicMasterById(mapperResp.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get Clinic Master Unit Test Case
        [Fact]
        public void Test_GetClinicMaster_Returns_DataCount()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaClinicMasters.Add(new CaClinicMaster { Id = 1, ClinicCode = "K0012", ClinicName = "Test Clinic", DoctorName = "Test Doctor", Street1 = "", Street2 = "", Street3 = "", Street4 = "", City = "Test", PostCode = "41200", StateId = 1, PhoneNo = "", FaxNo = "", MobilePhoneNo = "", EmailId = "", IsObsolete = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<ClinicMasterResponse>() { new ClinicMasterResponse { Id = 1, ClinicCode = "K0012", ClinicName = "Test Clinic" } };
            mapper.Setup(x => x.Map<List<ClinicMasterResponse>>(It.IsAny<List<CaClinicMaster>>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)  
            //Arrange
            var clinicMasterService = new ClinicMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = clinicMasterService.GetClinicMaster().Result.ToList();
            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, Convert.ToInt32(result.Count()));
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #endregion
    }
}
