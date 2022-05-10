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
    public class StateMasterServiceUnitTest
    {
        Mock<IHttpContextAccessor> httpContextProcessor;
        Mock<ICacheService> cacheService;
        Mock<IMapper> mapper;
        MalaysiaDbContext malaysiaDbContext;
        public StateMasterServiceUnitTest()
        {
            httpContextProcessor = new Mock<IHttpContextAccessor>();
            cacheService = new Mock<ICacheService>();
            mapper = new Mock<IMapper>();
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "stateMasterMalaysiaDatabase")
                            .Options;
            malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
        }

        #region State Master CRUD Operation Unit Test Cases
        #region Add State Master Unit Test Cases
        [Fact]
        public void Test_AddState_Returns_IsErrorFalseResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            StateMasterRequest requestData = new StateMasterRequest { Id = 1, Code = "PER", Description = "PERLIS", CountryId = 130, Smrpcode = "9", Smrpdescription = "PERLIS", EreturnStateId = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            CaStateMaster respData = new CaStateMaster { Id = 1, Code = "PER", Description = "PERLIS", CountryId = 130 };
            mapper.Setup(x => x.Map<StateMasterRequest, CaStateMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaStateMasters.Add(new CaStateMaster { Id = 2, Code = "TST", Description = "TEST", CountryId = 130 });
            malaysiaDbContext.SaveChangesAsync();

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var stateMasterService = new StateMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = stateMasterService.AddState(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_AddState_Returns_IsErrorTrueResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            StateMasterRequest requestData = null;
            CaStateMaster respData = new CaStateMaster { Id = 1, Code = "PER", Description = "PERLIS", CountryId = 130 };
            mapper.Setup(x => x.Map<StateMasterRequest, CaStateMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaStateMasters.Add(new CaStateMaster { Id = 2, Code = "TST", Description = "TEST", CountryId = 130 });
            malaysiaDbContext.SaveChangesAsync();

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var stateMasterService = new StateMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = stateMasterService.AddState(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Update State Master Unit Test Cases
        [Fact]
        public void Test_UpdateState_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaStateMasters.Add(new CaStateMaster { Id = 1, Code = "PER", Description = "PERLIS", CountryId = 130, Smrpcode = "9", Smrpdescription = "PERLIS", EreturnStateId = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            StateMasterRequest requestData = new StateMasterRequest { Id = 1, Code = "PER", Description = "PERLIS", CountryId = 130, Smrpcode = "9", Smrpdescription = "PERLIS", EreturnStateId = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            CaStateMaster respData = new CaStateMaster { Id = 1, Code = "PER", Description = "PERLIS", CountryId = 130 };
            mapper.Setup(x => x.Map<StateMasterRequest, CaStateMaster>(It.IsAny<StateMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var stateMasterService = new StateMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = stateMasterService.UpdateState(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_UpdateState_Returns_IsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaStateMasters.Add(new CaStateMaster { Id = 1, Code = "PER", Description = "PERLIS", CountryId = 130, Smrpcode = "9", Smrpdescription = "PERLIS", EreturnStateId = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            StateMasterRequest requestData = null;
            CaStateMaster respData = new CaStateMaster { Id = 1, Code = "PER", Description = "PERLIS", CountryId = 130 };
            mapper.Setup(x => x.Map<StateMasterRequest, CaStateMaster>(It.IsAny<StateMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var stateMasterService = new StateMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = stateMasterService.UpdateState(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Delete State Master Unit Test Cases
        [Fact]
        public void Test_DeleteState_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaStateMasters.Add(new CaStateMaster { Id = 1, Code = "PER", Description = "PERLIS", CountryId = 130, Smrpcode = "9", Smrpdescription = "PERLIS", EreturnStateId = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            StateMasterRequest requestData = new StateMasterRequest { Id = 1, Code = "PER", Description = "PERLIS", CountryId = 130, Smrpcode = "9", Smrpdescription = "PERLIS", EreturnStateId = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            CaStateMaster respData = new CaStateMaster { Id = 1, Code = "PER", Description = "PERLIS", CountryId = 130, IsDeleted =true };
            mapper.Setup(x => x.Map<StateMasterRequest, CaStateMaster>(It.IsAny<StateMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var stateMasterService = new StateMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = stateMasterService.DeleteState(requestData.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_DeleteState_Returns_IsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaStateMasters.Add(new CaStateMaster { Id = 1, Code = "PER", Description = "PERLIS", CountryId = 130, Smrpcode = "9", Smrpdescription = "PERLIS", EreturnStateId = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            StateMasterRequest requestData = new StateMasterRequest { Id = 1, Code = "PER", Description = "PERLIS", CountryId = 130, Smrpcode = "9", Smrpdescription = "PERLIS", EreturnStateId = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            CaStateMaster respData = new CaStateMaster { Id = 1, Code = "PER", Description = "PERLIS", CountryId = 130 };
            mapper.Setup(x => x.Map<StateMasterRequest, CaStateMaster>(It.IsAny<StateMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var stateMasterService = new StateMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = stateMasterService.DeleteState(requestData.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get State Master By Id Unit Test Case
        [Fact]
        public void Test_GetStateById_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaStateMasters.Add(new CaStateMaster { Id = 1, Code = "PER", Description = "PERLIS", CountryId = 130, Smrpcode = "9", Smrpdescription = "PERLIS", EreturnStateId = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new StateMasterResponse { Id = 1, Code = "PER", Description = "PERLIS", CountryId = 130, Smrpcode = "9", Smrpdescription = "PERLIS", EreturnStateId = null, FacilityCode = "KLAN" };
            mapper.Setup(x => x.Map<CaStateMaster, StateMasterResponse>(It.IsAny<CaStateMaster>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)
            //Arrange
            var stateMasterService = new StateMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = stateMasterService.GetStateById(mapperResp.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get State Master Unit Test Case
        [Fact]
        public void Test_GetStateMaster_Returns_DataCount()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaStateMasters.Add(new CaStateMaster { Id = 1, Code = "PER", Description = "PERLIS", CountryId = 130, Smrpcode = "9", Smrpdescription = "PERLIS", EreturnStateId = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<StateMasterResponse>() { new StateMasterResponse { Id = 1, Code = "PER", Description = "PERLIS", CountryId = 130, Smrpcode = "9", Smrpdescription = "PERLIS", EreturnStateId = null, FacilityCode = "KLAN" } };
            mapper.Setup(x => x.Map<List<StateMasterResponse>>(It.IsAny<List<CaStateMaster>>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)  
            //Arrange
            var stateMasterService = new StateMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = stateMasterService.GetStateMaster().Result.ToList();
            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, Convert.ToInt32(result.Count()));
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #endregion
    }
}
