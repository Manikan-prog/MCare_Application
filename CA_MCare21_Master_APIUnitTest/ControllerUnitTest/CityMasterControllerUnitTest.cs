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
    public class CityMasterControllerUnitTest
    {
        Mock<ILogger<CityMasterController>> _logger;
        IConfiguration _config;
        Mock<ICityMasterRepository> _cityMasterRepository;
        Mock<ICacheService> _cacheService;
        ErrorStatus error = new ErrorStatus();
        ErrorStatusCode errorcode = new ErrorStatusCode();


        public CityMasterControllerUnitTest()
        {
            _logger = new Mock<ILogger<CityMasterController>>();
            _cityMasterRepository = new Mock<ICityMasterRepository>();
            _cacheService = new Mock<ICacheService>();
        }

        #region Add City Master Unit Test Cases
        [Fact]
        public async void Task_AddCity_ValidData_Returns_OkResult()
        {
            CityMasterResponse cityMasterResponse = new CityMasterResponse { Id = 1, IsError = false };
            CityMasterRequest cityMasterRequest = new CityMasterRequest { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI", Smrpcode = "335", Smrpdescription = "Ayer Baloi", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADDED, StatusCode = errorcode.STATUS_DATA_ADDED, Result = cityMasterResponse };
            _cityMasterRepository.Setup(x => x.AddCity(cityMasterRequest)).ReturnsAsync(cityMasterResponse);

            //Arrange
            CityMasterController CityMasterController = new CityMasterController(_logger.Object, _config, _cityMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await CityMasterController.AddCity(cityMasterRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_AddCity_InvalidData_Returns_BadRequest()
        {
            CityMasterResponse cityMasterResponse = new CityMasterResponse { Id = 1, IsError = true };
            CityMasterRequest cityMasterRequest = new CityMasterRequest { Id = 1, StateId = 9, CityCode = "", Description = "", Smrpcode = "335", Smrpdescription = "Ayer Baloi", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADD_FAIL, StatusCode = errorcode.STATUS_DATA_ADD_FAIL, Result = cityMasterResponse };
            _cityMasterRepository.Setup(x => x.AddCity(cityMasterRequest)).ReturnsAsync(cityMasterResponse);

            //Arrange
            CityMasterController CityMasterController = new CityMasterController(_logger.Object, _config, _cityMasterRepository.Object, _cacheService.Object);
            CityMasterController.ModelState.AddModelError("Description", "Required");
            CityMasterController.ModelState.AddModelError("CityCode", "Required");
            //Act
            var result = await CityMasterController.AddCity(cityMasterRequest);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Update City Master Unit Test Cases
        [Fact]
        public async void Task_UpdateCity_ValidData_Returns_OkResult()
        {
            CityMasterResponse cityMasterResponse = new CityMasterResponse { Id = 1, IsError = false };
            CityMasterRequest cityMasterRequest = new CityMasterRequest { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI", Smrpcode = "335", Smrpdescription = "Ayer Baloi", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATED, StatusCode = errorcode.STATUS_DATA_UPDATED, Result = cityMasterResponse };
            _cityMasterRepository.Setup(x => x.UpdateCity(cityMasterRequest)).ReturnsAsync(cityMasterResponse);

            //Arrange
            CityMasterController CityMasterController = new CityMasterController(_logger.Object, _config, _cityMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await CityMasterController.UpdateCity(cityMasterRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_UpdateCity_InvalidData_Returns_BadRequest()
        {
            CityMasterResponse cityMasterResponse = new CityMasterResponse { Id = 1, IsError = true };
            CityMasterRequest cityMasterRequest = new CityMasterRequest { Id = 1, StateId = 9, CityCode = "", Description = "", Smrpcode = "335", Smrpdescription = "Ayer Baloi", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATE_FAIL, StatusCode = errorcode.STATUS_DATA_UPDATE_FAIL, Result = cityMasterResponse };
            _cityMasterRepository.Setup(x => x.UpdateCity(cityMasterRequest)).ReturnsAsync(cityMasterResponse);

            //Arrange
            CityMasterController CityMasterController = new CityMasterController(_logger.Object, _config, _cityMasterRepository.Object, _cacheService.Object);
            CityMasterController.ModelState.AddModelError("Description", "Required");
            CityMasterController.ModelState.AddModelError("CityCode", "Required");
            //Act
            var result = await CityMasterController.UpdateCity(cityMasterRequest);
            var actualResult = result as BadRequestResult;
            //Asert
            Assert.NotNull(result);
            // Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }
        #endregion

        #region Delete City Master Unit Test Cases
        [Fact]
        public async void Task_DeleteCity_ValidData_Returns_OkResult()
        {
            int Id = 1;
            CityMasterResponse cityMasterResponse = new CityMasterResponse { Id = 1, IsError = false };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETED, StatusCode = errorcode.STATUS_DATA_DELETED, Result = cityMasterResponse };
            _cityMasterRepository.Setup(x => x.DeleteCity(Id)).ReturnsAsync(cityMasterResponse);

            //Arrange
            CityMasterController CityMasterController = new CityMasterController(_logger.Object, _config, _cityMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await CityMasterController.DeleteCity(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_DeleteCity_InvalidData_Returns_BadRequest()
        {
            int? Id = null;
            CityMasterResponse cityMasterResponse = new CityMasterResponse { Id = 0, IsError = true };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETE_FAIL, StatusCode = errorcode.STATUS_DATA_DELETE_FAIL, Result = cityMasterResponse };
            _cityMasterRepository.Setup(x => x.DeleteCity(Convert.ToInt32(Id))).ReturnsAsync(cityMasterResponse);

            //Arrange
            CityMasterController CityMasterController = new CityMasterController(_logger.Object, _config, _cityMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await CityMasterController.DeleteCity(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get City Master By Id Unit Test Cases
        [Fact]
        public async void Task_GetCityById_Returns_OkResult()
        {
            int Id = 1;
            CityMasterResponse cityMasterResponse = new CityMasterResponse { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI", Smrpcode = "335", Smrpdescription = "Ayer Baloi" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = cityMasterResponse };
            _cityMasterRepository.Setup(x => x.GetCityById(Id)).ReturnsAsync(cityMasterResponse);

            //Arrange
            CityMasterController CityMasterController = new CityMasterController(_logger.Object, _config, _cityMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await CityMasterController.GetCityById(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetCityById_Returns_BadRequestResult()
        {
            int? Id = null;
            CityMasterResponse cityMasterResponse = new CityMasterResponse { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI", Smrpcode = "335", Smrpdescription = "Ayer Baloi" };
            DataResponse response = new DataResponse { Message = error.STATUS_INVALID_REQUEST, StatusCode = errorcode.STATUS_INVALID_REQUEST, Result = cityMasterResponse };
            _cityMasterRepository.Setup(x => x.GetCityById(Id)).ReturnsAsync(cityMasterResponse);

            //Arrange
            CityMasterController CityMasterController = new CityMasterController(_logger.Object, _config, _cityMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await CityMasterController.GetCityById(Id);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetCityById_Returns_NotFoundResult()
        {
            int Id = 1;
            CityMasterResponse cityMasterResponse = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = cityMasterResponse };
            _cityMasterRepository.Setup(x => x.GetCityById(Id)).ReturnsAsync(cityMasterResponse);
            //Arrange
            CityMasterController CityMasterController = new CityMasterController(_logger.Object, _config, _cityMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await CityMasterController.GetCityById(Id);
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get City Master Unit Test Cases
        [Fact]
        public async void Task_GetCityMaster_Returns_OkResult()
        {
            List<CityMasterResponse> data = new List<CityMasterResponse>() { new CityMasterResponse { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI", Smrpcode = "335", Smrpdescription = "Ayer Baloi" } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _cityMasterRepository.Setup(x => x.GetCityMaster()).ReturnsAsync(data);

            //Arrange
            CityMasterController CityMasterController = new CityMasterController(_logger.Object, _config, _cityMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await CityMasterController.GetCityMaster();
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetCityMaster_Returns_NotFoundResult()
        {
            List<CityMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _cityMasterRepository.Setup(x => x.GetCityMaster()).ReturnsAsync(data);
            //Arrange
            CityMasterController CityMasterController = new CityMasterController(_logger.Object, _config, _cityMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await CityMasterController.GetCityMaster();
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

    }
}
