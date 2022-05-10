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
  public  class RoomMasterControllerUnitTest
    {
        
            Mock<ILogger<RoomMasterController>> _logger;
            IConfiguration _config;
            Mock<IRoomMasterRepository> _roomMasterRepository;
            Mock<ICacheService> _cacheService;
            ErrorStatus error = new ErrorStatus();
            ErrorStatusCode errorcode = new ErrorStatusCode();


            public RoomMasterControllerUnitTest()
            {
                _logger = new Mock<ILogger<RoomMasterController>>();
               _roomMasterRepository = new Mock<IRoomMasterRepository>();
                _cacheService = new Mock<ICacheService>();
            }

        #region RoomMaster controller Unit test cases for GetRoomMasterById
        [Fact]
        public async void Test_GetRoomMasterById_OkResult()
        {
            int Id = 1;
            RoomMasterResponse data = new RoomMasterResponse { Id = 1, Description = "Test", RoomCode = "10", WardId = 1 };
            _roomMasterRepository.Setup(x => x.GetRoomMasterById(Id)).ReturnsAsync(data);
            RoomMasterController RoomMasterController = new RoomMasterController(_logger.Object, _config, _roomMasterRepository.Object, _cacheService.Object);
            var result = await RoomMasterController.GetRoomMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_GetRoomMasterById_NotFoundResult()
        {
            int Id = 1;
            RoomMasterResponse data = null; //new RoomMasterRequest { Id = 1, Description = "Test", RoomCode = "10", WardId = 1, GenderId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _roomMasterRepository.Setup(x => x.GetRoomMasterById(Id)).ReturnsAsync(data);
            RoomMasterController RoomMasterController = new RoomMasterController(_logger.Object, _config, _roomMasterRepository.Object, _cacheService.Object);
            var result = await RoomMasterController.GetRoomMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async void Task_GetRoomMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            RoomMasterResponse roomMasterResponse = new RoomMasterResponse { Id = 1, Description= "RoomMaster" };
            DataResponse response = new DataResponse { Message = error.STATUS_INVALID_REQUEST, StatusCode = errorcode.STATUS_INVALID_REQUEST, Result = roomMasterResponse };
            _roomMasterRepository.Setup(x => x.GetRoomMasterById(Id)).ReturnsAsync(roomMasterResponse);
            //Arrange
            RoomMasterController roomMasterController = new RoomMasterController(_logger.Object, _config, _roomMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await roomMasterController.GetRoomMasterById(Id);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region RoomMaster controller Unit test cases for DeleteRoomMaster
        [Fact]
        public async void Test_DeleteRoomMaster_OkResult()
        {
            int Id = 1;
            RoomMasterResponse data = new RoomMasterResponse { Id = 1, IsError = false };
            _roomMasterRepository.Setup(x => x.DeleteRoomMaster(Id)).ReturnsAsync(data);
            RoomMasterController RoomMasterController = new RoomMasterController(_logger.Object, _config, _roomMasterRepository.Object, _cacheService.Object);
            var result = await RoomMasterController.DeleteRoomMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_DeleteRoomMaster_BadRequestResult()
        {
            int Id = 1;
            RoomMasterResponse data = new RoomMasterResponse { Id = 1, IsError = true };
            _roomMasterRepository.Setup(x => x.DeleteRoomMaster(Id)).ReturnsAsync(data);
            RoomMasterController roomMasterController = new RoomMasterController(_logger.Object, _config, _roomMasterRepository.Object, _cacheService.Object);
            var result = await roomMasterController.DeleteRoomMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region RoomMaster controller Unit test cases for AddRoomMaster

        [Fact]
        public async void Test_AddRoomMaster_BadRequestResult()
        {

            RoomMasterResponse data = new RoomMasterResponse { Id = 1, IsError = true };
            RoomMasterRequest input = new RoomMasterRequest() { Description = "Test", RoomCode = "10", WardId = 1, GenderId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _roomMasterRepository.Setup(x => x.AddRoomMaster(input)).ReturnsAsync(data);
            RoomMasterController RoomMasterController = new RoomMasterController(_logger.Object, _config, _roomMasterRepository.Object, _cacheService.Object);
            var result = await RoomMasterController.AddRoomMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void Test_AddRoomMaster_OkResult()
        {

            RoomMasterResponse data = new RoomMasterResponse { Id = 1, IsError = false };
            RoomMasterRequest input = new RoomMasterRequest() { Description = "Test", RoomCode = "10", WardId = 1, GenderId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _roomMasterRepository.Setup(x => x.AddRoomMaster(input)).ReturnsAsync(data);
            RoomMasterController RoomMasterController = new RoomMasterController(_logger.Object, _config, _roomMasterRepository.Object, _cacheService.Object);
            var result = await RoomMasterController.AddRoomMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region RoomMaster controller Unit test cases for UpdateRoomMaster

        [Fact]
        public async void Test_UpdateRoomMaster_OkResult()
        {

            RoomMasterResponse data = new RoomMasterResponse { Id = 1, IsError = false };
            RoomMasterRequest input = new RoomMasterRequest() { Id = 1, Description = "Test", RoomCode = "10", WardId = 1, GenderId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _roomMasterRepository.Setup(x => x.UpdateRoomMaster(input)).ReturnsAsync(data);
            RoomMasterController RoomMasterController = new RoomMasterController(_logger.Object, _config, _roomMasterRepository.Object, _cacheService.Object);
            var result = await RoomMasterController.UpdateRoomMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_UpdateRoomMaster_BadRequestResult()
        {

            RoomMasterResponse data = new RoomMasterResponse { Id = 1, IsError = true };
            RoomMasterRequest input = new RoomMasterRequest() { Id = 1, Description = "Test", RoomCode = "10", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _roomMasterRepository.Setup(x => x.UpdateRoomMaster(input)).ReturnsAsync(data);
            RoomMasterController roomMasterController = new RoomMasterController(_logger.Object, _config, _roomMasterRepository.Object, _cacheService.Object);
            var result = await roomMasterController.UpdateRoomMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region RoomMaster controller Unit test cases for GetRoomMaster
        [Fact]
        public async void Test_GetRoomMaster_OkResult()
        {
            List<RoomMasterResponse> data = new List<RoomMasterResponse>() { new RoomMasterResponse { Id = 1, IsError = false } };
            _roomMasterRepository.Setup(x => x.GetRoomMaster()).ReturnsAsync(data);
            RoomMasterController roomMasterController = new RoomMasterController(_logger.Object, _config, _roomMasterRepository.Object, _cacheService.Object);
            var result = await roomMasterController.GetRoomMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_GetRoomMaster_NotFoundResult()
        {
            List<RoomMasterResponse> data = null;
            _roomMasterRepository.Setup(x => x.GetRoomMaster()).ReturnsAsync(data);
            RoomMasterController roomMasterController = new RoomMasterController(_logger.Object, _config, _roomMasterRepository.Object, _cacheService.Object);
            var result = await roomMasterController.GetRoomMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        #endregion


    }
}
