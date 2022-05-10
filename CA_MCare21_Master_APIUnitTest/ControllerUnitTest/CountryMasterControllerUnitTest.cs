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
    public class CountryMasterControllerUnitTest
    {
        Mock<ILogger<CountryMasterController>> _logger;
        IConfiguration _config;
        Mock<ICountryMasterRepository> _countryMasterRepository;
        Mock<ICacheService> _cacheService;
        ErrorStatus error = new ErrorStatus();
        ErrorStatusCode errorcode = new ErrorStatusCode();


        public CountryMasterControllerUnitTest()
        {
            _logger = new Mock<ILogger<CountryMasterController>>();
            _countryMasterRepository = new Mock<ICountryMasterRepository>();
            _cacheService = new Mock<ICacheService>();
        }


        #region Add Country Master Unit Test Cases
        [Fact]
        public async void Task_AddCountry_ValidData_Returns_OkResult()
        {
            CountryMasterResponse countryMasterResponse = new CountryMasterResponse { Id = 1, IsError = false };
            CountryMasterRequest countryMasterRequest = new CountryMasterRequest { Id = 1, Description = "TestCountry", Nationality = "TestCountry", NationalityRate = 1, CountryCode = "TST", CountryCodeNo = 1, Smrpcode = "TS", Smrpdescription = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADDED, StatusCode = errorcode.STATUS_DATA_ADDED, Result = countryMasterResponse };
            _countryMasterRepository.Setup(x => x.AddCountry(countryMasterRequest)).ReturnsAsync(countryMasterResponse);

            //Arrange
            CountryMasterController CountryMasterController = new CountryMasterController(_logger.Object, _config, _countryMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await CountryMasterController.AddCountry(countryMasterRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_AddCountry_InvalidData_Returns_BadRequest()
        {
            CountryMasterResponse countryMasterResponse = new CountryMasterResponse { Id = 0, IsError = true };
            CountryMasterRequest countryMasterRequest = new CountryMasterRequest { Id = 1, Description = "TestCountry", Nationality = "TestCountry", NationalityRate = 1, CountryCode = "TST", CountryCodeNo = 1, Smrpcode = "TS", Smrpdescription = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADD_FAIL, StatusCode = errorcode.STATUS_DATA_ADD_FAIL, Result = countryMasterResponse };
            _countryMasterRepository.Setup(x => x.AddCountry(countryMasterRequest)).ReturnsAsync(countryMasterResponse);

            //Arrange
            CountryMasterController CountryMasterController = new CountryMasterController(_logger.Object, _config, _countryMasterRepository.Object, _cacheService.Object);
            CountryMasterController.ModelState.AddModelError("Description", "Required");
            //Act
            var result = await CountryMasterController.AddCountry(countryMasterRequest);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Update Country Master Unit Test Cases
        [Fact]
        public async void Task_UpdateCountry_ValidData_Returns_OkResult()
        {
            CountryMasterResponse countryMasterResponse = new CountryMasterResponse { Id = 1, IsError = false };
            CountryMasterRequest countryMasterRequest = new CountryMasterRequest { Id = 1, Description = "TestCountry", Nationality = "TestCountry", NationalityRate = 1, CountryCode = "TST", CountryCodeNo = 1, Smrpcode = "TS", Smrpdescription = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATED, StatusCode = errorcode.STATUS_DATA_UPDATED, Result = countryMasterResponse };
            _countryMasterRepository.Setup(x => x.UpdateCountry(countryMasterRequest)).ReturnsAsync(countryMasterResponse);

            //Arrange
            CountryMasterController CountryMasterController = new CountryMasterController(_logger.Object, _config, _countryMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await CountryMasterController.UpdateCountry(countryMasterRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_UpdateCountry_InvalidData_Returns_BadRequest()
        {
            CountryMasterResponse countryMasterResponse = new CountryMasterResponse { Id = 0, IsError = true };
            CountryMasterRequest countryMasterRequest = new CountryMasterRequest { Id = 1, Description = "", Nationality = "TestCountry", NationalityRate = 1, CountryCode = "TST", CountryCodeNo = 1, Smrpcode = "TS", Smrpdescription = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATE_FAIL, StatusCode = errorcode.STATUS_DATA_UPDATE_FAIL, Result = countryMasterResponse };
            _countryMasterRepository.Setup(x => x.UpdateCountry(countryMasterRequest)).ReturnsAsync(countryMasterResponse);

            //Arrange
            CountryMasterController CountryMasterController = new CountryMasterController(_logger.Object, _config, _countryMasterRepository.Object, _cacheService.Object);
            CountryMasterController.ModelState.AddModelError("Description", "Required");
            //Act
            var result = await CountryMasterController.UpdateCountry(countryMasterRequest);
            var actualResult = result as BadRequestResult;
            //Asert
            Assert.NotNull(result);
            //Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }
        #endregion

        #region Delete Country Master Unit Test Cases
        [Fact]
        public async void Task_DeleteCountry_ValidData_Returns_OkResult()
        {
            int Id = 1;
            CountryMasterResponse countryMasterResponse = new CountryMasterResponse { Id = 1, IsError = false };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETED, StatusCode = errorcode.STATUS_DATA_DELETED, Result = countryMasterResponse };
            _countryMasterRepository.Setup(x => x.DeleteCountry(Id)).ReturnsAsync(countryMasterResponse);

            //Arrange
            CountryMasterController CountryMasterController = new CountryMasterController(_logger.Object, _config, _countryMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await CountryMasterController.DeleteCountry(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_DeleteCountry_InvalidData_Returns_BadRequest()
        {
            int? Id = null;
            CountryMasterResponse countryMasterResponse = new CountryMasterResponse { Id = 0, IsError = true };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETE_FAIL, StatusCode = errorcode.STATUS_DATA_DELETE_FAIL, Result = countryMasterResponse };
            _countryMasterRepository.Setup(x => x.DeleteCountry(Convert.ToInt32(Id))).ReturnsAsync(countryMasterResponse);

            //Arrange
            CountryMasterController CountryMasterController = new CountryMasterController(_logger.Object, _config, _countryMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await CountryMasterController.DeleteCountry(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Country Master By Id Unit Test Cases
        [Fact]
        public async void Task_GetCountryById_Returns_OkResult()
        {
            int Id = 1;
            CountryMasterResponse countryMasterResponse = new CountryMasterResponse { Id = 1, Description = "TestCountry", Nationality = "TestCountry", NationalityRate = 1, CountryCode = "TST", CountryCodeNo = 1, Smrpcode = "TS", Smrpdescription = "Test", FacilityCode = "KLAN" };
            _countryMasterRepository.Setup(x => x.GetCountryById(Id)).ReturnsAsync(countryMasterResponse);
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = countryMasterResponse };
            //Arrange
            CountryMasterController CountryMasterController = new CountryMasterController(_logger.Object, _config, _countryMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await CountryMasterController.GetCountryById(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetCountryById_Returns_BadRequestResult()
        {
            int? Id = null;
            CountryMasterResponse countryMasterResponse = new CountryMasterResponse { Id = 1, Description = "TestCountry", Nationality = "TestCountry", NationalityRate = 1, CountryCode = "TST", CountryCodeNo = 1, Smrpcode = "TS", Smrpdescription = "Test", FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_INVALID_REQUEST, StatusCode = errorcode.STATUS_INVALID_REQUEST, Result = countryMasterResponse };
            _countryMasterRepository.Setup(x => x.GetCountryById(Id)).ReturnsAsync(countryMasterResponse);

            //Arrange
            CountryMasterController CountryMasterController = new CountryMasterController(_logger.Object, _config, _countryMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await CountryMasterController.GetCountryById(Id);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetCountryById_Returns_NotFoundResult()
        {
            int Id = 1;
            CountryMasterResponse countryMasterResponse = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = countryMasterResponse };
            _countryMasterRepository.Setup(x => x.GetCountryById(Id)).ReturnsAsync(countryMasterResponse);
            //Arrange
            CountryMasterController CountryMasterController = new CountryMasterController(_logger.Object, _config, _countryMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await CountryMasterController.GetCountryById(Id);
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Country Master Unit Test Cases
        [Fact]
        public async void Task_GetCountryMaster_Returns_OkResult()
        {
            List<CountryMasterResponse> data = new List<CountryMasterResponse> { new CountryMasterResponse { Id = 1, Description = "TestCountry", Nationality = "TestCountry", NationalityRate = 1, CountryCode = "TST", CountryCodeNo = 1, Smrpcode = "TS", Smrpdescription = "Test", FacilityCode = "KLAN" } };
            _countryMasterRepository.Setup(x => x.GetCountryMaster()).ReturnsAsync(data);
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            //Arrange
            CountryMasterController CountryMasterController = new CountryMasterController(_logger.Object, _config, _countryMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await CountryMasterController.GetCountryMaster();
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetCountryMaster_Returns_NotFoundResult()
        {
            List<CountryMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _countryMasterRepository.Setup(x => x.GetCountryMaster()).ReturnsAsync(data);
            //Arrange
            CountryMasterController CountryMasterController = new CountryMasterController(_logger.Object, _config, _countryMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await CountryMasterController.GetCountryMaster();
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

    }
}
