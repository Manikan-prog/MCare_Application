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
  public class ClinicMasterControllerUnitTest
    {
        Mock<ILogger<ClinicMasterController>> _logger;
        IConfiguration _config;
        Mock<IClinicMasterRepository> _clinicMasterRepository;
        Mock<ICacheService> _cacheService;
        ErrorStatus error = new ErrorStatus();
        ErrorStatusCode errorcode = new ErrorStatusCode();


        public ClinicMasterControllerUnitTest()
        {
            _logger = new Mock<ILogger<ClinicMasterController>>();
            _clinicMasterRepository = new Mock<IClinicMasterRepository>();
            _cacheService = new Mock<ICacheService>();
        }


        #region Add Clinic Master Unit Test Cases
        [Fact]
        public async void Task_AddClinicMaster_ValidData_Returns_OkResult()
        {
            ClinicMasterResponse clinicMasterResponse = new ClinicMasterResponse { Id = 1, IsError = false };
            ClinicMasterRequest clinicMasterRequest = new ClinicMasterRequest { Id = 1, ClinicCode = "K0012", ClinicName = "Test Clinic", DoctorName = "Test Doctor", Street1 = "", Street2 = "", Street3 = "", Street4 = "", City = "Test", PostCode = "41200", StateId = 1, PhoneNo = "", FaxNo = "", MobilePhoneNo = "", EmailId = "", IsObsolete = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADDED, StatusCode = errorcode.STATUS_DATA_ADDED, Result = clinicMasterResponse };
            _clinicMasterRepository.Setup(x => x.AddClinicMaster(clinicMasterRequest)).ReturnsAsync(clinicMasterResponse);

            //Arrange
            ClinicMasterController ClinicMasterController = new ClinicMasterController(_logger.Object, _config, _clinicMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await ClinicMasterController.AddClinicMaster(clinicMasterRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_AddClinicMaster_InvalidData_Returns_BadRequest()
        {
            ClinicMasterResponse clinicMasterResponse = new ClinicMasterResponse { Id = 0, IsError = true };
            ClinicMasterRequest clinicMasterRequest = new ClinicMasterRequest { Id = 1, ClinicCode = "", ClinicName = "", DoctorName = "Test Doctor", Street1 = "", Street2 = "", Street3 = "", Street4 = "", City = "Test", PostCode = "41200", StateId = 1, PhoneNo = "", FaxNo = "", MobilePhoneNo = "", EmailId = "", IsObsolete = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADD_FAIL, StatusCode = errorcode.STATUS_DATA_ADD_FAIL, Result = clinicMasterResponse };
            _clinicMasterRepository.Setup(x => x.AddClinicMaster(clinicMasterRequest)).ReturnsAsync(clinicMasterResponse);

            //Arrange
            ClinicMasterController ClinicMasterController = new ClinicMasterController(_logger.Object, _config, _clinicMasterRepository.Object, _cacheService.Object);
            ClinicMasterController.ModelState.AddModelError("ClinicCode", "Required");
            ClinicMasterController.ModelState.AddModelError("ClinicName", "Required");
            //Act
            var result = await ClinicMasterController.AddClinicMaster(clinicMasterRequest);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Update Clinic Master Unit Test Cases
        [Fact]
        public async void Task_UpdateClinicMaster_ValidData_Returns_OkResult()
        {
            ClinicMasterResponse clinicMasterResponse = new ClinicMasterResponse { Id = 1, IsError = false };
            ClinicMasterRequest clinicMasterRequest = new ClinicMasterRequest { Id = 1, ClinicCode = "K0012", ClinicName = "Test Clinic", DoctorName = "Test Doctor", Street1 = "", Street2 = "", Street3 = "", Street4 = "", City = "Test", PostCode = "41200", StateId = 1, PhoneNo = "", FaxNo = "", MobilePhoneNo = "", EmailId = "", IsObsolete = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATED, StatusCode = errorcode.STATUS_DATA_UPDATED, Result = clinicMasterResponse };
            _clinicMasterRepository.Setup(x => x.UpdateClinicMaster(clinicMasterRequest)).ReturnsAsync(clinicMasterResponse);

            //Arrange
            ClinicMasterController ClinicMasterController = new ClinicMasterController(_logger.Object, _config, _clinicMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await ClinicMasterController.UpdateClinicMaster(clinicMasterRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_UpdateClinicMaster_InvalidData_Returns_BadRequest()
        {
            ClinicMasterResponse clinicMasterResponse = new ClinicMasterResponse { Id = 0, IsError = true };
            ClinicMasterRequest clinicMasterRequest = new ClinicMasterRequest { Id = 1, ClinicCode = "", ClinicName = "", DoctorName = "Test Doctor", Street1 = "", Street2 = "", Street3 = "", Street4 = "", City = "Test", PostCode = "41200", StateId = 1, PhoneNo = "", FaxNo = "", MobilePhoneNo = "", EmailId = "", IsObsolete = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATE_FAIL, StatusCode = errorcode.STATUS_DATA_UPDATE_FAIL, Result = clinicMasterResponse };
            _clinicMasterRepository.Setup(x => x.UpdateClinicMaster(clinicMasterRequest)).ReturnsAsync(clinicMasterResponse);

            //Arrange
            ClinicMasterController ClinicMasterController = new ClinicMasterController(_logger.Object, _config, _clinicMasterRepository.Object, _cacheService.Object);
            ClinicMasterController.ModelState.AddModelError("ClinicCode", "Required");
            ClinicMasterController.ModelState.AddModelError("ClinicName", "Required");
            //Act
            var result = await ClinicMasterController.UpdateClinicMaster(clinicMasterRequest);
            var actualResult = result as BadRequestResult;
            //Asert
            Assert.NotNull(result);
            //Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }
        #endregion

        #region Delete Clinic Master Unit Test Cases
        [Fact]
        public async void Task_DeleteClinicMaster_ValidData_Returns_OkResult()
        {
            int Id = 1;
            ClinicMasterResponse clinicMasterResponse = new ClinicMasterResponse { Id = 1, IsError = false };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETED, StatusCode = errorcode.STATUS_DATA_DELETED, Result = clinicMasterResponse };
            _clinicMasterRepository.Setup(x => x.DeleteClinicMaster(Id)).ReturnsAsync(clinicMasterResponse);

            //Arrange
            ClinicMasterController ClinicMasterController = new ClinicMasterController(_logger.Object, _config, _clinicMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await ClinicMasterController.DeleteClinicMaster(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_DeleteClinicMaster_InvalidData_Returns_BadRequest()
        {
            int? Id = null;
            ClinicMasterResponse clinicMasterResponse = new ClinicMasterResponse { Id = 0, IsError = true };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETE_FAIL, StatusCode = errorcode.STATUS_DATA_DELETE_FAIL, Result = clinicMasterResponse };
            _clinicMasterRepository.Setup(x => x.DeleteClinicMaster(Convert.ToInt32(Id))).ReturnsAsync(clinicMasterResponse);

            //Arrange
            ClinicMasterController ClinicMasterController = new ClinicMasterController(_logger.Object, _config, _clinicMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await ClinicMasterController.DeleteClinicMaster(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Clinic Master By Id Unit Test Cases
        [Fact]
        public async void Task_GetClinicMasterById_Returns_OkResult()
        {
            int Id = 1;
            ClinicMasterResponse clinicMasterResponse = new ClinicMasterResponse { Id = 1, ClinicCode = "K0012", ClinicName = "Test Clinic" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = clinicMasterResponse };
            _clinicMasterRepository.Setup(x => x.GetClinicMasterById(Id)).ReturnsAsync(clinicMasterResponse);

            //Arrange
            ClinicMasterController ClinicMasterController = new ClinicMasterController(_logger.Object, _config, _clinicMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await ClinicMasterController.GetClinicMasterById(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetClinicMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            ClinicMasterResponse clinicMasterResponse = new ClinicMasterResponse { Id = 1, ClinicCode = "K0012", ClinicName = "Test Clinic" };
            DataResponse response = new DataResponse { Message = error.STATUS_INVALID_REQUEST, StatusCode = errorcode.STATUS_INVALID_REQUEST, Result = clinicMasterResponse };
            _clinicMasterRepository.Setup(x => x.GetClinicMasterById(Id)).ReturnsAsync(clinicMasterResponse);

            //Arrange
            ClinicMasterController ClinicMasterController = new ClinicMasterController(_logger.Object, _config, _clinicMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await ClinicMasterController.GetClinicMasterById(Id);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetClinicMasterById_Returns_NotFoundResult()
        {
            int Id = 1;
            ClinicMasterResponse clinicMasterResponse = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = clinicMasterResponse };
            _clinicMasterRepository.Setup(x => x.GetClinicMasterById(Id)).ReturnsAsync(clinicMasterResponse);
            //Arrange
            ClinicMasterController ClinicMasterController = new ClinicMasterController(_logger.Object, _config, _clinicMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await ClinicMasterController.GetClinicMasterById(Id);
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Clinic Master Unit Test Cases
        [Fact]
        public async void Task_GetClinicMaster_Returns_OkResult()
        {
            List<ClinicMasterResponse> data = new List<ClinicMasterResponse>() { new ClinicMasterResponse { Id = 1, ClinicCode = "K0012", ClinicName = "Test Clinic" } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _clinicMasterRepository.Setup(x => x.GetClinicMaster()).ReturnsAsync(data);

            //Arrange
            ClinicMasterController ClinicMasterController = new ClinicMasterController(_logger.Object, _config, _clinicMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await ClinicMasterController.GetClinicMaster();
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetClinicMaster_Returns_NotFoundResult()
        {
            int Id = 1;
            List<ClinicMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _clinicMasterRepository.Setup(x => x.GetClinicMaster()).ReturnsAsync(data);
            //Arrange
            ClinicMasterController ClinicMasterController = new ClinicMasterController(_logger.Object, _config, _clinicMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await ClinicMasterController.GetClinicMaster();
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

    }
}
