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
   public class DepartmentMasterControllerUnitTest
    {
            Mock<ILogger<DepartmentMasterController>> _logger;
            IConfiguration _config;
            Mock<IDepartmentMasterRepository> _departmentMasterRepository;
            Mock<ICacheService> _cacheService;
            ErrorStatus error = new ErrorStatus();
            ErrorStatusCode errorcode = new ErrorStatusCode();


            public DepartmentMasterControllerUnitTest()
            {
                _logger = new Mock<ILogger<DepartmentMasterController>>();
                _departmentMasterRepository = new Mock<IDepartmentMasterRepository>();
                _cacheService = new Mock<ICacheService>();
            }

        #region Department Master controller Unit test cases for GetDepartmentMasterById
        [Fact]
        public async void Test_GetDepartmentMasterById_OkResult()
        {
            int Id = 1;
            DepartmentMasterReponse data = new DepartmentMasterReponse { Id = 1, DepartmentCode = "ABC" };
            _departmentMasterRepository.Setup(x => x.GetDepartmentMasterById(Id)).ReturnsAsync(data);
            DepartmentMasterController DepartmentMasterController = new DepartmentMasterController(_logger.Object, _config, _departmentMasterRepository.Object, _cacheService.Object);
            var result = await DepartmentMasterController.GetDepartmentMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_GetDepartmentMasterById_NotFoundResult()
        {
            int Id = 1;
            DepartmentMasterReponse data = null;
            _departmentMasterRepository.Setup(x => x.GetDepartmentMasterById(Id)).ReturnsAsync(data);
            DepartmentMasterController DepartmentMasterController = new DepartmentMasterController(_logger.Object, _config, _departmentMasterRepository.Object, _cacheService.Object);
            var result = await DepartmentMasterController.GetDepartmentMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async void Task_GetDepartmentMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            DepartmentMasterReponse departmentMasterReponse = new DepartmentMasterReponse { Id = 1, Description = "CauseOfDeathMaster" };
            DataResponse response = new DataResponse { Message = error.STATUS_INVALID_REQUEST, StatusCode = errorcode.STATUS_INVALID_REQUEST, Result = departmentMasterReponse };
            _departmentMasterRepository.Setup(x => x.GetDepartmentMasterById(Id)).ReturnsAsync(departmentMasterReponse);

            //Arrange
            DepartmentMasterController departmentMasterController = new DepartmentMasterController(_logger.Object, _config, _departmentMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await departmentMasterController.GetDepartmentMasterById(Id);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Department Master controller Unit test cases for DeleteDepartmentMaster
        [Fact]
        public async void Test_DeleteDepartmentMaster_OkResult()
        {
            int Id = 1;
            DepartmentMasterReponse data = new DepartmentMasterReponse { Id = 1, IsError = false };
            _departmentMasterRepository.Setup(x => x.DeleteDepartmentMaster(Id)).ReturnsAsync(data);
            DepartmentMasterController DepartmentMasterController = new DepartmentMasterController(_logger.Object, _config, _departmentMasterRepository.Object, _cacheService.Object);
            var result = await DepartmentMasterController.DeleteDepartmentMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_DeleteDepartmentMaster_BadRequestResult()
        {
            int Id = 1;
            DepartmentMasterReponse data = new DepartmentMasterReponse { Id = 1, IsError = true };
            _departmentMasterRepository.Setup(x => x.DeleteDepartmentMaster(Id)).ReturnsAsync(data);
            DepartmentMasterController DepartmentMasterController = new DepartmentMasterController(_logger.Object, _config, _departmentMasterRepository.Object, _cacheService.Object);
            var result = await DepartmentMasterController.DeleteDepartmentMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region Department Master controller Unit test cases for AddDepartmentMaster
        [Fact]
        public async void Test_AddDepartmentMaster_BadRequestResult()
        {

            DepartmentMasterReponse data = new DepartmentMasterReponse { Id = 1, IsError = true };
            DepartmentMasterRequest input = new DepartmentMasterRequest() { Id = 1, DepartmentCode = "ABC", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _departmentMasterRepository.Setup(x => x.AddDepartmentMaster(input)).ReturnsAsync(data);
            DepartmentMasterController DepartmentMasterController = new DepartmentMasterController(_logger.Object, _config, _departmentMasterRepository.Object, _cacheService.Object);
            var result = await DepartmentMasterController.AddDepartmentMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void Test_AddDepartmentMaster_OkResult()
        {

            DepartmentMasterReponse data = new DepartmentMasterReponse { Id = 1, IsError = false };
            DepartmentMasterRequest input = new DepartmentMasterRequest() { Id = 1, DepartmentCode = "ABC", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _departmentMasterRepository.Setup(x => x.AddDepartmentMaster(input)).ReturnsAsync(data);
            DepartmentMasterController DepartmentMasterController = new DepartmentMasterController(_logger.Object, _config, _departmentMasterRepository.Object, _cacheService.Object);
            var result = await DepartmentMasterController.AddDepartmentMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region Department Master controller Unit test cases for UpdateDepartmentMaster
        [Fact]
        public async void Test_UpdateDepartmentMaster_OkResult()
        {

            DepartmentMasterReponse data = new DepartmentMasterReponse { Id = 1, IsError = false };
            DepartmentMasterRequest input = new DepartmentMasterRequest() { Id = 1, DepartmentCode = "ABC", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _departmentMasterRepository.Setup(x => x.UpdateDepartmentMaster(input)).ReturnsAsync(data);
            DepartmentMasterController DepartmentMasterController = new DepartmentMasterController(_logger.Object, _config, _departmentMasterRepository.Object, _cacheService.Object);
            var result = await DepartmentMasterController.UpdateDepartmentMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_UpdateDepartmentMaster_BadRequestResult()
        {

            DepartmentMasterReponse data = new DepartmentMasterReponse { Id = 1, IsError = true };
            DepartmentMasterRequest input = new DepartmentMasterRequest() { Id = 1, DepartmentCode = "ABC", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _departmentMasterRepository.Setup(x => x.UpdateDepartmentMaster(input)).ReturnsAsync(data);
            DepartmentMasterController DepartmentMasterController = new DepartmentMasterController(_logger.Object, _config, _departmentMasterRepository.Object, _cacheService.Object);
            var result = await DepartmentMasterController.UpdateDepartmentMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region Department Master controller Unit test cases for GetDepartmentMaster
        [Fact]
        public async void Test_GetDepartmentMaster_OkResult()
        {
            List<DepartmentMasterReponse> data = new List<DepartmentMasterReponse>() { new DepartmentMasterReponse { Id = 1, Description = "Open" } };
            _departmentMasterRepository.Setup(x => x.GetDepartmentMaster()).ReturnsAsync(data);
            DepartmentMasterController departmentMasterController = new DepartmentMasterController(_logger.Object, _config, _departmentMasterRepository.Object, _cacheService.Object);
            var result = await departmentMasterController.GetDepartmentMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_GetDepartmentMaster_NotFoundResult()
        {
            List<DepartmentMasterReponse> data = null;
            _departmentMasterRepository.Setup(x => x.GetDepartmentMaster()).ReturnsAsync(data);
            DepartmentMasterController departmentMasterController = new DepartmentMasterController(_logger.Object, _config, _departmentMasterRepository.Object, _cacheService.Object);
            var result = await departmentMasterController.GetDepartmentMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        #endregion

    }
}
