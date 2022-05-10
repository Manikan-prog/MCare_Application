using AutoMapper;
using CA_MCare21_CrossCuttingConcern.Caching;
using CA_MCare21_MasterAPI.Controllers;
using CA_MCare21_MasterAPI.Models;
using CA_MCare21_MasterAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CA_MCare21_MasterAPIUnitTest
{
    public class CoreMasterControllerUnitTest
    {
        Mock<ILogger<CoreMasterController>> _logger;
        IConfiguration _config;
        Mock<ICoreMasterRepository> _coremasterRepository;
        Mock<ICacheService> _cacheService;
        ErrorStatus error = new ErrorStatus();
        ErrorStatusCode errorcode = new ErrorStatusCode();


        public CoreMasterControllerUnitTest()
        {
            _logger = new Mock<ILogger<CoreMasterController>>();
            _coremasterRepository = new Mock<ICoreMasterRepository>();
            _cacheService = new Mock<ICacheService>();
        }
        [Fact]
        public async void Test_GetStatusMaster_OkResult()
        {
            IEnumerable<StatusMasterResponse> data = new List<StatusMasterResponse>() { new StatusMasterResponse { Id = 1, Description = "Open", StatusCode = "Ope" } };
            _coremasterRepository.Setup(x => x.GetStatusMaster()).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.GetStatusMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Test_GetWardMasterById_OkResult()
        {
            int Id = 1;
            WardMasterResponse data = new WardMasterResponse { Id = 1, WardCode = "AB", WardName = "Test", DepartmentId = 1, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.GetWardMasterById(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.GetWardMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }

        #region room Master Unit test cases(Added By janhavi chaudhari)
        [Fact]
        public async void Test_GetRoomMasterById_OkResult()
        {
            int Id = 1;
            RoomMasterRequest data = new RoomMasterRequest { Id = 1, Description = "Test", RoomCode = "10", WardId = 1, GenderId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.GetRoomMasterById(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.GetRoomMasterById(Id);
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
            RoomMasterRequest data = null; //new RoomMasterRequest { Id = 1, Description = "Test", RoomCode = "10", WardId = 1, GenderId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.GetRoomMasterById(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.GetRoomMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async void Test_DeleteRoomMaster_OkResult()
        {
            int Id = 1;
            RoomMasterResponse data = new RoomMasterResponse { Id = 1, IsError = false };
            _coremasterRepository.Setup(x => x.DeleteRoomMaster(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.DeleteRoomMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_AddRoomMaster_BadRequestResult()
        {

            RoomMasterResponse data = new RoomMasterResponse { Id = 1, IsError = true };
            RoomMasterRequest input = new RoomMasterRequest() { Description = "Test", RoomCode = "10", WardId = 1, GenderId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.AddRoomMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.AddRoomMaster(input);
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
            _coremasterRepository.Setup(x => x.AddRoomMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.AddRoomMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        //[Fact]
        //public async void Test_UpdateRoomMaster_NotFoundResult()
        //{

        //    RoomMasterResponse data = new RoomMasterResponse { Id = 1, Message = "Success", Error = true };
        //    RoomMasterRequest input = new RoomMasterRequest() { Description = "Test", RoomCode = "10", WardId = 1, GenderId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
        //    _coremasterRepository.Setup(x => x.UpdateRoomMaster(input)).ReturnsAsync(data);
        //    CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
        //    var result = await coreMasterController.UpdateRoomMaster(input);
        //    var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
        //    Assert.NotNull(result);
        //    Assert.Equal(404, actualResult.StatusCode);
        //    Assert.Equal(actualResult, result);
        //    Assert.IsType<NotFoundObjectResult>(result);
        //}
        [Fact]
        public async void Test_UpdateRoomMaster_OkResult()
        {

            RoomMasterResponse data = new RoomMasterResponse { Id = 1, IsError = false };
            RoomMasterRequest input = new RoomMasterRequest() { Id = 1, Description = "Test", RoomCode = "10", WardId = 1, GenderId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.UpdateRoomMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.UpdateRoomMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region  marital Status Master Unit test cases(Added By janhavi chaudhari)
        [Fact]
        public async void Test_GetMaritalStatusMasterById_OkResult()
        {
            int Id = 1;
            MaritalStatusMasterRequest data = new MaritalStatusMasterRequest { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.GetMaritalStatusMasterById(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.GetMaritalStatusMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_GetMaritalStatusMasterById_NotFoundResult()
        {
            int Id = 1;
            MaritalStatusMasterRequest data = null; //new RoomMasterRequest { Id = 1, Description = "Test", RoomCode = "10", WardId = 1, GenderId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.GetMaritalStatusMasterById(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.GetMaritalStatusMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async void Test_DeleteMaritalStatusMaster_OkResult()
        {
            int Id = 1;
            MaritalStatusMasterResponse data = new MaritalStatusMasterResponse { Id = 1, IsError = false };
            _coremasterRepository.Setup(x => x.DeleteMaritalStatusMaster(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.DeleteMaritalStatusMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_AddMaritalStatusMaster_BadRequestResult()
        {

            MaritalStatusMasterResponse data = new MaritalStatusMasterResponse { Id = 1, IsError = true };
            MaritalStatusMasterRequest input = new MaritalStatusMasterRequest() { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.AddMaritalStatusMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.AddMaritalStatusMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void Test_AddMaritalStatusMaster_OkResult()
        {

            MaritalStatusMasterResponse data = new MaritalStatusMasterResponse { Id = 1, IsError = false };
            MaritalStatusMasterRequest input = new MaritalStatusMasterRequest() { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.AddMaritalStatusMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.AddMaritalStatusMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        //[Fact]
        //public async void Test_UpdateMaritalStatusMaster_NotFoundResult()
        //{

        //    MaritalStatusMasterResponse data = new MaritalStatusMasterResponse { Id = 1, Message = "Success", Error = true };
        //    MaritalStatusMasterRequest input = new MaritalStatusMasterRequest() { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
        //    _coremasterRepository.Setup(x => x.UpdateMaritalStatusMaster(input)).ReturnsAsync(data);
        //    CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
        //    var result = await coreMasterController.UpdateMaritalStatusMaster(input);
        //    var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
        //    Assert.NotNull(result);
        //    Assert.Equal(404, actualResult.StatusCode);
        //    Assert.Equal(actualResult, result);
        //    Assert.IsType<NotFoundObjectResult>(result);
        //}
        [Fact]
        public async void Test_UpdateMaritalStatusMaster_OkResult()
        {

            MaritalStatusMasterResponse data = new MaritalStatusMasterResponse { Id = 1, IsError = false };
            MaritalStatusMasterRequest input = new MaritalStatusMasterRequest() { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.UpdateMaritalStatusMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.UpdateMaritalStatusMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region  occupation Master Unit test cases(Added By janhavi chaudhari)
        [Fact]
        public async void Test_GetOccupationMasterById_OkResult()
        {
            int Id = 1;
            OccupationMasterRequest data = new OccupationMasterRequest { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.GetOccupationMasterById(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.GetOccupationMasterById(Id);
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
            OccupationMasterRequest data = null; //new RoomMasterRequest { Id = 1, Description = "Test", RoomCode = "10", WardId = 1, GenderId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.GetOccupationMasterById(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.GetOccupationMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async void Test_DeleteOccupationMaster_OkResult()
        {
            int Id = 1;
            OccupationMasterResponse data = new OccupationMasterResponse { Id = 1, IsError = false };
            _coremasterRepository.Setup(x => x.DeleteOccupationMaster(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.DeleteOccupationMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_AddOccupationMaster_BadRequestResult()
        {

            OccupationMasterResponse data = new OccupationMasterResponse { Id = 1, IsError = true };
            OccupationMasterRequest input = new OccupationMasterRequest() { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.AddOccupationMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.AddOccupationMaster(input);
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
            _coremasterRepository.Setup(x => x.AddOccupationMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.AddOccupationMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        //[Fact]
        //public async void Test_UpdateOccupationMaster_NotFoundResult()
        //{

        //    OccupationMasterResponse data = new OccupationMasterResponse { Id = 1, Message = "Success", Error = true };
        //    OccupationMasterRequest input = new OccupationMasterRequest() { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
        //    _coremasterRepository.Setup(x => x.UpdateOccupationMaster(input)).ReturnsAsync(data);
        //    CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
        //    var result = await coreMasterController.UpdateOccupationMaster(input);
        //    var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
        //    Assert.NotNull(result);
        //    Assert.Equal(404, actualResult.StatusCode);
        //    Assert.Equal(actualResult, result);
        //    Assert.IsType<NotFoundObjectResult>(result);
        //}
        [Fact]
        public async void Test_UpdateOccupationMaster_OkResult()
        {

            OccupationMasterResponse data = new OccupationMasterResponse { Id = 1, IsError = false };
            OccupationMasterRequest input = new OccupationMasterRequest() { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.UpdateOccupationMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.UpdateOccupationMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region CauseOfDeath Master Unit test cases(Added By janhavi chaudhari)
        [Fact]
        public async void Test_GetCauseOfDeathMasterById_OkResult()
        {
            int Id = 1;
            CauseOfDeathMasterRequest data = new CauseOfDeathMasterRequest { Id = 1, Description = "Test", Code = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.GetCauseOfDeathMasterById(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.GetCauseOfDeathMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_GetCauseOfDeathMasterById_NotFoundResult()
        {
            int Id = 1;
            CauseOfDeathMasterRequest data = null; //new RoomMasterRequest { Id = 1, Description = "Test", RoomCode = "10", WardId = 1, GenderId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.GetCauseOfDeathMasterById(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.GetCauseOfDeathMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async void Test_DeleteCauseOfDeathMaster_OkResult()
        {
            int Id = 1;
            CauseOfDeathMasterResponse data = new CauseOfDeathMasterResponse { Id = 1, IsError = false };
            _coremasterRepository.Setup(x => x.DeleteCauseOfDeathMaster(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.DeleteCauseOfDeathMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_AddCauseOfDeathMaster_BadRequestResult()
        {

            CauseOfDeathMasterResponse data = new CauseOfDeathMasterResponse { Id = 1, IsError = true };
            CauseOfDeathMasterRequest input = new CauseOfDeathMasterRequest() { Id = 1, Description = "Test", Code = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.AddCauseOfDeathMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.AddCauseOfDeathMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void Test_AddCauseOfDeathMaster_OkResult()
        {

            CauseOfDeathMasterResponse data = new CauseOfDeathMasterResponse { Id = 1, IsError = false };
            CauseOfDeathMasterRequest input = new CauseOfDeathMasterRequest() { Id = 1, Description = "Test", Code = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.AddCauseOfDeathMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.AddCauseOfDeathMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        //[Fact]
        //public async void Test_UpdateCauseOfDeathMaster_NotFoundResult()
        //{

        //    CauseOfDeathMasterResponse data = new CauseOfDeathMasterResponse { Id = 1, Message = "Success", Error = true };
        //    CauseOfDeathMasterRequest input = new CauseOfDeathMasterRequest() { Id = 1, Description = "Test", Code = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
        //    _coremasterRepository.Setup(x => x.UpdateCauseOfDeathMaster(input)).ReturnsAsync(data);
        //    CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
        //    var result = await coreMasterController.UpdateCauseOfDeathMaster(input);
        //    var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
        //    Assert.NotNull(result);
        //    Assert.Equal(404, actualResult.StatusCode);
        //    Assert.Equal(actualResult, result);
        //    Assert.IsType<NotFoundObjectResult>(result);
        //}
        [Fact]
        public async void Test_UpdateCauseOfDeathMaster_OkResult()
        {

            CauseOfDeathMasterResponse data = new CauseOfDeathMasterResponse { Id = 1, IsError = false };
            CauseOfDeathMasterRequest input = new CauseOfDeathMasterRequest() { Id = 1, Description = "Test", Code = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.UpdateCauseOfDeathMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.UpdateCauseOfDeathMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region Ward Master CRUD Operation Unit Test Cases (Added By Snehalata Thorat)

        #region Add Ward Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_AddWardMaster_InvalidData_Returns_BadRequest()
        {
            WardMasterResponse wardMasterResponse = new WardMasterResponse { Id = 0, IsError = true };
            WardMasterRequest wardMasterRequest = new WardMasterRequest { Id = 1, WardCode = "", WardName = "", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, DepartmentId = 21, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADD_FAIL, StatusCode = errorcode.STATUS_DATA_ADD_FAIL, Result = wardMasterRequest };
            _coremasterRepository.Setup(x => x.AddWardMaster(wardMasterRequest)).ReturnsAsync(wardMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("WardCode", "Required");
            coreMasterController.ModelState.AddModelError("WardName", "Required");
            //Act
            var result = await coreMasterController.AddWardMaster(wardMasterRequest);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            //Assert.Equal(400, actualResult.StatusCode);
            //Assert.Equal(actualResult, result);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_AddWardMaster_ValidData_Returns_OkResult()
        {
            WardMasterResponse wardMasterResponse = new WardMasterResponse { Id = 1, IsError = false };
            WardMasterRequest wardMasterRequest = new WardMasterRequest { Id = 1, WardCode = "TestWard", WardName = "TestWard", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, DepartmentId = 21, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADDED, StatusCode = errorcode.STATUS_DATA_ADDED, Result = wardMasterRequest };
            _coremasterRepository.Setup(x => x.AddWardMaster(wardMasterRequest)).ReturnsAsync(wardMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.AddWardMaster(wardMasterRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Update Ward Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_UpdateWardMaster_InvalidData_Returns_BadRequest()
        {
            WardMasterResponse wardMasterResponse = new WardMasterResponse { Id = 0, IsError = true };
            WardMasterRequest wardMasterRequest = new WardMasterRequest { Id = 1, WardCode = "", WardName = "", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, DepartmentId = 21, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATE_FAIL, StatusCode = errorcode.STATUS_DATA_UPDATE_FAIL, Result = wardMasterRequest };
            _coremasterRepository.Setup(x => x.UpdateWardMaster(wardMasterRequest)).ReturnsAsync(wardMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("WardCode", "Required");
            coreMasterController.ModelState.AddModelError("WardName", "Required");
            //Act
            var result = await coreMasterController.UpdateWardMaster(wardMasterRequest);
            var actualResult = result as BadRequestResult;
            //Asert
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async void Task_UpdateWardMaster_ValidData_Returns_OkResult()
        {
            WardMasterResponse wardMasterResponse = new WardMasterResponse { Id = 1, IsError = false };
            WardMasterRequest wardMasterRequest = new WardMasterRequest { Id = 1, WardCode = "TestWard", WardName = "TestWard", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, DepartmentId = 21, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATED, StatusCode = errorcode.STATUS_DATA_UPDATED, Result = wardMasterRequest };
            _coremasterRepository.Setup(x => x.UpdateWardMaster(wardMasterRequest)).ReturnsAsync(wardMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.UpdateWardMaster(wardMasterRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Delete Ward Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_DeleteWardMaster_InvalidData_Returns_BadRequest()
        {
            int? Id = null;
            WardMasterResponse wardMasterResponse = new WardMasterResponse { Id = 0, IsError = true };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETE_FAIL, StatusCode = errorcode.STATUS_DATA_DELETE_FAIL, Result = wardMasterResponse };
            _coremasterRepository.Setup(x => x.DeleteWardMaster(Id)).ReturnsAsync(wardMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteWardMaster(Id);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_DeleteWardMaster_ValidData_Returns_OkResult()
        {
            int Id = 1;
            WardMasterResponse wardMasterResponse = new WardMasterResponse { Id = 1, IsError = false };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETED, StatusCode = errorcode.STATUS_DATA_DELETED, Result = wardMasterResponse };
            _coremasterRepository.Setup(x => x.DeleteWardMaster(Id)).ReturnsAsync(wardMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteWardMaster(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Ward Master By Id Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_GetWardMasterById_Returns_OkResult()
        {
            int Id = 1;
            WardMasterResponse wardMasterResponse = new WardMasterResponse { Id = 1, WardCode = "TestWard", WardName = "TestWard", DepartmentId = 21, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = wardMasterResponse };
            _coremasterRepository.Setup(x => x.GetWardMasterById(Id)).ReturnsAsync(wardMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetWardMasterById(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetWardMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            WardMasterResponse wardMasterResponse = new WardMasterResponse { Id = 1, WardCode = "TestWard", WardName = "TestWard", DepartmentId = 21, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = wardMasterResponse };
            _coremasterRepository.Setup(x => x.GetWardMasterById(Id)).ReturnsAsync(wardMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetWardMasterById(Id);
            var actualResult = result as BadRequestResult;
            //Asert
            Assert.NotNull(result);
            // Assert.Equal(400, );
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async void Task_GetWardMasterById_Returns_NotFoundResult()
        {
            int Id = 1;
            WardMasterResponse wardMasterResponse = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = wardMasterResponse };
            _coremasterRepository.Setup(x => x.GetWardMasterById(Id)).ReturnsAsync(wardMasterResponse);
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetWardMasterById(Id);
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Ward Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_GetWardMaster_Returns_OkResult()
        {
            List<WardMasterResponse> data = new List<WardMasterResponse>() { new WardMasterResponse { Id = 1, WardCode = "TestWard", WardName = "TestWard", DepartmentId = 21, FacilityCode = "KLAN" } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _coremasterRepository.Setup(x => x.GetWardMaster()).ReturnsAsync(data);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetWardMaster();
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetWardMaster_Returns_NotFoundResult()
        {
            List<WardMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _coremasterRepository.Setup(x => x.GetWardMaster()).ReturnsAsync(data);
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetWardMaster();
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #endregion

        #region Bed Type Master CRUD Operation Unit Test Cases (Added By Snehalata Thorat)

        #region Add Bed Type Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_AddBedTypeMaster_ValidData_Returns_OkResult()
        {
            BedTypeMasterResponse bedTypeMasterResponse = new BedTypeMasterResponse { Id = 1, IsError = false };
            BedTypeMasterRequest bedTypeMasterRequest = new BedTypeMasterRequest { Id = 1, Description = "TestBed", BedTypeCode = "1T", MaximumBed = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADDED, StatusCode = errorcode.STATUS_DATA_ADDED, Result = bedTypeMasterResponse };
            _coremasterRepository.Setup(x => x.AddBedTypeMaster(bedTypeMasterRequest)).ReturnsAsync(bedTypeMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.AddBedTypeMaster(bedTypeMasterRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_AddBedTypeMaster_InvalidData_Returns_BadRequest()
        {
            BedTypeMasterResponse bedTypeMasterResponse = new BedTypeMasterResponse { Id = 0, IsError = true };
            BedTypeMasterRequest bedTypeMasterRequest = new BedTypeMasterRequest { Id = 1, Description = "", BedTypeCode = "", MaximumBed = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADD_FAIL, StatusCode = errorcode.STATUS_DATA_ADD_FAIL, Result = bedTypeMasterResponse };
            _coremasterRepository.Setup(x => x.AddBedTypeMaster(bedTypeMasterRequest)).ReturnsAsync(bedTypeMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("Description", "Required");
            coreMasterController.ModelState.AddModelError("BedTypeCode", "Required");
            //Act
            var result = await coreMasterController.AddBedTypeMaster(bedTypeMasterRequest);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Update Bed Type Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_UpdateBedTypeMaster_ValidData_Returns_OkResult()
        {
            BedTypeMasterResponse bedTypeMasterResponse = new BedTypeMasterResponse { Id = 1, IsError = false };
            BedTypeMasterRequest bedTypeMasterRequest = new BedTypeMasterRequest { Id = 1, Description = "TestBed", BedTypeCode = "1T", MaximumBed = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 1, UpdatedDate = DateTime.UtcNow, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.UpdateBedTypeMaster(bedTypeMasterRequest)).ReturnsAsync(bedTypeMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.UpdateBedTypeMaster(bedTypeMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_UpdateBedTypeMaster_InvalidData_Returns_BadRequest()
        {
            BedTypeMasterResponse bedTypeMasterResponse = new BedTypeMasterResponse { Id = 0, IsError = true };
            BedTypeMasterRequest bedTypeMasterRequest = new BedTypeMasterRequest { Id = 1, Description = "", BedTypeCode = "", MaximumBed = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATE_FAIL, StatusCode = errorcode.STATUS_DATA_UPDATE_FAIL, Result = bedTypeMasterResponse };
            _coremasterRepository.Setup(x => x.UpdateBedTypeMaster(bedTypeMasterRequest)).ReturnsAsync(bedTypeMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("Description", "Required");
            coreMasterController.ModelState.AddModelError("BedTypeCode", "Required");
            //Act
            var result = await coreMasterController.UpdateBedTypeMaster(bedTypeMasterRequest);
            var actualResult = result as BadRequestResult;
            //Asert
            Assert.NotNull(result);
            //Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }
        #endregion

        #region Delete Bed Type Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_DeleteBedTypeMaster_ValidData_Returns_OkResult()
        {
            int Id = 1;
            BedTypeMasterResponse bedTypeMasterResponse = new BedTypeMasterResponse { Id = 1, IsError = false };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETED, StatusCode = errorcode.STATUS_DATA_DELETED, Result = bedTypeMasterResponse };
            _coremasterRepository.Setup(x => x.DeleteBedTypeMaster(Id)).ReturnsAsync(bedTypeMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteBedTypeMaster(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_DeleteBedTypeMaster_InvalidData_Returns_BadRequest()
        {
            int? Id = null;
            BedTypeMasterResponse bedTypeMasterResponse = new BedTypeMasterResponse { Id = 0, IsError = true };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETE_FAIL, StatusCode = errorcode.STATUS_DATA_DELETE_FAIL, Result = bedTypeMasterResponse };
            _coremasterRepository.Setup(x => x.DeleteBedTypeMaster(Convert.ToInt32(Id))).ReturnsAsync(bedTypeMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteBedTypeMaster(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Bed Type Master By Id Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_GetBedTypeMasterById_Returns_OkResult()
        {
            int Id = 1;
            BedTypeMasterResponse bedTypeMasterResponse = new BedTypeMasterResponse { Id = 1, Description = "TestBed", BedTypeCode = "1T", MaximumBed = 1, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = bedTypeMasterResponse };
            _coremasterRepository.Setup(x => x.GetBedTypeMasterById(Id)).ReturnsAsync(bedTypeMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetBedTypeMasterById(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetBedTypeMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            BedTypeMasterResponse bedTypeMasterResponse = new BedTypeMasterResponse { Id = 1, Description = "TestBed", BedTypeCode = "1T", MaximumBed = 1, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = bedTypeMasterResponse };
            _coremasterRepository.Setup(x => x.GetBedTypeMasterById(Convert.ToInt32(Id))).ReturnsAsync(bedTypeMasterResponse);
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetBedTypeMasterById(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetBedTypeMasterById_Returns_NotFoundResult()
        {
            int Id = 1;
            BedTypeMasterResponse bedTypeMasterResponse = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = bedTypeMasterResponse };
            _coremasterRepository.Setup(x => x.GetBedTypeMasterById(Id)).ReturnsAsync(bedTypeMasterResponse);
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetBedTypeMasterById(Id);
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Bed Type Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_GetBedTypeMaster_Returns_OkResult()
        {
            List<BedTypeMasterResponse> data = new List<BedTypeMasterResponse>() { new BedTypeMasterResponse { Id = 1, Description = "TestBed", BedTypeCode = "1T", MaximumBed = 1, FacilityCode = "KLAN" } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _coremasterRepository.Setup(x => x.GetBedTypeMaster()).ReturnsAsync(data);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetBedTypeMaster();
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetBedTypeMaster_Returns_NotFoundResult()
        {
            List<BedTypeMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _coremasterRepository.Setup(x => x.GetBedTypeMaster()).ReturnsAsync(data);
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetBedTypeMaster();
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion
        #endregion

        #region Clinic Master CRUD Operation Unit Test Cases (Added By Snehalata Thorat)

        #region Add Clinic Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_AddClinicMaster_ValidData_Returns_OkResult()
        {
            ClinicMasterResponse clinicMasterResponse = new ClinicMasterResponse { Id = 1, IsError = false };
            ClinicMasterRequest clinicMasterRequest = new ClinicMasterRequest { Id = 1, ClinicCode = "K0012", ClinicName = "Test Clinic", DoctorName = "Test Doctor", Street1 = "", Street2 = "", Street3 = "", Street4 = "", City = "Test", PostCode = "41200", StateId = 1, PhoneNo = "", FaxNo = "", MobilePhoneNo = "", EmailId = "", IsObsolete = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADDED, StatusCode = errorcode.STATUS_DATA_ADDED, Result = clinicMasterResponse };
            _coremasterRepository.Setup(x => x.AddClinicMaster(clinicMasterRequest)).ReturnsAsync(clinicMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.AddClinicMaster(clinicMasterRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_AddClinicMaster_InvalidData_Returns_BadRequest()
        {
            ClinicMasterResponse clinicMasterResponse = new ClinicMasterResponse { Id = 0, IsError = true };
            ClinicMasterRequest clinicMasterRequest = new ClinicMasterRequest { Id = 1, ClinicCode = "", ClinicName = "", DoctorName = "Test Doctor", Street1 = "", Street2 = "", Street3 = "", Street4 = "", City = "Test", PostCode = "41200", StateId = 1, PhoneNo = "", FaxNo = "", MobilePhoneNo = "", EmailId = "", IsObsolete = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADD_FAIL, StatusCode = errorcode.STATUS_DATA_ADD_FAIL, Result = clinicMasterResponse };
            _coremasterRepository.Setup(x => x.AddClinicMaster(clinicMasterRequest)).ReturnsAsync(clinicMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("ClinicCode", "Required");
            coreMasterController.ModelState.AddModelError("ClinicName", "Required");
            //Act
            var result = await coreMasterController.AddClinicMaster(clinicMasterRequest);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Update Clinic Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_UpdateClinicMaster_ValidData_Returns_OkResult()
        {
            ClinicMasterResponse clinicMasterResponse = new ClinicMasterResponse { Id = 1, IsError = false };
            ClinicMasterRequest clinicMasterRequest = new ClinicMasterRequest { Id = 1, ClinicCode = "K0012", ClinicName = "Test Clinic", DoctorName = "Test Doctor", Street1 = "", Street2 = "", Street3 = "", Street4 = "", City = "Test", PostCode = "41200", StateId = 1, PhoneNo = "", FaxNo = "", MobilePhoneNo = "", EmailId = "", IsObsolete = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATED, StatusCode = errorcode.STATUS_DATA_UPDATED, Result = clinicMasterResponse };
            _coremasterRepository.Setup(x => x.UpdateClinicMaster(clinicMasterRequest)).ReturnsAsync(clinicMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.UpdateClinicMaster(clinicMasterRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_UpdateClinicMaster_InvalidData_Returns_BadRequest()
        {
            ClinicMasterResponse clinicMasterResponse = new ClinicMasterResponse { Id = 0, IsError = true };
            ClinicMasterRequest clinicMasterRequest = new ClinicMasterRequest { Id = 1, ClinicCode = "", ClinicName = "", DoctorName = "Test Doctor", Street1 = "", Street2 = "", Street3 = "", Street4 = "", City = "Test", PostCode = "41200", StateId = 1, PhoneNo = "", FaxNo = "", MobilePhoneNo = "", EmailId = "", IsObsolete = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATE_FAIL, StatusCode = errorcode.STATUS_DATA_UPDATE_FAIL, Result = clinicMasterResponse };
            _coremasterRepository.Setup(x => x.UpdateClinicMaster(clinicMasterRequest)).ReturnsAsync(clinicMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("ClinicCode", "Required");
            coreMasterController.ModelState.AddModelError("ClinicName", "Required");
            //Act
            var result = await coreMasterController.UpdateClinicMaster(clinicMasterRequest);
            var actualResult = result as BadRequestResult;
            //Asert
            Assert.NotNull(result);
            //Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }
        #endregion

        #region Delete Clinic Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_DeleteClinicMaster_ValidData_Returns_OkResult()
        {
            int Id = 1;
            ClinicMasterResponse clinicMasterResponse = new ClinicMasterResponse { Id = 1, IsError = false };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETED, StatusCode = errorcode.STATUS_DATA_DELETED, Result = clinicMasterResponse };
            _coremasterRepository.Setup(x => x.DeleteClinicMaster(Id)).ReturnsAsync(clinicMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteClinicMaster(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_DeleteClinicMaster_InvalidData_Returns_BadRequest()
        {
            int? Id = null;
            ClinicMasterResponse clinicMasterResponse = new ClinicMasterResponse { Id = 0, IsError = true };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETE_FAIL, StatusCode = errorcode.STATUS_DATA_DELETE_FAIL, Result = clinicMasterResponse };
            _coremasterRepository.Setup(x => x.DeleteClinicMaster(Convert.ToInt32(Id))).ReturnsAsync(clinicMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteClinicMaster(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Clinic Master By Id Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_GetClinicMasterById_Returns_OkResult()
        {
            int Id = 1;
            ClinicMasterResponse clinicMasterResponse = new ClinicMasterResponse { Id = 1, ClinicCode = "K0012", ClinicName = "Test Clinic" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = clinicMasterResponse };
            _coremasterRepository.Setup(x => x.GetClinicMasterById(Id)).ReturnsAsync(clinicMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetClinicMasterById(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetClinicMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            ClinicMasterResponse clinicMasterResponse = new ClinicMasterResponse { Id = 1, ClinicCode = "K0012", ClinicName = "Test Clinic" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = clinicMasterResponse };
            _coremasterRepository.Setup(x => x.GetClinicMasterById(Convert.ToInt32(Id))).ReturnsAsync(clinicMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetClinicMasterById(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetClinicMasterById_Returns_NotFoundResult()
        {
            int Id = 1;
            ClinicMasterResponse clinicMasterResponse = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = clinicMasterResponse };
            _coremasterRepository.Setup(x => x.GetClinicMasterById(Id)).ReturnsAsync(clinicMasterResponse);
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetClinicMasterById(Id);
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Clinic Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_GetClinicMaster_Returns_OkResult()
        {
            List<ClinicMasterResponse> data = new List<ClinicMasterResponse>() { new ClinicMasterResponse { Id = 1, ClinicCode = "K0012", ClinicName = "Test Clinic" } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _coremasterRepository.Setup(x => x.GetClinicMaster()).ReturnsAsync(data);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetClinicMaster();
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetClinicMaster_Returns_NotFoundResult()
        {
            int Id = 1;
            List<ClinicMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _coremasterRepository.Setup(x => x.GetClinicMaster()).ReturnsAsync(data);
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetClinicMaster();
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion
        #endregion

        #region Country Master CRUD Operation Unit Test Cases (Added By Snehalata Thorat)

        #region Add Country Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_AddCountry_ValidData_Returns_OkResult()
        {
            CountryMasterResponse countryMasterResponse = new CountryMasterResponse { Id = 1, IsError = false };
            CountryMasterRequest countryMasterRequest = new CountryMasterRequest { Id = 1, Description = "TestCountry", Nationality = "TestCountry", NationalityRate = 1, CountryCode = "TST", CountryCodeNo = 1, Smrpcode = "TS", Smrpdescription = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADDED, StatusCode = errorcode.STATUS_DATA_ADDED, Result = countryMasterResponse };
            _coremasterRepository.Setup(x => x.AddCountry(countryMasterRequest)).ReturnsAsync(countryMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.AddCountry(countryMasterRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_AddCountry_InvalidData_Returns_BadRequest()
        {
            CountryMasterResponse countryMasterResponse = new CountryMasterResponse { Id = 0, IsError = true };
            CountryMasterRequest countryMasterRequest = new CountryMasterRequest { Id = 1, Description = "TestCountry", Nationality = "TestCountry", NationalityRate = 1, CountryCode = "TST", CountryCodeNo = 1, Smrpcode = "TS", Smrpdescription = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADD_FAIL, StatusCode = errorcode.STATUS_DATA_ADD_FAIL, Result = countryMasterResponse };
            _coremasterRepository.Setup(x => x.AddCountry(countryMasterRequest)).ReturnsAsync(countryMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("Description", "Required");
            //Act
            var result = await coreMasterController.AddCountry(countryMasterRequest);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Update Country Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_UpdateCountry_ValidData_Returns_OkResult()
        {
            CountryMasterResponse countryMasterResponse = new CountryMasterResponse { Id = 1, IsError = false };
            CountryMasterRequest countryMasterRequest = new CountryMasterRequest { Id = 1, Description = "TestCountry", Nationality = "TestCountry", NationalityRate = 1, CountryCode = "TST", CountryCodeNo = 1, Smrpcode = "TS", Smrpdescription = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATED, StatusCode = errorcode.STATUS_DATA_UPDATED, Result = countryMasterResponse };
            _coremasterRepository.Setup(x => x.UpdateCountry(countryMasterRequest)).ReturnsAsync(countryMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.UpdateCountry(countryMasterRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_UpdateCountry_InvalidData_Returns_BadRequest()
        {
            CountryMasterResponse countryMasterResponse = new CountryMasterResponse { Id = 0, IsError = true };
            CountryMasterRequest countryMasterRequest = new CountryMasterRequest { Id = 1, Description = "", Nationality = "TestCountry", NationalityRate = 1, CountryCode = "TST", CountryCodeNo = 1, Smrpcode = "TS", Smrpdescription = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATE_FAIL, StatusCode = errorcode.STATUS_DATA_UPDATE_FAIL, Result = countryMasterResponse };
            _coremasterRepository.Setup(x => x.UpdateCountry(countryMasterRequest)).ReturnsAsync(countryMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("Description", "Required");
            //Act
            var result = await coreMasterController.UpdateCountry(countryMasterRequest);
            var actualResult = result as BadRequestResult;
            //Asert
            Assert.NotNull(result);
            //Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }
        #endregion

        #region Delete Country Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_DeleteCountry_ValidData_Returns_OkResult()
        {
            int Id = 1;
            CountryMasterResponse countryMasterResponse = new CountryMasterResponse { Id = 1, IsError = false };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETED, StatusCode = errorcode.STATUS_DATA_DELETED, Result = countryMasterResponse };
            _coremasterRepository.Setup(x => x.DeleteCountry(Id)).ReturnsAsync(countryMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteCountry(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_DeleteCountry_InvalidData_Returns_BadRequest()
        {
            int? Id = null;
            CountryMasterResponse countryMasterResponse = new CountryMasterResponse { Id = 0, IsError = true };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETE_FAIL, StatusCode = errorcode.STATUS_DATA_DELETE_FAIL, Result = countryMasterResponse };
            _coremasterRepository.Setup(x => x.DeleteCountry(Convert.ToInt32(Id))).ReturnsAsync(countryMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteCountry(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Country Master By Id Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_GetCountryById_Returns_OkResult()
        {
            int Id = 1;
            CountryMasterResponse countryMasterResponse = new CountryMasterResponse { Id = 1, Description = "TestCountry", Nationality = "TestCountry", NationalityRate = 1, CountryCode = "TST", CountryCodeNo = 1, Smrpcode = "TS", Smrpdescription = "Test", FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.GetCountryById(Id)).ReturnsAsync(countryMasterResponse);
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = countryMasterResponse };
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetCountryById(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetCountryById_Returns_BadRequestResult()
        {
            int? Id = null;
            CountryMasterResponse countryMasterResponse = new CountryMasterResponse { Id = 1, Description = "TestCountry", Nationality = "TestCountry", NationalityRate = 1, CountryCode = "TST", CountryCodeNo = 1, Smrpcode = "TS", Smrpdescription = "Test", FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = countryMasterResponse };
            _coremasterRepository.Setup(x => x.GetCountryById(Convert.ToInt32(Id))).ReturnsAsync(countryMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetCountryById(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetCountryById_Returns_NotFoundResult()
        {
            int Id = 1;
            CountryMasterResponse countryMasterResponse = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = countryMasterResponse };
            _coremasterRepository.Setup(x => x.GetCountryById(Id)).ReturnsAsync(countryMasterResponse);
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetCountryById(Id);
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Country Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_GetCountryMaster_Returns_OkResult()
        {
            List<CountryMasterResponse> data = new List<CountryMasterResponse> { new CountryMasterResponse { Id = 1, Description = "TestCountry", Nationality = "TestCountry", NationalityRate = 1, CountryCode = "TST", CountryCodeNo = 1, Smrpcode = "TS", Smrpdescription = "Test", FacilityCode = "KLAN" } };
            _coremasterRepository.Setup(x => x.GetCountryMaster()).ReturnsAsync(data);
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetCountryMaster();
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetCountryMaster_Returns_NotFoundResult()
        {
            List<CountryMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _coremasterRepository.Setup(x => x.GetCountryMaster()).ReturnsAsync(data);
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetCountryMaster();
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion
        #endregion

        #region Identification Master CRUD Operation Unit Test Cases (Added By Shubham Gaddam)

        #region Add Identification Master Unit Test Cases(Added By Shubham Gaddam)
        [Fact]
        public async void Task_AddIdentificationMaster_ValidData_Returns_OkResult()
        {
            IdentificationMasterResponse identificationMasterResponse = new IdentificationMasterResponse { Id = 3, IsError = false };
            IdentificationMasterRequest identificationMasterRequest = new IdentificationMasterRequest { Id = 3, Code = "IT003", Description = "Old IC", IdentificationFormat = "Alphanumeric", ExpiryDate = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 409, UpdatedDate = null, MasterIdentificationTypeId = 2, Smrpcode = "1", Smrpdescription = "Old IC", FacilityCode = "KLAN", IsLocal = false };
            _coremasterRepository.Setup(x => x.AddIdentificationMaster(identificationMasterRequest)).ReturnsAsync(identificationMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.AddIdentificationMaster(identificationMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_AddIdentificationMaster_InvalidData_Returns_BadRequestResult()
        {
            IdentificationMasterResponse identificationMasterResponse = new IdentificationMasterResponse { Id = 0, IsError = true };
            IdentificationMasterRequest identificationMasterRequest = new IdentificationMasterRequest { Id = 3, Code = "IT003", Description = "Old IC", IdentificationFormat = "Alphanumeric", ExpiryDate = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 409, UpdatedDate = null, MasterIdentificationTypeId = 2, Smrpcode = "1", Smrpdescription = "Old IC", FacilityCode = "KLAN", IsLocal = false };
            _coremasterRepository.Setup(x => x.AddIdentificationMaster(identificationMasterRequest)).ReturnsAsync(identificationMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.AddIdentificationMaster(identificationMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void AddIdentificationMaster_InvalidObjectPassed_Returns_BadRequest()
        {
            IdentificationMasterResponse identificationMasterResponse = new IdentificationMasterResponse { Id = 0, IsError = true };
            IdentificationMasterRequest identificationMasterRequest = new IdentificationMasterRequest { Id = 3, Code = "IT003", Description = "Old IC", IdentificationFormat = "Alphanumeric", ExpiryDate = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 409, UpdatedDate = null, MasterIdentificationTypeId = 2, Smrpcode = "1", Smrpdescription = "Old IC", FacilityCode = "KLAN", IsLocal = false };
            _coremasterRepository.Setup(x => x.AddIdentificationMaster(identificationMasterRequest)).ReturnsAsync(identificationMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("Code", "Required");
            coreMasterController.ModelState.AddModelError("Description", "Required");
            coreMasterController.ModelState.AddModelError("IdentificationFormat", "Required");
            coreMasterController.ModelState.AddModelError("SMRPCode", "Required");
            coreMasterController.ModelState.AddModelError("SMRPDescription", "Required");
            coreMasterController.ModelState.AddModelError("MasterIdentificationTypeId", "Required");
            //Act
            var result = await coreMasterController.AddIdentificationMaster(identificationMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async void AddIdentificationMaster_ValidObjectPassed_Returns_OkResult()
        {
            IdentificationMasterResponse identificationMasterResponse = new IdentificationMasterResponse { Id = 3, IsError = false };
            IdentificationMasterRequest identificationMasterRequest = new IdentificationMasterRequest { Id = 3, Code = "IT003", Description = "Old IC", IdentificationFormat = "Alphanumeric", ExpiryDate = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 409, UpdatedDate = null, MasterIdentificationTypeId = 2, Smrpcode = "1", Smrpdescription = "Old IC", FacilityCode = "KLAN", IsLocal = false };
            _coremasterRepository.Setup(x => x.AddIdentificationMaster(identificationMasterRequest)).ReturnsAsync(identificationMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.AddIdentificationMaster(identificationMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region Update Identification Master Unit Test Cases(Added By Shubham Gaddam)
        [Fact]
        public async void Task_UpdateIdentificationMaster_ValidData_Returns_OkResult()
        {
            IdentificationMasterResponse identificationMasterResponse = new IdentificationMasterResponse { Id = 3, IsError = false };
            IdentificationMasterRequest identificationMasterRequest = new IdentificationMasterRequest { Id = 3, Code = "IT003", Description = "Old IC", IdentificationFormat = "Alphanumeric", ExpiryDate = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 409, UpdatedDate = null, MasterIdentificationTypeId = 2, Smrpcode = "1", Smrpdescription = "Old IC", FacilityCode = "KLAN", IsLocal = false };
            _coremasterRepository.Setup(x => x.UpdateIdentificationMaster(identificationMasterRequest)).ReturnsAsync(identificationMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.UpdateIdentificationMaster(identificationMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_UpdateIdentificationMaster_InvalidData_Returns_BadRequestResult()
        {
            IdentificationMasterResponse identificationMasterResponse = new IdentificationMasterResponse { Id = 0, IsError = true };
            IdentificationMasterRequest identificationMasterRequest = new IdentificationMasterRequest { Id = 3, Code = "IT003", Description = "Old IC", IdentificationFormat = "Alphanumeric", ExpiryDate = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 409, UpdatedDate = null, MasterIdentificationTypeId = 2, Smrpcode = "1", Smrpdescription = "Old IC", FacilityCode = "KLAN", IsLocal = false };
            _coremasterRepository.Setup(x => x.UpdateIdentificationMaster(identificationMasterRequest)).ReturnsAsync(identificationMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.UpdateIdentificationMaster(identificationMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void UpdateIdentificationMaster_InvalidObjectPassed_Returns_BadRequest()
        {
            IdentificationMasterResponse identificationMasterResponse = new IdentificationMasterResponse { Id = 0, IsError = true };
            IdentificationMasterRequest identificationMasterRequest = new IdentificationMasterRequest { Id = 3, Code = "IT003", Description = "Old IC", IdentificationFormat = "Alphanumeric", ExpiryDate = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 409, UpdatedDate = null, MasterIdentificationTypeId = 2, Smrpcode = "1", Smrpdescription = "Old IC", FacilityCode = "KLAN", IsLocal = false };
            _coremasterRepository.Setup(x => x.UpdateIdentificationMaster(identificationMasterRequest)).ReturnsAsync(identificationMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("Code", "Required");
            coreMasterController.ModelState.AddModelError("Description", "Required");
            coreMasterController.ModelState.AddModelError("IdentificationFormat", "Required");
            coreMasterController.ModelState.AddModelError("SMRPCode", "Required");
            coreMasterController.ModelState.AddModelError("SMRPDescription", "Required");
            coreMasterController.ModelState.AddModelError("MasterIdentificationTypeId", "Required");
            //Act
            var result = await coreMasterController.UpdateIdentificationMaster(identificationMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async void UpdateIdentificationMaster_ValidObjectPassed_Returns_OkResult()
        {
            IdentificationMasterResponse identificationMasterResponse = new IdentificationMasterResponse { Id = 3, IsError = false };
            IdentificationMasterRequest identificationMasterRequest = new IdentificationMasterRequest { Id = 3, Code = "IT003", Description = "Old IC", IdentificationFormat = "Alphanumeric", ExpiryDate = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 409, UpdatedDate = null, MasterIdentificationTypeId = 2, Smrpcode = "1", Smrpdescription = "Old IC", FacilityCode = "KLAN", IsLocal = false };
            _coremasterRepository.Setup(x => x.UpdateIdentificationMaster(identificationMasterRequest)).ReturnsAsync(identificationMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.UpdateIdentificationMaster(identificationMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region Delete Identification Master Unit Test Cases(Added By Shubham Gaddam)
        [Fact]
        public async void Task_DeleteIdentificationMaster_ValidData_Returns_OkResult()
        {
            int Id = 3;
            IdentificationMasterResponse identificationMasterResponse = new IdentificationMasterResponse { Id = 3, IsError = false };
            _coremasterRepository.Setup(x => x.DeleteIdentificationMaster(Id)).ReturnsAsync(identificationMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteIdentificationMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_DeleteIdentificationMaster_InvalidData_Returns_BadRequestResult()
        {
            int Id = -1;
            IdentificationMasterResponse identificationMasterResponse = new IdentificationMasterResponse { Id = 0, IsError = true };
            _coremasterRepository.Setup(x => x.DeleteIdentificationMaster(Id)).ReturnsAsync(identificationMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteIdentificationMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void DeleteIdentificationMaster_InvalidObjectPassed_Returns_BadRequest()
        {
            int? Id = null;
            IdentificationMasterResponse identificationMasterResponse = new IdentificationMasterResponse { Id = 0, IsError = true };
            _coremasterRepository.Setup(x => x.DeleteIdentificationMaster(Id)).ReturnsAsync(identificationMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteIdentificationMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async void DeleteIdentificationMaster_ValidObjectPassed_Returns_OkResult()
        {
            int Id = 3;
            IdentificationMasterResponse identificationMasterResponse = new IdentificationMasterResponse { Id = 3, IsError = false };
            _coremasterRepository.Setup(x => x.DeleteIdentificationMaster(Id)).ReturnsAsync(identificationMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteIdentificationMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region Get Identification Master By Id Unit Test Cases(Added By Shubham Gaddam)
        [Fact]
        public async void Task_GetIdentificationMasterById_Returns_OkResult()
        {
            int Id = 3;
            IdentificationMasterRequest identificationMasterRequest = new IdentificationMasterRequest { Id = 3, Code = "IT003", Description = "Old IC", IdentificationFormat = "Alphanumeric", ExpiryDate = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 409, UpdatedDate = null, MasterIdentificationTypeId = 2, Smrpcode = "1", Smrpdescription = "Old IC", FacilityCode = "KLAN", IsLocal = false };
            _coremasterRepository.Setup(x => x.GetIdentificationMasterById(Id)).ReturnsAsync(identificationMasterRequest);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetIdentificationMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_GetIdentificationMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            IdentificationMasterRequest identificationMasterRequest = new IdentificationMasterRequest { Id = 3, Code = "IT003", Description = "Old IC", IdentificationFormat = "Alphanumeric", ExpiryDate = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 409, UpdatedDate = null, MasterIdentificationTypeId = 2, Smrpcode = "1", Smrpdescription = "Old IC", FacilityCode = "KLAN", IsLocal = false };
            _coremasterRepository.Setup(x => x.GetIdentificationMasterById(Id)).ReturnsAsync(identificationMasterRequest);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetIdentificationMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void GetIdentificationMasterById_Returns_NotFoundResult()
        {
            int Id = 1;
            IdentificationMasterRequest identificationMasterRequest = null;
            _coremasterRepository.Setup(x => x.GetIdentificationMasterById(Id)).ReturnsAsync(identificationMasterRequest);
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetIdentificationMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #endregion

        #region Religion Master CRUD Operation Unit Test Cases (Added By Shubham Gaddam)

        #region Add Religion Master Unit Test Cases(Added By Shubham Gaddam)
        [Fact]
        public async void Task_AddReligionMaster_ValidData_Returns_OkResult()
        {
            ReligionMasterResponse religionMasterResponse = new ReligionMasterResponse { Id = 3, IsError = false };
            ReligionMasterRequest religionMasterRequest = new ReligionMasterRequest { Id = 3, Description = "CATHOLIC", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "CT", Smrpcode = "2", Smrpdescription = "Christianity", FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.AddReligionMaster(religionMasterRequest)).ReturnsAsync(religionMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.AddReligionMaster(religionMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_AddReligionMaster_InvalidData_Returns_BadRequestResult()
        {
            ReligionMasterResponse religionMasterResponse = new ReligionMasterResponse { Id = 0, IsError = true };
            ReligionMasterRequest religionMasterRequest = new ReligionMasterRequest { Id = 3, Description = "CATHOLIC", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "CT", Smrpcode = "2", Smrpdescription = "Christianity", FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.AddReligionMaster(religionMasterRequest)).ReturnsAsync(religionMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.AddReligionMaster(religionMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void AddReligionMaster_InvalidObjectPassed_Returns_BadRequest()
        {
            ReligionMasterResponse religionMasterResponse = new ReligionMasterResponse { Id = 0, IsError = true };
            ReligionMasterRequest religionMasterRequest = new ReligionMasterRequest { Id = 3, Description = "CATHOLIC", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "CT", Smrpcode = "2", Smrpdescription = "Christianity", FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.AddReligionMaster(religionMasterRequest)).ReturnsAsync(religionMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("Code", "Required");
            coreMasterController.ModelState.AddModelError("Description", "Required");
            coreMasterController.ModelState.AddModelError("SMRPCode", "Required");
            coreMasterController.ModelState.AddModelError("SMRPDescription", "Required");
            //Act
            var result = await coreMasterController.AddReligionMaster(religionMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async void AddReligionMaster_ValidObjectPassed_Returns_OkResult()
        {
            ReligionMasterResponse religionMasterResponse = new ReligionMasterResponse { Id = 3, IsError = false };
            ReligionMasterRequest religionMasterRequest = new ReligionMasterRequest { Id = 3, Description = "CATHOLIC", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "CT", Smrpcode = "2", Smrpdescription = "Christianity", FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.AddReligionMaster(religionMasterRequest)).ReturnsAsync(religionMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.AddReligionMaster(religionMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region Update Religion Master Unit Test Cases(Added By Shubham Gaddam)
        [Fact]
        public async void Task_UpdateReligionMaster_ValidData_Returns_OkResult()
        {
            ReligionMasterResponse religionMasterResponse = new ReligionMasterResponse { Id = 3, IsError = false };
            ReligionMasterRequest religionMasterRequest = new ReligionMasterRequest { Id = 3, Description = "CATHOLIC", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "CT", Smrpcode = "2", Smrpdescription = "Christianity", FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.UpdateReligionMaster(religionMasterRequest)).ReturnsAsync(religionMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.UpdateReligionMaster(religionMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_UpdateReligionMaster_InvalidData_Returns_BadRequestResult()
        {
            ReligionMasterResponse religionMasterResponse = new ReligionMasterResponse { Id = 0, IsError = true };
            ReligionMasterRequest religionMasterRequest = new ReligionMasterRequest { Id = 3, Description = "CATHOLIC", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "CT", Smrpcode = "2", Smrpdescription = "Christianity", FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.UpdateReligionMaster(religionMasterRequest)).ReturnsAsync(religionMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.UpdateReligionMaster(religionMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void UpdateReligionMaster_InvalidObjectPassed_Returns_BadRequest()
        {
            ReligionMasterResponse religionMasterResponse = new ReligionMasterResponse { Id = 0, IsError = true };
            ReligionMasterRequest religionMasterRequest = new ReligionMasterRequest { Id = 3, Description = "CATHOLIC", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "CT", Smrpcode = "2", Smrpdescription = "Christianity", FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.UpdateReligionMaster(religionMasterRequest)).ReturnsAsync(religionMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("Code", "Required");
            coreMasterController.ModelState.AddModelError("Description", "Required");
            coreMasterController.ModelState.AddModelError("SMRPCode", "Required");
            coreMasterController.ModelState.AddModelError("SMRPDescription", "Required");
            //Act
            var result = await coreMasterController.UpdateReligionMaster(religionMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async void UpdateReligionMaster_ValidObjectPassed_Returns_OkResult()
        {
            ReligionMasterResponse religionMasterResponse = new ReligionMasterResponse { Id = 3, IsError = false };
            ReligionMasterRequest religionMasterRequest = new ReligionMasterRequest { Id = 3, Description = "CATHOLIC", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "CT", Smrpcode = "2", Smrpdescription = "Christianity", FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.UpdateReligionMaster(religionMasterRequest)).ReturnsAsync(religionMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.UpdateReligionMaster(religionMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region Delete Religion Master Unit Test Cases(Added By Shubham Gaddam)
        [Fact]
        public async void Task_DeleteReligionMaster_ValidData_Returns_OkResult()
        {
            int Id = 3;
            ReligionMasterResponse religionMasterResponse = new ReligionMasterResponse { Id = 3, IsError = false };
            _coremasterRepository.Setup(x => x.DeleteReligionMaster(Id)).ReturnsAsync(religionMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteReligionMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_DeleteReligionMaster_InvalidData_Returns_BadRequestResult()
        {
            int Id = -1;
            ReligionMasterResponse religionMasterResponse = new ReligionMasterResponse { Id = 0, IsError = true };
            _coremasterRepository.Setup(x => x.DeleteReligionMaster(Id)).ReturnsAsync(religionMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteReligionMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void DeleteReligionMaster_InvalidObjectPassed_Returns_BadRequest()
        {
            int? Id = null;
            ReligionMasterResponse religionMasterResponse = new ReligionMasterResponse { Id = 0, IsError = true };
            _coremasterRepository.Setup(x => x.DeleteReligionMaster(Id)).ReturnsAsync(religionMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteReligionMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async void DeleteReligionMaster_ValidObjectPassed_Returns_OkResult()
        {
            int Id = 3;
            ReligionMasterResponse religionMasterResponse = new ReligionMasterResponse { Id = 3, IsError = false };
            _coremasterRepository.Setup(x => x.DeleteReligionMaster(Id)).ReturnsAsync(religionMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteReligionMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region Get Religion Master By Id Unit Test Cases(Added By Shubham Gaddam)
        [Fact]
        public async void Task_GetReligionMasterById_Returns_OkResult()
        {
            int Id = 3;
            ReligionMasterRequest religionMasterRequest = new ReligionMasterRequest { Id = 3, Description = "CATHOLIC", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "CT", Smrpcode = "2", Smrpdescription = "Christianity", FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.GetReligionMasterById(Id)).ReturnsAsync(religionMasterRequest);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetReligionMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_GetReligionMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            ReligionMasterRequest religionMasterRequest = new ReligionMasterRequest { Id = 3, Description = "CATHOLIC", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "CT", Smrpcode = "2", Smrpdescription = "Christianity", FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.GetReligionMasterById(Id)).ReturnsAsync(religionMasterRequest);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetReligionMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void GetReligionMasterById_Returns_NotFoundResult()
        {
            int Id = 3;
            ReligionMasterRequest religionMasterRequest = null;
            _coremasterRepository.Setup(x => x.GetReligionMasterById(Id)).ReturnsAsync(religionMasterRequest);
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetReligionMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #endregion

        #region Relationship Master CRUD Operation Unit Test Cases (Added By Shubham Gaddam)

        #region Add Relationship Master Unit Test Cases(Added By Shubham Gaddam)
        [Fact]
        public async void Task_AddRelationshipMaster_ValidData_Returns_OkResult()
        {
            RelationshipMasterResponse relationshipMasterResponse = new RelationshipMasterResponse { Id = 3, IsError = false };
            RelationshipMasterRequest relationshipMasterRequest = new RelationshipMasterRequest { Id = 3, Description = "FATHER", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "FA", Icstatus = false, Smrpcode = "04", Smrpdescription = "Father", FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.AddRelationshipMaster(relationshipMasterRequest)).ReturnsAsync(relationshipMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.AddRelationshipMaster(relationshipMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_AddRelationshipMaster_InvalidData_Returns_BadRequestResult()
        {
            RelationshipMasterResponse relationshipMasterResponse = new RelationshipMasterResponse { Id = 0, IsError = true };
            RelationshipMasterRequest relationshipMasterRequest = new RelationshipMasterRequest { Id = 3, Description = "FATHER", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "FA", Icstatus = false, Smrpcode = "04", Smrpdescription = "Father", FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.AddRelationshipMaster(relationshipMasterRequest)).ReturnsAsync(relationshipMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.AddRelationshipMaster(relationshipMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void AddRelationshipMaster_InvalidObjectPassed_Returns_BadRequest()
        {
            RelationshipMasterResponse relationshipMasterResponse = new RelationshipMasterResponse { Id = 0, IsError = true };
            RelationshipMasterRequest relationshipMasterRequest = new RelationshipMasterRequest { Id = 3, Description = "FATHER", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "FA", Icstatus = false, Smrpcode = "04", Smrpdescription = "Father", FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.AddRelationshipMaster(relationshipMasterRequest)).ReturnsAsync(relationshipMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("Code", "Required");
            coreMasterController.ModelState.AddModelError("Description", "Required");
            coreMasterController.ModelState.AddModelError("SMRPCode", "Required");
            coreMasterController.ModelState.AddModelError("SMRPDescription", "Required");
            //Act
            var result = await coreMasterController.AddRelationshipMaster(relationshipMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async void AddRelationshipMaster_ValidObjectPassed_Returns_OkResult()
        {
            RelationshipMasterResponse relationshipMasterResponse = new RelationshipMasterResponse { Id = 3, IsError = false };
            RelationshipMasterRequest relationshipMasterRequest = new RelationshipMasterRequest { Id = 3, Description = "FATHER", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "FA", Icstatus = false, Smrpcode = "04", Smrpdescription = "Father", FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.AddRelationshipMaster(relationshipMasterRequest)).ReturnsAsync(relationshipMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.AddRelationshipMaster(relationshipMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region Update Relationship Master Unit Test Cases(Added By Shubham Gaddam)
        [Fact]
        public async void Task_UpdateRelationshipMaster_ValidData_Returns_OkResult()
        {
            RelationshipMasterResponse relationshipMasterResponse = new RelationshipMasterResponse { Id = 3, IsError = false };
            RelationshipMasterRequest relationshipMasterRequest = new RelationshipMasterRequest { Id = 3, Description = "FATHER", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "FA", Icstatus = false, Smrpcode = "04", Smrpdescription = "Father", FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.UpdateRelationshipMaster(relationshipMasterRequest)).ReturnsAsync(relationshipMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.UpdateRelationshipMaster(relationshipMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_UpdateRelationshipMaster_InvalidData_Returns_BadRequestResult()
        {
            RelationshipMasterResponse relationshipMasterResponse = new RelationshipMasterResponse { Id = 0, IsError = true };
            RelationshipMasterRequest relationshipMasterRequest = new RelationshipMasterRequest { Id = 3, Description = "FATHER", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "FA", Icstatus = false, Smrpcode = "04", Smrpdescription = "Father", FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.UpdateRelationshipMaster(relationshipMasterRequest)).ReturnsAsync(relationshipMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.UpdateRelationshipMaster(relationshipMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void UpdateRelationshipMaster_InvalidObjectPassed_Returns_BadRequest()
        {
            RelationshipMasterResponse relationshipMasterResponse = new RelationshipMasterResponse { Id = 0, IsError = true };
            RelationshipMasterRequest relationshipMasterRequest = new RelationshipMasterRequest { Id = 3, Description = "FATHER", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "FA", Icstatus = false, Smrpcode = "04", Smrpdescription = "Father", FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.UpdateRelationshipMaster(relationshipMasterRequest)).ReturnsAsync(relationshipMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("Code", "Required");
            coreMasterController.ModelState.AddModelError("Description", "Required");
            coreMasterController.ModelState.AddModelError("SMRPCode", "Required");
            coreMasterController.ModelState.AddModelError("SMRPDescription", "Required");
            //Act
            var result = await coreMasterController.UpdateRelationshipMaster(relationshipMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async void UpdateRelationshipMaster_ValidObjectPassed_Returns_OkResult()
        {
            RelationshipMasterResponse relationshipMasterResponse = new RelationshipMasterResponse { Id = 3, IsError = false };
            RelationshipMasterRequest relationshipMasterRequest = new RelationshipMasterRequest { Id = 3, Description = "FATHER", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "FA", Icstatus = false, Smrpcode = "04", Smrpdescription = "Father", FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.UpdateRelationshipMaster(relationshipMasterRequest)).ReturnsAsync(relationshipMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.UpdateRelationshipMaster(relationshipMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region Delete Relationship Master Unit Test Cases(Added By Shubham Gaddam)
        [Fact]
        public async void Task_DeleteRelationshipMaster_ValidData_Returns_OkResult()
        {
            int Id = 3;
            RelationshipMasterResponse relationshipMasterResponse = new RelationshipMasterResponse { Id = 3, IsError = false };
            _coremasterRepository.Setup(x => x.DeleteRelationshipMaster(Id)).ReturnsAsync(relationshipMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteRelationshipMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_DeleteRelationshipMaster_InvalidData_Returns_BadRequestResult()
        {
            int Id = -1;
            RelationshipMasterResponse relationshipMasterResponse = new RelationshipMasterResponse { Id = 0, IsError = true };
            _coremasterRepository.Setup(x => x.DeleteRelationshipMaster(Id)).ReturnsAsync(relationshipMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteRelationshipMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void DeleteRelationshipMaster_InvalidObjectPassed_Returns_BadRequest()
        {
            int? Id = null;
            RelationshipMasterResponse relationshipMasterResponse = new RelationshipMasterResponse { Id = 0, IsError = true };
            _coremasterRepository.Setup(x => x.DeleteRelationshipMaster(Id)).ReturnsAsync(relationshipMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteRelationshipMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async void DeleteRelationshipMaster_ValidObjectPassed_Returns_OkResult()
        {
            int Id = 3;
            RelationshipMasterResponse relationshipMasterResponse = new RelationshipMasterResponse { Id = 3, IsError = false };
            _coremasterRepository.Setup(x => x.DeleteRelationshipMaster(Id)).ReturnsAsync(relationshipMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteRelationshipMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region Get Relationship Master By Id Unit Test Cases(Added By Shubham Gaddam)
        [Fact]
        public async void Task_GetRelationshipMasterById_Returns_OkResult()
        {
            int Id = 3;
            RelationshipMasterRequest relationshipMasterRequest = new RelationshipMasterRequest { Id = 3, Description = "FATHER", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "FA", Icstatus = false, Smrpcode = "04", Smrpdescription = "Father", FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.GetRelationshipMasterById(Id)).ReturnsAsync(relationshipMasterRequest);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetRelationshipMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_GetRelationshipMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            RelationshipMasterRequest relationshipMasterRequest = new RelationshipMasterRequest { Id = 3, Description = "FATHER", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, Code = "FA", Icstatus = false, Smrpcode = "04", Smrpdescription = "Father", FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.GetRelationshipMasterById(Id)).ReturnsAsync(relationshipMasterRequest);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetRelationshipMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void GetRelationshipMasterById_Returns_NotFoundResult()
        {
            int Id = 3;
            RelationshipMasterRequest relationshipMasterRequest = null;
            _coremasterRepository.Setup(x => x.GetRelationshipMasterById(Id)).ReturnsAsync(relationshipMasterRequest);
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetRelationshipMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #endregion

        #region Insurance Term Master CRUD Operation Unit Test Cases (Added By Shubham Gaddam)

        #region Add Insurance Term Master Unit Test Cases(Added By Shubham Gaddam)
        [Fact]
        public async void Task_AddInsuranceTermMaster_ValidData_Returns_OkResult()
        {
            InsuranceTermMasterResponse insuranceTermMasterResponse = new InsuranceTermMasterResponse { Id = 3, IsError = false };
            InsuranceTermMasterRequest insuranceTermMasterRequest = new InsuranceTermMasterRequest { Id = 3, InsuranceTerm = "Co-Insurance", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.AddInsuranceTermMaster(insuranceTermMasterRequest)).ReturnsAsync(insuranceTermMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.AddInsuranceTermMaster(insuranceTermMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_AddInsuranceTermMaster_InvalidData_Returns_BadRequestResult()
        {
            InsuranceTermMasterResponse insuranceTermMasterResponse = new InsuranceTermMasterResponse { Id = 0, IsError = true };
            InsuranceTermMasterRequest insuranceTermMasterRequest = new InsuranceTermMasterRequest { Id = 3, InsuranceTerm = "Co-Insurance", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.AddInsuranceTermMaster(insuranceTermMasterRequest)).ReturnsAsync(insuranceTermMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.AddInsuranceTermMaster(insuranceTermMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void AddInsuranceTermMaster_InvalidObjectPassed_Returns_BadRequest()
        {
            InsuranceTermMasterResponse insuranceTermMasterResponse = new InsuranceTermMasterResponse { Id = 0, IsError = true };
            InsuranceTermMasterRequest insuranceTermMasterRequest = new InsuranceTermMasterRequest { Id = 3, InsuranceTerm = "Co-Insurance", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.AddInsuranceTermMaster(insuranceTermMasterRequest)).ReturnsAsync(insuranceTermMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("InsuranceTerm", "Required");
            //Act
            var result = await coreMasterController.AddInsuranceTermMaster(insuranceTermMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async void AddInsuranceTermMaster_ValidObjectPassed_Returns_OkResult()
        {
            InsuranceTermMasterResponse insuranceTermMasterResponse = new InsuranceTermMasterResponse { Id = 3, IsError = false };
            InsuranceTermMasterRequest insuranceTermMasterRequest = new InsuranceTermMasterRequest { Id = 3, InsuranceTerm = "Co-Insurance", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.AddInsuranceTermMaster(insuranceTermMasterRequest)).ReturnsAsync(insuranceTermMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.AddInsuranceTermMaster(insuranceTermMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region Update Insurance Term Master Unit Test Cases(Added By Shubham Gaddam)
        [Fact]
        public async void Task_UpdateInsuranceTermMaster_ValidData_Returns_OkResult()
        {
            InsuranceTermMasterResponse insuranceTermMasterResponse = new InsuranceTermMasterResponse { Id = 3, IsError = false };
            InsuranceTermMasterRequest insuranceTermMasterRequest = new InsuranceTermMasterRequest { Id = 3, InsuranceTerm = "Co-Insurance", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.UpdateInsuranceTermMaster(insuranceTermMasterRequest)).ReturnsAsync(insuranceTermMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.UpdateInsuranceTermMaster(insuranceTermMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_UpdateInsuranceTermMaster_InvalidData_Returns_BadRequestResult()
        {
            InsuranceTermMasterResponse insuranceTermMasterResponse = new InsuranceTermMasterResponse { Id = 0, IsError = true };
            InsuranceTermMasterRequest insuranceTermMasterRequest = new InsuranceTermMasterRequest { Id = 3, InsuranceTerm = "Co-Insurance", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.UpdateInsuranceTermMaster(insuranceTermMasterRequest)).ReturnsAsync(insuranceTermMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.UpdateInsuranceTermMaster(insuranceTermMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void UpdateInsuranceTermMaster_InvalidObjectPassed_Returns_BadRequest()
        {
            InsuranceTermMasterResponse insuranceTermMasterResponse = new InsuranceTermMasterResponse { Id = 0, IsError = true };
            InsuranceTermMasterRequest insuranceTermMasterRequest = new InsuranceTermMasterRequest { Id = 3, InsuranceTerm = "Co-Insurance", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.UpdateInsuranceTermMaster(insuranceTermMasterRequest)).ReturnsAsync(insuranceTermMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("InsuranceTerm", "Required");
            //Act
            var result = await coreMasterController.UpdateInsuranceTermMaster(insuranceTermMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async void UpdateInsuranceTermMaster_ValidObjectPassed_Returns_OkResult()
        {
            InsuranceTermMasterResponse insuranceTermMasterResponse = new InsuranceTermMasterResponse { Id = 3, IsError = false };
            InsuranceTermMasterRequest insuranceTermMasterRequest = new InsuranceTermMasterRequest { Id = 3, InsuranceTerm = "Co-Insurance", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.UpdateInsuranceTermMaster(insuranceTermMasterRequest)).ReturnsAsync(insuranceTermMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.UpdateInsuranceTermMaster(insuranceTermMasterRequest);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region Delete Insurance Term Master Unit Test Cases(Added By Shubham Gaddam)
        [Fact]
        public async void Task_DeleteInsuranceTermMaster_ValidData_Returns_OkResult()
        {
            int Id = 3;
            InsuranceTermMasterResponse insuranceTermMasterResponse = new InsuranceTermMasterResponse { Id = 3, IsError = false };
            _coremasterRepository.Setup(x => x.DeleteInsuranceTermMaster(Id)).ReturnsAsync(insuranceTermMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteInsuranceTermMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_DeleteInsuranceTermMaster_InvalidData_Returns_BadRequestResult()
        {
            int Id = -1;
            InsuranceTermMasterResponse insuranceTermMasterResponse = new InsuranceTermMasterResponse { Id = 0, IsError = true };
            _coremasterRepository.Setup(x => x.DeleteInsuranceTermMaster(Id)).ReturnsAsync(insuranceTermMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteInsuranceTermMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void DeleteInsuranceTermMaster_InvalidObjectPassed_Returns_BadRequest()
        {
            int? Id = null;
            InsuranceTermMasterResponse insuranceTermMasterResponse = new InsuranceTermMasterResponse { Id = 0, IsError = true };
            _coremasterRepository.Setup(x => x.DeleteInsuranceTermMaster(Id)).ReturnsAsync(insuranceTermMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteInsuranceTermMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async void DeleteInsuranceTermMaster_ValidObjectPassed_Returns_OkResult()
        {
            int Id = 3;
            InsuranceTermMasterResponse insuranceTermMasterResponse = new InsuranceTermMasterResponse { Id = 3, IsError = false };
            _coremasterRepository.Setup(x => x.DeleteInsuranceTermMaster(Id)).ReturnsAsync(insuranceTermMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteInsuranceTermMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        #endregion

        #region Get Insurance Term Master By Id Unit Test Cases(Added By Shubham Gaddam)
        [Fact]
        public async void Task_GetInsuranceTermMasterById_Returns_OkResult()
        {
            int Id = 3;
            InsuranceTermMasterRequest insuranceTermMasterRequest = new InsuranceTermMasterRequest { Id = 3, InsuranceTerm = "Co-Insurance", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.GetInsuranceTermMasterById(Id)).ReturnsAsync(insuranceTermMasterRequest);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetInsuranceTermMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Task_GetInsuranceTermMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            InsuranceTermMasterRequest insuranceTermMasterRequest = new InsuranceTermMasterRequest { Id = 3, InsuranceTerm = "Co-Insurance", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.GetInsuranceTermMasterById(Id)).ReturnsAsync(insuranceTermMasterRequest);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetInsuranceTermMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void GetInsuranceTermMasterById_Returns_NotFoundResult()
        {
            int Id = 3;
            InsuranceTermMasterRequest insuranceTermMasterRequest = null;
            _coremasterRepository.Setup(x => x.GetInsuranceTermMasterById(Id)).ReturnsAsync(insuranceTermMasterRequest);
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetInsuranceTermMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundResult)result;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundResult>(result);
        }
        #endregion

        #endregion

        #region Reason Master CRUD Operation (Added by janhavi chaudhari)
        [Fact]
        public async void Test_GetReasonMasterById_OkResult()
        {
            int Id = 1;
            ReasonMasterRequest data = new ReasonMasterRequest { Id = 1, IsBilling=true, IsContract=true, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.GetReasonMasterById(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.GetReasonMasterById(Id);
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
            ReasonMasterRequest data = null; //new RoomMasterRequest { Id = 1, Description = "Test", RoomCode = "10", WardId = 1, GenderId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.GetReasonMasterById(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.GetReasonMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async void Test_DeleteReasonMaster_OkResult()
        {
            int Id = 1;
            ReasonMasterResponse data = new ReasonMasterResponse  { Id = 1, IsError = false };
            _coremasterRepository.Setup(x => x.DeleteReasonMaster(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.DeleteReasonMaster(Id);
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
            _coremasterRepository.Setup(x => x.DeleteReasonMaster(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.DeleteReasonMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void Test_AddReasonMaster_BadRequestResult()
        {

            ReasonMasterResponse data = new ReasonMasterResponse { Id = 1, IsError = true };
            ReasonMasterRequest input = new ReasonMasterRequest() { Id = 1, IsBilling = true, IsContract = true, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.AddReasonMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.AddReasonMaster(input);
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
            _coremasterRepository.Setup(x => x.AddReasonMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.AddReasonMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_UpdateReasonMaster_OkResult()
        {

            ReasonMasterResponse data = new ReasonMasterResponse { Id = 1, IsError = false };
            ReasonMasterRequest input = new ReasonMasterRequest() { Id = 1, IsBilling = true, IsContract = true, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.UpdateReasonMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.UpdateReasonMaster(input);
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
            _coremasterRepository.Setup(x => x.UpdateReasonMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.UpdateReasonMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region Department Master CRUD Operation (Added by janhavi chaudhari)
        [Fact]
        public async void Test_GetDepartmentMasterById_OkResult()
        {
            int Id = 1;
            DepartmentMasterRequest data = new DepartmentMasterRequest { Id = 1, DepartmentCode = "ABC", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.GetDepartmentMasterById(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.GetDepartmentMasterById(Id);
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
            DepartmentMasterRequest data = null; 
            _coremasterRepository.Setup(x => x.GetDepartmentMasterById(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.GetDepartmentMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async void Test_DeleteDepartmentMaster_OkResult()
        {
            int Id = 1;
            DepartmentMasterReponse data = new DepartmentMasterReponse { Id = 1, IsError = false };
            _coremasterRepository.Setup(x => x.DeleteDepartmentMaster(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.DeleteDepartmentMaster(Id);
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
            _coremasterRepository.Setup(x => x.DeleteDepartmentMaster(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.DeleteDepartmentMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void Test_AddDepartmentMaster_BadRequestResult()
        {

            DepartmentMasterReponse data = new DepartmentMasterReponse { Id = 1, IsError = true };
            DepartmentMasterRequest input = new DepartmentMasterRequest() { Id=1, DepartmentCode="ABC", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.AddDepartmentMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.AddDepartmentMaster(input);
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
            _coremasterRepository.Setup(x => x.AddDepartmentMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.AddDepartmentMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_UpdateDepartmentMaster_OkResult()
        {

            DepartmentMasterReponse data = new DepartmentMasterReponse { Id = 1, IsError = false };
            DepartmentMasterRequest input = new DepartmentMasterRequest() { Id = 1, DepartmentCode = "ABC", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.UpdateDepartmentMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.UpdateDepartmentMaster(input);
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
            _coremasterRepository.Setup(x => x.UpdateDepartmentMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.UpdateDepartmentMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region City Master CRUD Operation Unit Test Cases (Added By Snehalata Thorat)

        #region Add City Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_AddCity_ValidData_Returns_OkResult()
        {
            CityMasterResponse cityMasterResponse = new CityMasterResponse { Id = 1, IsError = false };
            CityMasterRequest cityMasterRequest = new CityMasterRequest { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI", Smrpcode = "335", Smrpdescription = "Ayer Baloi", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADDED, StatusCode = errorcode.STATUS_DATA_ADDED, Result = cityMasterResponse };
            _coremasterRepository.Setup(x => x.AddCity(cityMasterRequest)).ReturnsAsync(cityMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.AddCity(cityMasterRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_AddCity_InvalidData_Returns_BadRequest()
        {
            CityMasterResponse cityMasterResponse = new CityMasterResponse { Id = 1, IsError = true };
            CityMasterRequest cityMasterRequest = new CityMasterRequest { Id = 1, StateId = 9, CityCode = "", Description = "", Smrpcode = "335", Smrpdescription = "Ayer Baloi", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADD_FAIL, StatusCode = errorcode.STATUS_DATA_ADD_FAIL, Result = cityMasterResponse };
            _coremasterRepository.Setup(x => x.AddCity(cityMasterRequest)).ReturnsAsync(cityMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("Description", "Required");
            coreMasterController.ModelState.AddModelError("CityCode", "Required");
            //Act
            var result = await coreMasterController.AddCity(cityMasterRequest);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Update City Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_UpdateCity_ValidData_Returns_OkResult()
        {
            CityMasterResponse cityMasterResponse = new CityMasterResponse { Id = 1, IsError = false };
            CityMasterRequest cityMasterRequest = new CityMasterRequest { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI", Smrpcode = "335", Smrpdescription = "Ayer Baloi", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATED, StatusCode = errorcode.STATUS_DATA_UPDATED, Result = cityMasterResponse };
            _coremasterRepository.Setup(x => x.UpdateCity(cityMasterRequest)).ReturnsAsync(cityMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.UpdateCity(cityMasterRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_UpdateCity_InvalidData_Returns_BadRequest()
        {
            CityMasterResponse cityMasterResponse = new CityMasterResponse { Id = 1, IsError = true };
            CityMasterRequest cityMasterRequest = new CityMasterRequest { Id = 1, StateId = 9, CityCode = "", Description = "", Smrpcode = "335", Smrpdescription = "Ayer Baloi", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATE_FAIL, StatusCode = errorcode.STATUS_DATA_UPDATE_FAIL, Result = cityMasterResponse };
            _coremasterRepository.Setup(x => x.UpdateCity(cityMasterRequest)).ReturnsAsync(cityMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("Description", "Required");
            coreMasterController.ModelState.AddModelError("CityCode", "Required");
            //Act
            var result = await coreMasterController.UpdateCity(cityMasterRequest);
            var actualResult = result as BadRequestResult;
            //Asert
            Assert.NotNull(result);
            // Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }
        #endregion

        #region Delete City Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_DeleteCity_ValidData_Returns_OkResult()
        {
            int Id = 1;
            CityMasterResponse cityMasterResponse = new CityMasterResponse { Id = 1, IsError = false };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETED, StatusCode = errorcode.STATUS_DATA_DELETED, Result = cityMasterResponse };
            _coremasterRepository.Setup(x => x.DeleteCity(Id)).ReturnsAsync(cityMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteCity(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_DeleteCity_InvalidData_Returns_BadRequest()
        {
            int? Id = null;
            CityMasterResponse cityMasterResponse = new CityMasterResponse { Id = 0, IsError = true };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETE_FAIL, StatusCode = errorcode.STATUS_DATA_DELETE_FAIL, Result = cityMasterResponse };
            _coremasterRepository.Setup(x => x.DeleteCity(Convert.ToInt32(Id))).ReturnsAsync(cityMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteCity(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get City Master By Id Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_GetCityById_Returns_OkResult()
        {
            int Id = 1;
            CityMasterResponse cityMasterResponse = new CityMasterResponse { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI", Smrpcode = "335", Smrpdescription = "Ayer Baloi" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = cityMasterResponse };
            _coremasterRepository.Setup(x => x.GetCityById(Id)).ReturnsAsync(cityMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetCityById(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetCityById_Returns_BadRequestResult()
        {
            int? Id = null;
            CityMasterResponse cityMasterResponse = new CityMasterResponse { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI", Smrpcode = "335", Smrpdescription = "Ayer Baloi" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = cityMasterResponse };
            _coremasterRepository.Setup(x => x.GetCityById(Convert.ToInt32(Id))).ReturnsAsync(cityMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetCityById(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetCityById_Returns_NotFoundResult()
        {
            int Id = 1;
            CityMasterResponse cityMasterResponse = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = cityMasterResponse };
            _coremasterRepository.Setup(x => x.GetCityById(Id)).ReturnsAsync(cityMasterResponse);
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetCityById(Id);
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get City Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_GetCityMaster_Returns_OkResult()
        {
            List<CityMasterResponse> data = new List<CityMasterResponse>() { new CityMasterResponse { Id = 1, StateId = 9, CityCode = "AB", Description = "AYER BALOI", Smrpcode = "335", Smrpdescription = "Ayer Baloi" } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _coremasterRepository.Setup(x => x.GetCityMaster()).ReturnsAsync(data);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetCityMaster();
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetCityMaster_Returns_NotFoundResult()
        {
            List<CityMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _coremasterRepository.Setup(x => x.GetCityMaster()).ReturnsAsync(data);
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetCityMaster();
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion
        #endregion

        #region Add State Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_AddState_ValidData_Returns_OkResult()
        {
            StateMasterResponse stateMasterResponse = new StateMasterResponse { Id = 1, IsError = false };
            StateMasterRequest stateMasterRequest = new StateMasterRequest { Id = 1, Code = "PER", Description = "PERLIS", CountryId = 130, Smrpcode = "9", Smrpdescription = "PERLIS", EreturnStateId = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADDED, StatusCode = errorcode.STATUS_DATA_ADDED, Result = stateMasterResponse };
            _coremasterRepository.Setup(x => x.AddState(stateMasterRequest)).ReturnsAsync(stateMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.AddState(stateMasterRequest);
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
            _coremasterRepository.Setup(x => x.AddState(stateMasterRequest)).ReturnsAsync(stateMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("Description", "Required");
            coreMasterController.ModelState.AddModelError("Code", "Required");
            //Act
            var result = await coreMasterController.AddState(stateMasterRequest);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Public Holidays Master CRUD Operation (Added by janhavi chaudhari)
        [Fact]
        public async void Test_GetPublicHolidaysMasterById_OkResult()
        {
            int Id = 1;
            PublicHolidaysMasterRequest data = new PublicHolidaysMasterRequest { Id = 1, Reason = "fever", HoliDayName = "friday", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.GetPublicHolidaysMasterById(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.GetPublicHolidaysMasterById(Id);
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
            PublicHolidaysMasterRequest data = null;
            _coremasterRepository.Setup(x => x.GetPublicHolidaysMasterById(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.GetPublicHolidaysMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async void Test_DeletePublicHolidaysMaster_OkResult()
        {
            int Id = 1;
            PublicHolidaysMasterResponse data = new PublicHolidaysMasterResponse { Id = 1, IsError = false };
            _coremasterRepository.Setup(x => x.DeletePublicHolidaysMaster(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.DeletePublicHolidaysMaster(Id);
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
            _coremasterRepository.Setup(x => x.DeletePublicHolidaysMaster(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.DeletePublicHolidaysMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void Test_AddPublicHolidaysMaster_BadRequestResult()
        {

            PublicHolidaysMasterResponse data = new PublicHolidaysMasterResponse { Id = 1, IsError = true };
            PublicHolidaysMasterRequest input = new PublicHolidaysMasterRequest() { Id = 1, Reason = "fever", HoliDayName = "friday", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.AddPublicHolidaysMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.AddPublicHolidaysMaster(input);
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
            PublicHolidaysMasterRequest input = new PublicHolidaysMasterRequest() { Id = 1, Reason="fever", HoliDayName="friday", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.AddPublicHolidaysMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.AddPublicHolidaysMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_UpdatePublicHolidaysMaster_OkResult()
        {

            PublicHolidaysMasterResponse data = new PublicHolidaysMasterResponse { Id = 1, IsError = false };
            PublicHolidaysMasterRequest input = new PublicHolidaysMasterRequest() { Id = 1, Reason = "fever", HoliDayName = "friday", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.UpdatePublicHolidaysMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.UpdatePublicHolidaysMaster(input);
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
            _coremasterRepository.Setup(x => x.UpdatePublicHolidaysMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.UpdatePublicHolidaysMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        #endregion

        #region Mode Of Arrival Master CRUD Operation (Added by janhavi chaudhari)
        [Fact]
        public async void Test_GetModeOfArrivalMasterById_OkResult()
        {
            int Id = 1;
            ModeOfArrivalMasterRequest data = new ModeOfArrivalMasterRequest { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.GetModeOfArrivalMasterById(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.GetModeOfArrivalMasterById(Id);
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
            ModeOfArrivalMasterRequest data = null;
            _coremasterRepository.Setup(x => x.GetModeOfArrivalMasterById(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.GetModeOfArrivalMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async void Test_DeleteModeOfArrivalMaster_OkResult()
        {
            int Id = 1;
            ModeOfArrivalMasterResponse data = new ModeOfArrivalMasterResponse { Id = 1, IsError = false };
            _coremasterRepository.Setup(x => x.DeleteModeOfArrivalMaster(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.DeleteModeOfArrivalMaster(Id);
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
            _coremasterRepository.Setup(x => x.DeleteModeOfArrivalMaster(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.DeleteModeOfArrivalMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void Test_AddModeOfArrivalMaster_BadRequestResult()
        {

            ModeOfArrivalMasterResponse data = new ModeOfArrivalMasterResponse { Id = 1, IsError = true };
            ModeOfArrivalMasterRequest input = new ModeOfArrivalMasterRequest() { Id = 1,Description="test", IsDeleted = false, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.AddModeOfArrivalMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.AddModeOfArrivalMaster(input);
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
            _coremasterRepository.Setup(x => x.AddModeOfArrivalMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.AddModeOfArrivalMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_UpdateModeOfArrivalMaster_OkResult()
        {

            ModeOfArrivalMasterResponse data = new ModeOfArrivalMasterResponse { Id = 1, IsError = false };
            ModeOfArrivalMasterRequest input = new ModeOfArrivalMasterRequest() { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.UpdateModeOfArrivalMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.UpdateModeOfArrivalMaster(input);
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
            _coremasterRepository.Setup(x => x.UpdateModeOfArrivalMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.UpdateModeOfArrivalMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
       
        #endregion

        #region Delivery Method Master CRUD Operation (Added by janhavi chaudhari)
        [Fact]
        public async void Test_GetDeliveryMethodMasterById_OkResult()
        {
            int Id = 1;
            DeliveryMethodMasterRequest data = new DeliveryMethodMasterRequest { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.GetDeliveryMethodMasterById(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.GetDeliveryMethodMasterById(Id);
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
            DeliveryMethodMasterRequest data = null;
            _coremasterRepository.Setup(x => x.GetDeliveryMethodMasterById(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.GetDeliveryMethodMasterById(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async void Test_DeleteDeliveryMethodMaster_OkResult()
        {
            int Id = 1;
            DeliveryMethodMasterResponse data = new DeliveryMethodMasterResponse { Id = 1, IsError = false };
            _coremasterRepository.Setup(x => x.DeleteDeliveryMethodMaster(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.DeleteDeliveryMethodMaster(Id);
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
            _coremasterRepository.Setup(x => x.DeleteDeliveryMethodMaster(Id)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.DeleteDeliveryMethodMaster(Id);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void Test_AddDeliveryMethodMaster_BadRequestResult()
        {

            DeliveryMethodMasterResponse data = new DeliveryMethodMasterResponse { Id = 1, IsError = true };
            DeliveryMethodMasterRequest input = new DeliveryMethodMasterRequest() { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.AddDeliveryMethodMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.AddDeliveryMethodMaster(input);
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
            _coremasterRepository.Setup(x => x.AddDeliveryMethodMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.AddDeliveryMethodMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_UpdateDeliveryMethodMaster_OkResult()
        {

            DeliveryMethodMasterResponse data = new DeliveryMethodMasterResponse { Id = 1, IsError = false };
            DeliveryMethodMasterRequest input = new DeliveryMethodMasterRequest() { Id = 1, Description = "test", IsDeleted = false, FacilityCode = "KLAN" };
            _coremasterRepository.Setup(x => x.UpdateDeliveryMethodMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.UpdateDeliveryMethodMaster(input);
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
            _coremasterRepository.Setup(x => x.UpdateDeliveryMethodMaster(input)).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.UpdateDeliveryMethodMaster(input);
            var actualResult = (Microsoft.AspNetCore.Mvc.BadRequestObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<BadRequestObjectResult>(result);
        }
       
        [Fact]
        public async void Test_GetDeliveryMethodMaster_OkResult()
        {
            List<DeliveryMethodMasterRequest> data = new List<DeliveryMethodMasterRequest>() { new DeliveryMethodMasterRequest { Id = 1, Description = "Open" } };
            _coremasterRepository.Setup(x => x.GetDeliveryMethodMaster()).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.GetDeliveryMethodMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_GetDeliveryMethodMaster_NotFoundResult()
        {
            List<DeliveryMethodMasterRequest> data = null;
            _coremasterRepository.Setup(x => x.GetDeliveryMethodMaster()).ReturnsAsync(data);
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            var result = await coreMasterController.GetDeliveryMethodMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        #region Update State Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_UpdateState_ValidData_Returns_OkResult()
        {
            StateMasterResponse stateMasterResponse = new StateMasterResponse { Id = 1, IsError = false };
            StateMasterRequest stateMasterRequest = new StateMasterRequest { Id = 1, Code = "PER", Description = "PERLIS", CountryId = 130, Smrpcode = "9", Smrpdescription = "PERLIS", EreturnStateId = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATED, StatusCode = errorcode.STATUS_DATA_UPDATED, Result = stateMasterResponse };
            _coremasterRepository.Setup(x => x.UpdateState(stateMasterRequest)).ReturnsAsync(stateMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.UpdateState(stateMasterRequest);
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
            _coremasterRepository.Setup(x => x.UpdateState(stateMasterRequest)).ReturnsAsync(stateMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("Description", "Required");
            coreMasterController.ModelState.AddModelError("Code", "Required");
            //Act
            var result = await coreMasterController.UpdateState(stateMasterRequest);
            var actualResult = result as BadRequestResult;
            //Asert
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }
        #endregion

        #region Delete State Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_DeleteState_ValidData_Returns_OkResult()
        {
            int Id = 1;
            StateMasterResponse stateMasterResponse = new StateMasterResponse { Id = 1, IsError = false };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETED, StatusCode = errorcode.STATUS_DATA_DELETED, Result = stateMasterResponse };
            _coremasterRepository.Setup(x => x.DeleteState(Id)).ReturnsAsync(stateMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteState(Id);
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
            _coremasterRepository.Setup(x => x.DeleteState(Convert.ToInt32(Id))).ReturnsAsync(stateMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteState(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get State Master By Id Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_GetStateById_Returns_OkResult()
        {
            int Id = 1;
            StateMasterResponse stateMasterResponse = new StateMasterResponse { Id = 1, Code = "PER", Description = "PERLIS", CountryId = 130, Smrpcode = "9", Smrpdescription = "PERLIS", EreturnStateId = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = stateMasterResponse };
            _coremasterRepository.Setup(x => x.GetStateById(Id)).ReturnsAsync(stateMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetStateById(Id);
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
            _coremasterRepository.Setup(x => x.GetStateById(Convert.ToInt32(Id))).ReturnsAsync(stateMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetStateById(Convert.ToInt32(Id));
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
            _coremasterRepository.Setup(x => x.GetStateById(Id)).ReturnsAsync(stateMasterResponse);
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetStateById(Id);
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get State Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_GetStateMaster_Returns_OkResult()
        {
            List<StateMasterResponse> data = new List<StateMasterResponse>() { new StateMasterResponse { Id = 1, Code = "PER", Description = "PERLIS", CountryId = 130, Smrpcode = "9", Smrpdescription = "PERLIS", EreturnStateId = null, FacilityCode = "KLAN" } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _coremasterRepository.Setup(x => x.GetStateMaster()).ReturnsAsync(data);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetStateMaster();
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
            _coremasterRepository.Setup(x => x.GetStateMaster()).ReturnsAsync(data);
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetStateMaster();
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion
        #endregion

        #region Privilage Master CRUD Operation Unit Test Cases (Added By Snehalata Thorat)

        #region Add Privilage Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_AddPrivilageMaster_ValidData_Returns_OkResult()
        {
            PrivilageMasterResponse privilageMasterResponse = new PrivilageMasterResponse { Id = 1, IsError = false };
            PrivilageMasterRequest privilageMasterRequest = new PrivilageMasterRequest { Id = 1, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1, IsDiscountPolicy = true, PolicyId = 1, DisplayRemarksInBill = false, DisplayDiscountInBill = true, Remarks = "DISCOUNT FOR REGISTRATION FEE, MO AND PHARMACY", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADDED, StatusCode = errorcode.STATUS_DATA_ADDED, Result = privilageMasterResponse };
            _coremasterRepository.Setup(x => x.AddPrivilageMaster(privilageMasterRequest)).ReturnsAsync(privilageMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.AddPrivilageMaster(privilageMasterRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_AddPrivilageMaster_InvalidData_Returns_BadRequest()
        {
            PrivilageMasterResponse privilageMasterResponse = new PrivilageMasterResponse { Id = 1, IsError = true };
            PrivilageMasterRequest privilageMasterRequest = new PrivilageMasterRequest { Id = 1, PrivilageCode = "", Description = "", StartDate = DateTime.UtcNow, Duration = 1, IsDiscountPolicy = true, PolicyId = 1, DisplayRemarksInBill = false, DisplayDiscountInBill = true, Remarks = "DISCOUNT FOR REGISTRATION FEE, MO AND PHARMACY", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADD_FAIL, StatusCode = errorcode.STATUS_DATA_ADD_FAIL, Result = privilageMasterResponse };
            _coremasterRepository.Setup(x => x.AddPrivilageMaster(privilageMasterRequest)).ReturnsAsync(privilageMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("Description", "Required");
            coreMasterController.ModelState.AddModelError("PrivilageCode", "Required");
            //Act
            var result = await coreMasterController.AddPrivilageMaster(privilageMasterRequest);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Update Privilage Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_UpdatePrivilageMaster_ValidData_Returns_OkResult()
        {
            PrivilageMasterResponse privilageMasterResponse = new PrivilageMasterResponse { Id = 1, IsError = false };
            PrivilageMasterRequest privilageMasterRequest = new PrivilageMasterRequest { Id = 1, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1, IsDiscountPolicy = true, PolicyId = 1, DisplayRemarksInBill = false, DisplayDiscountInBill = true, Remarks = "DISCOUNT FOR REGISTRATION FEE, MO AND PHARMACY", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATED, StatusCode = errorcode.STATUS_DATA_UPDATED, Result = privilageMasterResponse };
            _coremasterRepository.Setup(x => x.UpdatePrivilageMaster(privilageMasterRequest)).ReturnsAsync(privilageMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.UpdatePrivilageMaster(privilageMasterRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_UpdatePrivilageMaster_InvalidData_Returns_BadRequest()
        {
            PrivilageMasterResponse privilageMasterResponse = new PrivilageMasterResponse { Id = 1, IsError = true };
            PrivilageMasterRequest privilageMasterRequest = new PrivilageMasterRequest { Id = 1, PrivilageCode = "", Description = "", StartDate = DateTime.UtcNow, Duration = 1, IsDiscountPolicy = true, PolicyId = 1, DisplayRemarksInBill = false, DisplayDiscountInBill = true, Remarks = "DISCOUNT FOR REGISTRATION FEE, MO AND PHARMACY", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATE_FAIL, StatusCode = errorcode.STATUS_DATA_UPDATE_FAIL, Result = privilageMasterResponse };
            _coremasterRepository.Setup(x => x.UpdatePrivilageMaster(privilageMasterRequest)).ReturnsAsync(privilageMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("Description", "Required");
            coreMasterController.ModelState.AddModelError("PrivilageCode", "Required");
            //Act
            var result = await coreMasterController.UpdatePrivilageMaster(privilageMasterRequest);
            var actualResult = result as BadRequestResult;
            //Asert
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }
        #endregion

        #region Delete Privilage Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_DeletePrivilageMaster_ValidData_Returns_OkResult()
        {
            int Id = 1;
            PrivilageMasterResponse privilageMasterResponse = new PrivilageMasterResponse { Id = 1, IsError = false };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETED, StatusCode = errorcode.STATUS_DATA_DELETED, Result = privilageMasterResponse };
            _coremasterRepository.Setup(x => x.DeletePrivilageMaster(Id)).ReturnsAsync(privilageMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeletePrivilageMaster(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_DeletePrivilageMaster_InvalidData_Returns_BadRequest()
        {
            int? Id = null;
            PrivilageMasterResponse privilageMasterResponse = new PrivilageMasterResponse { Id = 0, IsError = true };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETE_FAIL, StatusCode = errorcode.STATUS_DATA_DELETE_FAIL, Result = privilageMasterResponse };
            _coremasterRepository.Setup(x => x.DeletePrivilageMaster(Convert.ToInt32(Id))).ReturnsAsync(privilageMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeletePrivilageMaster(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Privilage Master By Id Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_GetPrivilageMasterById_Returns_OkResult()
        {
            int Id = 1;
            PrivilageMasterResponse privilageMasterResponse = new PrivilageMasterResponse { Id = 1, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1, IsDiscountPolicy = true, PolicyId = 1, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = privilageMasterResponse };
            _coremasterRepository.Setup(x => x.GetPrivilageMasterById(Id)).ReturnsAsync(privilageMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetPrivilageMasterById(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetPrivilageMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            PrivilageMasterResponse privilageMasterResponse = new PrivilageMasterResponse { Id = 1, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1, IsDiscountPolicy = true, PolicyId = 1, FacilityCode = "KLAN" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = privilageMasterResponse };
            _coremasterRepository.Setup(x => x.GetPrivilageMasterById(Convert.ToInt32(Id))).ReturnsAsync(privilageMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetPrivilageMasterById(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetPrivilageMasterById_Returns_NotFoundResult()
        {
            int Id = 1;
            PrivilageMasterResponse privilageMasterResponse = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = privilageMasterResponse };
            _coremasterRepository.Setup(x => x.GetPrivilageMasterById(Id)).ReturnsAsync(privilageMasterResponse);
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetPrivilageMasterById(Id);
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Privilage Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_GetPrivilageMaster_Returns_OkResult()
        {
            List<PrivilageMasterResponse> data = new List<PrivilageMasterResponse>() { new PrivilageMasterResponse { Id = 1, PrivilageCode = "CA KIDS (OP-SELFPAY)", Description = "CA KIDS (OUTPATIENT-SELF PAY)", StartDate = DateTime.UtcNow, Duration = 1, IsDiscountPolicy = true, PolicyId = 1, FacilityCode = "KLAN" } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _coremasterRepository.Setup(x => x.GetPrivilageMaster()).ReturnsAsync(data);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetPrivilageMaster();
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetPrivilageMaster_Returns_NotFoundResult()
        {
            List<PrivilageMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _coremasterRepository.Setup(x => x.GetPrivilageMaster()).ReturnsAsync(data);
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetPrivilageMaster();
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion
        #endregion

        #region Facility Master CRUD Operation Unit Test Cases (Added By Snehalata Thorat)

        #region Add Facility Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_AddFacilityMaster_ValidData_Returns_OkResult()
        {
            FacilityMasterResponse facilityMasterResponse = new FacilityMasterResponse { Id = 1, IsError = false };
            FacilityMasterRequest facilityMasterRequest = new FacilityMasterRequest { Id = 1, FacilityCode = "KLAN", Description = "Test Facility", PrincipalCurrencyId = 1, ForeignCurrencyId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADDED, StatusCode = errorcode.STATUS_DATA_ADDED, Result = facilityMasterResponse };
            _coremasterRepository.Setup(x => x.AddFacilityMaster(facilityMasterRequest)).ReturnsAsync(facilityMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.AddFacilityMaster(facilityMasterRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_AddFacilityMaster_InvalidData_Returns_BadRequest()
        {
            FacilityMasterResponse facilityMasterResponse = new FacilityMasterResponse { Id = 1, IsError = true };
            FacilityMasterRequest facilityMasterRequest = new FacilityMasterRequest { Id = 1, FacilityCode = "", Description = "", PrincipalCurrencyId = 1, ForeignCurrencyId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADD_FAIL, StatusCode = errorcode.STATUS_DATA_ADD_FAIL, Result = facilityMasterResponse };
            _coremasterRepository.Setup(x => x.AddFacilityMaster(facilityMasterRequest)).ReturnsAsync(facilityMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("Description", "Required");
            coreMasterController.ModelState.AddModelError("FacilityCode", "Required");
            //Act
            var result = await coreMasterController.AddFacilityMaster(facilityMasterRequest);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Update Facility Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_UpdateFacilityMaster_ValidData_Returns_OkResult()
        {
            FacilityMasterResponse facilityMasterResponse = new FacilityMasterResponse { Id = 1, IsError = false };
            FacilityMasterRequest facilityMasterRequest = new FacilityMasterRequest { Id = 1, FacilityCode = "KLAN", Description = "Test Facility", PrincipalCurrencyId = 1, ForeignCurrencyId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATED, StatusCode = errorcode.STATUS_DATA_UPDATED, Result = facilityMasterResponse };
            _coremasterRepository.Setup(x => x.UpdateFacilityMaster(facilityMasterRequest)).ReturnsAsync(facilityMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.UpdateFacilityMaster(facilityMasterRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_UpdateFacilityMaster_InvalidData_Returns_BadRequest()
        {
            FacilityMasterResponse facilityMasterResponse = new FacilityMasterResponse { Id = 1, IsError = true };
            FacilityMasterRequest facilityMasterRequest = new FacilityMasterRequest { Id = 1, FacilityCode = "", Description = "", PrincipalCurrencyId = 1, ForeignCurrencyId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATE_FAIL, StatusCode = errorcode.STATUS_DATA_UPDATE_FAIL, Result = facilityMasterResponse };
            _coremasterRepository.Setup(x => x.UpdateFacilityMaster(facilityMasterRequest)).ReturnsAsync(facilityMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("Description", "Required");
            coreMasterController.ModelState.AddModelError("FacilityCode", "Required");
            //Act
            var result = await coreMasterController.UpdateFacilityMaster(facilityMasterRequest);
            var actualResult = result as BadRequestResult;
            //Asert
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }
        #endregion

        #region Delete Facility Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_DeleteFacilityMaster_ValidData_Returns_OkResult()
        {
            int Id = 1;
            FacilityMasterResponse facilityMasterResponse = new FacilityMasterResponse { Id = 1, IsError = false };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETED, StatusCode = errorcode.STATUS_DATA_DELETED, Result = facilityMasterResponse };
            _coremasterRepository.Setup(x => x.DeleteFacilityMaster(Id)).ReturnsAsync(facilityMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteFacilityMaster(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_DeleteFacilityMaster_InvalidData_Returns_BadRequest()
        {
            int? Id = null;
            FacilityMasterResponse facilityMasterResponse = new FacilityMasterResponse { Id = 0, IsError = true };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETE_FAIL, StatusCode = errorcode.STATUS_DATA_DELETE_FAIL, Result = facilityMasterResponse };
            _coremasterRepository.Setup(x => x.DeleteFacilityMaster(Convert.ToInt32(Id))).ReturnsAsync(facilityMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteFacilityMaster(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Facility Master By Id Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_GetFacilityMasterById_Returns_OkResult()
        {
            int Id = 1;
            FacilityMasterResponse facilityMasterResponse = new FacilityMasterResponse { Id = 1, FacilityCode = "KLAN", Description = "Test Facility", ForeignCurrencyId = 1, PrincipalCurrencyId = 1 };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = facilityMasterResponse };
            _coremasterRepository.Setup(x => x.GetFacilityMasterById(Id)).ReturnsAsync(facilityMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetFacilityMasterById(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetFacilityMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            FacilityMasterResponse facilityMasterResponse = new FacilityMasterResponse { Id = 1, FacilityCode = "KLAN", Description = "Test Facility", ForeignCurrencyId = 1, PrincipalCurrencyId = 1 };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = facilityMasterResponse };
            _coremasterRepository.Setup(x => x.GetFacilityMasterById(Convert.ToInt32(Id))).ReturnsAsync(facilityMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetFacilityMasterById(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetFacilityMasterById_Returns_NotFoundResult()
        {
            int Id = 1;
            FacilityMasterResponse facilityMasterResponse = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = facilityMasterResponse };
            _coremasterRepository.Setup(x => x.GetFacilityMasterById(Id)).ReturnsAsync(facilityMasterResponse);
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetFacilityMasterById(Id);
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Facility Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_GetFacilityMaster_Returns_OkResult()
        {
            List<FacilityMasterResponse> data = new List<FacilityMasterResponse>() { new FacilityMasterResponse { Id = 1, FacilityCode = "KLAN", Description = "Test Facility", ForeignCurrencyId = 1, PrincipalCurrencyId = 1 } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _coremasterRepository.Setup(x => x.GetFacilityMaster()).ReturnsAsync(data);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetFacilityMaster();
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetFacilityMaster_Returns_NotFoundResult()
        {
            List<FacilityMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _coremasterRepository.Setup(x => x.GetFacilityMaster()).ReturnsAsync(data);
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetFacilityMaster();
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion
        #endregion

        #region Disclaimer Master CRUD Operation Unit Test Cases (Added By Snehalata Thorat)

        #region Add Disclaimer Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_AddDisclaimer_ValidData_Returns_OkResult()
        {
            DisclaimerMasterResponse disclaimer = new DisclaimerMasterResponse { Id = 1, IsError = false };
            DisclaimerRequest disclaimerRequest = new DisclaimerRequest { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADDED, StatusCode = errorcode.STATUS_DATA_ADDED, Result = disclaimer };
            _coremasterRepository.Setup(x => x.AddDisclaimer(disclaimerRequest)).ReturnsAsync(disclaimer);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.AddDisclaimer(disclaimerRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_AddDisclaimer_InvalidData_Returns_BadRequest()
        {
            DisclaimerMasterResponse disclaimer = new DisclaimerMasterResponse { Id = 1, IsError = true };
            DisclaimerRequest disclaimerRequest = new DisclaimerRequest { Id = 1, Description = "", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADD_FAIL, StatusCode = errorcode.STATUS_DATA_ADD_FAIL, Result = disclaimer };
            _coremasterRepository.Setup(x => x.AddDisclaimer(disclaimerRequest)).ReturnsAsync(disclaimer);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("Description", "Required");
            //Act
            var result = await coreMasterController.AddDisclaimer(disclaimerRequest);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Update Disclaimer Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_UpdateDisclaimer_ValidData_Returns_OkResult()
        {
            DisclaimerMasterResponse disclaimer = new DisclaimerMasterResponse { Id = 1, IsError = false };
            DisclaimerRequest disclaimerRequest = new DisclaimerRequest { Id = 1, FacilityCode = "KLAN", Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATED, StatusCode = errorcode.STATUS_DATA_UPDATED, Result = disclaimer };
            _coremasterRepository.Setup(x => x.UpdateDisclaimer(disclaimerRequest)).ReturnsAsync(disclaimer);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.UpdateDisclaimer(disclaimerRequest);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_UpdateDisclaimer_InvalidData_Returns_BadRequest()
        {
            DisclaimerMasterResponse disclaimer = new DisclaimerMasterResponse { Id = 1, IsError = true };
            DisclaimerRequest disclaimerRequest = new DisclaimerRequest { Id = 1, FacilityCode = "KLAN", Description = "", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATE_FAIL, StatusCode = errorcode.STATUS_DATA_UPDATE_FAIL, Result = disclaimer };
            _coremasterRepository.Setup(x => x.UpdateDisclaimer(disclaimerRequest)).ReturnsAsync(disclaimer);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("Description", "Required");
            //Act
            var result = await coreMasterController.UpdateDisclaimer(disclaimerRequest);
            var actualResult = result as BadRequestResult;
            //Asert
            Assert.NotNull(result);
            // Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }
        #endregion

        #region Delete Disclaimer Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_DeleteDisclaimer_ValidData_Returns_OkResult()
        {
            int Id = 1;
            DisclaimerMasterResponse disclaimer = new DisclaimerMasterResponse { Id = 1, IsError = false };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETED, StatusCode = errorcode.STATUS_DATA_DELETED, Result = disclaimer };
            _coremasterRepository.Setup(x => x.DeleteDisclaimer(Id)).ReturnsAsync(disclaimer);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteDisclaimer(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_DeleteDisclaimer_InvalidData_Returns_BadRequest()
        {
            int? Id = null;
            DisclaimerMasterResponse disclaimer = new DisclaimerMasterResponse { Id = 0, IsError = true };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETE_FAIL, StatusCode = errorcode.STATUS_DATA_DELETE_FAIL, Result = disclaimer };
            _coremasterRepository.Setup(x => x.DeleteDisclaimer(Convert.ToInt32(Id))).ReturnsAsync(disclaimer);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteDisclaimer(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Disclaimer Master By Id Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_GetDisclaimerById_Returns_OkResult()
        {
            int Id = 1;
            DisclaimerMasterResponse disclaimer = new DisclaimerMasterResponse { Id = 1, Description = "Test" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = disclaimer };
            _coremasterRepository.Setup(x => x.GetDisclaimerById(Id)).ReturnsAsync(disclaimer);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetDisclaimerById(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetDisclaimerById_Returns_BadRequestResult()
        {
            int? Id = null;
            DisclaimerMasterResponse disclaimer = new DisclaimerMasterResponse { Id = 1, Description = "Test" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = disclaimer };
            _coremasterRepository.Setup(x => x.GetDisclaimerById(Convert.ToInt32(Id))).ReturnsAsync(disclaimer);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetDisclaimerById(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetDisclaimerById_Returns_NotFoundResult()
        {
            int Id = 1;
            DisclaimerMasterResponse disclaimer = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = disclaimer };
            _coremasterRepository.Setup(x => x.GetDisclaimerById(Id)).ReturnsAsync(disclaimer);
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetDisclaimerById(Id);
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Disclaimer Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_GetDisclaimer_Returns_OkResult()
        {
            List<DisclaimerMasterResponse> data = new List<DisclaimerMasterResponse>() { new DisclaimerMasterResponse { Id = 1, Description = "Test" } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _coremasterRepository.Setup(x => x.GetDisclaimer()).ReturnsAsync(data);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetDisclaimer();
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetDisclaimer_Returns_NotFoundResult()
        {
            List<DisclaimerMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _coremasterRepository.Setup(x => x.GetDisclaimer()).ReturnsAsync(data);
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetDisclaimer();
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion
        #endregion

        #region TermsandCondition Master CRUD Operation Unit Test Cases (Added By Snehalata Thorat)

        #region Add TermsandCondition Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_AddTerms_ValidData_Returns_OkResult()
        {
            TermsAndConditionMasterResponse terms = new TermsAndConditionMasterResponse { Id = 1, IsError = false };
            TermsAndConditionMasterRequest termsRequest = new TermsAndConditionMasterRequest { Id = 1, TermsAndConditions = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADDED, StatusCode = errorcode.STATUS_DATA_ADDED, Result = terms };
            _coremasterRepository.Setup(x => x.AddTerms(termsRequest)).ReturnsAsync(terms);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.AddTerms(termsRequest);
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
            _coremasterRepository.Setup(x => x.AddTerms(termsRequest)).ReturnsAsync(terms);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("Description", "Required");
            //Act
            var result = await coreMasterController.AddTerms(termsRequest);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Update TermsandCondition Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_UpdateTerms_ValidData_Returns_OkResult()
        {
            TermsAndConditionMasterResponse terms = new TermsAndConditionMasterResponse { Id = 1, IsError = false };
            TermsAndConditionMasterRequest termsRequest = new TermsAndConditionMasterRequest { Id = 1, FacilityCode = "KLAN", TermsAndConditions = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATED, StatusCode = errorcode.STATUS_DATA_UPDATED, Result = terms };
            _coremasterRepository.Setup(x => x.UpdateTerms(termsRequest)).ReturnsAsync(terms);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.UpdateTerms(termsRequest);
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
            _coremasterRepository.Setup(x => x.UpdateTerms(termsRequest)).ReturnsAsync(terms);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("TermsAndConditions", "Required");
            //Act
            var result = await coreMasterController.UpdateTerms(termsRequest);
            var actualResult = result as BadRequestResult;
            //Asert
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result);
        }
        #endregion

        #region Delete TermsandCondition Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_DeleteTerms_ValidData_Returns_OkResult()
        {
            int Id = 1;
            TermsAndConditionMasterResponse terms = new TermsAndConditionMasterResponse { Id = 1, IsError = false };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETED, StatusCode = errorcode.STATUS_DATA_DELETED, Result = terms };
            _coremasterRepository.Setup(x => x.DeleteTerms(Id)).ReturnsAsync(terms);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteTerms(Id);
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
            _coremasterRepository.Setup(x => x.DeleteTerms(Convert.ToInt32(Id))).ReturnsAsync(terms);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteTerms(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get TermsandConcition Master By Id Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_GetTermsById_Returns_OkResult()
        {
            int Id = 1;
            TermsAndConditionMasterResponse terms = new TermsAndConditionMasterResponse { Id = 1, TermsAndConditions = "Test" };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = terms };
            _coremasterRepository.Setup(x => x.GetTermsById(Id)).ReturnsAsync(terms);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetTermsById(Id);
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
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = terms };
            _coremasterRepository.Setup(x => x.GetTermsById(Convert.ToInt32(Id))).ReturnsAsync(terms);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetTermsById(Convert.ToInt32(Id));
            var actualResult = result as BadRequestResult;
            //Asert
            Assert.NotNull(result);
            //Assert.Equal(400, actualResult.StatusCode);
            //Assert.IsType<DataResponse>(actualResult.Value);
            Assert.IsType<BadRequestResult>(result);
        }
        [Fact]
        public async void Task_GetTermsById_Returns_NotFoundResult()
        {
            int Id = 1;
            TermsAndConditionMasterResponse terms = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = terms };
            _coremasterRepository.Setup(x => x.GetTermsById(Id)).ReturnsAsync(terms);
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetTermsById(Id);
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get TermsandCondition Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_GetTerms_Returns_OkResult()
        {
            List<TermsAndConditionMasterResponse> data = new List<TermsAndConditionMasterResponse>() { new TermsAndConditionMasterResponse { Id = 1, TermsAndConditions = "Test" } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _coremasterRepository.Setup(x => x.GetTerms()).ReturnsAsync(data);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetTerms();
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
            _coremasterRepository.Setup(x => x.GetTerms()).ReturnsAsync(data);
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetTerms();
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion
        #endregion

        #region Admission Cases Master CRUD Operation Unit Test Cases (Added By Snehalata Thorat)

        #region Add Admission Cases Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_AddAdmissionCasesMaster_ValidData_Returns_OkResult()
        {
            AdmissionCasesMasterResponse admissionCasesMasterResponse = new AdmissionCasesMasterResponse { Id = 1, IsError = false };
            AdmissionCasesMasterRequest admissionCases = new AdmissionCasesMasterRequest { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1", EstimatedCostFrom = 64320.00, EstimatedCostTo = 4324264564.00, DepositCollected = 500.12, FacilityCode = "KLAN", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADDED, StatusCode = errorcode.STATUS_DATA_ADDED, Result = admissionCasesMasterResponse };
            _coremasterRepository.Setup(x => x.AddAdmissionCasesMaster(admissionCases)).ReturnsAsync(admissionCasesMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.AddAdmissionCasesMaster(admissionCases);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_AddAdmissionCasesMaster_InvalidData_Returns_BadRequest()
        {
            AdmissionCasesMasterResponse admissionCasesMasterResponse = new AdmissionCasesMasterResponse { Id = 1, IsError = true };
            AdmissionCasesMasterRequest admissionCases = new AdmissionCasesMasterRequest { Id = 1, Code = "", Description = "", EstimatedCostFrom = 64320.00, EstimatedCostTo = 4324264564.00, DepositCollected = 500.12, FacilityCode = "KLAN", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_ADD_FAIL, StatusCode = errorcode.STATUS_DATA_ADD_FAIL, Result = admissionCasesMasterResponse };
            _coremasterRepository.Setup(x => x.AddAdmissionCasesMaster(admissionCases)).ReturnsAsync(admissionCasesMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("Description", "Required");
            coreMasterController.ModelState.AddModelError("Code", "Required");
            //Act
            var result = await coreMasterController.AddAdmissionCasesMaster(admissionCases);
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Update Admission Cases Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_UpdateAdmissionCaseMaster_ValidData_Returns_OkResult()
        {
            AdmissionCasesMasterResponse admissionCasesMasterResponse = new AdmissionCasesMasterResponse { Id = 1, IsError = false };
            AdmissionCasesMasterRequest admissionCases = new AdmissionCasesMasterRequest { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1", EstimatedCostFrom = 64320.00, EstimatedCostTo = 4324264564.00, DepositCollected = 500.12, FacilityCode = "KLAN", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATED, StatusCode = errorcode.STATUS_DATA_UPDATED, Result = admissionCasesMasterResponse };
            _coremasterRepository.Setup(x => x.UpdateAdmissionCasesMaster(admissionCases)).ReturnsAsync(admissionCasesMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.UpdateAdmissionCasesMaster(admissionCases);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_UpdateAdmissionCasesMaster_InvalidData_Returns_BadRequest()
        {
            AdmissionCasesMasterResponse admissionCasesMasterResponse = new AdmissionCasesMasterResponse { Id = 1, IsError = true };
            AdmissionCasesMasterRequest admissionCases = new AdmissionCasesMasterRequest { Id = 1, Code = "", Description = "", EstimatedCostFrom = 64320.00, EstimatedCostTo = 4324264564.00, DepositCollected = 500.12, FacilityCode = "KLAN", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 0, UpdatedDate = null };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_UPDATE_FAIL, StatusCode = errorcode.STATUS_DATA_UPDATE_FAIL, Result = admissionCasesMasterResponse };
            _coremasterRepository.Setup(x => x.UpdateAdmissionCasesMaster(admissionCases)).ReturnsAsync(admissionCasesMasterResponse);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            coreMasterController.ModelState.AddModelError("Description", "Required");
            coreMasterController.ModelState.AddModelError("Code", "Required");
            //Act
            var result = await coreMasterController.UpdateAdmissionCasesMaster(admissionCases);
            var actualResult = result as BadRequestResult;
            //Asert
            Assert.NotNull(result);
            //  Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<BadRequestResult>(result);
        }
        #endregion

        #region Delete Admission Case Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_DeleteAdmissionCasesMaster_ValidData_Returns_OkResult()
        {
            int Id = 1;
            AdmissionCasesMasterResponse admissionCases = new AdmissionCasesMasterResponse { Id = 1, IsError = false };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETED, StatusCode = errorcode.STATUS_DATA_DELETED, Result = admissionCases };
            _coremasterRepository.Setup(x => x.DeleteAdmissionCasesMaster(Id)).ReturnsAsync(admissionCases);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteAdmissionCasesMaster(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_DeleteAdmissionCasesMaster_InvalidData_Returns_BadRequest()
        {
            int? Id = null;
            AdmissionCasesMasterResponse admissionCases = new AdmissionCasesMasterResponse { Id = 0, IsError = true };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_DELETE_FAIL, StatusCode = errorcode.STATUS_DATA_DELETE_FAIL, Result = admissionCases };
            _coremasterRepository.Setup(x => x.DeleteAdmissionCasesMaster(Convert.ToInt32(Id))).ReturnsAsync(admissionCases);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.DeleteAdmissionCasesMaster(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Admission Cases Master By Id Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_GetAdmissionCaseMasterById_Returns_OkResult()
        {
            int Id = 1;
            AdmissionCasesMasterResponse admissionCases = new AdmissionCasesMasterResponse { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1", EstimatedCostFrom = 64320.00, EstimatedCostTo = 4324264564.00, DepositCollected = 500.12 };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = admissionCases };
            _coremasterRepository.Setup(x => x.GetAdmissionCaseMasterById(Id)).ReturnsAsync(admissionCases);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetAdmissionCaseMasterById(Id);
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetAdmissionCaseMasterById_Returns_BadRequestResult()
        {
            int? Id = null;
            AdmissionCasesMasterResponse admissionCases = new AdmissionCasesMasterResponse { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1", EstimatedCostFrom = 64320.00, EstimatedCostTo = 4324264564.00, DepositCollected = 500.12 };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = admissionCases };
            _coremasterRepository.Setup(x => x.GetAdmissionCaseMasterById(Convert.ToInt32(Id))).ReturnsAsync(admissionCases);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetAdmissionCaseMasterById(Convert.ToInt32(Id));
            var actualResult = result as BadRequestObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(400, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetAdmissionCaseMasterById_Returns_NotFoundResult()
        {
            int Id = 1;
            FacilityMasterResponse facilityMasterResponse = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = facilityMasterResponse };
            _coremasterRepository.Setup(x => x.GetFacilityMasterById(Id)).ReturnsAsync(facilityMasterResponse);
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetFacilityMasterById(Id);
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion

        #region Get Admission Cases Master Unit Test Cases(Added By Snehalata Thorat)
        [Fact]
        public async void Task_GetAdmissionCasesMaster_Returns_OkResult()
        {
            List<AdmissionCasesMasterResponse> data = new List<AdmissionCasesMasterResponse>() { new AdmissionCasesMasterResponse { Id = 1, Code = "INA1", Description = "INPATIENT ADMISSION 1", EstimatedCostFrom = 64320.00, EstimatedCostTo = 4324264564.00, DepositCollected = 500.12 } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _coremasterRepository.Setup(x => x.GetAdmissionCaseMaster()).ReturnsAsync(data);

            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetAdmissionCaseMaster();
            var actualResult = result as OkObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Task_GetAdmissionCaseMaster_Returns_NotFoundResult()
        {
            List<AdmissionCasesMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _coremasterRepository.Setup(x => x.GetAdmissionCaseMaster()).ReturnsAsync(data);
            //Arrange
            CoreMasterController coreMasterController = new CoreMasterController(_logger.Object, _config, _coremasterRepository.Object, _cacheService.Object);
            //Act
            var result = await coreMasterController.GetFacilityMaster();
            var actualResult = result as NotFoundObjectResult;
            //Asert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion
        #endregion

    
    }
}
