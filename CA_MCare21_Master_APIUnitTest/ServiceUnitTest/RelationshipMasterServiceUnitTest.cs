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
    public class RelationshipMasterServiceUnitTest
    {
        Mock<IHttpContextAccessor> httpContextProcessor;
        Mock<ICacheService> cacheService;
        Mock<IMapper> mapper;
        MalaysiaDbContext malaysiaDbContext;
        public RelationshipMasterServiceUnitTest()
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

        #region Relationship Master CRUD Operation Unit Test Cases
        #region Add Relationship Master Unit Test Cases
        [Fact]
        public void Test_AddRelationshipMaster_ReturnsErrorFalseResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            RelationshipMasterRequest relationshipMasterRequest = new RelationshipMasterRequest() { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" };
            CaRelationshipMaster mapperResp = new CaRelationshipMaster() { Id = 1, Description = "test" };
            mapper.Setup(x => x.Map<RelationshipMasterRequest, CaRelationshipMaster>(relationshipMasterRequest)).Returns(mapperResp);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaRelationshipMasters.Add(new CaRelationshipMaster() { Id = 2, Description = "test" });
            malaysiaDbContext.SaveChangesAsync();
            var relationshipMasterService = new RelationshipMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = relationshipMasterService.AddRelationshipMaster(relationshipMasterRequest).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_AddRelationshipMaster_ReturnsErrorTrueResponse()
        {
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            //RelationshipMasterRequest requestData = new RelationshipMasterRequest { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" };
            RelationshipMasterRequest requestData = null;
            CaRelationshipMaster respData = new CaRelationshipMaster { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" };
            mapper.Setup(x => x.Map<RelationshipMasterRequest, CaRelationshipMaster>(requestData)).Returns(respData);
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaRelationshipMasters.Add(new CaRelationshipMaster() { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChangesAsync();

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var relationshipMasterService = new RelationshipMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = relationshipMasterService.AddRelationshipMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Update Relationship Master Unit Test Cases
        [Fact]
        public void Test_UpdateRelationshipMaster_ReturnsErrorFalseResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaRelationshipMasters.Add(new CaRelationshipMaster { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            RelationshipMasterRequest relationshipMasterInput = new RelationshipMasterRequest() { Id = 1, Description = "Test1", FacilityCode = "Test" };
            CaRelationshipMaster mapperResp = new CaRelationshipMaster() { Id = 1, Description = "Test1", FacilityCode = "Test" };
            mapper.Setup(x => x.Map<RelationshipMasterRequest, CaRelationshipMaster>(It.IsAny<RelationshipMasterRequest>())).Returns(mapperResp);

            //Execute method of SUT (CoreMasterRepository)
            var relationshipMasterService = new RelationshipMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = relationshipMasterService.UpdateRelationshipMaster(relationshipMasterInput).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_UpdateRelationshipMaster_ReturnsErrorTrueResponse()
        {
            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaRelationshipMasters.Add(new CaRelationshipMaster { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            malaysiaDbContext.ChangeTracker.Clear();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            // RelationshipMasterRequest requestData = new RelationshipMasterRequest { Id = 1, Description = "Test1", FacilityCode = "Test" };
            RelationshipMasterRequest requestData = null;
            CaRelationshipMaster respData = new CaRelationshipMaster { Id = 1, Description = "Test1", FacilityCode = "Test" };
            mapper.Setup(x => x.Map<RelationshipMasterRequest, CaRelationshipMaster>(It.IsAny<RelationshipMasterRequest>())).Returns(respData);

            //Arrange
            //Execute method of SUT (CoreMasterRepository)  
            var relationshipMasterService = new RelationshipMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = relationshipMasterService.UpdateRelationshipMaster(requestData).Result;
            //Assert
            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Delete Relationship Master Unit Test Cases
        [Fact]
        public void Test_DeleteRelationshipMaster_ReturnsErrorFalseResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaRelationshipMasters.Add(new CaRelationshipMaster { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            RelationshipMasterRequest relationshipMasterInput = new RelationshipMasterRequest() { Id = 1, Description = "Test", FacilityCode = "Test" };
            CaRelationshipMaster mapperResp = new CaRelationshipMaster() { Id = 1, Description = "Test", IsDeleted = true };
            mapper.Setup(x => x.Map<RelationshipMasterRequest, CaRelationshipMaster>(It.IsAny<RelationshipMasterRequest>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var relationshipMasterService = new RelationshipMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = relationshipMasterService.DeleteRelationshipMaster(relationshipMasterInput.Id).Result;

            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        [Fact]
        public void Test_DeleteRelationshipMaster_ReturnsErrorTrueResponse()
        {

            malaysiaDbContext.Database.EnsureDeleted();
            malaysiaDbContext.CaRelationshipMasters.Add(new CaRelationshipMaster { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            RelationshipMasterRequest relationshipMasterInput = new RelationshipMasterRequest() { Id = 1, Description = "Test", FacilityCode = "Test" };
            CaRelationshipMaster mapperResp = new CaRelationshipMaster() { Id = 1, Description = "Test" };
            mapper.Setup(x => x.Map<RelationshipMasterRequest, CaRelationshipMaster>(It.IsAny<RelationshipMasterRequest>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var relationshipMasterService = new RelationshipMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var result = relationshipMasterService.DeleteRelationshipMaster(relationshipMasterInput.Id).Result;

            Assert.NotNull(result);
            Assert.True(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get Relationship Master By Id Unit Test Case
        [Fact]
        public void Test_GetRelationshipMasterById_ReturnsErrorFalseResponse()
        {
            malaysiaDbContext.CaRelationshipMasters.Add(new CaRelationshipMaster { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new RelationshipMasterResponse() { Id = 1, Description = "test" };
            mapper.Setup(x => x.Map<CaRelationshipMaster, RelationshipMasterResponse>(It.IsAny<CaRelationshipMaster>())).Returns(mapperResp);
            var relationshipMasterService = new RelationshipMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var result = relationshipMasterService.GetRelationshipMasterById(mapperResp.Id).Result;
            Assert.NotNull(result);
            Assert.False(result.IsError);
            malaysiaDbContext.Database.EnsureDeleted();
        }
        #endregion

        #region Get Relationship Master Unit Test Case
        [Fact]
        public void Test_GetRelationshipMaster_ReturnsDataCount()
        {

            var httpContextProcessor = new Mock<IHttpContextAccessor>();
            var cacheService = new Mock<ICacheService>();
            var mapper = new Mock<IMapper>();
            malaysiaDbContext.CaRelationshipMasters.Add(new CaRelationshipMaster { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, Code = "Testing", Icstatus = true, Smrpcode = "TST", Smrpdescription = "Testings", FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<RelationshipMasterResponse>() { new RelationshipMasterResponse { Id = 1, Description = "test" } };
            mapper.Setup(x => x.Map<IEnumerable<RelationshipMasterResponse>>(It.IsAny<List<CaRelationshipMaster>>())).Returns(mapperResp);
            var relationshipMasterService = new RelationshipMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            var relationshipMasters = relationshipMasterService.GetRelationshipMaster().Result.ToList();
            Assert.NotNull(relationshipMasters);
            Assert.Equal(1, Convert.ToInt32(relationshipMasters.Count()));
        }
        #endregion
        #endregion

    }
}
