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
    public class RaceMasterControllerUnitTest
    {
        Mock<ILogger<RaceMasterController>> _logger;
        IConfiguration _config;
        Mock<IRaceMasterRepository> _raceMasterRepository;
        Mock<ICacheService> _cacheService;
        ErrorStatus error = new ErrorStatus();
        ErrorStatusCode errorcode = new ErrorStatusCode();


        public RaceMasterControllerUnitTest()
        {
            _logger = new Mock<ILogger<RaceMasterController>>();
            _raceMasterRepository = new Mock<IRaceMasterRepository>();
            _cacheService = new Mock<ICacheService>();
        }

        #region Add Race Master Unit Test Cases
        [Fact]
        public async void Task_AddRaceMaster_ValidData_Returns_OkResult()
        {
            RaceMasterResponse raceMasterResponse = new RaceMasterResponse { Id = 3, IsError = false };
            RaceMasterRequest raceMasterRequest = new RaceMasterRequest { Id = 1, Description = "MALAY", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 409, UpdatedDate = null, Code = "ML", Smrpcode = "01", Smrpdescription = "Melayu", FacilityCode = "KLAN" };
            _raceMasterRepository.Setup(x => x.AddRaceMaster(raceMasterRequest)).ReturnsAsync(raceMasterResponse);

            //Arrange
            RaceMasterController RaceMasterController = new RaceMasterController(_logger.Object, _config, _raceMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await RaceMasterController.AddRaceMaster(raceMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_AddRaceMaster_InvalidData_Returns_BadRequestResult()
        {
            RaceMasterResponse raceMasterResponse = new RaceMasterResponse { Id = 0, IsError = true };
            RaceMasterRequest raceMasterRequest = new RaceMasterRequest { Id = 1, Description = "MALAY", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 409, UpdatedDate = null, Code = "ML", Smrpcode = "01", Smrpdescription = "Melayu", FacilityCode = "KLAN" };
            _raceMasterRepository.Setup(x => x.AddRaceMaster(raceMasterRequest)).ReturnsAsync(raceMasterResponse);

            //Arrange
            RaceMasterController RaceMasterController = new RaceMasterController(_logger.Object, _config, _raceMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await RaceMasterController.AddRaceMaster(raceMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }        
        #endregion

        #region Update Race Master Unit Test Cases
        [Fact]
        public async void Task_UpdateRaceMaster_ValidData_Returns_OkResult()
        {
            RaceMasterResponse raceMasterResponse = new RaceMasterResponse { Id = 1, IsError = false };
            RaceMasterRequest raceMasterRequest = new RaceMasterRequest { Id = 1, Description = "MALAY", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 409, UpdatedDate = null, Code = "ML", Smrpcode = "01", Smrpdescription = "Melayu", FacilityCode = "KLAN" };
            _raceMasterRepository.Setup(x => x.UpdateRaceMaster(raceMasterRequest)).ReturnsAsync(raceMasterResponse);

            //Arrange
            RaceMasterController RaceMasterController = new RaceMasterController(_logger.Object, _config, _raceMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await RaceMasterController.UpdateRaceMaster(raceMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_UpdateRaceMaster_InvalidData_Returns_BadRequestResult()
        {
            RaceMasterResponse raceMasterResponse = new RaceMasterResponse { Id = 0, IsError = true };
            RaceMasterRequest raceMasterRequest = new RaceMasterRequest { Id = 1, Description = "MALAY", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 409, UpdatedDate = null, Code = "ML", Smrpcode = "01", Smrpdescription = "Melayu", FacilityCode = "KLAN" };
            _raceMasterRepository.Setup(x => x.UpdateRaceMaster(raceMasterRequest)).ReturnsAsync(raceMasterResponse);

            //Arrange
            RaceMasterController RaceMasterController = new RaceMasterController(_logger.Object, _config, _raceMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await RaceMasterController.UpdateRaceMaster(raceMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region Delete Race Master Unit Test Cases
        [Fact]
        public async void Task_DeleteRaceMaster_ValidData_Returns_OkResult()
        {
            int Id = 1;
            RaceMasterResponse race = new RaceMasterResponse { Id = 1, IsError = false };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETED, StatusCode = errorcode.STATUS_DATA_DELETED, Result = race };
            _raceMasterRepository.Setup(x => x.DeleteRaceMaster(Id)).ReturnsAsync(race);

            //Arrange
            RaceMasterController RaceMasterController = new RaceMasterController(_logger.Object, _config, _raceMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await RaceMasterController.DeleteRaceMaster(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_DeleteRaceMaster_InvalidData_Returns_BadRequest()
        {
            int? Id = null;
            RaceMasterResponse race = new RaceMasterResponse { Id = 0, IsError = true };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETE_FAIL, StatusCode = errorcode.STATUS_DATA_DELETE_FAIL, Result = race };
            _raceMasterRepository.Setup(x => x.DeleteRaceMaster(Convert.ToInt32(Id))).ReturnsAsync(race);

            //Arrange
            RaceMasterController RaceMasterController = new RaceMasterController(_logger.Object, _config, _raceMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await RaceMasterController.DeleteRaceMaster(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Race Master By Id Unit Test Cases
        [Fact]
        public async void Task_GetRaceMasterById_Returns_OkResult()
        {
            int Id = 1;
            RaceMasterResponse data = new RaceMasterResponse { Id = 1, Description = "MALAY", Code = "ML", Smrpcode = "01", Smrpdescription = "Melayu" };
            _raceMasterRepository.Setup(x => x.GetRaceMasterById(Id)).ReturnsAsync(data);
            RaceMasterController RaceMasterController = new RaceMasterController(_logger.Object, _config, _raceMasterRepository.Object, _cacheService.Object);
            var result = await RaceMasterController.GetRaceMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_GetRaceMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            RaceMasterResponse race = new RaceMasterResponse { Id = 1, Description = "MALAY", Code = "ML", Smrpcode = "01", Smrpdescription = "Melayu" };
            DataResponse response = new DataResponse { Message = error.STATUS_INVALID_REQUEST, StatusCode = errorcode.STATUS_INVALID_REQUEST, Result = race };
            _raceMasterRepository.Setup(x => x.GetRaceMasterById(Id)).ReturnsAsync(race);

            //Arrange
            RaceMasterController RaceMasterController = new RaceMasterController(_logger.Object, _config, _raceMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await RaceMasterController.GetRaceMasterById(Id);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetRaceMasterById_Returns_NotFoundResult()
        {
            int Id = 1;
            RaceMasterResponse race = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = race };
            _raceMasterRepository.Setup(x => x.GetRaceMasterById(Id)).ReturnsAsync(race);
            //Arrange
            RaceMasterController RaceMasterController = new RaceMasterController(_logger.Object, _config, _raceMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await RaceMasterController.GetRaceMasterById(Id);
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Race Master Unit Test Cases
        [Fact]
        public async void Task_GetRaceMaster_Returns_OkResult()
        {
            List<RaceMasterResponse> data = new List<RaceMasterResponse>() { new RaceMasterResponse { Id = 1, Description = "MALAY", Code = "ML", Smrpcode = "01", Smrpdescription = "Melayu" } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _raceMasterRepository.Setup(x => x.GetRaceMaster()).ReturnsAsync(data);

            //Arrange
            RaceMasterController RaceMasterController = new RaceMasterController(_logger.Object, _config, _raceMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await RaceMasterController.GetRaceMaster();
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetRaceMaster_Returns_NotFoundResult()
        {
            List<RaceMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _raceMasterRepository.Setup(x => x.GetRaceMaster()).ReturnsAsync(data);
            //Arrange
            RaceMasterController RaceMasterController = new RaceMasterController(_logger.Object, _config, _raceMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await RaceMasterController.GetRaceMaster();
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

    }
}
