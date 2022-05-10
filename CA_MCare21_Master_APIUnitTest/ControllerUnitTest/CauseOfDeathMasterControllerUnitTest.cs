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
    public class CauseOfDeathMasterControllerUnitTest
    {

        Mock<ILogger<CauseOfDeathMasterController>> _logger;
        IConfiguration _config;
        Mock<ICauseOfDeathMasterRepository> _causeOfDeathMasterRepository;
        Mock<ICacheService> _cacheService;
        ErrorStatus error = new ErrorStatus();
        ErrorStatusCode errorcode = new ErrorStatusCode();


        public CauseOfDeathMasterControllerUnitTest()
        {
            _logger = new Mock<ILogger<CauseOfDeathMasterController>>();
            _causeOfDeathMasterRepository = new Mock<ICauseOfDeathMasterRepository>();
            _cacheService = new Mock<ICacheService>();
        }
        #region CauseOfDeath Master controller Unit test cases for GetCauseOfDeathMasterById
        [Fact]
        public async void Test_GetCauseOfDeathMasterById_OkResult()
        {
            int Id = 1;
            CauseOfDeathMasterResponse data = new CauseOfDeathMasterResponse { Id = 1, Description = "Test" };
            _causeOfDeathMasterRepository.Setup(x => x.GetCauseOfDeathMasterById(Id)).ReturnsAsync(data);
            CauseOfDeathMasterController CauseOfDeathMasterController = new CauseOfDeathMasterController(_logger.Object, _config, _causeOfDeathMasterRepository.Object, _cacheService.Object);
            var result = await CauseOfDeathMasterController.GetCauseOfDeathMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_GetCauseOfDeathMasterById_NotFoundResult()
        {
            int Id = 1;
            CauseOfDeathMasterResponse data = null; //new RoomMasterRequest { Id = 1, Description = "Test", RoomCode = "10", WardId = 1, GenderId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _causeOfDeathMasterRepository.Setup(x => x.GetCauseOfDeathMasterById(Id)).ReturnsAsync(data);
            CauseOfDeathMasterController CauseOfDeathMasterController = new CauseOfDeathMasterController(_logger.Object, _config, _causeOfDeathMasterRepository.Object, _cacheService.Object);
            var result = await CauseOfDeathMasterController.GetCauseOfDeathMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async void Task_GetCauseOfDeathMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            CauseOfDeathMasterResponse causeOfDeathMasterResponse = new CauseOfDeathMasterResponse { Id = 1, Description = "CauseOfDeathMaster" };
            DataResponse response = new DataResponse { Message = error.STATUS_INVALID_REQUEST, StatusCode = errorcode.STATUS_INVALID_REQUEST, Result = causeOfDeathMasterResponse };
            _causeOfDeathMasterRepository.Setup(x => x.GetCauseOfDeathMasterById(Id)).ReturnsAsync(causeOfDeathMasterResponse);

            //Arrange
            CauseOfDeathMasterController causeOfDeathMasterController = new CauseOfDeathMasterController(_logger.Object, _config, _causeOfDeathMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await causeOfDeathMasterController.GetCauseOfDeathMasterById(Id);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region CauseOfDeath Master controller Unit test cases for DeleteCauseOfDeathMaster
        [Fact]
        public async void Test_DeleteCauseOfDeathMaster_OkResult()
        {
            int Id = 1;
            CauseOfDeathMasterResponse data = new CauseOfDeathMasterResponse { Id = 1, IsError = false };
            _causeOfDeathMasterRepository.Setup(x => x.DeleteCauseOfDeathMaster(Id)).ReturnsAsync(data);
            CauseOfDeathMasterController CauseOfDeathMasterController = new CauseOfDeathMasterController(_logger.Object, _config, _causeOfDeathMasterRepository.Object, _cacheService.Object);
            var result = await CauseOfDeathMasterController.DeleteCauseOfDeathMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_DeleteCauseOfDeathMaster_BadRequest()
        {
            int Id = 1;
            CauseOfDeathMasterResponse data = new CauseOfDeathMasterResponse { Id = 1, IsError = true };
            _causeOfDeathMasterRepository.Setup(x => x.DeleteCauseOfDeathMaster(Id)).ReturnsAsync(data);
            CauseOfDeathMasterController CauseOfDeathMasterController = new CauseOfDeathMasterController(_logger.Object, _config, _causeOfDeathMasterRepository.Object, _cacheService.Object);
            var result = await CauseOfDeathMasterController.DeleteCauseOfDeathMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region CauseOfDeath Master controller Unit test cases for AddCauseOfDeathMaster
        [Fact]
        public async void Test_AddCauseOfDeathMaster_BadRequestResult()
        {

            CauseOfDeathMasterResponse data = new CauseOfDeathMasterResponse { Id = 1, IsError = true };
            CauseOfDeathMasterRequest input = new CauseOfDeathMasterRequest() { Id = 1, Description = "Test", Code = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _causeOfDeathMasterRepository.Setup(x => x.AddCauseOfDeathMaster(input)).ReturnsAsync(data);
            CauseOfDeathMasterController CauseOfDeathMasterController = new CauseOfDeathMasterController(_logger.Object, _config, _causeOfDeathMasterRepository.Object, _cacheService.Object);
            var result = await CauseOfDeathMasterController.AddCauseOfDeathMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void Test_AddCauseOfDeathMaster_OkResult()
        {

            CauseOfDeathMasterResponse data = new CauseOfDeathMasterResponse { Id = 1, IsError = false };
            CauseOfDeathMasterRequest input = new CauseOfDeathMasterRequest() { Id = 1, Description = "Test", Code = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _causeOfDeathMasterRepository.Setup(x => x.AddCauseOfDeathMaster(input)).ReturnsAsync(data);
            CauseOfDeathMasterController CauseOfDeathMasterController = new CauseOfDeathMasterController(_logger.Object, _config, _causeOfDeathMasterRepository.Object, _cacheService.Object);
            var result = await CauseOfDeathMasterController.AddCauseOfDeathMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region CauseOfDeath Master controller Unit test cases for UpdateCauseOfDeathMaster
        [Fact]
        public async void Test_UpdateCauseOfDeathMaster_OkResult()
        {

            CauseOfDeathMasterResponse data = new CauseOfDeathMasterResponse { Id = 1, IsError = false };
            CauseOfDeathMasterRequest input = new CauseOfDeathMasterRequest() { Id = 1, Description = "Test", Code = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _causeOfDeathMasterRepository.Setup(x => x.UpdateCauseOfDeathMaster(input)).ReturnsAsync(data);
            CauseOfDeathMasterController CauseOfDeathMasterController = new CauseOfDeathMasterController(_logger.Object, _config, _causeOfDeathMasterRepository.Object, _cacheService.Object);
            var result = await CauseOfDeathMasterController.UpdateCauseOfDeathMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_UpdateCauseOfDeathMaster__BadRequestResult()
        {

            CauseOfDeathMasterResponse data = new CauseOfDeathMasterResponse { Id = 1, IsError = true };
            CauseOfDeathMasterRequest input = new CauseOfDeathMasterRequest() { Id = 1, Description = "Test", Code = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _causeOfDeathMasterRepository.Setup(x => x.UpdateCauseOfDeathMaster(input)).ReturnsAsync(data);
            CauseOfDeathMasterController CauseOfDeathMasterController = new CauseOfDeathMasterController(_logger.Object, _config, _causeOfDeathMasterRepository.Object, _cacheService.Object);
            var result = await CauseOfDeathMasterController.UpdateCauseOfDeathMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region CauseOfDeath Master controller Unit test cases for GetCauseOfDeathMaster
        [Fact]
        public async void Test_GetCauseOfDeathMaster_OkResult()
        {
            List<CauseOfDeathMasterResponse> data = new List<CauseOfDeathMasterResponse>() { new CauseOfDeathMasterResponse { Id = 1, Description = "Open" } };
            _causeOfDeathMasterRepository.Setup(x => x.GetCauseOfDeathMaster()).ReturnsAsync(data);
            CauseOfDeathMasterController causeOfDeathMasterController = new CauseOfDeathMasterController(_logger.Object, _config, _causeOfDeathMasterRepository.Object, _cacheService.Object);
            var result = await causeOfDeathMasterController.GetCauseOfDeathMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_GetCauseOfDeathMaster_NotFoundResult()
        {
            List<CauseOfDeathMasterResponse> data = null;
            _causeOfDeathMasterRepository.Setup(x => x.GetCauseOfDeathMaster()).ReturnsAsync(data);
            CauseOfDeathMasterController causeOfDeathMasterController = new CauseOfDeathMasterController(_logger.Object, _config, _causeOfDeathMasterRepository.Object, _cacheService.Object);
            var result = await causeOfDeathMasterController.GetCauseOfDeathMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        #endregion

    }
}
