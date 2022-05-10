using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CA_MCare21_CrossCuttingConcern.Caching;
using CA_MCare21_MasterAPI.Controllers;
using CA_MCare21_MasterAPI.Models;
using CA_MCare21_MasterAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;


namespace CA_MCare21_MasterAPIUnitTest.ControllerUnitTest
{
    public class RelationshipMasterControllerUnitTest
    {
        Mock<ILogger<RelationshipMasterController>> _logger;
        IConfiguration _config;
        Mock<IRelationshipMasterRepository> _relationshipMasterRepository;
        Mock<ICacheService> _cacheService;
        ErrorStatus error = new ErrorStatus();
        ErrorStatusCode errorcode = new ErrorStatusCode();


        public RelationshipMasterControllerUnitTest()
        {
            _logger = new Mock<ILogger<RelationshipMasterController>>();
            _relationshipMasterRepository = new Mock<IRelationshipMasterRepository>();
            _cacheService = new Mock<ICacheService>();
        }

        #region Add Relationship Master Unit Test Cases
        [Fact]
        public async void Task_AddRelationshipMaster_ValidData_Returns_OkResult()
        {
            RelationshipMasterResponse relationshipMasterResponse = new RelationshipMasterResponse { Id = 3, IsError = false };
            RelationshipMasterRequest relationshipMasterRequest = new RelationshipMasterRequest { Id = 3, Description = "FATHER", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "FA", Icstatus = false, Smrpcode = "04", Smrpdescription = "Father", FacilityCode = "KLAN" };
            _relationshipMasterRepository.Setup(x => x.AddRelationshipMaster(relationshipMasterRequest)).ReturnsAsync(relationshipMasterResponse);

            //Arrange
            RelationshipMasterController RelationshipMasterController = new RelationshipMasterController(_logger.Object, _config, _relationshipMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await RelationshipMasterController.AddRelationshipMaster(relationshipMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_AddRelationshipMaster_InvalidData_Returns_BadRequestResult()
        {
            RelationshipMasterResponse relationshipMasterResponse = new RelationshipMasterResponse { Id = 0, IsError = true };
            RelationshipMasterRequest relationshipMasterRequest = new RelationshipMasterRequest { Id = 3, Description = "FATHER", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "FA", Icstatus = false, Smrpcode = "04", Smrpdescription = "Father", FacilityCode = "KLAN" };
            _relationshipMasterRepository.Setup(x => x.AddRelationshipMaster(relationshipMasterRequest)).ReturnsAsync(relationshipMasterResponse);

            //Arrange
            RelationshipMasterController RelationshipMasterController = new RelationshipMasterController(_logger.Object, _config, _relationshipMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await RelationshipMasterController.AddRelationshipMaster(relationshipMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }        
        #endregion

        #region Update Relationship Master Unit Test Cases
        [Fact]
        public async void Task_UpdateRelationshipMaster_ValidData_Returns_OkResult()
        {
            RelationshipMasterResponse relationshipMasterResponse = new RelationshipMasterResponse { Id = 3, IsError = false };
            RelationshipMasterRequest relationshipMasterRequest = new RelationshipMasterRequest { Id = 3, Description = "FATHER", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "FA", Icstatus = false, Smrpcode = "04", Smrpdescription = "Father", FacilityCode = "KLAN" };
            _relationshipMasterRepository.Setup(x => x.UpdateRelationshipMaster(relationshipMasterRequest)).ReturnsAsync(relationshipMasterResponse);

            //Arrange
            RelationshipMasterController RelationshipMasterController = new RelationshipMasterController(_logger.Object, _config, _relationshipMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await RelationshipMasterController.UpdateRelationshipMaster(relationshipMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_UpdateRelationshipMaster_InvalidData_Returns_BadRequestResult()
        {
            RelationshipMasterResponse relationshipMasterResponse = new RelationshipMasterResponse { Id = 0, IsError = true };
            RelationshipMasterRequest relationshipMasterRequest = new RelationshipMasterRequest { Id = 3, Description = "FATHER", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "FA", Icstatus = false, Smrpcode = "04", Smrpdescription = "Father", FacilityCode = "KLAN" };
            _relationshipMasterRepository.Setup(x => x.UpdateRelationshipMaster(relationshipMasterRequest)).ReturnsAsync(relationshipMasterResponse);

            //Arrange
            RelationshipMasterController RelationshipMasterController = new RelationshipMasterController(_logger.Object, _config, _relationshipMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await RelationshipMasterController.UpdateRelationshipMaster(relationshipMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region Delete Relationship Master Unit Test Cases
        [Fact]
        public async void Task_DeleteRelationshipMaster_ValidData_Returns_OkResult()
        {
            int Id = 1;
            RelationshipMasterResponse relationship = new RelationshipMasterResponse { Id = 1, IsError = false };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETED, StatusCode = errorcode.STATUS_DATA_DELETED, Result = relationship };
            _relationshipMasterRepository.Setup(x => x.DeleteRelationshipMaster(Id)).ReturnsAsync(relationship);

            //Arrange
            RelationshipMasterController RelationshipMasterController = new RelationshipMasterController(_logger.Object, _config, _relationshipMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await RelationshipMasterController.DeleteRelationshipMaster(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_DeleteRelationshipMaster_InvalidData_Returns_BadRequest()
        {
            int? Id = null;
            RelationshipMasterResponse relationship = new RelationshipMasterResponse { Id = 0, IsError = true };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETE_FAIL, StatusCode = errorcode.STATUS_DATA_DELETE_FAIL, Result = relationship };
            _relationshipMasterRepository.Setup(x => x.DeleteRelationshipMaster(Convert.ToInt32(Id))).ReturnsAsync(relationship);

            //Arrange
            RelationshipMasterController RelationshipMasterController = new RelationshipMasterController(_logger.Object, _config, _relationshipMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await RelationshipMasterController.DeleteRelationshipMaster(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Relationship Master By Id Unit Test Cases
        [Fact]
        public async void Task_GetRelationshipMasterById_Returns_OkResult()
        {
            int Id = 3;
            RelationshipMasterResponse data = new RelationshipMasterResponse { Id = 1, Description = "FATHER", Code = "FA", SmrpCode = "04", SmrpDescription = "Father" };
            _relationshipMasterRepository.Setup(x => x.GetRelationshipMasterById(Id)).ReturnsAsync(data);
            RelationshipMasterController RelationshipMasterController = new RelationshipMasterController(_logger.Object, _config, _relationshipMasterRepository.Object, _cacheService.Object);
            var result = await RelationshipMasterController.GetRelationshipMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_GetRelationshipMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            RelationshipMasterResponse relationship = new RelationshipMasterResponse { Id = 1, Description = "FATHER", Code = "FA", SmrpCode = "04", SmrpDescription = "Father" };
            DataResponse response = new DataResponse { Message = error.STATUS_INVALID_REQUEST, StatusCode = errorcode.STATUS_INVALID_REQUEST, Result = relationship };
            _relationshipMasterRepository.Setup(x => x.GetRelationshipMasterById(Id)).ReturnsAsync(relationship);

            //Arrange
            RelationshipMasterController RelationshipMasterController = new RelationshipMasterController(_logger.Object, _config, _relationshipMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await RelationshipMasterController.GetRelationshipMasterById(Id);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetRelationshipMasterById_Returns_NotFoundResult()
        {
            int Id = 1;
            RelationshipMasterResponse relationship = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = relationship };
            _relationshipMasterRepository.Setup(x => x.GetRelationshipMasterById(Id)).ReturnsAsync(relationship);
            //Arrange
            RelationshipMasterController RelationshipMasterController = new RelationshipMasterController(_logger.Object, _config, _relationshipMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await RelationshipMasterController.GetRelationshipMasterById(Id);
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Relationship Master Unit Test Cases
        [Fact]
        public async void Task_GetRelationshipMaster_Returns_OkResult()
        {
            List<RelationshipMasterResponse> data = new List<RelationshipMasterResponse>() { new RelationshipMasterResponse { Id = 1, Description = "FATHER", Code = "FA", SmrpCode = "04", SmrpDescription = "Father" } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _relationshipMasterRepository.Setup(x => x.GetRelationshipMaster()).ReturnsAsync(data);

            //Arrange
            RelationshipMasterController RelationshipMasterController = new RelationshipMasterController(_logger.Object, _config, _relationshipMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await RelationshipMasterController.GetRelationshipMaster();
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetRelationshipMaster_Returns_NotFoundResult()
        {
            List<RelationshipMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _relationshipMasterRepository.Setup(x => x.GetRelationshipMaster()).ReturnsAsync(data);
            //Arrange
            RelationshipMasterController RelationshipMasterController = new RelationshipMasterController(_logger.Object, _config, _relationshipMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await RelationshipMasterController.GetRelationshipMaster();
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

    }
}