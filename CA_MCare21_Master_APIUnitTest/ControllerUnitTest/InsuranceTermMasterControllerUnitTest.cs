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
    public class InsuranceTermMasterControllerUnitTest
    {
        Mock<ILogger<InsuranceTermMasterController>> _logger;
        IConfiguration _config;
        Mock<IInsuranceTermMasterRepository> _insuranceTermMasterRepository;
        Mock<ICacheService> _cacheService;
        ErrorStatus error = new ErrorStatus();
        ErrorStatusCode errorcode = new ErrorStatusCode();


        public InsuranceTermMasterControllerUnitTest()
        {
            _logger = new Mock<ILogger<InsuranceTermMasterController>>();
            _insuranceTermMasterRepository = new Mock<IInsuranceTermMasterRepository>();
            _cacheService = new Mock<ICacheService>();
        }

        #region Add Insurance Term Master Unit Test Cases
        [Fact]
        public async void Task_AddInsuranceTermMaster_ValidData_Returns_OkResult()
        {
            InsuranceTermMasterResponse insuranceTermMasterResponse = new InsuranceTermMasterResponse { Id = 3, IsError = false };
            InsuranceTermMasterRequest insuranceTermMasterRequest = new InsuranceTermMasterRequest { Id = 3, InsuranceTerm = "Co-Insurance", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, FacilityCode = "KLAN" };
            _insuranceTermMasterRepository.Setup(x => x.AddInsuranceTermMaster(insuranceTermMasterRequest)).ReturnsAsync(insuranceTermMasterResponse);

            //Arrange
            InsuranceTermMasterController InsuranceTermMasterController = new InsuranceTermMasterController(_logger.Object, _config, _insuranceTermMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await InsuranceTermMasterController.AddInsuranceTermMaster(insuranceTermMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_AddInsuranceTermMaster_InvalidData_Returns_BadRequestResult()
        {
            InsuranceTermMasterResponse insuranceTermMasterResponse = new InsuranceTermMasterResponse { Id = 0, IsError = true };
            InsuranceTermMasterRequest insuranceTermMasterRequest = new InsuranceTermMasterRequest { Id = 3, InsuranceTerm = "Co-Insurance", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, FacilityCode = "KLAN" };
            _insuranceTermMasterRepository.Setup(x => x.AddInsuranceTermMaster(insuranceTermMasterRequest)).ReturnsAsync(insuranceTermMasterResponse);

            //Arrange
            InsuranceTermMasterController InsuranceTermMasterController = new InsuranceTermMasterController(_logger.Object, _config, _insuranceTermMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await InsuranceTermMasterController.AddInsuranceTermMaster(insuranceTermMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }        
        #endregion

        #region Update Insurance Term Master Unit Test Cases
        [Fact]
        public async void Task_UpdateInsuranceTermMaster_ValidData_Returns_OkResult()
        {
            InsuranceTermMasterResponse insuranceTermMasterResponse = new InsuranceTermMasterResponse { Id = 3, IsError = false };
            InsuranceTermMasterRequest insuranceTermMasterRequest = new InsuranceTermMasterRequest { Id = 3, InsuranceTerm = "Co-Insurance", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, FacilityCode = "KLAN" };
            _insuranceTermMasterRepository.Setup(x => x.UpdateInsuranceTermMaster(insuranceTermMasterRequest)).ReturnsAsync(insuranceTermMasterResponse);

            //Arrange
            InsuranceTermMasterController InsuranceTermMasterController = new InsuranceTermMasterController(_logger.Object, _config, _insuranceTermMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await InsuranceTermMasterController.UpdateInsuranceTermMaster(insuranceTermMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_UpdateInsuranceTermMaster_InvalidData_Returns_BadRequestResult()
        {
            InsuranceTermMasterResponse insuranceTermMasterResponse = new InsuranceTermMasterResponse { Id = 0, IsError = true };
            InsuranceTermMasterRequest insuranceTermMasterRequest = new InsuranceTermMasterRequest { Id = 3, InsuranceTerm = "Co-Insurance", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, FacilityCode = "KLAN" };
            _insuranceTermMasterRepository.Setup(x => x.UpdateInsuranceTermMaster(insuranceTermMasterRequest)).ReturnsAsync(insuranceTermMasterResponse);

            //Arrange
            InsuranceTermMasterController InsuranceTermMasterController = new InsuranceTermMasterController(_logger.Object, _config, _insuranceTermMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await InsuranceTermMasterController.UpdateInsuranceTermMaster(insuranceTermMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region Delete Insurance Term Master Unit Test Cases
        [Fact]
        public async void Task_DeleteInsuranceTermMaster_ValidData_Returns_OkResult()
        {
            int Id = 1;
            InsuranceTermMasterResponse insuranceTerm = new InsuranceTermMasterResponse { Id = 1, IsError = false };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETED, StatusCode = errorcode.STATUS_DATA_DELETED, Result = insuranceTerm };
            _insuranceTermMasterRepository.Setup(x => x.DeleteInsuranceTermMaster(Id)).ReturnsAsync(insuranceTerm);

            //Arrange
            InsuranceTermMasterController InsuranceTermMasterController = new InsuranceTermMasterController(_logger.Object, _config, _insuranceTermMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await InsuranceTermMasterController.DeleteInsuranceTermMaster(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_DeleteInsuranceTermMaster_InvalidData_Returns_BadRequest()
        {
            int? Id = null;
            InsuranceTermMasterResponse insuranceTerm = new InsuranceTermMasterResponse { Id = 0, IsError = true };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETE_FAIL, StatusCode = errorcode.STATUS_DATA_DELETE_FAIL, Result = insuranceTerm };
            _insuranceTermMasterRepository.Setup(x => x.DeleteInsuranceTermMaster(Convert.ToInt32(Id))).ReturnsAsync(insuranceTerm);

            //Arrange
            InsuranceTermMasterController InsuranceTermMasterController = new InsuranceTermMasterController(_logger.Object, _config, _insuranceTermMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await InsuranceTermMasterController.DeleteInsuranceTermMaster(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Insurance Term Master By Id Unit Test Cases
        [Fact]
        public async void Task_GetInsuranceTermMasterById_Returns_OkResult()
        {
            int Id = 5;
            InsuranceTermMasterResponse data = new InsuranceTermMasterResponse { Id = 5, InsuranceTerm = "Deductible" };
            _insuranceTermMasterRepository.Setup(x => x.GetInsuranceTermMasterById(Id)).ReturnsAsync(data);
            InsuranceTermMasterController InsuranceTermMasterController = new InsuranceTermMasterController(_logger.Object, _config, _insuranceTermMasterRepository.Object, _cacheService.Object);
            var result = await InsuranceTermMasterController.GetInsuranceTermMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_GetInsuranceTermMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            InsuranceTermMasterResponse insuranceTerm = new InsuranceTermMasterResponse { Id = 5, InsuranceTerm = "Deductible" };
            DataResponse response = new DataResponse { Message = error.STATUS_INVALID_REQUEST, StatusCode = errorcode.STATUS_INVALID_REQUEST, Result = insuranceTerm };
            _insuranceTermMasterRepository.Setup(x => x.GetInsuranceTermMasterById(Id)).ReturnsAsync(insuranceTerm);

            //Arrange
            InsuranceTermMasterController InsuranceTermMasterController = new InsuranceTermMasterController(_logger.Object, _config, _insuranceTermMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await InsuranceTermMasterController.GetInsuranceTermMasterById(Id);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetInsuranceTermMasterById_Returns_NotFoundResult()
        {
            int Id = 1;
            InsuranceTermMasterResponse insuranceTerm = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = insuranceTerm };
            _insuranceTermMasterRepository.Setup(x => x.GetInsuranceTermMasterById(Id)).ReturnsAsync(insuranceTerm);
            //Arrange
            InsuranceTermMasterController InsuranceTermMasterController = new InsuranceTermMasterController(_logger.Object, _config, _insuranceTermMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await InsuranceTermMasterController.GetInsuranceTermMasterById(Id);
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Insurance Term Master Unit Test Cases
        [Fact]
        public async void Task_GetInsuranceTermMaster_Returns_OkResult()
        {
            List<InsuranceTermMasterResponse> data = new List<InsuranceTermMasterResponse>() { new InsuranceTermMasterResponse { Id = 5, InsuranceTerm = "Deductible" } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _insuranceTermMasterRepository.Setup(x => x.GetInsuranceTermMaster()).ReturnsAsync(data);

            //Arrange
            InsuranceTermMasterController InsuranceTermMasterController = new InsuranceTermMasterController(_logger.Object, _config, _insuranceTermMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await InsuranceTermMasterController.GetInsuranceTermMaster();
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetInsuranceTermMaster_Returns_NotFoundResult()
        {
            List<InsuranceTermMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _insuranceTermMasterRepository.Setup(x => x.GetInsuranceTermMaster()).ReturnsAsync(data);
            //Arrange
            InsuranceTermMasterController InsuranceTermMasterController = new InsuranceTermMasterController(_logger.Object, _config, _insuranceTermMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await InsuranceTermMasterController.GetInsuranceTermMaster();
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

    }
}
