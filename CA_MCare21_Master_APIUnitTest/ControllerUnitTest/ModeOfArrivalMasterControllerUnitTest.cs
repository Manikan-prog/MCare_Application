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
   public class ModeOfArrivalMasterControllerUnitTest
    {
        
            Mock<ILogger<ModeOfArrivalMasterController>> _logger;
            IConfiguration _config;
            Mock<IModeOfArrivalMasterRepository> _modeOfArrivalMasterRepository;
            Mock<ICacheService> _cacheService;
            ErrorStatus error = new ErrorStatus();
            ErrorStatusCode errorcode = new ErrorStatusCode();


            public ModeOfArrivalMasterControllerUnitTest()
            {
                _logger = new Mock<ILogger<ModeOfArrivalMasterController>>();
                _modeOfArrivalMasterRepository = new Mock<IModeOfArrivalMasterRepository>();
                _cacheService = new Mock<ICacheService>();
            }

        #region Mode Of Arrival controller Unit test cases for GetModeOfArrivalMasterById

        [Fact]
        public async void Test_GetModeOfArrivalMasterById_OkResult()
        {
            int Id = 1;
            ModeOfArrivalMasterResponse data = new ModeOfArrivalMasterResponse { Id = 1, Description = "test" };
            _modeOfArrivalMasterRepository.Setup(x => x.GetModeOfArrivalMasterById(Id)).ReturnsAsync(data);
            ModeOfArrivalMasterController ModeOfArrivalMasterController = new ModeOfArrivalMasterController(_logger.Object, _config, _modeOfArrivalMasterRepository.Object, _cacheService.Object);
            var result = await ModeOfArrivalMasterController.GetModeOfArrivalMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_GetModeOfArrivalMasterById_NotFoundResult()
        {
            int Id = 1;
            ModeOfArrivalMasterResponse data = null;
            _modeOfArrivalMasterRepository.Setup(x => x.GetModeOfArrivalMasterById(Id)).ReturnsAsync(data);
            ModeOfArrivalMasterController ModeOfArrivalMasterController = new ModeOfArrivalMasterController(_logger.Object, _config, _modeOfArrivalMasterRepository.Object, _cacheService.Object);
            var result = await ModeOfArrivalMasterController.GetModeOfArrivalMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async void Task_GetModeOfArrivalMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            ModeOfArrivalMasterResponse modeOfArrivalMasterResponse = new ModeOfArrivalMasterResponse { Id = 1, Description = "CauseOfDeathMaster" };
            DataResponse response = new DataResponse { Message = error.STATUS_INVALID_REQUEST, StatusCode = errorcode.STATUS_INVALID_REQUEST, Result = modeOfArrivalMasterResponse };
            _modeOfArrivalMasterRepository.Setup(x => x.GetModeOfArrivalMasterById(Id)).ReturnsAsync(modeOfArrivalMasterResponse);
            //Arrange
            ModeOfArrivalMasterController modeOfArrivalMasterController = new ModeOfArrivalMasterController(_logger.Object, _config, _modeOfArrivalMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await modeOfArrivalMasterController.GetModeOfArrivalMasterById(Id);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Mode Of Arrival controller Unit test cases for DeleteModeOfArrivalMaster


        [Fact]
        public async void Test_DeleteModeOfArrivalMaster_OkResult()
        {
            int Id = 1;
            ModeOfArrivalMasterResponse data = new ModeOfArrivalMasterResponse { Id = 1, IsError = false };
            _modeOfArrivalMasterRepository.Setup(x => x.DeleteModeOfArrivalMaster(Id)).ReturnsAsync(data);
            ModeOfArrivalMasterController ModeOfArrivalMasterController = new ModeOfArrivalMasterController(_logger.Object, _config, _modeOfArrivalMasterRepository.Object, _cacheService.Object);
            var result = await ModeOfArrivalMasterController.DeleteModeOfArrivalMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_DeleteModeOfArrivalMaster_BadRequestResult()
        {
            int Id = 1;
            ModeOfArrivalMasterResponse data = new ModeOfArrivalMasterResponse { Id = 1, IsError = true };
            _modeOfArrivalMasterRepository.Setup(x => x.DeleteModeOfArrivalMaster(Id)).ReturnsAsync(data);
            ModeOfArrivalMasterController ModeOfArrivalMasterController = new ModeOfArrivalMasterController(_logger.Object, _config, _modeOfArrivalMasterRepository.Object, _cacheService.Object);
            var result = await ModeOfArrivalMasterController.DeleteModeOfArrivalMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region Mode Of Arrival controller Unit test cases for AddModeOfArrivalMaster

        [Fact]
        public async void Test_AddModeOfArrivalMaster_BadRequestResult()
        {

            ModeOfArrivalMasterResponse data = new ModeOfArrivalMasterResponse { Id = 1, IsError = true };
            ModeOfArrivalMasterRequest input = new ModeOfArrivalMasterRequest() { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" };
            _modeOfArrivalMasterRepository.Setup(x => x.AddModeOfArrivalMaster(input)).ReturnsAsync(data);
            ModeOfArrivalMasterController ModeOfArrivalMasterController = new ModeOfArrivalMasterController(_logger.Object, _config, _modeOfArrivalMasterRepository.Object, _cacheService.Object);
            var result = await ModeOfArrivalMasterController.AddModeOfArrivalMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void Test_AddModeOfArrivalMaster_OkResult()
        {

            ModeOfArrivalMasterResponse data = new ModeOfArrivalMasterResponse { Id = 1, IsError = false };
            ModeOfArrivalMasterRequest input = new ModeOfArrivalMasterRequest() { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" };
            _modeOfArrivalMasterRepository.Setup(x => x.AddModeOfArrivalMaster(input)).ReturnsAsync(data);
            ModeOfArrivalMasterController ModeOfArrivalMasterController = new ModeOfArrivalMasterController(_logger.Object, _config, _modeOfArrivalMasterRepository.Object, _cacheService.Object);
            var result = await ModeOfArrivalMasterController.AddModeOfArrivalMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region Mode Of Arrival controller Unit test cases for UpdateModeOfArrivalMaster

        [Fact]
        public async void Test_UpdateModeOfArrivalMaster_OkResult()
        {

            ModeOfArrivalMasterResponse data = new ModeOfArrivalMasterResponse { Id = 1, IsError = false };
            ModeOfArrivalMasterRequest input = new ModeOfArrivalMasterRequest() { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" };
            _modeOfArrivalMasterRepository.Setup(x => x.UpdateModeOfArrivalMaster(input)).ReturnsAsync(data);
            ModeOfArrivalMasterController ModeOfArrivalMasterController = new ModeOfArrivalMasterController(_logger.Object, _config, _modeOfArrivalMasterRepository.Object, _cacheService.Object);
            var result = await ModeOfArrivalMasterController.UpdateModeOfArrivalMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_UpdateModeOfArrivalMaster_BadRequestResult()
        {

            ModeOfArrivalMasterResponse data = new ModeOfArrivalMasterResponse { Id = 1, IsError = true };
            ModeOfArrivalMasterRequest input = new ModeOfArrivalMasterRequest() { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" };
            _modeOfArrivalMasterRepository.Setup(x => x.UpdateModeOfArrivalMaster(input)).ReturnsAsync(data);
            ModeOfArrivalMasterController ModeOfArrivalMasterController = new ModeOfArrivalMasterController(_logger.Object, _config, _modeOfArrivalMasterRepository.Object, _cacheService.Object);
            var result = await ModeOfArrivalMasterController.UpdateModeOfArrivalMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region Mode Of Arrival controller Unit test cases for GetModeOfArrivalMaster

        [Fact]
        public async void Test_GetModeOfArrivalMaster_OkResult()
        {
            List<ModeOfArrivalMasterResponse> data = new List<ModeOfArrivalMasterResponse>() { new ModeOfArrivalMasterResponse { Id = 1, Description = "Open" } };
            _modeOfArrivalMasterRepository.Setup(x => x.GetModeOfArrivalMaster()).ReturnsAsync(data);
            ModeOfArrivalMasterController modeOfArrivalMasterController = new ModeOfArrivalMasterController(_logger.Object, _config, _modeOfArrivalMasterRepository.Object, _cacheService.Object);
            var result = await modeOfArrivalMasterController.GetModeOfArrivalMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_GetModeOfArrivalMaster_NotFoundResult()
        {
            List<ModeOfArrivalMasterResponse> data = null;
            _modeOfArrivalMasterRepository.Setup(x => x.GetModeOfArrivalMaster()).ReturnsAsync(data);
            ModeOfArrivalMasterController modeOfArrivalMasterController = new ModeOfArrivalMasterController(_logger.Object, _config, _modeOfArrivalMasterRepository.Object, _cacheService.Object);
            var result = await modeOfArrivalMasterController.GetModeOfArrivalMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        #endregion
    }
}

