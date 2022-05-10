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
    public class WardMasterControllerUnitTest
    {
        Mock<ILogger<WardMasterController>> _logger;
        IConfiguration _config;
        Mock<IWardMasterRepository> _wardmasterRepository;
        Mock<ICacheService> _cacheService;
        ErrorStatus error = new ErrorStatus();
        ErrorStatusCode errorcode = new ErrorStatusCode();


        public WardMasterControllerUnitTest()
        {
            _logger = new Mock<ILogger<WardMasterController>>();
            _wardmasterRepository = new Mock<IWardMasterRepository>();
            _cacheService = new Mock<ICacheService>();
        }


        #region Add Ward Master Unit Test Cases
        [Fact]
        public async void Task_AddWardMaster_InvalidData_Returns_BadRequest()
        {
            WardMasterResponse wardMasterResponse = new WardMasterResponse { Id = 0, IsError = true };
            WardMasterRequest wardMasterRequest = new WardMasterRequest { Id = 1, WardCode = "", WardName = "", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, DepartmentId = 21, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADD_FAIL, StatusCode = errorcode.STATUS_DATA_ADD_FAIL, Result = wardMasterRequest };
            _wardmasterRepository.Setup(x => x.AddWardMaster(wardMasterRequest)).ReturnsAsync(wardMasterResponse);

            //Arrange
            WardMasterController WardMasterController = new WardMasterController(_logger.Object, _config, _wardmasterRepository.Object, _cacheService.Object);
            WardMasterController.ModelState.AddModelError("WardCode", "Required");
            WardMasterController.ModelState.AddModelError("WardName", "Required");
            //Act
            var result = await WardMasterController.AddWardMaster(wardMasterRequest);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            //Assert.Equal(400, actualResult.StatusCode);
            //Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(actualResult);
        }
        [Fact]
        public async void Task_AddWardMaster_ValidData_Returns_OkResult()
        {
            WardMasterResponse wardMasterResponse = new WardMasterResponse { Id = 1, IsError = false };
            WardMasterRequest wardMasterRequest = new WardMasterRequest { Id = 1, WardCode = "TestWard", WardName = "TestWard", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, DepartmentId = 21, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADDED, StatusCode = errorcode.STATUS_DATA_ADDED, Result = wardMasterRequest };
            _wardmasterRepository.Setup(x => x.AddWardMaster(wardMasterRequest)).ReturnsAsync(wardMasterResponse);

            //Arrange
            WardMasterController WardMasterController = new WardMasterController(_logger.Object, _config, _wardmasterRepository.Object, _cacheService.Object);
            //Act
            var result = await WardMasterController.AddWardMaster(wardMasterRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Update Ward Master Unit Test Cases
        [Fact]
        public async void Task_UpdateWardMaster_InvalidData_Returns_BadRequest()
        {
            WardMasterResponse wardMasterResponse = new WardMasterResponse { Id = 0, IsError = true };
            WardMasterRequest wardMasterRequest = new WardMasterRequest { Id = 1, WardCode = "", WardName = "", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, DepartmentId = 21, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATE_FAIL, StatusCode = errorcode.STATUS_DATA_UPDATE_FAIL, Result = wardMasterRequest };
            _wardmasterRepository.Setup(x => x.UpdateWardMaster(wardMasterRequest)).ReturnsAsync(wardMasterResponse);

            //Arrange
            WardMasterController WardMasterController = new WardMasterController(_logger.Object, _config, _wardmasterRepository.Object, _cacheService.Object);
            WardMasterController.ModelState.AddModelError("WardCode", "Required");
            WardMasterController.ModelState.AddModelError("WardName", "Required");
            //Act
            var result = await WardMasterController.UpdateWardMaster(wardMasterRequest);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_UpdateWardMaster_ValidData_Returns_OkResult()
        {
            WardMasterResponse wardMasterResponse = new WardMasterResponse { Id = 1, IsError = false };
            WardMasterRequest wardMasterRequest = new WardMasterRequest { Id = 1, WardCode = "TestWard", WardName = "TestWard", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, DepartmentId = 21, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATED, StatusCode = errorcode.STATUS_DATA_UPDATED, Result = wardMasterRequest };
            _wardmasterRepository.Setup(x => x.UpdateWardMaster(wardMasterRequest)).ReturnsAsync(wardMasterResponse);

            //Arrange
            WardMasterController WardMasterController = new WardMasterController(_logger.Object, _config, _wardmasterRepository.Object, _cacheService.Object);
            //Act
            var result = await WardMasterController.UpdateWardMaster(wardMasterRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Delete Ward Master Unit Test Cases
        [Fact]
        public async void Task_DeleteWardMaster_InvalidData_Returns_BadRequest()
        {
            int? Id = null;
            WardMasterResponse wardMasterResponse = new WardMasterResponse { Id = 0, IsError = true };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETE_FAIL, StatusCode = errorcode.STATUS_DATA_DELETE_FAIL, Result = wardMasterResponse };
            _wardmasterRepository.Setup(x => x.DeleteWardMaster(Id)).ReturnsAsync(wardMasterResponse);

            //Arrange
            WardMasterController WardMasterController = new WardMasterController(_logger.Object, _config, _wardmasterRepository.Object, _cacheService.Object);
            //Act
            var result = await WardMasterController.DeleteWardMaster(Id);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_DeleteWardMaster_ValidData_Returns_OkResult()
        {
            int Id = 1;
            WardMasterResponse wardMasterResponse = new WardMasterResponse { Id = 1, IsError = false };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETED, StatusCode = errorcode.STATUS_DATA_DELETED, Result = wardMasterResponse };
            _wardmasterRepository.Setup(x => x.DeleteWardMaster(Id)).ReturnsAsync(wardMasterResponse);

            //Arrange
            WardMasterController WardMasterController = new WardMasterController(_logger.Object, _config, _wardmasterRepository.Object, _cacheService.Object);
            //Act
            var result = await WardMasterController.DeleteWardMaster(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Ward Master By Id Unit Test Cases
        [Fact]
        public async void Task_GetWardMasterById_Returns_OkResult()
        {
            int Id = 1;
            WardMasterResponse wardMasterResponse = new WardMasterResponse { Id = 1, WardCode = "TestWard", WardName = "TestWard", DepartmentId = 21, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = wardMasterResponse };
            _wardmasterRepository.Setup(x => x.GetWardMasterById(Id)).ReturnsAsync(wardMasterResponse);

            //Arrange
            WardMasterController WardMasterController = new WardMasterController(_logger.Object, _config, _wardmasterRepository.Object, _cacheService.Object);
            //Act
            var result = await WardMasterController.GetWardMasterById(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetWardMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            WardMasterResponse wardMasterResponse = new WardMasterResponse { Id = 1, WardCode = "TestWard", WardName = "TestWard", DepartmentId = 21, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_INVALID_REQUEST, StatusCode = errorcode.STATUS_INVALID_REQUEST, Result = wardMasterResponse };
            _wardmasterRepository.Setup(x => x.GetWardMasterById(Id)).ReturnsAsync(wardMasterResponse);

            //Arrange
            WardMasterController WardMasterController = new WardMasterController(_logger.Object, _config, _wardmasterRepository.Object, _cacheService.Object);
            //Act
            var result = await WardMasterController.GetWardMasterById(Id);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetWardMasterById_Returns_NotFoundResult()
        {
            int Id = 1;
            WardMasterResponse wardMasterResponse = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = wardMasterResponse };
            _wardmasterRepository.Setup(x => x.GetWardMasterById(Id)).ReturnsAsync(wardMasterResponse);
            //Arrange
            WardMasterController WardMasterController = new WardMasterController(_logger.Object, _config, _wardmasterRepository.Object, _cacheService.Object);
            //Act
            var result = await WardMasterController.GetWardMasterById(Id);
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Ward Master Unit Test Cases
        [Fact]
        public async void Task_GetWardMaster_Returns_OkResult()
        {
            List<WardMasterResponse> data = new List<WardMasterResponse>() { new WardMasterResponse { Id = 1, WardCode = "TestWard", WardName = "TestWard", DepartmentId = 21, FacilityCode = "KLAN" } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _wardmasterRepository.Setup(x => x.GetWardMaster()).ReturnsAsync(data);

            //Arrange
            WardMasterController WardMasterController = new WardMasterController(_logger.Object, _config, _wardmasterRepository.Object, _cacheService.Object);
            //Act
            var result = await WardMasterController.GetWardMaster();
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetWardMaster_Returns_NotFoundResult()
        {
            List<WardMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _wardmasterRepository.Setup(x => x.GetWardMaster()).ReturnsAsync(data);
            //Arrange
            WardMasterController WardMasterController = new WardMasterController(_logger.Object, _config, _wardmasterRepository.Object, _cacheService.Object);
            //Act
            var result = await WardMasterController.GetWardMaster();
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

    }
}
