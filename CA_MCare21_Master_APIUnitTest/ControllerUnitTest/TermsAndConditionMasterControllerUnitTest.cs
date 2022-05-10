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
    public class TermsAndConditionMasterControllerUnitTest
    {
        Mock<ILogger<TermsAndConditionMasterController>> _logger;
        IConfiguration _config;
        Mock<ITermsAndConditionMasterRepository> _termsAndConditionMasterRepository;
        Mock<ICacheService> _cacheService;
        ErrorStatus error = new ErrorStatus();
        ErrorStatusCode errorcode = new ErrorStatusCode();


        public TermsAndConditionMasterControllerUnitTest()
        {
            _logger = new Mock<ILogger<TermsAndConditionMasterController>>();
            _termsAndConditionMasterRepository = new Mock<ITermsAndConditionMasterRepository>();
            _cacheService = new Mock<ICacheService>();
        }


        #region Add TermsandCondition Master Unit Test Cases 
        [Fact]
        public async void Task_AddTerms_ValidData_Returns_OkResult()
        {
            TermsAndConditionMasterResponse terms = new TermsAndConditionMasterResponse { Id = 1, IsError = false };
            TermsAndConditionMasterRequest termsRequest = new TermsAndConditionMasterRequest { Id = 1, TermsAndConditions = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADDED, StatusCode = errorcode.STATUS_DATA_ADDED, Result = terms };
            _termsAndConditionMasterRepository.Setup(x => x.AddTerms(termsRequest)).ReturnsAsync(terms);

            //Arrange
            TermsAndConditionMasterController TermsAndConditionMasterController = new TermsAndConditionMasterController(_logger.Object, _config, _termsAndConditionMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await TermsAndConditionMasterController.AddTerms(termsRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_AddTerms_InvalidData_Returns_BadRequest()
        {
            TermsAndConditionMasterResponse terms = new TermsAndConditionMasterResponse { Id = 1, IsError = true };
            TermsAndConditionMasterRequest termsRequest = new TermsAndConditionMasterRequest { Id = 1, TermsAndConditions = "", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADD_FAIL, StatusCode = errorcode.STATUS_DATA_ADD_FAIL, Result = terms };
            _termsAndConditionMasterRepository.Setup(x => x.AddTerms(termsRequest)).ReturnsAsync(terms);

            //Arrange
            TermsAndConditionMasterController TermsAndConditionMasterController = new TermsAndConditionMasterController(_logger.Object, _config, _termsAndConditionMasterRepository.Object, _cacheService.Object);
            TermsAndConditionMasterController.ModelState.AddModelError("Description", "Required");
            //Act
            var result = await TermsAndConditionMasterController.AddTerms(termsRequest);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Update TermsandCondition Master Unit Test Cases 
        [Fact]
        public async void Task_UpdateTerms_ValidData_Returns_OkResult()
        {
            TermsAndConditionMasterResponse terms = new TermsAndConditionMasterResponse { Id = 1, IsError = false };
            TermsAndConditionMasterRequest termsRequest = new TermsAndConditionMasterRequest { Id = 1, FacilityCode = "KLAN", TermsAndConditions = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATED, StatusCode = errorcode.STATUS_DATA_UPDATED, Result = terms };
            _termsAndConditionMasterRepository.Setup(x => x.UpdateTerms(termsRequest)).ReturnsAsync(terms);

            //Arrange
            TermsAndConditionMasterController TermsAndConditionMasterController = new TermsAndConditionMasterController(_logger.Object, _config, _termsAndConditionMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await TermsAndConditionMasterController.UpdateTerms(termsRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_UpdateTerms_InvalidData_Returns_BadRequest()
        {
            TermsAndConditionMasterResponse terms = new TermsAndConditionMasterResponse { Id = 1, IsError = true };
            TermsAndConditionMasterRequest termsRequest = new TermsAndConditionMasterRequest { Id = 1, FacilityCode = "KLAN", TermsAndConditions = "", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATE_FAIL, StatusCode = errorcode.STATUS_DATA_UPDATE_FAIL, Result = terms };
            _termsAndConditionMasterRepository.Setup(x => x.UpdateTerms(termsRequest)).ReturnsAsync(terms);

            //Arrange
            TermsAndConditionMasterController TermsAndConditionMasterController = new TermsAndConditionMasterController(_logger.Object, _config, _termsAndConditionMasterRepository.Object, _cacheService.Object);
            TermsAndConditionMasterController.ModelState.AddModelError("TermsAndConditions", "Required");
            //Act
            var result = await TermsAndConditionMasterController.UpdateTerms(termsRequest);
            var actualResult = result as BadRequestResult;
            //Asert
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }
        #endregion

        #region Delete TermsandCondition Master Unit Test Cases 
        [Fact]
        public async void Task_DeleteTerms_ValidData_Returns_OkResult()
        {
            int Id = 1;
            TermsAndConditionMasterResponse terms = new TermsAndConditionMasterResponse { Id = 1, IsError = false };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETED, StatusCode = errorcode.STATUS_DATA_DELETED, Result = terms };
            _termsAndConditionMasterRepository.Setup(x => x.DeleteTerms(Id)).ReturnsAsync(terms);

            //Arrange
            TermsAndConditionMasterController TermsAndConditionMasterController = new TermsAndConditionMasterController(_logger.Object, _config, _termsAndConditionMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await TermsAndConditionMasterController.DeleteTerms(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_DeleteTerms_InvalidData_Returns_BadRequest()
        {
            int? Id = null;
            TermsAndConditionMasterResponse terms = new TermsAndConditionMasterResponse { Id = 0, IsError = true };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETE_FAIL, StatusCode = errorcode.STATUS_DATA_DELETE_FAIL, Result = terms };
            _termsAndConditionMasterRepository.Setup(x => x.DeleteTerms(Convert.ToInt32(Id))).ReturnsAsync(terms);

            //Arrange
            TermsAndConditionMasterController TermsAndConditionMasterController = new TermsAndConditionMasterController(_logger.Object, _config, _termsAndConditionMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await TermsAndConditionMasterController.DeleteTerms(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get TermsandConcition Master By Id Unit Test Cases 
        [Fact]
        public async void Task_GetTermsById_Returns_OkResult()
        {
            int Id = 1;
            TermsAndConditionMasterResponse terms = new TermsAndConditionMasterResponse { Id = 1, TermsAndConditions = "Test" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = terms };
            _termsAndConditionMasterRepository.Setup(x => x.GetTermsById(Id)).ReturnsAsync(terms);

            //Arrange
            TermsAndConditionMasterController TermsAndConditionMasterController = new TermsAndConditionMasterController(_logger.Object, _config, _termsAndConditionMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await TermsAndConditionMasterController.GetTermsById(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetTermsById_Returns_BadRequestResult()
        {
            int? Id = null;
            TermsAndConditionMasterResponse terms = new TermsAndConditionMasterResponse { Id = 1, TermsAndConditions = "Test" };
            DataResponse response = new DataResponse { Message = error.STATUS_INVALID_REQUEST, StatusCode = errorcode.STATUS_INVALID_REQUEST, Result = terms };
            _termsAndConditionMasterRepository.Setup(x => x.GetTermsById(Id)).ReturnsAsync(terms);

            //Arrange
            TermsAndConditionMasterController TermsAndConditionMasterController = new TermsAndConditionMasterController(_logger.Object, _config, _termsAndConditionMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await TermsAndConditionMasterController.GetTermsById(Id);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetTermsById_Returns_NotFoundResult()
        {
            int Id = 1;
            TermsAndConditionMasterResponse terms = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = terms };
            _termsAndConditionMasterRepository.Setup(x => x.GetTermsById(Id)).ReturnsAsync(terms);
            //Arrange
            TermsAndConditionMasterController TermsAndConditionMasterController = new TermsAndConditionMasterController(_logger.Object, _config, _termsAndConditionMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await TermsAndConditionMasterController.GetTermsById(Id);
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get TermsandCondition Master Unit Test Cases
        [Fact]
        public async void Task_GetTerms_Returns_OkResult()
        {
            List<TermsAndConditionMasterResponse> data = new List<TermsAndConditionMasterResponse>() { new TermsAndConditionMasterResponse { Id = 1, TermsAndConditions = "Test" } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _termsAndConditionMasterRepository.Setup(x => x.GetTerms()).ReturnsAsync(data);

            //Arrange
            TermsAndConditionMasterController TermsAndConditionMasterController = new TermsAndConditionMasterController(_logger.Object, _config, _termsAndConditionMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await TermsAndConditionMasterController.GetTerms();
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetTerms_Returns_NotFoundResult()
        {
            List<TermsAndConditionMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _termsAndConditionMasterRepository.Setup(x => x.GetTerms()).ReturnsAsync(data);
            //Arrange
            TermsAndConditionMasterController TermsAndConditionMasterController = new TermsAndConditionMasterController(_logger.Object, _config, _termsAndConditionMasterRepository.Object, _cacheService.Object);
            //Act
            var result = await TermsAndConditionMasterController.GetTerms();
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion


    }
}
