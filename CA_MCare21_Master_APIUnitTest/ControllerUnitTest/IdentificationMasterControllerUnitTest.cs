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
    public class IdentificationMasterControllerUnitTest
    {
        Mock<ILogger<IdentificationMasterController>> _logger;
        IConfiguration _config;
        Mock<IIdentificationMasterRepository> _identificationMasterRepository;
        Mock<ICacheService> _cacheService;
        ErrorStatus error = new ErrorStatus();
        ErrorStatusCode errorcode = new ErrorStatusCode();


        public IdentificationMasterControllerUnitTest()
        {
            _logger = new Mock<ILogger<IdentificationMasterController>>();
            _identificationMasterRepository = new Mock<IIdentificationMasterRepository>();
            _cacheService = new Mock<ICacheService>();
        }

        #region Add Identification Master Unit Test Cases
        [Fact]
        public async void Task_AddIdentificationMaster_ValidData_Returns_OkResult()
        {
            IdentificationMasterResponse identificationMasterResponse = new IdentificationMasterResponse { Id = 3, IsError = false };
            IdentificationMasterRequest identificationMasterRequest = new IdentificationMasterRequest { Id = 3, Code = "IT003", Description = "Old IC", IdentificationFormat = "Alphanumeric", ExpiryDate = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 409, UpdatedDate = null, MasterIdentificationTypeId = 2, Smrpcode = "1", Smrpdescription = "Old IC", FacilityCode = "KLAN", IsLocal = false };
            _identificationMasterRepository.Setup(x => x.AddIdentificationMaster(identificationMasterRequest)).ReturnsAsync(identificationMasterResponse);

            //Arrange
            IdentificationMasterController IdentificationMasterController = new IdentificationMasterController(_logger.Object, _config, _identificationMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await IdentificationMasterController.AddIdentificationMaster(identificationMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_AddIdentificationMaster_InvalidData_Returns_BadRequestResult()
        {
            IdentificationMasterResponse identificationMasterResponse = new IdentificationMasterResponse { Id = 0, IsError = true };
            IdentificationMasterRequest identificationMasterRequest = new IdentificationMasterRequest { Id = 3, Code = "IT003", Description = "Old IC", IdentificationFormat = "Alphanumeric", ExpiryDate = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 409, UpdatedDate = null, MasterIdentificationTypeId = 2, Smrpcode = "1", Smrpdescription = "Old IC", FacilityCode = "KLAN", IsLocal = false };
            _identificationMasterRepository.Setup(x => x.AddIdentificationMaster(identificationMasterRequest)).ReturnsAsync(identificationMasterResponse);

            //Arrange
            IdentificationMasterController IdentificationMasterController = new IdentificationMasterController(_logger.Object, _config, _identificationMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await IdentificationMasterController.AddIdentificationMaster(identificationMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }        
        #endregion

        #region Update Identification Master Unit Test Cases
        [Fact]
        public async void Task_UpdateIdentificationMaster_ValidData_Returns_OkResult()
        {
            IdentificationMasterResponse identificationMasterResponse = new IdentificationMasterResponse { Id = 3, IsError = false };
            IdentificationMasterRequest identificationMasterRequest = new IdentificationMasterRequest { Id = 3, Code = "IT003", Description = "Old IC", IdentificationFormat = "Alphanumeric", ExpiryDate = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 409, UpdatedDate = null, MasterIdentificationTypeId = 2, Smrpcode = "1", Smrpdescription = "Old IC", FacilityCode = "KLAN", IsLocal = false };
            _identificationMasterRepository.Setup(x => x.UpdateIdentificationMaster(identificationMasterRequest)).ReturnsAsync(identificationMasterResponse);

            //Arrange
            IdentificationMasterController IdentificationMasterController = new IdentificationMasterController(_logger.Object, _config, _identificationMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await IdentificationMasterController.UpdateIdentificationMaster(identificationMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_UpdateIdentificationMaster_InvalidData_Returns_BadRequestResult()
        {
            IdentificationMasterResponse identificationMasterResponse = new IdentificationMasterResponse { Id = 0, IsError = true };
            IdentificationMasterRequest identificationMasterRequest = new IdentificationMasterRequest { Id = 3, Code = "IT003", Description = "Old IC", IdentificationFormat = "Alphanumeric", ExpiryDate = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 409, UpdatedDate = null, MasterIdentificationTypeId = 2, Smrpcode = "1", Smrpdescription = "Old IC", FacilityCode = "KLAN", IsLocal = false };
            _identificationMasterRepository.Setup(x => x.UpdateIdentificationMaster(identificationMasterRequest)).ReturnsAsync(identificationMasterResponse);

            //Arrange
            IdentificationMasterController IdentificationMasterController = new IdentificationMasterController(_logger.Object, _config, _identificationMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await IdentificationMasterController.UpdateIdentificationMaster(identificationMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region Delete Identification Master Unit Test Cases
        [Fact]
        public async void Task_DeleteIdentificationMaster_ValidData_Returns_OkResult()
        {
            int Id = 1;
            IdentificationMasterResponse identification = new IdentificationMasterResponse { Id = 1, IsError = false };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETED, StatusCode = errorcode.STATUS_DATA_DELETED, Result = identification };
            _identificationMasterRepository.Setup(x => x.DeleteIdentificationMaster(Id)).ReturnsAsync(identification);

            //Arrange
            IdentificationMasterController IdentificationMasterController = new IdentificationMasterController(_logger.Object, _config, _identificationMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await IdentificationMasterController.DeleteIdentificationMaster(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_DeleteIdentificationMaster_InvalidData_Returns_BadRequest()
        {
            int? Id = null;
            IdentificationMasterResponse identification = new IdentificationMasterResponse { Id = 0, IsError = true };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETE_FAIL, StatusCode = errorcode.STATUS_DATA_DELETE_FAIL, Result = identification };
            _identificationMasterRepository.Setup(x => x.DeleteIdentificationMaster(Convert.ToInt32(Id))).ReturnsAsync(identification);

            //Arrange
            IdentificationMasterController IdentificationMasterController = new IdentificationMasterController(_logger.Object, _config, _identificationMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await IdentificationMasterController.DeleteIdentificationMaster(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Identification Master By Id Unit Test Cases
        [Fact]
        public async void Task_GetIdentificationMasterById_Returns_OkResult()
        {
            int Id = 3;
            IdentificationMasterResponse data = new IdentificationMasterResponse { Id = 3, Code = "IT003", Description = "Old IC", IdentificationFormat = "Alphanumeric", SmrpCode = "1", SmrpDescription = "Old IC" };
            _identificationMasterRepository.Setup(x => x.GetIdentificationMasterById(Id)).ReturnsAsync(data);
            IdentificationMasterController IdentificationMasterController = new IdentificationMasterController(_logger.Object, _config, _identificationMasterRepository.Object, _cacheService.Object);
            var result = await IdentificationMasterController.GetIdentificationMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_GetIdentificationMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            IdentificationMasterResponse identification = new IdentificationMasterResponse { Id = 3, Code = "IT003", Description = "Old IC", IdentificationFormat = "Alphanumeric", SmrpCode = "1", SmrpDescription = "Old IC" };
            DataResponse response = new DataResponse { Message = error.STATUS_INVALID_REQUEST, StatusCode = errorcode.STATUS_INVALID_REQUEST, Result = identification };
            _identificationMasterRepository.Setup(x => x.GetIdentificationMasterById(Id)).ReturnsAsync(identification);

            //Arrange
            IdentificationMasterController IdentificationMasterController = new IdentificationMasterController(_logger.Object, _config, _identificationMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await IdentificationMasterController.GetIdentificationMasterById(Id);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetIdentificationMasterById_Returns_NotFoundResult()
        {
            int Id = 1;
            IdentificationMasterResponse identification = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = identification };
            _identificationMasterRepository.Setup(x => x.GetIdentificationMasterById(Id)).ReturnsAsync(identification);
            //Arrange
            IdentificationMasterController IdentificationMasterController = new IdentificationMasterController(_logger.Object, _config, _identificationMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await IdentificationMasterController.GetIdentificationMasterById(Id);
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Identification Master Unit Test Cases
        [Fact]
        public async void Task_GetIdentificationMaster_Returns_OkResult()
        {
            List<IdentificationMasterResponse> data = new List<IdentificationMasterResponse>() { new IdentificationMasterResponse { Id = 3, Code = "IT003", Description = "Old IC", IdentificationFormat = "Alphanumeric", SmrpCode = "1", SmrpDescription = "Old IC" } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _identificationMasterRepository.Setup(x => x.GetIdentificationMaster()).ReturnsAsync(data);

            //Arrange
            IdentificationMasterController IdentificationMasterController = new IdentificationMasterController(_logger.Object, _config, _identificationMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await IdentificationMasterController.GetIdentificationMaster();
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetIdentificationMaster_Returns_NotFoundResult()
        {
            List<IdentificationMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _identificationMasterRepository.Setup(x => x.GetIdentificationMaster()).ReturnsAsync(data);
            //Arrange
            IdentificationMasterController IdentificationMasterController = new IdentificationMasterController(_logger.Object, _config, _identificationMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await IdentificationMasterController.GetIdentificationMaster();
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

    }
}
