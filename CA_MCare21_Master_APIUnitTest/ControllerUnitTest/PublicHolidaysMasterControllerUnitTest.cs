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
    public class PublicHolidaysMasterControllerUnitTest
    {
     
            Mock<ILogger<PublicHolidaysMasterController>> _logger;
            IConfiguration _config;
            Mock<IPublicHolidaysMasterRepository> _publicHolidaysMasterRepository;
            Mock<ICacheService> _cacheService;
            ErrorStatus error = new ErrorStatus();
            ErrorStatusCode errorcode = new ErrorStatusCode();


            public PublicHolidaysMasterControllerUnitTest()
            {
                _logger = new Mock<ILogger<PublicHolidaysMasterController>>();
                _publicHolidaysMasterRepository = new Mock<IPublicHolidaysMasterRepository>();
                _cacheService = new Mock<ICacheService>();
            }

        #region PublicHolidaysMaster controller Unit test cases for GetPublicHolidaysMasterById
        [Fact]
        public async void Test_GetPublicHolidaysMasterById_OkResult()
        {
            int Id = 1;
            PublicHolidaysMasterResponse data = new PublicHolidaysMasterResponse { Id = 1, Reason = "fever", HoliDayName = "friday"};
            _publicHolidaysMasterRepository.Setup(x => x.GetPublicHolidaysMasterById(Id)).ReturnsAsync(data);
            PublicHolidaysMasterController PublicHolidaysMasterController = new PublicHolidaysMasterController(_logger.Object, _config, _publicHolidaysMasterRepository.Object, _cacheService.Object);
            var result = await PublicHolidaysMasterController.GetPublicHolidaysMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_GetPublicHolidaysMasterById_NotFoundResult()
        {
            int Id = 1;
            PublicHolidaysMasterResponse data = null;
            _publicHolidaysMasterRepository.Setup(x => x.GetPublicHolidaysMasterById(Id)).ReturnsAsync(data);
            PublicHolidaysMasterController PublicHolidaysMasterController = new PublicHolidaysMasterController(_logger.Object, _config, _publicHolidaysMasterRepository.Object, _cacheService.Object);
            var result = await PublicHolidaysMasterController.GetPublicHolidaysMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async void Task_GetPublicHolidaysMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            PublicHolidaysMasterResponse publicHolidaysMasterResponse = new PublicHolidaysMasterResponse { Id = 1,  HoliDayName="sunday" };
            DataResponse response = new DataResponse { Message = error.STATUS_INVALID_REQUEST, StatusCode = errorcode.STATUS_INVALID_REQUEST, Result = publicHolidaysMasterResponse };
            _publicHolidaysMasterRepository.Setup(x => x.GetPublicHolidaysMasterById(Id)).ReturnsAsync(publicHolidaysMasterResponse);
            //Arrange
            PublicHolidaysMasterController publicHolidaysMasterController = new PublicHolidaysMasterController(_logger.Object, _config, _publicHolidaysMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await publicHolidaysMasterController.GetPublicHolidaysMasterById(Id);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region PublicHolidaysMaster controller Unit test cases for DeletePublicHolidaysMaster

        [Fact]
        public async void Test_DeletePublicHolidaysMaster_OkResult()
        {
            int Id = 1;
            PublicHolidaysMasterResponse data = new PublicHolidaysMasterResponse { Id = 1, IsError = false };
            _publicHolidaysMasterRepository.Setup(x => x.DeletePublicHolidaysMaster(Id)).ReturnsAsync(data);
            PublicHolidaysMasterController PublicHolidaysMasterController = new PublicHolidaysMasterController(_logger.Object, _config, _publicHolidaysMasterRepository.Object, _cacheService.Object);
            var result = await PublicHolidaysMasterController.DeletePublicHolidaysMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_DeletePublicHolidaysMaster_BadRequestResult()
        {
            int Id = 1;
            PublicHolidaysMasterResponse data = new PublicHolidaysMasterResponse { Id = 1, IsError = true };
            _publicHolidaysMasterRepository.Setup(x => x.DeletePublicHolidaysMaster(Id)).ReturnsAsync(data);
            PublicHolidaysMasterController PublicHolidaysMasterController = new PublicHolidaysMasterController(_logger.Object, _config, _publicHolidaysMasterRepository.Object, _cacheService.Object);
            var result = await PublicHolidaysMasterController.DeletePublicHolidaysMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region PublicHolidaysMaster controller Unit test cases for AddPublicHolidaysMaster

        [Fact]
        public async void Test_AddPublicHolidaysMaster_BadRequestResult()
        {

            PublicHolidaysMasterResponse data = new PublicHolidaysMasterResponse { Id = 1, IsError = true };
            PublicHolidaysMasterRequest input = new PublicHolidaysMasterRequest() { Id = 1, Reason = "fever", HoliDayName = "friday", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _publicHolidaysMasterRepository.Setup(x => x.AddPublicHolidaysMaster(input)).ReturnsAsync(data);
            PublicHolidaysMasterController PublicHolidaysMasterController = new PublicHolidaysMasterController(_logger.Object, _config, _publicHolidaysMasterRepository.Object, _cacheService.Object);
            var result = await PublicHolidaysMasterController.AddPublicHolidaysMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void Test_AddPublicHolidaysMaster_OkResult()
        {

            PublicHolidaysMasterResponse data = new PublicHolidaysMasterResponse { Id = 1, IsError = false };
            PublicHolidaysMasterRequest input = new PublicHolidaysMasterRequest() { Id = 1, Reason = "fever", HoliDayName = "friday", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _publicHolidaysMasterRepository.Setup(x => x.AddPublicHolidaysMaster(input)).ReturnsAsync(data);
            PublicHolidaysMasterController PublicHolidaysMasterController = new PublicHolidaysMasterController(_logger.Object, _config, _publicHolidaysMasterRepository.Object, _cacheService.Object);
            var result = await PublicHolidaysMasterController.AddPublicHolidaysMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region PublicHolidaysMaster controller Unit test cases for UpdatePublicHolidaysMaster

        [Fact]
        public async void Test_UpdatePublicHolidaysMaster_OkResult()
        {

            PublicHolidaysMasterResponse data = new PublicHolidaysMasterResponse { Id = 1, IsError = false };
            PublicHolidaysMasterRequest input = new PublicHolidaysMasterRequest() { Id = 1, Reason = "fever", HoliDayName = "friday", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _publicHolidaysMasterRepository.Setup(x => x.UpdatePublicHolidaysMaster(input)).ReturnsAsync(data);
            PublicHolidaysMasterController PublicHolidaysMasterController = new PublicHolidaysMasterController(_logger.Object, _config, _publicHolidaysMasterRepository.Object, _cacheService.Object);
            var result = await PublicHolidaysMasterController.UpdatePublicHolidaysMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_UpdatePublicHolidaysMaster_BadRequestResult()
        {

            PublicHolidaysMasterResponse data = new PublicHolidaysMasterResponse { Id = 1, IsError = true };
            PublicHolidaysMasterRequest input = new PublicHolidaysMasterRequest() { Id = 1, Reason = "fever", HoliDayName = "friday", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _publicHolidaysMasterRepository.Setup(x => x.UpdatePublicHolidaysMaster(input)).ReturnsAsync(data);
            PublicHolidaysMasterController PublicHolidaysMasterController = new PublicHolidaysMasterController(_logger.Object, _config, _publicHolidaysMasterRepository.Object, _cacheService.Object);
            var result = await PublicHolidaysMasterController.UpdatePublicHolidaysMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region PublicHolidaysMaster controller Unit test cases for GetPublicHolidaysMaster

        [Fact]
        public async void Test_GetPublicHolidaysMaster_OkResult()
        {
            List<PublicHolidaysMasterResponse> data = new List<PublicHolidaysMasterResponse>() { new PublicHolidaysMasterResponse { Id = 1, IsError=false} };
            _publicHolidaysMasterRepository.Setup(x => x.GetPublicHolidaysMaster()).ReturnsAsync(data);
            PublicHolidaysMasterController publicHolidaysMasterController = new PublicHolidaysMasterController(_logger.Object, _config, _publicHolidaysMasterRepository.Object, _cacheService.Object);
            var result = await publicHolidaysMasterController.GetPublicHolidaysMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_GetPublicHolidaysMaster_NotFoundResult()
        {
            List<PublicHolidaysMasterResponse> data = null;
            _publicHolidaysMasterRepository.Setup(x => x.GetPublicHolidaysMaster()).ReturnsAsync(data);
            PublicHolidaysMasterController publicHolidaysMasterController = new PublicHolidaysMasterController(_logger.Object, _config, _publicHolidaysMasterRepository.Object, _cacheService.Object);
            var result = await publicHolidaysMasterController.GetPublicHolidaysMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        #endregion

    }
}
