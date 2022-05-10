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
   public class ReasonMasterControllerUnitTest
    {
          Mock<ILogger<ReasonMasterController>> _logger;
            IConfiguration _config;
            Mock<IReasonMasterRepository> _reasonMasterRepository;
            Mock<ICacheService> _cacheService;
            ErrorStatus error = new ErrorStatus();
            ErrorStatusCode errorcode = new ErrorStatusCode();


            public ReasonMasterControllerUnitTest()
            {
                _logger = new Mock<ILogger<ReasonMasterController>>();
                _reasonMasterRepository = new Mock<IReasonMasterRepository>();
                _cacheService = new Mock<ICacheService>();
            }

        #region ReasonMaster controller Unit test cases for GetReasonMasterById
        [Fact]
        public async void Test_GetReasonMasterById_OkResult()
        {
            int Id = 1;
            ReasonMasterResponse data = new ReasonMasterResponse { Id = 1, ReasonCode="abc", ReasonName="fever" };
            _reasonMasterRepository.Setup(x => x.GetReasonMasterById(Id)).ReturnsAsync(data);
            ReasonMasterController ReasonMasterController = new ReasonMasterController(_logger.Object, _config, _reasonMasterRepository.Object, _cacheService.Object);
            var result = await ReasonMasterController.GetReasonMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_GetReasonMasterById_NotFoundResult()
        {
            int Id = 1;
            ReasonMasterResponse data = null; //new RoomMasterRequest { Id = 1, Description = "Test", RoomCode = "10", WardId = 1, GenderId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _reasonMasterRepository.Setup(x => x.GetReasonMasterById(Id)).ReturnsAsync(data);
            ReasonMasterController ReasonMasterController = new ReasonMasterController(_logger.Object, _config, _reasonMasterRepository.Object, _cacheService.Object);
            var result = await ReasonMasterController.GetReasonMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async void Task_GetReasonMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            ReasonMasterResponse reasonMasterResponse = new ReasonMasterResponse { Id = 1, ReasonCode="abc" };
            DataResponse response = new DataResponse { Message = error.STATUS_INVALID_REQUEST, StatusCode = errorcode.STATUS_INVALID_REQUEST, Result = reasonMasterResponse };
            _reasonMasterRepository.Setup(x => x.GetReasonMasterById(Id)).ReturnsAsync(reasonMasterResponse);
            //Arrange
            ReasonMasterController reasonMasterController = new ReasonMasterController(_logger.Object, _config, _reasonMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await reasonMasterController.GetReasonMasterById(Id);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region ReasonMaster controller Unit test cases for DeleteReasonMaster

        [Fact]
        public async void Test_DeleteReasonMaster_OkResult()
        {
            int Id = 1;
            ReasonMasterResponse data = new ReasonMasterResponse { Id = 1, IsError = false };
            _reasonMasterRepository.Setup(x => x.DeleteReasonMaster(Id)).ReturnsAsync(data);
            ReasonMasterController ReasonMasterController = new ReasonMasterController(_logger.Object, _config, _reasonMasterRepository.Object, _cacheService.Object);
            var result = await ReasonMasterController.DeleteReasonMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_DeleteReasonMaster_BadRequestResult()
        {
            int Id = 1;
            ReasonMasterResponse data = new ReasonMasterResponse { Id = 1, IsError = true };
            _reasonMasterRepository.Setup(x => x.DeleteReasonMaster(Id)).ReturnsAsync(data);
            ReasonMasterController ReasonMasterController = new ReasonMasterController(_logger.Object, _config, _reasonMasterRepository.Object, _cacheService.Object);
            var result = await ReasonMasterController.DeleteReasonMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region ReasonMaster controller Unit test cases for AddReasonMaster

        [Fact]
        public async void Test_AddReasonMaster_BadRequestResult()
        {

            ReasonMasterResponse data = new ReasonMasterResponse { Id = 1, IsError = true };
            ReasonMasterRequest input = new ReasonMasterRequest() { Id = 1, IsBilling = true, IsContract = true, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _reasonMasterRepository.Setup(x => x.AddReasonMaster(input)).ReturnsAsync(data);
            ReasonMasterController ReasonMasterController = new ReasonMasterController(_logger.Object, _config, _reasonMasterRepository.Object, _cacheService.Object);
            var result = await ReasonMasterController.AddReasonMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void Test_AddReasonMaster_OkResult()
        {

            ReasonMasterResponse data = new ReasonMasterResponse { Id = 1, IsError = false };
            ReasonMasterRequest input = new ReasonMasterRequest() { Id = 1, IsBilling = true, IsContract = true, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _reasonMasterRepository.Setup(x => x.AddReasonMaster(input)).ReturnsAsync(data);
            ReasonMasterController ReasonMasterController = new ReasonMasterController(_logger.Object, _config, _reasonMasterRepository.Object, _cacheService.Object);
            var result = await ReasonMasterController.AddReasonMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region ReasonMaster controller Unit test cases for UpdateReasonMaster

        [Fact]
        public async void Test_UpdateReasonMaster_OkResult()
        {

            ReasonMasterResponse data = new ReasonMasterResponse { Id = 1, IsError = false };
            ReasonMasterRequest input = new ReasonMasterRequest() { Id = 1, IsBilling = true, IsContract = true, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _reasonMasterRepository.Setup(x => x.UpdateReasonMaster(input)).ReturnsAsync(data);
            ReasonMasterController ReasonMasterController = new ReasonMasterController(_logger.Object, _config, _reasonMasterRepository.Object, _cacheService.Object);
            var result = await ReasonMasterController.UpdateReasonMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_UpdateReasonMaster_BadRequestResult()
        {

            ReasonMasterResponse data = new ReasonMasterResponse { Id = 1, IsError = true };
            ReasonMasterRequest input = new ReasonMasterRequest() { Id = 1, IsBilling = true, IsContract = true, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _reasonMasterRepository.Setup(x => x.UpdateReasonMaster(input)).ReturnsAsync(data);
            ReasonMasterController ReasonMasterController = new ReasonMasterController(_logger.Object, _config, _reasonMasterRepository.Object, _cacheService.Object);
            var result = await ReasonMasterController.UpdateReasonMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region ReasonMaster controller Unit test cases for GetReasonMaster
        [Fact]
        public async void Test_GetReasonMaster_OkResult()
        {
            List<ReasonMasterResponse> data = new List<ReasonMasterResponse>() { new ReasonMasterResponse { Id = 1, IsError = false } };
            _reasonMasterRepository.Setup(x => x.GetReasonMaster()).ReturnsAsync(data);
            ReasonMasterController reasonMasterController = new ReasonMasterController(_logger.Object, _config, _reasonMasterRepository.Object, _cacheService.Object);
            var result = await reasonMasterController.GetReasonMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_GetReasonMaster_NotFoundResult()
        {
            List<ReasonMasterResponse> data = null;
            _reasonMasterRepository.Setup(x => x.GetReasonMaster()).ReturnsAsync(data);
            ReasonMasterController reasonMasterController = new ReasonMasterController(_logger.Object, _config, _reasonMasterRepository.Object, _cacheService.Object);
            var result = await reasonMasterController.GetReasonMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        #endregion
    }
}
