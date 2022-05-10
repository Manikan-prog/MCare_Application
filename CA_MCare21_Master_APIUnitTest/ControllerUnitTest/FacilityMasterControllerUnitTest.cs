using AutoMapper;
using CA_MCare21_CrossCuttingConcern.Caching;
using CA_MCare21_MasterAPI.Controllers;
using CA_MCare21_MasterAPI.Models;
using CA_MCare21_MasterAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;


namespace CA_MCare21_MasterAPIUnitTest.ControllerUnitTest
{
    public class FacilityMasterControllerUnitTest
    {
        Mock<ILogger<FacilityMasterController>> _logger;
        IConfiguration _config;
        Mock<IFacilityMasterRepository> _facilityMasterRepository;
        Mock<ICacheService> _cacheService;
        ErrorStatus error = new ErrorStatus();
        ErrorStatusCode errorcode = new ErrorStatusCode();


        public FacilityMasterControllerUnitTest()
        {
            _logger = new Mock<ILogger<FacilityMasterController>>();
            _facilityMasterRepository = new Mock<IFacilityMasterRepository>();
            _cacheService = new Mock<ICacheService>();
        }


        #region Add Facility Master Unit Test Cases
        [Fact]
        public async void Task_AddFacilityMaster_ValidData_Returns_OkResult()
        {
            FacilityMasterResponse facilityMasterResponse = new FacilityMasterResponse { Id = 1, IsError = false };
            FacilityMasterRequest facilityMasterRequest = new FacilityMasterRequest { Id = 1, FacilityCode = "KLAN", Description = "Test Facility", PrincipalCurrencyId = 1, ForeignCurrencyId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADDED, StatusCode = errorcode.STATUS_DATA_ADDED, Result = facilityMasterResponse };
            _facilityMasterRepository.Setup(x => x.AddFacilityMaster(facilityMasterRequest)).ReturnsAsync(facilityMasterResponse);

            //Arrange
            FacilityMasterController FacilityMasterController = new FacilityMasterController(_logger.Object, _config, _facilityMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await FacilityMasterController.AddFacilityMaster(facilityMasterRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_AddFacilityMaster_InvalidData_Returns_BadRequest()
        {
            FacilityMasterResponse facilityMasterResponse = new FacilityMasterResponse { Id = 1, IsError = true };
            FacilityMasterRequest facilityMasterRequest = new FacilityMasterRequest { Id = 1, FacilityCode = "", Description = "", PrincipalCurrencyId = 1, ForeignCurrencyId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADD_FAIL, StatusCode = errorcode.STATUS_DATA_ADD_FAIL, Result = facilityMasterResponse };
            _facilityMasterRepository.Setup(x => x.AddFacilityMaster(facilityMasterRequest)).ReturnsAsync(facilityMasterResponse);

            //Arrange
            FacilityMasterController FacilityMasterController = new FacilityMasterController(_logger.Object, _config, _facilityMasterRepository.Object, _cacheService.Object);
            FacilityMasterController.ModelState.AddModelError("Description", "Required");
            FacilityMasterController.ModelState.AddModelError("FacilityCode", "Required");
            //Act
            var result = await FacilityMasterController.AddFacilityMaster(facilityMasterRequest);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Update Facility Master Unit Test Cases
        [Fact]
        public async void Task_UpdateFacilityMaster_ValidData_Returns_OkResult()
        {
            FacilityMasterResponse facilityMasterResponse = new FacilityMasterResponse { Id = 1, IsError = false };
            FacilityMasterRequest facilityMasterRequest = new FacilityMasterRequest { Id = 1, FacilityCode = "KLAN", Description = "Test Facility", PrincipalCurrencyId = 1, ForeignCurrencyId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATED, StatusCode = errorcode.STATUS_DATA_UPDATED, Result = facilityMasterResponse };
            _facilityMasterRepository.Setup(x => x.UpdateFacilityMaster(facilityMasterRequest)).ReturnsAsync(facilityMasterResponse);

            //Arrange
            FacilityMasterController FacilityMasterController = new FacilityMasterController(_logger.Object, _config, _facilityMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await FacilityMasterController.UpdateFacilityMaster(facilityMasterRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_UpdateFacilityMaster_InvalidData_Returns_BadRequest()
        {
            FacilityMasterResponse facilityMasterResponse = new FacilityMasterResponse { Id = 1, IsError = true };
            FacilityMasterRequest facilityMasterRequest = new FacilityMasterRequest { Id = 1, FacilityCode = "", Description = "", PrincipalCurrencyId = 1, ForeignCurrencyId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATE_FAIL, StatusCode = errorcode.STATUS_DATA_UPDATE_FAIL, Result = facilityMasterResponse };
            _facilityMasterRepository.Setup(x => x.UpdateFacilityMaster(facilityMasterRequest)).ReturnsAsync(facilityMasterResponse);

            //Arrange
            FacilityMasterController FacilityMasterController = new FacilityMasterController(_logger.Object, _config, _facilityMasterRepository.Object, _cacheService.Object);
            FacilityMasterController.ModelState.AddModelError("Description", "Required");
            FacilityMasterController.ModelState.AddModelError("FacilityCode", "Required");
            //Act
            var result = await FacilityMasterController.UpdateFacilityMaster(facilityMasterRequest);
            var actualResult = result as BadRequestResult;
            //Asert
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }
        #endregion

        #region Delete Facility Master Unit Test Cases
        [Fact]
        public async void Task_DeleteFacilityMaster_ValidData_Returns_OkResult()
        {
            int Id = 1;
            FacilityMasterResponse facilityMasterResponse = new FacilityMasterResponse { Id = 1, IsError = false };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETED, StatusCode = errorcode.STATUS_DATA_DELETED, Result = facilityMasterResponse };
            _facilityMasterRepository.Setup(x => x.DeleteFacilityMaster(Id)).ReturnsAsync(facilityMasterResponse);

            //Arrange
            FacilityMasterController FacilityMasterController = new FacilityMasterController(_logger.Object, _config, _facilityMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await FacilityMasterController.DeleteFacilityMaster(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_DeleteFacilityMaster_InvalidData_Returns_BadRequest()
        {
            int? Id = null;
            FacilityMasterResponse facilityMasterResponse = new FacilityMasterResponse { Id = 0, IsError = true };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETE_FAIL, StatusCode = errorcode.STATUS_DATA_DELETE_FAIL, Result = facilityMasterResponse };
            _facilityMasterRepository.Setup(x => x.DeleteFacilityMaster(Convert.ToInt32(Id))).ReturnsAsync(facilityMasterResponse);

            //Arrange
            FacilityMasterController FacilityMasterController = new FacilityMasterController(_logger.Object, _config, _facilityMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await FacilityMasterController.DeleteFacilityMaster(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Facility Master By Id Unit Test Cases
        [Fact]
        public async void Task_GetFacilityMasterById_Returns_OkResult()
        {
            int Id = 1;
            FacilityMasterResponse facilityMasterResponse = new FacilityMasterResponse { Id = 1, FacilityCode = "KLAN", Description = "Test Facility", ForeignCurrencyId = 1, PrincipalCurrencyId = 1 };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = facilityMasterResponse };
            _facilityMasterRepository.Setup(x => x.GetFacilityMasterById(Id)).ReturnsAsync(facilityMasterResponse);

            //Arrange
            FacilityMasterController FacilityMasterController = new FacilityMasterController(_logger.Object, _config, _facilityMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await FacilityMasterController.GetFacilityMasterById(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetFacilityMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            FacilityMasterResponse facilityMasterResponse = new FacilityMasterResponse { Id = 1, FacilityCode = "KLAN", Description = "Test Facility", ForeignCurrencyId = 1, PrincipalCurrencyId = 1 };
            DataResponse response = new DataResponse { Message = error.STATUS_INVALID_REQUEST, StatusCode = errorcode.STATUS_INVALID_REQUEST, Result = facilityMasterResponse };
            _facilityMasterRepository.Setup(x => x.GetFacilityMasterById(Id)).ReturnsAsync(facilityMasterResponse);

            //Arrange
            FacilityMasterController FacilityMasterController = new FacilityMasterController(_logger.Object, _config, _facilityMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await FacilityMasterController.GetFacilityMasterById(Id);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetFacilityMasterById_Returns_NotFoundResult()
        {
            int Id = 1;
            FacilityMasterResponse facilityMasterResponse = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = facilityMasterResponse };
            _facilityMasterRepository.Setup(x => x.GetFacilityMasterById(Id)).ReturnsAsync(facilityMasterResponse);
            //Arrange
            FacilityMasterController FacilityMasterController = new FacilityMasterController(_logger.Object, _config, _facilityMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await FacilityMasterController.GetFacilityMasterById(Id);
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Facility Master Unit Test Cases
        [Fact]
        public async void Task_GetFacilityMaster_Returns_OkResult()
        {
            List<FacilityMasterResponse> data = new List<FacilityMasterResponse>() { new FacilityMasterResponse { Id = 1, FacilityCode = "KLAN", Description = "Test Facility", ForeignCurrencyId = 1, PrincipalCurrencyId = 1 } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _facilityMasterRepository.Setup(x => x.GetFacilityMaster()).ReturnsAsync(data);

            //Arrange
            FacilityMasterController FacilityMasterController = new FacilityMasterController(_logger.Object, _config, _facilityMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await FacilityMasterController.GetFacilityMaster();
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetFacilityMaster_Returns_NotFoundResult()
        {
            List<FacilityMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _facilityMasterRepository.Setup(x => x.GetFacilityMaster()).ReturnsAsync(data);
            //Arrange
            FacilityMasterController FacilityMasterController = new FacilityMasterController(_logger.Object, _config, _facilityMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await FacilityMasterController.GetFacilityMaster();
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

    }
}
