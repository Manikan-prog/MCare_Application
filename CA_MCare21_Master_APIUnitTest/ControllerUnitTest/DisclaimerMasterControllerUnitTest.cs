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
    public class DisclaimerMasterControllerUnitTest
    {
        Mock<ILogger<DisclaimerMasterController>> _logger;
        IConfiguration _config;
        Mock<IDisclaimerMasterRepository> _disclaimerMasterRepository;
        Mock<ICacheService> _cacheService;
        ErrorStatus error = new ErrorStatus();
        ErrorStatusCode errorcode = new ErrorStatusCode();


        public DisclaimerMasterControllerUnitTest()
        {
            _logger = new Mock<ILogger<DisclaimerMasterController>>();
            _disclaimerMasterRepository = new Mock<IDisclaimerMasterRepository>();
            _cacheService = new Mock<ICacheService>();
        }


        #region Add Disclaimer Master Unit Test Cases
        [Fact]
        public async void Task_AddDisclaimer_ValidData_Returns_OkResult()
        {
            DisclaimerMasterResponse disclaimer = new DisclaimerMasterResponse { Id = 1, IsError = false };
            DisclaimerRequest disclaimerRequest = new DisclaimerRequest { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADDED, StatusCode = errorcode.STATUS_DATA_ADDED, Result = disclaimer };
            _disclaimerMasterRepository.Setup(x => x.AddDisclaimer(disclaimerRequest)).ReturnsAsync(disclaimer);

            //Arrange
            DisclaimerMasterController DisclaimerMasterController = new DisclaimerMasterController(_logger.Object, _config, _disclaimerMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await DisclaimerMasterController.AddDisclaimer(disclaimerRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_AddDisclaimer_InvalidData_Returns_BadRequest()
        {
            DisclaimerMasterResponse disclaimer = new DisclaimerMasterResponse { Id = 1, IsError = true };
            DisclaimerRequest disclaimerRequest = new DisclaimerRequest { Id = 1, Description = "", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADD_FAIL, StatusCode = errorcode.STATUS_DATA_ADD_FAIL, Result = disclaimer };
            _disclaimerMasterRepository.Setup(x => x.AddDisclaimer(disclaimerRequest)).ReturnsAsync(disclaimer);

            //Arrange
            DisclaimerMasterController DisclaimerMasterController = new DisclaimerMasterController(_logger.Object, _config, _disclaimerMasterRepository.Object, _cacheService.Object);
            DisclaimerMasterController.ModelState.AddModelError("Description", "Required");
            //Act
            var result = await DisclaimerMasterController.AddDisclaimer(disclaimerRequest);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Update Disclaimer Master Unit Test Cases
        [Fact]
        public async void Task_UpdateDisclaimer_ValidData_Returns_OkResult()
        {
            DisclaimerMasterResponse disclaimer = new DisclaimerMasterResponse { Id = 1, IsError = false };
            DisclaimerRequest disclaimerRequest = new DisclaimerRequest { Id = 1, FacilityCode = "KLAN", Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATED, StatusCode = errorcode.STATUS_DATA_UPDATED, Result = disclaimer };
            _disclaimerMasterRepository.Setup(x => x.UpdateDisclaimer(disclaimerRequest)).ReturnsAsync(disclaimer);

            //Arrange
            DisclaimerMasterController DisclaimerMasterController = new DisclaimerMasterController(_logger.Object, _config, _disclaimerMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await DisclaimerMasterController.UpdateDisclaimer(disclaimerRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_UpdateDisclaimer_InvalidData_Returns_BadRequest()
        {
            DisclaimerMasterResponse disclaimer = new DisclaimerMasterResponse { Id = 1, IsError = true };
            DisclaimerRequest disclaimerRequest = new DisclaimerRequest { Id = 1, FacilityCode = "KLAN", Description = "", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATE_FAIL, StatusCode = errorcode.STATUS_DATA_UPDATE_FAIL, Result = disclaimer };
            _disclaimerMasterRepository.Setup(x => x.UpdateDisclaimer(disclaimerRequest)).ReturnsAsync(disclaimer);

            //Arrange
            DisclaimerMasterController DisclaimerMasterController = new DisclaimerMasterController(_logger.Object, _config, _disclaimerMasterRepository.Object, _cacheService.Object);
            DisclaimerMasterController.ModelState.AddModelError("Description", "Required");
            //Act
            var result = await DisclaimerMasterController.UpdateDisclaimer(disclaimerRequest);
            var actualResult = result as BadRequestResult;
            //Asert
            Assert.NotNull(result);
            // Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }
        #endregion

        #region Delete Disclaimer Master Unit Test Cases
        [Fact]
        public async void Task_DeleteDisclaimer_ValidData_Returns_OkResult()
        {
            int Id = 1;
            DisclaimerMasterResponse disclaimer = new DisclaimerMasterResponse { Id = 1, IsError = false };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETED, StatusCode = errorcode.STATUS_DATA_DELETED, Result = disclaimer };
            _disclaimerMasterRepository.Setup(x => x.DeleteDisclaimer(Id)).ReturnsAsync(disclaimer);

            //Arrange
            DisclaimerMasterController DisclaimerMasterController = new DisclaimerMasterController(_logger.Object, _config, _disclaimerMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await DisclaimerMasterController.DeleteDisclaimer(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_DeleteDisclaimer_InvalidData_Returns_BadRequest()
        {
            int? Id = null;
            DisclaimerMasterResponse disclaimer = new DisclaimerMasterResponse { Id = 0, IsError = true };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETE_FAIL, StatusCode = errorcode.STATUS_DATA_DELETE_FAIL, Result = disclaimer };
            _disclaimerMasterRepository.Setup(x => x.DeleteDisclaimer(Convert.ToInt32(Id))).ReturnsAsync(disclaimer);

            //Arrange
            DisclaimerMasterController DisclaimerMasterController = new DisclaimerMasterController(_logger.Object, _config, _disclaimerMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await DisclaimerMasterController.DeleteDisclaimer(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Disclaimer Master By Id Unit Test Cases
        [Fact]
        public async void Task_GetDisclaimerById_Returns_OkResult()
        {
            int Id = 1;
            DisclaimerMasterResponse disclaimer = new DisclaimerMasterResponse { Id = 1, Description = "Test" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = disclaimer };
            _disclaimerMasterRepository.Setup(x => x.GetDisclaimerById(Id)).ReturnsAsync(disclaimer);

            //Arrange
            DisclaimerMasterController DisclaimerMasterController = new DisclaimerMasterController(_logger.Object, _config, _disclaimerMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await DisclaimerMasterController.GetDisclaimerById(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetDisclaimerById_Returns_BadRequestResult()
        {
            int? Id = null;
            DisclaimerMasterResponse disclaimer = new DisclaimerMasterResponse { Id = 1, Description = "Test" };
            DataResponse response = new DataResponse { Message = error.STATUS_INVALID_REQUEST, StatusCode = errorcode.STATUS_INVALID_REQUEST, Result = disclaimer };
            _disclaimerMasterRepository.Setup(x => x.GetDisclaimerById(Id)).ReturnsAsync(disclaimer);

            //Arrange
            DisclaimerMasterController DisclaimerMasterController = new DisclaimerMasterController(_logger.Object, _config, _disclaimerMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await DisclaimerMasterController.GetDisclaimerById(Id);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetDisclaimerById_Returns_NotFoundResult()
        {
            int Id = 1;
            DisclaimerMasterResponse disclaimer = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = disclaimer };
            _disclaimerMasterRepository.Setup(x => x.GetDisclaimerById(Id)).ReturnsAsync(disclaimer);
            //Arrange
            DisclaimerMasterController DisclaimerMasterController = new DisclaimerMasterController(_logger.Object, _config, _disclaimerMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await DisclaimerMasterController.GetDisclaimerById(Id);
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Disclaimer Master Unit Test Cases
        [Fact]
        public async void Task_GetDisclaimer_Returns_OkResult()
        {
            List<DisclaimerMasterResponse> data = new List<DisclaimerMasterResponse>() { new DisclaimerMasterResponse { Id = 1, Description = "Test" } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _disclaimerMasterRepository.Setup(x => x.GetDisclaimer()).ReturnsAsync(data);

            //Arrange
            DisclaimerMasterController DisclaimerMasterController = new DisclaimerMasterController(_logger.Object, _config, _disclaimerMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await DisclaimerMasterController.GetDisclaimer();
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetDisclaimer_Returns_NotFoundResult()
        {
            List<DisclaimerMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _disclaimerMasterRepository.Setup(x => x.GetDisclaimer()).ReturnsAsync(data);
            //Arrange
            DisclaimerMasterController DisclaimerMasterController = new DisclaimerMasterController(_logger.Object, _config, _disclaimerMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await DisclaimerMasterController.GetDisclaimer();
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

    }
}
