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
    public class MaritalStatusControllerUnitTest
    {
        Mock<ILogger<MaritalStatusController>> _logger;
        IConfiguration _config;
        Mock<IMaritalStatusRepository> _maritalStatusRepository;
        Mock<ICacheService> _cacheService;
        ErrorStatus error = new ErrorStatus();
        ErrorStatusCode errorcode = new ErrorStatusCode();


        public MaritalStatusControllerUnitTest()
        {
            _logger = new Mock<ILogger<MaritalStatusController>>();
            _maritalStatusRepository = new Mock<IMaritalStatusRepository>();
            _cacheService = new Mock<ICacheService>();
        }

        #region  marital Status controller Unit test cases for GetMaritalStatusMasterById
        [Fact]
        public async void Test_GetMaritalStatusMasterById_OkResult()
        {
            int Id = 1;
            MaritalStatusMasterResponse data = new MaritalStatusMasterResponse { Id = 1, Description = "Test" };
            _maritalStatusRepository.Setup(x => x.GetMaritalStatusMasterById(Id)).ReturnsAsync(data);
            MaritalStatusController MaritalStatusController = new MaritalStatusController(_logger.Object, _config, _maritalStatusRepository.Object, _cacheService.Object);
            var result = await MaritalStatusController.GetMaritalStatusMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_GetMaritalStatusMasterById_NotFoundResult()
        {
            int Id = 1;
            MaritalStatusMasterResponse data = null; //new RoomMasterRequest { Id = 1, Description = "Test", RoomCode = "10", WardId = 1, GenderId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _maritalStatusRepository.Setup(x => x.GetMaritalStatusMasterById(Id)).ReturnsAsync(data);
            MaritalStatusController MaritalStatusController = new MaritalStatusController(_logger.Object, _config, _maritalStatusRepository.Object, _cacheService.Object);
            var result = await MaritalStatusController.GetMaritalStatusMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async void Task_GetMaritalStatusMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            MaritalStatusMasterResponse maritalStatusMasterResponse = new MaritalStatusMasterResponse { Id = 1, Description = "CauseOfDeathMaster" };
            DataResponse response = new DataResponse { Message = error.STATUS_INVALID_REQUEST, StatusCode = errorcode.STATUS_INVALID_REQUEST, Result = maritalStatusMasterResponse };
            _maritalStatusRepository.Setup(x => x.GetMaritalStatusMasterById(Id)).ReturnsAsync(maritalStatusMasterResponse);

            //Arrange
            MaritalStatusController maritalStatusController = new MaritalStatusController(_logger.Object, _config, _maritalStatusRepository.Object, _cacheService.Object);
            //Act
            var result = await maritalStatusController.GetMaritalStatusMasterById(Id);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region  marital Status controller Unit test cases for DeleteMaritalStatusMaster

        [Fact]
        public async void Test_DeleteMaritalStatusMaster_OkResult()
        {
            int Id = 1;
            MaritalStatusMasterResponse data = new MaritalStatusMasterResponse { Id = 1, IsError = false };
            _maritalStatusRepository.Setup(x => x.DeleteMaritalStatusMaster(Id)).ReturnsAsync(data);
            MaritalStatusController MaritalStatusController = new MaritalStatusController(_logger.Object, _config, _maritalStatusRepository.Object, _cacheService.Object);
            var result = await MaritalStatusController.DeleteMaritalStatusMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_DeleteMaritalStatusMaster_BadRequestResult()
        {
            int Id = 1;
            MaritalStatusMasterResponse data = new MaritalStatusMasterResponse { Id = 1, IsError = true };
            _maritalStatusRepository.Setup(x => x.DeleteMaritalStatusMaster(Id)).ReturnsAsync(data);
            MaritalStatusController maritalStatusController = new MaritalStatusController(_logger.Object, _config, _maritalStatusRepository.Object, _cacheService.Object);
            var result = await maritalStatusController.DeleteMaritalStatusMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region  marital Status controller Unit test cases for AddMaritalStatusMaster

        [Fact]
        public async void Test_AddMaritalStatusMaster_BadRequestResult()
        {

            MaritalStatusMasterResponse data = new MaritalStatusMasterResponse { Id = 1, IsError = true };
            MaritalStatusMasterRequest input = new MaritalStatusMasterRequest() { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _maritalStatusRepository.Setup(x => x.AddMaritalStatusMaster(input)).ReturnsAsync(data);
            MaritalStatusController MaritalStatusController = new MaritalStatusController(_logger.Object, _config, _maritalStatusRepository.Object, _cacheService.Object);
            var result = await MaritalStatusController.AddMaritalStatusMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void Test_AddMaritalStatusMaster_OkResult()
        {

            MaritalStatusMasterResponse data = new MaritalStatusMasterResponse { Id = 1, IsError = false };
            MaritalStatusMasterRequest input = new MaritalStatusMasterRequest() { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _maritalStatusRepository.Setup(x => x.AddMaritalStatusMaster(input)).ReturnsAsync(data);
            MaritalStatusController MaritalStatusController = new MaritalStatusController(_logger.Object, _config, _maritalStatusRepository.Object, _cacheService.Object);
            var result = await MaritalStatusController.AddMaritalStatusMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region  marital Status controller Unit test cases for UpdateMaritalStatusMaster

        [Fact]
        public async void Test_UpdateMaritalStatusMaster_OkResult()
        {

            MaritalStatusMasterResponse data = new MaritalStatusMasterResponse { Id = 1, IsError = false };
            MaritalStatusMasterRequest input = new MaritalStatusMasterRequest() { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _maritalStatusRepository.Setup(x => x.UpdateMaritalStatusMaster(input)).ReturnsAsync(data);
            MaritalStatusController MaritalStatusController = new MaritalStatusController(_logger.Object, _config, _maritalStatusRepository.Object, _cacheService.Object);
            var result = await MaritalStatusController.UpdateMaritalStatusMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_UpdateMaritalStatusMaster_BadRequestResult()
        {

            MaritalStatusMasterResponse data = new MaritalStatusMasterResponse { Id = 1, IsError = true };
            MaritalStatusMasterRequest input = new MaritalStatusMasterRequest() { Id = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _maritalStatusRepository.Setup(x => x.UpdateMaritalStatusMaster(input)).ReturnsAsync(data);
            MaritalStatusController maritalStatusController = new MaritalStatusController(_logger.Object, _config, _maritalStatusRepository.Object, _cacheService.Object);
            var result = await maritalStatusController.UpdateMaritalStatusMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region  marital Status controller Unit test cases for GetMaritalStatusMaster
        [Fact]
        public async void Test_GetMaritalStatusMaster_OkResult()
        {
            List<MaritalStatusMasterResponse> data = new List<MaritalStatusMasterResponse>() { new MaritalStatusMasterResponse { Id = 1, Description = "Open" } };
            _maritalStatusRepository.Setup(x => x.GetMaritalStatusMaster()).ReturnsAsync(data);
            MaritalStatusController maritalStatusController = new MaritalStatusController(_logger.Object, _config, _maritalStatusRepository.Object, _cacheService.Object);
            var result = await maritalStatusController.GetMaritalStatusMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_GetMaritalStatusMaster_NotFoundResult()
        {
            List<MaritalStatusMasterResponse> data = null;
            _maritalStatusRepository.Setup(x => x.GetMaritalStatusMaster()).ReturnsAsync(data);
            MaritalStatusController maritalStatusController = new MaritalStatusController(_logger.Object, _config, _maritalStatusRepository.Object, _cacheService.Object);
            var result = await maritalStatusController.GetMaritalStatusMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        #endregion
    }
}
