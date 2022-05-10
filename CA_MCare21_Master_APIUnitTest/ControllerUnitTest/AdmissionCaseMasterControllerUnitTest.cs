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
    public class AdmissionCaseMasterControllerUnitTest
    {
        Mock<ILogger<AdmissionCaseMasterController>> _logger;
        IConfiguration _config;
        Mock<IAdmissionCaseMasterRepository> _AdmissionCaseMasterRepository;
        Mock<ICacheService> _cacheService;
        ErrorStatus error = new ErrorStatus();
        ErrorStatusCode errorcode = new ErrorStatusCode();


        public AdmissionCaseMasterControllerUnitTest()
        {
            _logger = new Mock<ILogger<AdmissionCaseMasterController>>();
            _AdmissionCaseMasterRepository = new Mock<IAdmissionCaseMasterRepository>();
            _cacheService = new Mock<ICacheService>();
        }

        #region Add Admission Cases Master Unit Test Cases
        [Fact]
        public async void Task_AddAdmissionCasesMaster_ValidData_Returns_OkResult()
        {
            AdmissionCasesMasterResponse admissionCasesMasterResponse = new AdmissionCasesMasterResponse { Id = 1, IsError = false };
            AdmissionCasesMasterRequest admissionCases = new AdmissionCasesMasterRequest { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1", EstimatedCostFrom = 64320.00, EstimatedCostTo = 4324264564.00, DepositCollected = 500.12, FacilityCode = "KLAN", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADDED, StatusCode = errorcode.STATUS_DATA_ADDED, Result = admissionCasesMasterResponse };
            _AdmissionCaseMasterRepository.Setup(x => x.AddAdmissionCasesMaster(admissionCases)).ReturnsAsync(admissionCasesMasterResponse);

            //Arrange
            AdmissionCaseMasterController AdmissionCaseMasterController = new AdmissionCaseMasterController(_logger.Object, _config, _AdmissionCaseMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await AdmissionCaseMasterController.AddAdmissionCasesMaster(admissionCases);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_AddAdmissionCasesMaster_InvalidData_Returns_BadRequest()
        {
            AdmissionCasesMasterResponse admissionCasesMasterResponse = new AdmissionCasesMasterResponse { Id = 1, IsError = true };
            AdmissionCasesMasterRequest admissionCases = new AdmissionCasesMasterRequest { Id = 1, Code = "", Description = "", EstimatedCostFrom = 64320.00, EstimatedCostTo = 4324264564.00, DepositCollected = 500.12, FacilityCode = "KLAN", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADD_FAIL, StatusCode = errorcode.STATUS_DATA_ADD_FAIL, Result = admissionCasesMasterResponse };
            _AdmissionCaseMasterRepository.Setup(x => x.AddAdmissionCasesMaster(admissionCases)).ReturnsAsync(admissionCasesMasterResponse);

            //Arrange
            AdmissionCaseMasterController AdmissionCaseMasterController = new AdmissionCaseMasterController(_logger.Object, _config, _AdmissionCaseMasterRepository.Object, _cacheService.Object);
            AdmissionCaseMasterController.ModelState.AddModelError("Description", "Required");
            AdmissionCaseMasterController.ModelState.AddModelError("Code", "Required");
            //Act
            var result = await AdmissionCaseMasterController.AddAdmissionCasesMaster(admissionCases);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Update Admission Cases Master Unit Test Cases
        [Fact]
        public async void Task_UpdateAdmissionCaseMaster_ValidData_Returns_OkResult()
        {
            AdmissionCasesMasterResponse admissionCasesMasterResponse = new AdmissionCasesMasterResponse { Id = 1, IsError = false };
            AdmissionCasesMasterRequest admissionCases = new AdmissionCasesMasterRequest { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1", EstimatedCostFrom = 64320.00, EstimatedCostTo = 4324264564.00, DepositCollected = 500.12, FacilityCode = "KLAN", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATED, StatusCode = errorcode.STATUS_DATA_UPDATED, Result = admissionCasesMasterResponse };
            _AdmissionCaseMasterRepository.Setup(x => x.UpdateAdmissionCasesMaster(admissionCases)).ReturnsAsync(admissionCasesMasterResponse);

            //Arrange
            AdmissionCaseMasterController AdmissionCaseMasterController = new AdmissionCaseMasterController(_logger.Object, _config, _AdmissionCaseMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await AdmissionCaseMasterController.UpdateAdmissionCasesMaster(admissionCases);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_UpdateAdmissionCasesMaster_InvalidData_Returns_BadRequest()
        {
            AdmissionCasesMasterResponse admissionCasesMasterResponse = new AdmissionCasesMasterResponse { Id = 1, IsError = true };
            AdmissionCasesMasterRequest admissionCases = new AdmissionCasesMasterRequest { Id = 1, Code = "", Description = "", EstimatedCostFrom = 64320.00, EstimatedCostTo = 4324264564.00, DepositCollected = 500.12, FacilityCode = "KLAN", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATE_FAIL, StatusCode = errorcode.STATUS_DATA_UPDATE_FAIL, Result = admissionCasesMasterResponse };
            _AdmissionCaseMasterRepository.Setup(x => x.UpdateAdmissionCasesMaster(admissionCases)).ReturnsAsync(admissionCasesMasterResponse);

            //Arrange
            AdmissionCaseMasterController AdmissionCaseMasterController = new AdmissionCaseMasterController(_logger.Object, _config, _AdmissionCaseMasterRepository.Object, _cacheService.Object);
            AdmissionCaseMasterController.ModelState.AddModelError("Description", "Required");
            AdmissionCaseMasterController.ModelState.AddModelError("Code", "Required");
            //Act
            var result = await AdmissionCaseMasterController.UpdateAdmissionCasesMaster(admissionCases);
            var actualResult = result as BadRequestResult;
            //Asert
            Assert.NotNull(result);
            //  Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }
        #endregion

        #region Delete Admission Case Master Unit Test Cases
        [Fact]
        public async void Task_DeleteAdmissionCasesMaster_ValidData_Returns_OkResult()
        {
            int Id = 1;
            AdmissionCasesMasterResponse admissionCases = new AdmissionCasesMasterResponse { Id = 1, IsError = false };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETED, StatusCode = errorcode.STATUS_DATA_DELETED, Result = admissionCases };
            _AdmissionCaseMasterRepository.Setup(x => x.DeleteAdmissionCasesMaster(Id)).ReturnsAsync(admissionCases);

            //Arrange
            AdmissionCaseMasterController AdmissionCaseMasterController = new AdmissionCaseMasterController(_logger.Object, _config, _AdmissionCaseMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await AdmissionCaseMasterController.DeleteAdmissionCasesMaster(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_DeleteAdmissionCasesMaster_InvalidData_Returns_BadRequest()
        {
            int? Id = null;
            AdmissionCasesMasterResponse admissionCases = new AdmissionCasesMasterResponse { Id = 0, IsError = true };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETE_FAIL, StatusCode = errorcode.STATUS_DATA_DELETE_FAIL, Result = admissionCases };
            _AdmissionCaseMasterRepository.Setup(x => x.DeleteAdmissionCasesMaster(Convert.ToInt32(Id))).ReturnsAsync(admissionCases);

            //Arrange
            AdmissionCaseMasterController AdmissionCaseMasterController = new AdmissionCaseMasterController(_logger.Object, _config, _AdmissionCaseMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await AdmissionCaseMasterController.DeleteAdmissionCasesMaster(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Admission Cases Master By Id Unit Test Cases
        [Fact]
        public async void Task_GetAdmissionCaseMasterById_Returns_OkResult()
        {
            int Id = 1;
            AdmissionCasesMasterResponse admissionCases = new AdmissionCasesMasterResponse { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1", EstimatedCostFrom = 64320.00m, EstimatedCostTo = 4324264564.00m, DepositCollected = 500.12m };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = admissionCases };
            _AdmissionCaseMasterRepository.Setup(x => x.GetAdmissionCaseMasterById(Id)).ReturnsAsync(admissionCases);

            //Arrange
            AdmissionCaseMasterController AdmissionCaseMasterController = new AdmissionCaseMasterController(_logger.Object, _config, _AdmissionCaseMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await AdmissionCaseMasterController.GetAdmissionCaseMasterById(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetAdmissionCaseMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            AdmissionCasesMasterResponse admissionCases = new AdmissionCasesMasterResponse { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1", EstimatedCostFrom = 64320.00m, EstimatedCostTo = 4324264564.00m, DepositCollected = 500.12m };
            DataResponse response = new DataResponse { Message = error.STATUS_INVALID_REQUEST, StatusCode = errorcode.STATUS_INVALID_REQUEST, Result = admissionCases };
            _AdmissionCaseMasterRepository.Setup(x => x.GetAdmissionCaseMasterById(Id)).ReturnsAsync(admissionCases);

            //Arrange
            AdmissionCaseMasterController AdmissionCaseMasterController = new AdmissionCaseMasterController(_logger.Object, _config, _AdmissionCaseMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await AdmissionCaseMasterController.GetAdmissionCaseMasterById(Id);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetAdmissionCaseMasterById_Returns_NotFoundResult()
        {
            int Id = 1;
            AdmissionCasesMasterResponse admissionCases = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = admissionCases };
            _AdmissionCaseMasterRepository.Setup(x => x.GetAdmissionCaseMasterById(Id)).ReturnsAsync(admissionCases);
            //Arrange
            AdmissionCaseMasterController AdmissionCaseMasterController = new AdmissionCaseMasterController(_logger.Object, _config, _AdmissionCaseMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await AdmissionCaseMasterController.GetAdmissionCaseMasterById(Id);
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Admission Cases Master Unit Test Cases
        [Fact]
        public async void Task_GetAdmissionCasesMaster_Returns_OkResult()
        {
            List<AdmissionCasesMasterResponse> data = new List<AdmissionCasesMasterResponse>() { new AdmissionCasesMasterResponse { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1", EstimatedCostFrom = 64320.00m, EstimatedCostTo = 4324264564.00m, DepositCollected = 500.12m } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _AdmissionCaseMasterRepository.Setup(x => x.GetAdmissionCaseMaster()).ReturnsAsync(data);

            //Arrange
            AdmissionCaseMasterController AdmissionCaseMasterController = new AdmissionCaseMasterController(_logger.Object, _config, _AdmissionCaseMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await AdmissionCaseMasterController.GetAdmissionCaseMaster();
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetAdmissionCaseMaster_Returns_NotFoundResult()
        {
            List<AdmissionCasesMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _AdmissionCaseMasterRepository.Setup(x => x.GetAdmissionCaseMaster()).ReturnsAsync(data);
            //Arrange
            AdmissionCaseMasterController AdmissionCaseMasterController = new AdmissionCaseMasterController(_logger.Object, _config, _AdmissionCaseMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await AdmissionCaseMasterController.GetAdmissionCaseMaster();
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

    }
}
