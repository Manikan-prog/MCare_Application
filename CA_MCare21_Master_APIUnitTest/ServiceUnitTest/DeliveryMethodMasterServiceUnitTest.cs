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
    public class DeliveryMethodMasterServiceUnitTest
    {
        Mock<IHttpContextAccessor> httpContextProcessor;
        Mock<ICacheService> cacheService;
        Mock<IMapper> mapper;
        MalaysiaDbContext malaysiaDbContext;
        public DeliveryMethodMasterServiceUnitTest()
        {
            httpContextProcessor = new Mock<IHttpContextAccessor>();
            cacheService = new Mock<ICacheService>();
            mapper = new Mock<IMapper>();
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "DeliveryMethodMasterMalaysiaDatabase")
                            .Options;
            malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
        }
        #region Delivery service Unit test cases for GetDeliveryMethodMaster

        [Fact]
        public void Test_GetDeliveryMethodMaster_ReturnsDataCount()
        {

            var httpContextProcessor = new Mock<IHttpContextAccessor>();
            var cacheService = new Mock<ICacheService>();
            var mapper = new Mock<IMapper>();
            malaysiaDbContext.CaDeliveryMethodMasters.Add(new CaDeliveryMethodMaster { Id = 1, Description = "test", CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "01", IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<DeliveryMethodMasterResponse>() { new DeliveryMethodMasterResponse { Id = 1, Description = "test" } };
            mapper.Setup(x => x.Map<IEnumerable<DeliveryMethodMasterResponse>>(It.IsAny<List<CaDeliveryMethodMaster>>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var deliveryMethodMasterService = new DeliveryMethodMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = deliveryMethodMasterService.GetDeliveryMethodMaster().Result.ToList();
            Assert.NotNull(result);
            Assert.Equal(1, Convert.ToInt32(result.Count()));
            malaysiaDbContext.Database.EnsureDeleted();

        }
        #endregion

        #region Delivery service Unit test cases for GetDeliveryMethodMasterById
        [Fact]
        public void Test_GetDeliveryMethodMasterById_ReturnsErrorFalseResponse()
        {
            malaysiaDbContext.CaDeliveryMethodMasters.Add(new CaDeliveryMethodMaster { Id = 1, Description = "test", CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "01", IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new DeliveryMethodMasterResponse() { Id = 1, Description = "test" };
            mapper.Setup(x => x.Map<CaDeliveryMethodMaster, DeliveryMethodMasterResponse>(It.IsAny<CaDeliveryMethodMaster>())).Returns(mapperResp);
            var deliveryMethodMasterService = new DeliveryMethodMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = deliveryMethodMasterService.GetDeliveryMethodMasterById(mapperResp.Id).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Delivery service Unit test cases for AddDeliveryMethodMaster
        [Fact]
        public void Test_AddDeliveryMethodMaster_ReturnsErrorFalseResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            DeliveryMethodMasterRequest deliveryMethodMasterRequest = new DeliveryMethodMasterRequest() { Id = 1, Description = "test", CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "01", IsDeleted = false, FacilityCode = "KLAN" };
            CaDeliveryMethodMaster mapperResp = new CaDeliveryMethodMaster() { Id = 1, Description = "Test" };
            mapper.Setup(x => x.Map<DeliveryMethodMasterRequest, CaDeliveryMethodMaster>(deliveryMethodMasterRequest)).Returns(mapperResp);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaDeliveryMethodMasters.Add(new CaDeliveryMethodMaster() { Id = 2, Description = "Test" });
            malaysiaDbContext.SaveChangesAsync();
            var deliveryMethodMasterService = new DeliveryMethodMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = deliveryMethodMasterService.AddDeliveryMethodMaster(deliveryMethodMasterRequest).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_AddDeliveryMethodMaster_Returns_ReturnsErrorTrueResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            DeliveryMethodMasterRequest requestData = null;
            CaDeliveryMethodMaster respData = new CaDeliveryMethodMaster { Id = 1, Code = "INA1", Description = "DeliveryMethodMaster" };
            mapper.Setup(x => x.Map<DeliveryMethodMasterRequest, CaDeliveryMethodMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaDeliveryMethodMasters.Add(new CaDeliveryMethodMaster() { Id = 2, Code = "INA2", Description = "INPATIENT ADMISSION 2" });
            malaysiaDbContext.SaveChangesAsync();

            var deliveryMethodMasterService = new DeliveryMethodMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);

            var result = deliveryMethodMasterService.AddDeliveryMethodMaster(requestData).Result;

            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Delivery service Unit test cases for DeleteDeliveryMethodMaster
        [Fact]
        public void Test_DeleteDeliveryMethodMaster_ReturnsErrorFalseResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaDeliveryMethodMasters.Add(new CaDeliveryMethodMaster { Id = 1, Description = "Test", Code = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            DeliveryMethodMasterRequest deliveryMethodMasterRequest = new DeliveryMethodMasterRequest() { Id = 1, Description = "Test", Code = "Test", FacilityCode = "Test" };
            CaDeliveryMethodMaster mapperResp = new CaDeliveryMethodMaster() { Id = 1, Description = "Test", IsDeleted = true };
            mapper.Setup(x => x.Map<DeliveryMethodMasterRequest, CaDeliveryMethodMaster>(It.IsAny<DeliveryMethodMasterRequest>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var deliveryMethodMasterService = new DeliveryMethodMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = deliveryMethodMasterService.DeleteDeliveryMethodMaster(deliveryMethodMasterRequest.Id).Result;

            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_DeleteDeliveryMethodMaster_ReturnsErrorTrueResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaDeliveryMethodMasters.Add(new CaDeliveryMethodMaster { Id = 1, Description = "Test", Code = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            DeliveryMethodMasterRequest deliveryMethodMasterRequest = new DeliveryMethodMasterRequest() { Id = 1, Description = "Test", Code = "Test", FacilityCode = "Test" };
            CaDeliveryMethodMaster mapperResp = new CaDeliveryMethodMaster() { Id = 1, Description = "Test" };
            mapper.Setup(x => x.Map<DeliveryMethodMasterRequest, CaDeliveryMethodMaster>(It.IsAny<DeliveryMethodMasterRequest>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var deliveryMethodMasterService = new DeliveryMethodMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = deliveryMethodMasterService.DeleteDeliveryMethodMaster(deliveryMethodMasterRequest.Id).Result;

            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Delivery service Unit test cases for UpdateDeliveryMethodMaster
        [Fact]
        public void Test_UpdateDeliveryMethodMaster_ReturnsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaDeliveryMethodMasters.Add(new CaDeliveryMethodMaster { Id = 1, Description = "Test", Code = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            DeliveryMethodMasterRequest deliveryMethodMasterRequest = new DeliveryMethodMasterRequest() { Id = 1, Description = "Test1", Code = "Test", FacilityCode = "Test" };
            CaDeliveryMethodMaster mapperResp = new CaDeliveryMethodMaster() { Id = 1, Description = "Test1", Code = "Test", FacilityCode = "Test" };
            mapper.Setup(x => x.Map<DeliveryMethodMasterRequest, CaDeliveryMethodMaster>(It.IsAny<DeliveryMethodMasterRequest>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)
            var deliveryMethodMasterService = new DeliveryMethodMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = deliveryMethodMasterService.UpdateDeliveryMethodMaster(deliveryMethodMasterRequest).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_UpdateDeliveryMethodMaster_Returns_IsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaDeliveryMethodMasters.Add(new CaDeliveryMethodMaster { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1", FacilityCode = "KLAN", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            DeliveryMethodMasterRequest requestData = null;
            CaDeliveryMethodMaster respData = new CaDeliveryMethodMaster { Id = 1, Code = "INA1", Description = "DeliveryMethodMaster" };
            mapper.Setup(x => x.Map<DeliveryMethodMasterRequest, CaDeliveryMethodMaster>(It.IsAny<DeliveryMethodMasterRequest>())).Returns(respData);
            var deliveryMethodMasterService = new DeliveryMethodMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = deliveryMethodMasterService.UpdateDeliveryMethodMaster(requestData).Result;
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
     
        #endregion
    }
}
