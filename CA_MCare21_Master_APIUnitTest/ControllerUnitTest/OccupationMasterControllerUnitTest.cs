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
   public class OccupationMasterControllerUnitTest
    {
        
            Mock<ILogger<OccupationMasterController>> _logger;
            IConfiguration _config;
            Mock<IOccupationMasterRepository> _occupationMasterRepository;
            Mock<ICacheService> _cacheService;
            ErrorStatus error = new ErrorStatus();
            ErrorStatusCode errorcode = new ErrorStatusCode();


            public OccupationMasterControllerUnitTest()
            {
                _logger = new Mock<ILogger<OccupationMasterController>>();
                _occupationMasterRepository = new Mock<IOccupationMasterRepository>();
                _cacheService = new Mock<ICacheService>();
            }


        #region occupation controller Unit test cases for GetOccupationMasterById
        [Fact]
        public async void Test_GetOccupationMasterById_OkResult()
        {
            int Id = 1;
            OccupationMasterResponse data = new OccupationMasterResponse { Id = 1, Description = "Test" };
            _occupationMasterRepository.Setup(x => x.GetOccupationMasterById(Id)).ReturnsAsync(data);
            OccupationMasterController OccupationMasterController = new OccupationMasterController(_logger.Object, _config, _occupationMasterRepository.Object, _cacheService.Object);
            var result = await OccupationMasterController.GetOccupationMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_GetOccupationMasterById_NotFoundResult()
        {
            int Id = 1;
            OccupationMasterResponse data = null; //new RoomMasterRequest { Id = 1, Description = "Test", RoomCode = "10", WardId = 1, GenderId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _occupationMasterRepository.Setup(x => x.GetOccupationMasterById(Id)).ReturnsAsync(data);
            OccupationMasterController OccupationMasterController = new OccupationMasterController(_logger.Object, _config, _occupationMasterRepository.Object, _cacheService.Object);
            var result = await OccupationMasterController.GetOccupationMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async void Task_GetMaritalStatusMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            OccupationMasterResponse occupationMasterResponse = new OccupationMasterResponse { Id = 1, Description = "CauseOfDeathMaster" };
            DataResponse response = new DataResponse { Message = error.STATUS_INVALID_REQUEST, StatusCode = errorcode.STATUS_INVALID_REQUEST, Result = occupationMasterResponse };
            _occupationMasterRepository.Setup(x => x.GetOccupationMasterById(Id)).ReturnsAsync(occupationMasterResponse);
            //Arrange
            OccupationMasterController occupationMasterController = new OccupationMasterController(_logger.Object, _config, _occupationMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await occupationMasterController.GetOccupationMasterById(Id);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region occupation controller Unit test cases for DeleteOccupationMaster
        [Fact]
        public async void Test_DeleteOccupationMaster_OkResult()
        {
            int Id = 1;
            OccupationMasterResponse data = new OccupationMasterResponse { Id = 1, IsError = false };
            _occupationMasterRepository.Setup(x => x.DeleteOccupationMaster(Id)).ReturnsAsync(data);
            OccupationMasterController OccupationMasterController = new OccupationMasterController(_logger.Object, _config, _occupationMasterRepository.Object, _cacheService.Object);
            var result = await OccupationMasterController.DeleteOccupationMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_DeleteOccupationMaster_BadRequestResult()
        {
            int Id = 1;
            OccupationMasterResponse data = new OccupationMasterResponse { Id = 1, IsError = true };
            _occupationMasterRepository.Setup(x => x.DeleteOccupationMaster(Id)).ReturnsAsync(data);
            OccupationMasterController occupationMasterController = new OccupationMasterController(_logger.Object, _config, _occupationMasterRepository.Object, _cacheService.Object);
            var result = await occupationMasterController.DeleteOccupationMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region occupation controller Unit test cases for AddOccupationMaster

        [Fact]
        public async void Test_AddOccupationMaster_BadRequestResult()
        {

            OccupationMasterResponse data = new OccupationMasterResponse { Id = 1, IsError = true };
            OccupationMasterRequest input = new OccupationMasterRequest() { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _occupationMasterRepository.Setup(x => x.AddOccupationMaster(input)).ReturnsAsync(data);
            OccupationMasterController OccupationMasterController = new OccupationMasterController(_logger.Object, _config, _occupationMasterRepository.Object, _cacheService.Object);
            var result = await OccupationMasterController.AddOccupationMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void Test_AddOccupationMaster_OkResult()
        {

            OccupationMasterResponse data = new OccupationMasterResponse { Id = 1, IsError = false };
            OccupationMasterRequest input = new OccupationMasterRequest() { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _occupationMasterRepository.Setup(x => x.AddOccupationMaster(input)).ReturnsAsync(data);
            OccupationMasterController OccupationMasterController = new OccupationMasterController(_logger.Object, _config, _occupationMasterRepository.Object, _cacheService.Object);
            var result = await OccupationMasterController.AddOccupationMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region occupation controller Unit test cases for UpdateOccupationMaster
        [Fact]
        public async void Test_UpdateOccupationMaster_OkResult()
        {

            OccupationMasterResponse data = new OccupationMasterResponse { Id = 1, IsError = false };
            OccupationMasterRequest input = new OccupationMasterRequest() { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _occupationMasterRepository.Setup(x => x.UpdateOccupationMaster(input)).ReturnsAsync(data);
            OccupationMasterController OccupationMasterController = new OccupationMasterController(_logger.Object, _config, _occupationMasterRepository.Object, _cacheService.Object);
            var result = await OccupationMasterController.UpdateOccupationMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_UpdateOccupationMaster_BadRequestResult()
        {

            OccupationMasterResponse data = new OccupationMasterResponse { Id = 1, IsError = true };
            OccupationMasterRequest input = new OccupationMasterRequest() { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" };
            _occupationMasterRepository.Setup(x => x.UpdateOccupationMaster(input)).ReturnsAsync(data);
            OccupationMasterController occupationMasterController = new OccupationMasterController(_logger.Object, _config, _occupationMasterRepository.Object, _cacheService.Object);
            var result = await occupationMasterController.UpdateOccupationMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region occupation controller Unit test cases for GetOccupationMaster

        [Fact]
        public async void Test_GetOccupationMaster_OkResult()
        {
            List<OccupationMasterResponse> data = new List<OccupationMasterResponse>() { new OccupationMasterResponse { Id = 1, Description = "Open" } };
            _occupationMasterRepository.Setup(x => x.GetOccupationMaster()).ReturnsAsync(data);
            OccupationMasterController occupationMasterController = new OccupationMasterController(_logger.Object, _config, _occupationMasterRepository.Object, _cacheService.Object);
            var result = await occupationMasterController.GetOccupationMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_GetOccupationMaster_NotFoundResult()
        {
            List<OccupationMasterResponse> data = null;
            _occupationMasterRepository.Setup(x => x.GetOccupationMaster()).ReturnsAsync(data);
            OccupationMasterController occupationMasterController = new OccupationMasterController(_logger.Object, _config, _occupationMasterRepository.Object, _cacheService.Object);
            var result = await occupationMasterController.GetOccupationMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        #endregion
    }
}
