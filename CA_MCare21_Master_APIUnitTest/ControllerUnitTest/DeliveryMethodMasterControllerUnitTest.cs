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
  public  class DeliveryMethodMasterControllerUnitTest
    {
        Mock<ILogger<DeliveryMethodMasterController>> _logger;
        IConfiguration _config;
        Mock<IDeliveryMethodMasterRepository> _deliveryMethodMasterRepository;
        Mock<ICacheService> _cacheService;
        ErrorStatus error = new ErrorStatus();
        ErrorStatusCode errorcode = new ErrorStatusCode();


        public DeliveryMethodMasterControllerUnitTest()
        {
            _logger = new Mock<ILogger<DeliveryMethodMasterController>>();
            _deliveryMethodMasterRepository = new Mock<IDeliveryMethodMasterRepository>();
            _cacheService = new Mock<ICacheService>();
        }
        #region DeliveryMethod Master controller Unit test cases for GetDeliveryMethodMasterById
        [Fact]
        public async void Test_GetDeliveryMethodMasterById_OkResult()
        {
            int Id = 1;
            DeliveryMethodMasterResponse data = new DeliveryMethodMasterResponse { Id = 1, Description = "test" };
            _deliveryMethodMasterRepository.Setup(x => x.GetDeliveryMethodMasterById(Id)).ReturnsAsync(data);
            DeliveryMethodMasterController DeliveryMethodMasterController = new DeliveryMethodMasterController(_logger.Object, _config, _deliveryMethodMasterRepository.Object, _cacheService.Object);
            var result = await DeliveryMethodMasterController.GetDeliveryMethodMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_GetDeliveryMethodMasterById_NotFoundResult()
        {
            int Id = 1;
            DeliveryMethodMasterResponse data = null;
            _deliveryMethodMasterRepository.Setup(x => x.GetDeliveryMethodMasterById(Id)).ReturnsAsync(data);
            DeliveryMethodMasterController DeliveryMethodMasterController = new DeliveryMethodMasterController(_logger.Object, _config, _deliveryMethodMasterRepository.Object, _cacheService.Object);
            var result = await DeliveryMethodMasterController.GetDeliveryMethodMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async void Task_GetDeliveryMethodMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            DeliveryMethodMasterResponse deliveryMethodMasterResponse = new DeliveryMethodMasterResponse { Id = 1, Description = "CauseOfDeathMaster" };
            DataResponse response = new DataResponse { Message = error.STATUS_INVALID_REQUEST, StatusCode = errorcode.STATUS_INVALID_REQUEST, Result = deliveryMethodMasterResponse };
            _deliveryMethodMasterRepository.Setup(x => x.GetDeliveryMethodMasterById(Id)).ReturnsAsync(deliveryMethodMasterResponse);

            //Arrange
            DeliveryMethodMasterController deliveryMethodMasterController = new DeliveryMethodMasterController(_logger.Object, _config, _deliveryMethodMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await deliveryMethodMasterController.GetDeliveryMethodMasterById(Id);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region DeliveryMethod Master controller Unit test cases for DeleteDeliveryMethodMaster
        [Fact]
        public async void Test_DeleteDeliveryMethodMaster_OkResult()
        {
            int Id = 1;
            DeliveryMethodMasterResponse data = new DeliveryMethodMasterResponse { Id = 1, IsError = false };
            _deliveryMethodMasterRepository.Setup(x => x.DeleteDeliveryMethodMaster(Id)).ReturnsAsync(data);
            DeliveryMethodMasterController DeliveryMethodMasterController = new DeliveryMethodMasterController(_logger.Object, _config, _deliveryMethodMasterRepository.Object, _cacheService.Object);
            var result = await DeliveryMethodMasterController.DeleteDeliveryMethodMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_DeleteDeliveryMethodMaster_BadRequestResult()
        {
            int Id = 1;
            DeliveryMethodMasterResponse data = new DeliveryMethodMasterResponse { Id = 1, IsError = true };
            _deliveryMethodMasterRepository.Setup(x => x.DeleteDeliveryMethodMaster(Id)).ReturnsAsync(data);
            DeliveryMethodMasterController DeliveryMethodMasterController = new DeliveryMethodMasterController(_logger.Object, _config, _deliveryMethodMasterRepository.Object, _cacheService.Object);
            var result = await DeliveryMethodMasterController.DeleteDeliveryMethodMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region DeliveryMethod Master controller Unit test cases for AddDeliveryMethodMaster
        [Fact]
        public async void Test_AddDeliveryMethodMaster_BadRequestResult()
        {

            DeliveryMethodMasterResponse data = new DeliveryMethodMasterResponse { Id = 1, IsError = true };
            DeliveryMethodMasterRequest input = new DeliveryMethodMasterRequest() { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" };
            _deliveryMethodMasterRepository.Setup(x => x.AddDeliveryMethodMaster(input)).ReturnsAsync(data);
            DeliveryMethodMasterController DeliveryMethodMasterController = new DeliveryMethodMasterController(_logger.Object, _config, _deliveryMethodMasterRepository.Object, _cacheService.Object);
            var result = await DeliveryMethodMasterController.AddDeliveryMethodMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void Test_AddDeliveryMethodMaster_OkResult()
        {

            DeliveryMethodMasterResponse data = new DeliveryMethodMasterResponse { Id = 1, IsError = false };
            DeliveryMethodMasterRequest input = new DeliveryMethodMasterRequest() { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" };
            _deliveryMethodMasterRepository.Setup(x => x.AddDeliveryMethodMaster(input)).ReturnsAsync(data);
            DeliveryMethodMasterController DeliveryMethodMasterController = new DeliveryMethodMasterController(_logger.Object, _config, _deliveryMethodMasterRepository.Object, _cacheService.Object);
            var result = await DeliveryMethodMasterController.AddDeliveryMethodMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region DeliveryMethod Master controller Unit test cases for UpdateDeliveryMethodMaster
        [Fact]
        public async void Test_UpdateDeliveryMethodMaster_OkResult()
        {

            DeliveryMethodMasterResponse data = new DeliveryMethodMasterResponse { Id = 1, IsError = false };
            DeliveryMethodMasterRequest input = new DeliveryMethodMasterRequest() { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" };
            _deliveryMethodMasterRepository.Setup(x => x.UpdateDeliveryMethodMaster(input)).ReturnsAsync(data);
            DeliveryMethodMasterController DeliveryMethodMasterController = new DeliveryMethodMasterController(_logger.Object, _config, _deliveryMethodMasterRepository.Object, _cacheService.Object);
            var result = await DeliveryMethodMasterController.UpdateDeliveryMethodMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_UpdateDeliveryMethodMaster_BadRequestResult()
        {

            DeliveryMethodMasterResponse data = new DeliveryMethodMasterResponse { Id = 1, IsError = true };
            DeliveryMethodMasterRequest input = new DeliveryMethodMasterRequest() { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" };
            _deliveryMethodMasterRepository.Setup(x => x.UpdateDeliveryMethodMaster(input)).ReturnsAsync(data);
            DeliveryMethodMasterController DeliveryMethodMasterController = new DeliveryMethodMasterController(_logger.Object, _config, _deliveryMethodMasterRepository.Object, _cacheService.Object);
            var result = await DeliveryMethodMasterController.UpdateDeliveryMethodMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region DeliveryMethod Master controller Unit test cases for GetDeliveryMethodMaster
        [Fact]
        public async void Test_GetDeliveryMethodMaster_OkResult()
        {
            List<DeliveryMethodMasterResponse> data = new List<DeliveryMethodMasterResponse>() { new DeliveryMethodMasterResponse { Id = 1, Description = "Open" } };
            _deliveryMethodMasterRepository.Setup(x => x.GetDeliveryMethodMaster()).ReturnsAsync(data);
            DeliveryMethodMasterController DeliveryMethodMasterController = new DeliveryMethodMasterController(_logger.Object, _config, _deliveryMethodMasterRepository.Object, _cacheService.Object);
            var result = await DeliveryMethodMasterController.GetDeliveryMethodMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_GetDeliveryMethodMaster_NotFoundResult()
        {
            List<DeliveryMethodMasterResponse> data = null;
            _deliveryMethodMasterRepository.Setup(x => x.GetDeliveryMethodMaster()).ReturnsAsync(data);
            DeliveryMethodMasterController DeliveryMethodMasterController = new DeliveryMethodMasterController(_logger.Object, _config, _deliveryMethodMasterRepository.Object, _cacheService.Object);
            var result = await DeliveryMethodMasterController.GetDeliveryMethodMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
             
        #endregion
    }
}
