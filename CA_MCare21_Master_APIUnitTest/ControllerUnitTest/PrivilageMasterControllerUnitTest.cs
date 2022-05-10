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
    public class PrivilageMasterControllerUnitTest
    {
        Mock<ILogger<PrivilageMasterController>> _logger;
        IConfiguration _config;
        Mock<IPrivilageMasterRepository> _privilageMasterRepository;
        Mock<ICacheService> _cacheService;
        ErrorStatus error = new ErrorStatus();
        ErrorStatusCode errorcode = new ErrorStatusCode();


        public PrivilageMasterControllerUnitTest()
        {
            _logger = new Mock<ILogger<PrivilageMasterController>>();
            _privilageMasterRepository = new Mock<IPrivilageMasterRepository>();
            _cacheService = new Mock<ICacheService>();
        }


        #region Add Privilage Master Unit Test Cases
        [Fact]
        public async void Task_AddPrivilageMaster_ValidData_Returns_OkResult()
        {
            PrivilageMasterResponse privilageMasterResponse = new PrivilageMasterResponse { Id = 1, IsError = false };
            PrivilageMasterRequest privilageMasterRequest = new PrivilageMasterRequest { Id = 1, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1, IsDiscountPolicy = true, PolicyId = 1, DisplayRemarksInBill = false, DisplayDiscountInBill = true, Remarks = "DISCOUNT FOR REGISTRATION FEE, MO AND PHARMACY", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADDED, StatusCode = errorcode.STATUS_DATA_ADDED, Result = privilageMasterResponse };
            _privilageMasterRepository.Setup(x => x.AddPrivilageMaster(privilageMasterRequest)).ReturnsAsync(privilageMasterResponse);

            //Arrange
            PrivilageMasterController PrivilageMasterController = new PrivilageMasterController(_logger.Object, _config, _privilageMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await PrivilageMasterController.AddPrivilageMaster(privilageMasterRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_AddPrivilageMaster_InvalidData_Returns_BadRequest()
        {
            PrivilageMasterResponse privilageMasterResponse = new PrivilageMasterResponse { Id = 1, IsError = true };
            PrivilageMasterRequest privilageMasterRequest = new PrivilageMasterRequest { Id = 1, PrivilageCode = "", Description = "", StartDate = DateTime.UtcNow, Duration = 1, IsDiscountPolicy = true, PolicyId = 1, DisplayRemarksInBill = false, DisplayDiscountInBill = true, Remarks = "DISCOUNT FOR REGISTRATION FEE, MO AND PHARMACY", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADD_FAIL, StatusCode = errorcode.STATUS_DATA_ADD_FAIL, Result = privilageMasterResponse };
            _privilageMasterRepository.Setup(x => x.AddPrivilageMaster(privilageMasterRequest)).ReturnsAsync(privilageMasterResponse);

            //Arrange
            PrivilageMasterController PrivilageMasterController = new PrivilageMasterController(_logger.Object, _config, _privilageMasterRepository.Object, _cacheService.Object);
            PrivilageMasterController.ModelState.AddModelError("Description", "Required");
            PrivilageMasterController.ModelState.AddModelError("PrivilageCode", "Required");
            //Act
            var result = await PrivilageMasterController.AddPrivilageMaster(privilageMasterRequest);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Update Privilage Master Unit Test Cases
        [Fact]
        public async void Task_UpdatePrivilageMaster_ValidData_Returns_OkResult()
        {
            PrivilageMasterResponse privilageMasterResponse = new PrivilageMasterResponse { Id = 1, IsError = false };
            PrivilageMasterRequest privilageMasterRequest = new PrivilageMasterRequest { Id = 1, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1, IsDiscountPolicy = true, PolicyId = 1, DisplayRemarksInBill = false, DisplayDiscountInBill = true, Remarks = "DISCOUNT FOR REGISTRATION FEE, MO AND PHARMACY", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATED, StatusCode = errorcode.STATUS_DATA_UPDATED, Result = privilageMasterResponse };
            _privilageMasterRepository.Setup(x => x.UpdatePrivilageMaster(privilageMasterRequest)).ReturnsAsync(privilageMasterResponse);

            //Arrange
            PrivilageMasterController PrivilageMasterController = new PrivilageMasterController(_logger.Object, _config, _privilageMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await PrivilageMasterController.UpdatePrivilageMaster(privilageMasterRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_UpdatePrivilageMaster_InvalidData_Returns_BadRequest()
        {
            PrivilageMasterResponse privilageMasterResponse = new PrivilageMasterResponse { Id = 1, IsError = true };
            PrivilageMasterRequest privilageMasterRequest = new PrivilageMasterRequest { Id = 1, PrivilageCode = "", Description = "", StartDate = DateTime.UtcNow, Duration = 1, IsDiscountPolicy = true, PolicyId = 1, DisplayRemarksInBill = false, DisplayDiscountInBill = true, Remarks = "DISCOUNT FOR REGISTRATION FEE, MO AND PHARMACY", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATE_FAIL, StatusCode = errorcode.STATUS_DATA_UPDATE_FAIL, Result = privilageMasterResponse };
            _privilageMasterRepository.Setup(x => x.UpdatePrivilageMaster(privilageMasterRequest)).ReturnsAsync(privilageMasterResponse);

            //Arrange
            PrivilageMasterController PrivilageMasterController = new PrivilageMasterController(_logger.Object, _config, _privilageMasterRepository.Object, _cacheService.Object);
            PrivilageMasterController.ModelState.AddModelError("Description", "Required");
            PrivilageMasterController.ModelState.AddModelError("PrivilageCode", "Required");
            //Act
            var result = await PrivilageMasterController.UpdatePrivilageMaster(privilageMasterRequest);
            var actualResult = result as BadRequestResult;
            //Asert
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }
        #endregion

        #region Delete Privilage Master Unit Test Cases
        [Fact]
        public async void Task_DeletePrivilageMaster_ValidData_Returns_OkResult()
        {
            int Id = 1;
            PrivilageMasterResponse privilageMasterResponse = new PrivilageMasterResponse { Id = 1, IsError = false };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETED, StatusCode = errorcode.STATUS_DATA_DELETED, Result = privilageMasterResponse };
            _privilageMasterRepository.Setup(x => x.DeletePrivilageMaster(Id)).ReturnsAsync(privilageMasterResponse);

            //Arrange
            PrivilageMasterController PrivilageMasterController = new PrivilageMasterController(_logger.Object, _config, _privilageMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await PrivilageMasterController.DeletePrivilageMaster(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_DeletePrivilageMaster_InvalidData_Returns_BadRequest()
        {
            int? Id = null;
            PrivilageMasterResponse privilageMasterResponse = new PrivilageMasterResponse { Id = 0, IsError = true };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETE_FAIL, StatusCode = errorcode.STATUS_DATA_DELETE_FAIL, Result = privilageMasterResponse };
            _privilageMasterRepository.Setup(x => x.DeletePrivilageMaster(Convert.ToInt32(Id))).ReturnsAsync(privilageMasterResponse);

            //Arrange
            PrivilageMasterController PrivilageMasterController = new PrivilageMasterController(_logger.Object, _config, _privilageMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await PrivilageMasterController.DeletePrivilageMaster(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Privilage Master By Id Unit Test Cases
        [Fact]
        public async void Task_GetPrivilageMasterById_Returns_OkResult()
        {
            int Id = 1;
            PrivilageMasterResponse privilageMasterResponse = new PrivilageMasterResponse { Id = 1, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1, IsDiscountPolicy = true, PolicyId = 1, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = privilageMasterResponse };
            _privilageMasterRepository.Setup(x => x.GetPrivilageMasterById(Id)).ReturnsAsync(privilageMasterResponse);

            //Arrange
            PrivilageMasterController PrivilageMasterController = new PrivilageMasterController(_logger.Object, _config, _privilageMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await PrivilageMasterController.GetPrivilageMasterById(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetPrivilageMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            PrivilageMasterResponse privilageMasterResponse = new PrivilageMasterResponse { Id = 1, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1, IsDiscountPolicy = true, PolicyId = 1, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_INVALID_REQUEST, StatusCode = errorcode.STATUS_INVALID_REQUEST, Result = privilageMasterResponse };
            _privilageMasterRepository.Setup(x => x.GetPrivilageMasterById(Id)).ReturnsAsync(privilageMasterResponse);

            //Arrange
            PrivilageMasterController PrivilageMasterController = new PrivilageMasterController(_logger.Object, _config, _privilageMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await PrivilageMasterController.GetPrivilageMasterById(Id);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetPrivilageMasterById_Returns_NotFoundResult()
        {
            int Id = 1;
            PrivilageMasterResponse privilageMasterResponse = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = privilageMasterResponse };
            _privilageMasterRepository.Setup(x => x.GetPrivilageMasterById(Id)).ReturnsAsync(privilageMasterResponse);
            //Arrange
            PrivilageMasterController PrivilageMasterController = new PrivilageMasterController(_logger.Object, _config, _privilageMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await PrivilageMasterController.GetPrivilageMasterById(Id);
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Privilage Master Unit Test Cases
        [Fact]
        public async void Task_GetPrivilageMaster_Returns_OkResult()
        {
            List<PrivilageMasterResponse> data = new List<PrivilageMasterResponse>() { new PrivilageMasterResponse { Id = 1, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1, IsDiscountPolicy = true, PolicyId = 1, FacilityCode = "KLAN" } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _privilageMasterRepository.Setup(x => x.GetPrivilageMaster()).ReturnsAsync(data);

            //Arrange
            PrivilageMasterController PrivilageMasterController = new PrivilageMasterController(_logger.Object, _config, _privilageMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await PrivilageMasterController.GetPrivilageMaster();
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetPrivilageMaster_Returns_NotFoundResult()
        {
            List<PrivilageMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _privilageMasterRepository.Setup(x => x.GetPrivilageMaster()).ReturnsAsync(data);
            //Arrange
            PrivilageMasterController PrivilageMasterController = new PrivilageMasterController(_logger.Object, _config, _privilageMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await PrivilageMasterController.GetPrivilageMaster();
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

    }
}
