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
    public class StateMasterControllerUnitTest
    {
        Mock<ILogger<StateMasterController>> _logger;
        IConfiguration _config;
        Mock<IStateMasterRepository> _stateMasterRepository;
        Mock<ICacheService> _cacheService;
        ErrorStatus error = new ErrorStatus();
        ErrorStatusCode errorcode = new ErrorStatusCode();


        public StateMasterControllerUnitTest()
        {
            _logger = new Mock<ILogger<StateMasterController>>();
            _stateMasterRepository = new Mock<IStateMasterRepository>();
            _cacheService = new Mock<ICacheService>();
        }
        #region Add State Master Unit Test Cases
        [Fact]
        public async void Task_AddState_ValidData_Returns_OkResult()
        {
            StateMasterResponse stateMasterResponse = new StateMasterResponse { Id = 1, IsError = false };
            StateMasterRequest stateMasterRequest = new StateMasterRequest { Id = 1, Code = "PER", Description = "PERLIS", CountryId = 130, Smrpcode = "9", Smrpdescription = "PERLIS", EreturnStateId = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADDED, StatusCode = errorcode.STATUS_DATA_ADDED, Result = stateMasterResponse };
            _stateMasterRepository.Setup(x => x.AddState(stateMasterRequest)).ReturnsAsync(stateMasterResponse);

            //Arrange
            StateMasterController stateMasterController = new StateMasterController(_logger.Object, _config, _stateMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await stateMasterController.AddState(stateMasterRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_AddState_InvalidData_Returns_BadRequest()
        {
            StateMasterResponse stateMasterResponse = new StateMasterResponse { Id = 1, IsError = true };
            StateMasterRequest stateMasterRequest = new StateMasterRequest { Id = 1, Code = "", Description = "", CountryId = 130, Smrpcode = "9", Smrpdescription = "PERLIS", EreturnStateId = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADD_FAIL, StatusCode = errorcode.STATUS_DATA_ADD_FAIL, Result = stateMasterResponse };
            _stateMasterRepository.Setup(x => x.AddState(stateMasterRequest)).ReturnsAsync(stateMasterResponse);

            //Arrange
            StateMasterController stateMasterController = new StateMasterController(_logger.Object, _config, _stateMasterRepository.Object, _cacheService.Object);
            stateMasterController.ModelState.AddModelError("Description", "Required");
            stateMasterController.ModelState.AddModelError("Code", "Required");
            //Act
            var result = await stateMasterController.AddState(stateMasterRequest);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Update State Master Unit Test Cases
        [Fact]
        public async void Task_UpdateState_ValidData_Returns_OkResult()
        {
            StateMasterResponse stateMasterResponse = new StateMasterResponse { Id = 1, IsError = false };
            StateMasterRequest stateMasterRequest = new StateMasterRequest { Id = 1, Code = "PER", Description = "PERLIS", CountryId = 130, Smrpcode = "9", Smrpdescription = "PERLIS", EreturnStateId = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATED, StatusCode = errorcode.STATUS_DATA_UPDATED, Result = stateMasterResponse };
            _stateMasterRepository.Setup(x => x.UpdateState(stateMasterRequest)).ReturnsAsync(stateMasterResponse);

            //Arrange
            StateMasterController stateMasterController = new StateMasterController(_logger.Object, _config, _stateMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await stateMasterController.UpdateState(stateMasterRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_UpdateState_InvalidData_Returns_BadRequest()
        {
            StateMasterResponse stateMasterResponse = new StateMasterResponse { Id = 1, IsError = true };
            StateMasterRequest stateMasterRequest = new StateMasterRequest { Id = 1, Code = "", Description = "", CountryId = 130, Smrpcode = "9", Smrpdescription = "PERLIS", EreturnStateId = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" }; CityMasterRequest cityMasterRequest = new CityMasterRequest { Id = 1, StateId = 9, CityCode = "", Description = "", Smrpcode = "335", Smrpdescription = "Ayer Baloi", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATE_FAIL, StatusCode = errorcode.STATUS_DATA_UPDATE_FAIL, Result = stateMasterResponse };
            _stateMasterRepository.Setup(x => x.UpdateState(stateMasterRequest)).ReturnsAsync(stateMasterResponse);

            //Arrange
            StateMasterController stateMasterController = new StateMasterController(_logger.Object, _config, _stateMasterRepository.Object, _cacheService.Object);
            stateMasterController.ModelState.AddModelError("Description", "Required");
            stateMasterController.ModelState.AddModelError("Code", "Required");
            //Act
            var result = await stateMasterController.UpdateState(stateMasterRequest);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Delete State Master Unit Test Cases
        [Fact]
        public async void Task_DeleteState_ValidData_Returns_OkResult()
        {
            int Id = 1;
            StateMasterResponse stateMasterResponse = new StateMasterResponse { Id = 1, IsError = false };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETED, StatusCode = errorcode.STATUS_DATA_DELETED, Result = stateMasterResponse };
            _stateMasterRepository.Setup(x => x.DeleteState(Id)).ReturnsAsync(stateMasterResponse);

            //Arrange
            StateMasterController stateMasterController = new StateMasterController(_logger.Object, _config, _stateMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await stateMasterController.DeleteState(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_DeleteState_InvalidData_Returns_BadRequest()
        {
            int? Id = null;
            StateMasterResponse stateMasterResponse = new StateMasterResponse { Id = 0, IsError = true };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETE_FAIL, StatusCode = errorcode.STATUS_DATA_DELETE_FAIL, Result = stateMasterResponse };
            _stateMasterRepository.Setup(x => x.DeleteState(Convert.ToInt32(Id))).ReturnsAsync(stateMasterResponse);

            //Arrange
            StateMasterController stateMasterController = new StateMasterController(_logger.Object, _config, _stateMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await stateMasterController.DeleteState(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get State Master By Id Unit Test Cases
        [Fact]
        public async void Task_GetStateById_Returns_OkResult()
        {
            int Id = 1;
            StateMasterResponse stateMasterResponse = new StateMasterResponse { Id = 1, Code = "PER", Description = "PERLIS", CountryId = 130, Smrpcode = "9", Smrpdescription = "PERLIS", EreturnStateId = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = stateMasterResponse };
            _stateMasterRepository.Setup(x => x.GetStateById(Id)).ReturnsAsync(stateMasterResponse);

            //Arrange
            StateMasterController stateMasterController = new StateMasterController(_logger.Object, _config, _stateMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await stateMasterController.GetStateById(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetStateById_Returns_BadRequestResult()
        {
            int? Id = null;
            StateMasterResponse stateMasterResponse = new StateMasterResponse { Id = 1, Code = "PER", Description = "PERLIS", CountryId = 130, Smrpcode = "9", Smrpdescription = "PERLIS", EreturnStateId = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = stateMasterResponse };
            _stateMasterRepository.Setup(x => x.GetStateById(Id)).ReturnsAsync(stateMasterResponse);

            //Arrange
            StateMasterController stateMasterController = new StateMasterController(_logger.Object, _config, _stateMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await stateMasterController.GetStateById(Id);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetStateById_Returns_NotFoundResult()
        {
            int Id = 1;
            StateMasterResponse stateMasterResponse = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = stateMasterResponse };
            _stateMasterRepository.Setup(x => x.GetStateById(Id)).ReturnsAsync(stateMasterResponse);
            //Arrange
            StateMasterController stateMasterController = new StateMasterController(_logger.Object, _config, _stateMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await stateMasterController.GetStateById(Id);
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get State Master Unit Test Cases
        [Fact]
        public async void Task_GetStateMaster_Returns_OkResult()
        {
            List<StateMasterResponse> data = new List<StateMasterResponse>() { new StateMasterResponse { Id = 1, Code = "PER", Description = "PERLIS", CountryId = 130, Smrpcode = "9", Smrpdescription = "PERLIS", EreturnStateId = null, FacilityCode = "KLAN" } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _stateMasterRepository.Setup(x => x.GetStateMaster()).ReturnsAsync(data);

            //Arrange
            StateMasterController stateMasterController = new StateMasterController(_logger.Object, _config, _stateMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await stateMasterController.GetStateMaster();
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetStateMaster_Returns_NotFoundResult()
        {
            List<StateMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _stateMasterRepository.Setup(x => x.GetStateMaster()).ReturnsAsync(data);
            //Arrange
            StateMasterController stateMasterController = new StateMasterController(_logger.Object, _config, _stateMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await stateMasterController.GetStateMaster();
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion
    }
}
