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
    public class TermsAndConditionMasterServiceUnitTest
    {
        Mock<IHttpContextAccessor> httpContextProcessor;
        Mock<ICacheService> cacheService;
        Mock<IMapper> mapper;
        MalaysiaDbContext malaysiaDbContext;
        public TermsAndConditionMasterServiceUnitTest()
        {
            httpContextProcessor = new Mock<IHttpContextAccessor>();
            cacheService = new Mock<ICacheService>();
            mapper = new Mock<IMapper>();
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "termsandconditionMasterMalaysiaDatabase")
                            .Options;
            malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
        }

        #region Terms and Condition Master CRUD Operation Unit Test Cases
        #region Add Terms and Condition Master Unit Test Cases
        [Fact]
        public void Test_AddTerms_Returns_IsErrorFalseResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            TermsAndConditionMasterRequest requestData = new TermsAndConditionMasterRequest { Id = 1, TermsAndConditions = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            CaTermsAndConditionsMaster respData = new CaTermsAndConditionsMaster { Id = 1, TermsAndConditions = "Test" };
            mapper.Setup(x => x.Map<TermsAndConditionMasterRequest, CaTermsAndConditionsMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaTermsAndConditionsMasters.Add(new CaTermsAndConditionsMaster { Id = 2, TermsAndConditions = "Test 2" });
            malaysiaDbContext.SaveChangesAsync();

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var termsAndConditionMasterService = new TermsAndConditionMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = termsAndConditionMasterService.AddTerms(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_AddTerms_Returns_IsErrorTrueResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            TermsAndConditionMasterRequest requestData = null;
            CaTermsAndConditionsMaster respData = new CaTermsAndConditionsMaster { Id = 1, TermsAndConditions = "Test" };
            mapper.Setup(x => x.Map<TermsAndConditionMasterRequest, CaTermsAndConditionsMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaTermsAndConditionsMasters.Add(new CaTermsAndConditionsMaster { Id = 2, TermsAndConditions = "Test 2" });
            malaysiaDbContext.SaveChangesAsync();

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var termsAndConditionMasterService = new TermsAndConditionMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = termsAndConditionMasterService.AddTerms(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Update Terms and Condition Master Unit Test Cases
        [Fact]
        public void Test_UpdateTerms_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaTermsAndConditionsMasters.Add(new CaTermsAndConditionsMaster { Id = 1, TermsAndConditions = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            TermsAndConditionMasterRequest requestData = new TermsAndConditionMasterRequest { Id = 1, TermsAndConditions = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            CaTermsAndConditionsMaster respData = new CaTermsAndConditionsMaster { Id = 1, TermsAndConditions="Test" };
            mapper.Setup(x => x.Map<TermsAndConditionMasterRequest, CaTermsAndConditionsMaster>(It.IsAny<TermsAndConditionMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var termsAndConditionMasterService = new TermsAndConditionMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = termsAndConditionMasterService.UpdateTerms(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_UpdateTerms_Returns_IsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaTermsAndConditionsMasters.Add(new CaTermsAndConditionsMaster { Id = 1, TermsAndConditions = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            TermsAndConditionMasterRequest requestData = null;
            CaTermsAndConditionsMaster respData = new CaTermsAndConditionsMaster { Id = 1, TermsAndConditions = "Test" };
            mapper.Setup(x => x.Map<TermsAndConditionMasterRequest, CaTermsAndConditionsMaster>(It.IsAny<TermsAndConditionMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var termsAndConditionMasterService = new TermsAndConditionMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = termsAndConditionMasterService.UpdateTerms(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Delete Terms and Condition Master Unit Test Cases
        [Fact]
        public void Test_DeleteTerms_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaTermsAndConditionsMasters.Add(new CaTermsAndConditionsMaster { Id = 1, TermsAndConditions = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            TermsAndConditionMasterRequest requestData = new TermsAndConditionMasterRequest { Id = 1, TermsAndConditions = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            CaTermsAndConditionsMaster respData = new CaTermsAndConditionsMaster { Id = 1, TermsAndConditions = "Test" ,IsDeleted=true};
            mapper.Setup(x => x.Map<TermsAndConditionMasterRequest, CaTermsAndConditionsMaster>(It.IsAny<TermsAndConditionMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var termsAndConditionMasterService = new TermsAndConditionMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = termsAndConditionMasterService.DeleteTerms(requestData.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_DeleteTerms_Returns_IsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaTermsAndConditionsMasters.Add(new CaTermsAndConditionsMaster { Id = 1, TermsAndConditions = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            TermsAndConditionMasterRequest requestData = new TermsAndConditionMasterRequest { Id = 1, TermsAndConditions = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            CaTermsAndConditionsMaster respData = new CaTermsAndConditionsMaster { Id = 1, TermsAndConditions = "Test"};
            mapper.Setup(x => x.Map<TermsAndConditionMasterRequest, CaTermsAndConditionsMaster>(It.IsAny<TermsAndConditionMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var termsAndConditionMasterService = new TermsAndConditionMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = termsAndConditionMasterService.DeleteTerms(requestData.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get Terms and Condition Master By Id Unit Test Case
        [Fact]
        public void Test_GetTermsById_Returns_IsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaTermsAndConditionsMasters.Add(new CaTermsAndConditionsMaster { Id = 1, TermsAndConditions = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new TermsAndConditionMasterResponse { Id = 1, TermsAndConditions = "Test" };
            mapper.Setup(x => x.Map<CaTermsAndConditionsMaster, TermsAndConditionMasterResponse>(It.IsAny<CaTermsAndConditionsMaster>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)
            //Arrange
            var termsAndConditionMasterService = new TermsAndConditionMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = termsAndConditionMasterService.GetTermsById(mapperResp.Id).Result;
            //Assert
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get Terms and Codition Master Unit Test Case
        [Fact]
        public void Test_GetTerms_Returns_DataCount()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaTermsAndConditionsMasters.Add(new CaTermsAndConditionsMaster { Id = 1, TermsAndConditions = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<TermsAndConditionMasterResponse>() { new TermsAndConditionMasterResponse { Id = 1, TermsAndConditions = "Test" } };
            mapper.Setup(x => x.Map<List<TermsAndConditionMasterResponse>>(It.IsAny<List<CaTermsAndConditionsMaster>>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)  
            //Arrange
            var termsAndConditionMasterService = new TermsAndConditionMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = termsAndConditionMasterService.GetTerms().Result.ToList();
            //Assert
            Assert.NotNull(result);
            Assert.Equal(1, Convert.ToInt32(result.Count()));
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #endregion
    }
}
