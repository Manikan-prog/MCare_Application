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
   public class BedTypeMasterControllerUnitTest
    {
        Mock<ILogger<BedTypeMasterController>> _logger;
        IConfiguration _config;
        Mock<IBedTypeMasterRepository> _BedTypeMasterRepository;
        Mock<ICacheService> _cacheService;
        ErrorStatus error = new ErrorStatus();
        ErrorStatusCode errorcode = new ErrorStatusCode();


        public BedTypeMasterControllerUnitTest()
        {
            _logger = new Mock<ILogger<BedTypeMasterController>>();
            _BedTypeMasterRepository = new Mock<IBedTypeMasterRepository>();
            _cacheService = new Mock<ICacheService>();
        }

        #region Add Bed Type Master Unit Test Cases
        [Fact]
        public async void Task_AddBedTypeMaster_ValidData_Returns_OkResult()
        {
            BedTypeMasterResponse bedTypeMasterResponse = new BedTypeMasterResponse { Id = 1, IsError = false };
            BedTypeMasterRequest bedTypeMasterRequest = new BedTypeMasterRequest { Id = 1, Description = "TestBed", BedTypeCode = "1T", MaximumBed = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADDED, StatusCode = errorcode.STATUS_DATA_ADDED, Result = bedTypeMasterResponse };
            _BedTypeMasterRepository.Setup(x => x.AddBedTypeMaster(bedTypeMasterRequest)).ReturnsAsync(bedTypeMasterResponse);

            //Arrange
            BedTypeMasterController BedTypeMasterController = new BedTypeMasterController(_logger.Object, _config, _BedTypeMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await BedTypeMasterController.AddBedTypeMaster(bedTypeMasterRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_AddBedTypeMaster_InvalidData_Returns_BadRequest()
        {
            BedTypeMasterResponse bedTypeMasterResponse = new BedTypeMasterResponse { Id = 0, IsError = true };
            BedTypeMasterRequest bedTypeMasterRequest = new BedTypeMasterRequest { Id = 1, Description = "", BedTypeCode = "", MaximumBed = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADD_FAIL, StatusCode = errorcode.STATUS_DATA_ADD_FAIL, Result = bedTypeMasterResponse };
            _BedTypeMasterRepository.Setup(x => x.AddBedTypeMaster(bedTypeMasterRequest)).ReturnsAsync(bedTypeMasterResponse);

            //Arrange
            BedTypeMasterController BedTypeMasterController = new BedTypeMasterController(_logger.Object, _config, _BedTypeMasterRepository.Object, _cacheService.Object);
            BedTypeMasterController.ModelState.AddModelError("Description", "Required");
            BedTypeMasterController.ModelState.AddModelError("BedTypeCode", "Required");
            //Act
            var result = await BedTypeMasterController.AddBedTypeMaster(bedTypeMasterRequest);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Update Bed Type Master Unit Test Cases
        [Fact]
        public async void Task_UpdateBedTypeMaster_ValidData_Returns_OkResult()
        {
            BedTypeMasterResponse bedTypeMasterResponse = new BedTypeMasterResponse { Id = 1, IsError = false };
            BedTypeMasterRequest bedTypeMasterRequest = new BedTypeMasterRequest { Id = 1, Description = "TestBed", BedTypeCode = "1T", MaximumBed = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 1, UpdatedDate = DateTime.UtcNow, FacilityCode = "KLAN" };
            _BedTypeMasterRepository.Setup(x => x.UpdateBedTypeMaster(bedTypeMasterRequest)).ReturnsAsync(bedTypeMasterResponse);

            //Arrange
            BedTypeMasterController BedTypeMasterController = new BedTypeMasterController(_logger.Object, _config, _BedTypeMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await BedTypeMasterController.UpdateBedTypeMaster(bedTypeMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_UpdateBedTypeMaster_InvalidData_Returns_BadRequest()
        {
            BedTypeMasterResponse bedTypeMasterResponse = new BedTypeMasterResponse { Id = 0, IsError = true };
            BedTypeMasterRequest bedTypeMasterRequest = new BedTypeMasterRequest { Id = 1, Description = "", BedTypeCode = "", MaximumBed = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATE_FAIL, StatusCode = errorcode.STATUS_DATA_UPDATE_FAIL, Result = bedTypeMasterResponse };
            _BedTypeMasterRepository.Setup(x => x.UpdateBedTypeMaster(bedTypeMasterRequest)).ReturnsAsync(bedTypeMasterResponse);

            //Arrange
            BedTypeMasterController BedTypeMasterController = new BedTypeMasterController(_logger.Object, _config, _BedTypeMasterRepository.Object, _cacheService.Object);
            BedTypeMasterController.ModelState.AddModelError("Description", "Required");
            BedTypeMasterController.ModelState.AddModelError("BedTypeCode", "Required");
            //Act
            var result = await BedTypeMasterController.UpdateBedTypeMaster(bedTypeMasterRequest);
            var actualResult = result as BadRequestResult;
            //Asert
            Assert.NotNull(result);
            //Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }
        #endregion

        #region Delete Bed Type Master Unit Test Cases
        [Fact]
        public async void Task_DeleteBedTypeMaster_ValidData_Returns_OkResult()
        {
            int Id = 1;
            BedTypeMasterResponse bedTypeMasterResponse = new BedTypeMasterResponse { Id = 1, IsError = false };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETED, StatusCode = errorcode.STATUS_DATA_DELETED, Result = bedTypeMasterResponse };
            _BedTypeMasterRepository.Setup(x => x.DeleteBedTypeMaster(Id)).ReturnsAsync(bedTypeMasterResponse);

            //Arrange
            BedTypeMasterController BedTypeMasterController = new BedTypeMasterController(_logger.Object, _config, _BedTypeMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await BedTypeMasterController.DeleteBedTypeMaster(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_DeleteBedTypeMaster_InvalidData_Returns_BadRequest()
        {
            int? Id = null;
            BedTypeMasterResponse bedTypeMasterResponse = new BedTypeMasterResponse { Id = 0, IsError = true };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETE_FAIL, StatusCode = errorcode.STATUS_DATA_DELETE_FAIL, Result = bedTypeMasterResponse };
            _BedTypeMasterRepository.Setup(x => x.DeleteBedTypeMaster(Convert.ToInt32(Id))).ReturnsAsync(bedTypeMasterResponse);

            //Arrange
            BedTypeMasterController BedTypeMasterController = new BedTypeMasterController(_logger.Object, _config, _BedTypeMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await BedTypeMasterController.DeleteBedTypeMaster(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Bed Type Master By Id Unit Test Cases
        [Fact]
        public async void Task_GetBedTypeMasterById_Returns_OkResult()
        {
            int Id = 1;
            BedTypeMasterResponse bedTypeMasterResponse = new BedTypeMasterResponse { Id = 1, Description = "TestBed", BedTypeCode = "1T", MaximumBed = 1, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = bedTypeMasterResponse };
            _BedTypeMasterRepository.Setup(x => x.GetBedTypeMasterById(Id)).ReturnsAsync(bedTypeMasterResponse);

            //Arrange
            BedTypeMasterController BedTypeMasterController = new BedTypeMasterController(_logger.Object, _config, _BedTypeMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await BedTypeMasterController.GetBedTypeMasterById(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetBedTypeMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            BedTypeMasterResponse bedTypeMasterResponse = new BedTypeMasterResponse { Id = 1, Description = "TestBed", BedTypeCode = "1T", MaximumBed = 1, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_INVALID_REQUEST, StatusCode = errorcode.STATUS_INVALID_REQUEST, Result = bedTypeMasterResponse };
            _BedTypeMasterRepository.Setup(x => x.GetBedTypeMasterById(Id)).ReturnsAsync(bedTypeMasterResponse);
            //Arrange
            BedTypeMasterController BedTypeMasterController = new BedTypeMasterController(_logger.Object, _config, _BedTypeMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await BedTypeMasterController.GetBedTypeMasterById(Id);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetBedTypeMasterById_Returns_NotFoundResult()
        {
            int Id = 1;
            BedTypeMasterResponse bedTypeMasterResponse = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = bedTypeMasterResponse };
            _BedTypeMasterRepository.Setup(x => x.GetBedTypeMasterById(Id)).ReturnsAsync(bedTypeMasterResponse);
            //Arrange
            BedTypeMasterController BedTypeMasterController = new BedTypeMasterController(_logger.Object, _config, _BedTypeMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await BedTypeMasterController.GetBedTypeMasterById(Id);
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Bed Type Master Unit Test Cases
        [Fact]
        public async void Task_GetBedTypeMaster_Returns_OkResult()
        {
            List<BedTypeMasterResponse> data = new List<BedTypeMasterResponse>() { new BedTypeMasterResponse { Id = 1, Description = "TestBed", BedTypeCode = "1T", MaximumBed = 1, FacilityCode = "KLAN" } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _BedTypeMasterRepository.Setup(x => x.GetBedTypeMaster()).ReturnsAsync(data);

            //Arrange
            BedTypeMasterController BedTypeMasterController = new BedTypeMasterController(_logger.Object, _config, _BedTypeMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await BedTypeMasterController.GetBedTypeMaster();
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetBedTypeMaster_Returns_NotFoundResult()
        {
            List<BedTypeMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _BedTypeMasterRepository.Setup(x => x.GetBedTypeMaster()).ReturnsAsync(data);
            //Arrange
            BedTypeMasterController BedTypeMasterController = new BedTypeMasterController(_logger.Object, _config, _BedTypeMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await BedTypeMasterController.GetBedTypeMaster();
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

    }
}
