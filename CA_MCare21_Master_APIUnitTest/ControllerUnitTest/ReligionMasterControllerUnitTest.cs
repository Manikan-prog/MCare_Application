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
    public class ReligionMasterControllerUnitTest
    {
        Mock<ILogger<ReligionMasterController>> _logger;
        IConfiguration _config;
        Mock<IReligionMasterRepository> _religionMasterRepository;
        Mock<ICacheService> _cacheService;
        ErrorStatus error = new ErrorStatus();
        ErrorStatusCode errorcode = new ErrorStatusCode();


        public ReligionMasterControllerUnitTest()
        {
            _logger = new Mock<ILogger<ReligionMasterController>>();
            _religionMasterRepository = new Mock<IReligionMasterRepository>();
            _cacheService = new Mock<ICacheService>();
        }

        #region Add Religion Master Unit Test Cases
        [Fact]
        public async void Task_AddReligionMaster_ValidData_Returns_OkResult()
        {
            ReligionMasterResponse religionMasterResponse = new ReligionMasterResponse { Id = 3, IsError = false };
            ReligionMasterRequest religionMasterRequest = new ReligionMasterRequest { Id = 3, Description = "CATHOLIC", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "CT", Smrpcode = "2", Smrpdescription = "Christianity", FacilityCode = "KLAN" };
            _religionMasterRepository.Setup(x => x.AddReligionMaster(religionMasterRequest)).ReturnsAsync(religionMasterResponse);

            //Arrange
            ReligionMasterController ReligionMasterController = new ReligionMasterController(_logger.Object, _config, _religionMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await ReligionMasterController.AddReligionMaster(religionMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_AddReligionMaster_InvalidData_Returns_BadRequestResult()
        {
            ReligionMasterResponse religionMasterResponse = new ReligionMasterResponse { Id = 0, IsError = true };
            ReligionMasterRequest religionMasterRequest = new ReligionMasterRequest { Id = 3, Description = "CATHOLIC", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "CT", Smrpcode = "2", Smrpdescription = "Christianity", FacilityCode = "KLAN" };
            _religionMasterRepository.Setup(x => x.AddReligionMaster(religionMasterRequest)).ReturnsAsync(religionMasterResponse);

            //Arrange
            ReligionMasterController ReligionMasterController = new ReligionMasterController(_logger.Object, _config, _religionMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await ReligionMasterController.AddReligionMaster(religionMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }        
        #endregion

        #region Update Religion Master Unit Test Cases
        [Fact]
        public async void Task_UpdateReligionMaster_ValidData_Returns_OkResult()
        {
            ReligionMasterResponse religionMasterResponse = new ReligionMasterResponse { Id = 3, IsError = false };
            ReligionMasterRequest religionMasterRequest = new ReligionMasterRequest { Id = 3, Description = "CATHOLIC", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "CT", Smrpcode = "2", Smrpdescription = "Christianity", FacilityCode = "KLAN" };
            _religionMasterRepository.Setup(x => x.UpdateReligionMaster(religionMasterRequest)).ReturnsAsync(religionMasterResponse);

            //Arrange
            ReligionMasterController ReligionMasterController = new ReligionMasterController(_logger.Object, _config, _religionMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await ReligionMasterController.UpdateReligionMaster(religionMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_UpdateReligionMaster_InvalidData_Returns_BadRequestResult()
        {
            ReligionMasterResponse religionMasterResponse = new ReligionMasterResponse { Id = 0, IsError = true };
            ReligionMasterRequest religionMasterRequest = new ReligionMasterRequest { Id = 3, Description = "CATHOLIC", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "CT", Smrpcode = "2", Smrpdescription = "Christianity", FacilityCode = "KLAN" };
            _religionMasterRepository.Setup(x => x.UpdateReligionMaster(religionMasterRequest)).ReturnsAsync(religionMasterResponse);

            //Arrange
            ReligionMasterController ReligionMasterController = new ReligionMasterController(_logger.Object, _config, _religionMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await ReligionMasterController.UpdateReligionMaster(religionMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region Delete Religion Master Unit Test Cases
        [Fact]
        public async void Task_DeleteReligionMaster_ValidData_Returns_OkResult()
        {
            int Id = 1;
            ReligionMasterResponse religion = new ReligionMasterResponse { Id = 1, IsError = false };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETED, StatusCode = errorcode.STATUS_DATA_DELETED, Result = religion };
            _religionMasterRepository.Setup(x => x.DeleteReligionMaster(Id)).ReturnsAsync(religion);

            //Arrange
            ReligionMasterController ReligionMasterController = new ReligionMasterController(_logger.Object, _config, _religionMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await ReligionMasterController.DeleteReligionMaster(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_DeleteReligionMaster_InvalidData_Returns_BadRequest()
        {
            int? Id = null;
            ReligionMasterResponse religion = new ReligionMasterResponse { Id = 0, IsError = true };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETE_FAIL, StatusCode = errorcode.STATUS_DATA_DELETE_FAIL, Result = religion };
            _religionMasterRepository.Setup(x => x.DeleteReligionMaster(Convert.ToInt32(Id))).ReturnsAsync(religion);

            //Arrange
            ReligionMasterController ReligionMasterController = new ReligionMasterController(_logger.Object, _config, _religionMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await ReligionMasterController.DeleteReligionMaster(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Religion Master By Id Unit Test Cases
        [Fact]
        public async void Task_GetReligionMasterById_Returns_OkResult()
        {
            int Id = 3;
            ReligionMasterResponse data = new ReligionMasterResponse { Id = 3, Description = "CATHOLIC", Code = "CT", SmrpCode = "2", SmrpDescription = "Christianity" };
            _religionMasterRepository.Setup(x => x.GetReligionMasterById(Id)).ReturnsAsync(data);
            ReligionMasterController ReligionMasterController = new ReligionMasterController(_logger.Object, _config, _religionMasterRepository.Object, _cacheService.Object);
            var result = await ReligionMasterController.GetReligionMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_GetReligionMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            ReligionMasterResponse religion = new ReligionMasterResponse { Id = 3, Description = "CATHOLIC", Code = "CT", SmrpCode = "2", SmrpDescription = "Christianity" };
            DataResponse response = new DataResponse { Message = error.STATUS_INVALID_REQUEST, StatusCode = errorcode.STATUS_INVALID_REQUEST, Result = religion };
            _religionMasterRepository.Setup(x => x.GetReligionMasterById(Id)).ReturnsAsync(religion);

            //Arrange
            ReligionMasterController ReligionMasterController = new ReligionMasterController(_logger.Object, _config, _religionMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await ReligionMasterController.GetReligionMasterById(Id);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetReligionMasterById_Returns_NotFoundResult()
        {
            int Id = 1;
            ReligionMasterResponse religion = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = religion };
            _religionMasterRepository.Setup(x => x.GetReligionMasterById(Id)).ReturnsAsync(religion);
            //Arrange
            ReligionMasterController ReligionMasterController = new ReligionMasterController(_logger.Object, _config, _religionMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await ReligionMasterController.GetReligionMasterById(Id);
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Religion Master Unit Test Cases
        [Fact]
        public async void Task_GetReligionMaster_Returns_OkResult()
        {
            List<ReligionMasterResponse> data = new List<ReligionMasterResponse>() { new ReligionMasterResponse { Id = 3, Description = "CATHOLIC", Code = "CT", SmrpCode = "2", SmrpDescription = "Christianity" } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _religionMasterRepository.Setup(x => x.GetReligionMaster()).ReturnsAsync(data);

            //Arrange
            ReligionMasterController ReligionMasterController = new ReligionMasterController(_logger.Object, _config, _religionMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await ReligionMasterController.GetReligionMaster();
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetIdentificationMaster_Returns_NotFoundResult()
        {
            List<ReligionMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _religionMasterRepository.Setup(x => x.GetReligionMaster()).ReturnsAsync(data);
            //Arrange
            ReligionMasterController ReligionMasterController = new ReligionMasterController(_logger.Object, _config, _religionMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await ReligionMasterController.GetReligionMaster();
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

    }
}
