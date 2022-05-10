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
    public class UserMasterControllerUnitTest
    {
        Mock<ILogger<UserMasterController>> _logger;
        IConfiguration _config;
        Mock<IUserMasterRepository> _usermasterRepository;
        Mock<ICacheService> _cacheService;
        ErrorStatus error = new ErrorStatus();
        ErrorStatusCode errorcode = new ErrorStatusCode();

        public UserMasterControllerUnitTest()
        {
            _logger = new Mock<ILogger<UserMasterController>>();
            //_config = new IConfiguration();
            //_config.Setup(x => x.Value).Returns();
            _usermasterRepository = new Mock<IUserMasterRepository>();
            _cacheService = new Mock<ICacheService>();
        }

        [Fact]
        public async void Test_GetUsersMaster_OkResult()
        {
            List<UserMasterResponse> data = new List<UserMasterResponse>() { new UserMasterResponse { Id = 1, UserName = "JANHAVI", Password = "ABC123" } };
            _usermasterRepository.Setup(x => x.GetUsersMaster()).ReturnsAsync(data);
            UserMasterController userMasterController = new UserMasterController(_logger.Object, _config, _usermasterRepository.Object, _cacheService.Object);
            var result = await userMasterController.GetUsersMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_GetUsersMaster_NotFoundResult()
        {
            List<UserMasterResponse> data = null;
            _usermasterRepository.Setup(x => x.GetUsersMaster()).ReturnsAsync(data);
            UserMasterController userMasterController = new UserMasterController(_logger.Object, _config, _usermasterRepository.Object, _cacheService.Object);
            var result = await userMasterController.GetUsersMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            //Assert.Null(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async void Test_GetRolesMaster_OkResult()
        {
            List<RolesMasterResponse> data = new List<RolesMasterResponse>() { new RolesMasterResponse { Id = 1, RoleName = "HR", RoleDescription = "ABC123" } };
            _usermasterRepository.Setup(x => x.GetRolesMaster()).ReturnsAsync(data);
            UserMasterController userMasterController = new UserMasterController(_logger.Object, _config, _usermasterRepository.Object, _cacheService.Object);
            var result = await userMasterController.GetRolesMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_GetRolesMaster_NotFoundResult()
        {
            List<RolesMasterResponse> data = null;
            _usermasterRepository.Setup(x => x.GetRolesMaster()).ReturnsAsync(data);
            UserMasterController userMasterController = new UserMasterController(_logger.Object, _config, _usermasterRepository.Object, _cacheService.Object);
            var result = await userMasterController.GetRolesMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            //Assert.Null(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async void Test_GetUsersInRolesMaster_OkResult()
        {
            List<UsersInRolesMasterResponse> data = new List<UsersInRolesMasterResponse>() { new UsersInRolesMasterResponse { UserId=1, RoleId=1,IsPrimaryRole=true } };
            _usermasterRepository.Setup(x => x.GetUsersInRolesMaster()).ReturnsAsync(data);
            UserMasterController userMasterController = new UserMasterController(_logger.Object, _config, _usermasterRepository.Object, _cacheService.Object);
            var result = await userMasterController.GetUsersInRolesMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_GetUsersInRolesMaster_NotFoundResult()
        {
            List<UsersInRolesMasterResponse> data = null;
            _usermasterRepository.Setup(x => x.GetUsersInRolesMaster()).ReturnsAsync(data);
            UserMasterController userMasterController = new UserMasterController(_logger.Object, _config, _usermasterRepository.Object, _cacheService.Object);
            var result = await userMasterController.GetUsersInRolesMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            //Assert.Null(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async void Test_GetUserLogMaster_OkResult()
        {
            List<UserLogMasterResponse> data = new List<UserLogMasterResponse>() { new UserLogMasterResponse { Id = 1, LoginDatetime = DateTime.Now.Date, LogoutDatetime = DateTime.Now.Date } };
            _usermasterRepository.Setup(x => x.GetUserLogMaster()).ReturnsAsync(data);
            UserMasterController userMasterController = new UserMasterController(_logger.Object, _config, _usermasterRepository.Object, _cacheService.Object);
            var result = await userMasterController.GetUserLogMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_GetUserLogMaster_NotFoundResult()
        {
            List<UserLogMasterResponse> data = null;
            _usermasterRepository.Setup(x => x.GetUserLogMaster()).ReturnsAsync(data);
            UserMasterController userMasterController = new UserMasterController(_logger.Object, _config, _usermasterRepository.Object, _cacheService.Object);
            var result = await userMasterController.GetUserLogMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            //Assert.Null(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async void Test_GetEmployeeTypeMaster_OkResult()
        {
            List<EmployeeTypeMasterResponse> data = new List<EmployeeTypeMasterResponse>() { new EmployeeTypeMasterResponse { Id = 1, EmployeeTypeCode="ABC",EmployeeTypeName="JANHAVI" } };
            _usermasterRepository.Setup(x => x.GetEmployeeTypeMaster()).ReturnsAsync(data);
            UserMasterController userMasterController = new UserMasterController(_logger.Object, _config, _usermasterRepository.Object, _cacheService.Object);
            var result = await userMasterController.GetEmployeeTypeMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_GetEmployeeTypeMaster_NotFoundResult()
        {
            List<EmployeeTypeMasterResponse> data = null;
            _usermasterRepository.Setup(x => x.GetEmployeeTypeMaster()).ReturnsAsync(data);
            UserMasterController userMasterController = new UserMasterController(_logger.Object, _config, _usermasterRepository.Object, _cacheService.Object);
            var result = await userMasterController.GetEmployeeTypeMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            //Assert.Null(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async void Test_GetUserProfileMaster_OkResult()
        {
            List<UserProfileMasterResponse> data = new List<UserProfileMasterResponse>() { new UserProfileMasterResponse { Id = 1, FirstName="janhavi", LastName="Raut" } };
            _usermasterRepository.Setup(x => x.GetUserProfileMaster()).ReturnsAsync(data);
            UserMasterController userMasterController = new UserMasterController(_logger.Object, _config, _usermasterRepository.Object, _cacheService.Object);
            var result = await userMasterController.GetEmployeeTypeMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.OkObjectResult)result;
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async void Test_GetUserProfileMaster_NotFoundResult()
        {
            List<UserProfileMasterResponse> data = null;
            _usermasterRepository.Setup(x => x.GetUserProfileMaster()).ReturnsAsync(data);
            UserMasterController userMasterController = new UserMasterController(_logger.Object, _config, _usermasterRepository.Object, _cacheService.Object);
            var result = await userMasterController.GetUserProfileMaster();
            var actualResult = (Microsoft.AspNetCore.Mvc.NotFoundObjectResult)result;
            //Assert.Null(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.Equal(actualResult, result);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        #region User Master Unit test cases (Added by Snehalata Thorat)
        [Fact]
        public async void Test_GetUserAdditionalDepartmentMaster_Returns_OkResult()
        {
            List<UserAdditionalDepartmentMasterResponse> data = new List<UserAdditionalDepartmentMasterResponse>() { new UserAdditionalDepartmentMasterResponse { Id = 1, UserId = 1, DepartmentId = 1 } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _usermasterRepository.Setup(x => x.GetUserAdditionalDepartmentMaster()).ReturnsAsync(data);
           //Arrange
            UserMasterController userMasterController = new UserMasterController(_logger.Object, _config, _usermasterRepository.Object, _cacheService.Object);
           //Act
            var result = await userMasterController.GetUserAdditionalDepartmentMaster();
            var actualResult = result as OkObjectResult;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Test_GetUserAdditionalDepartmentMaster_Returns_NotFoundResult()
        {
            List<UserAdditionalDepartmentMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _usermasterRepository.Setup(x => x.GetUserAdditionalDepartmentMaster()).ReturnsAsync(data);
            //Arrange
            UserMasterController userMasterController = new UserMasterController(_logger.Object, _config, _usermasterRepository.Object, _cacheService.Object);
            //Act
            var result = await userMasterController.GetUserAdditionalDepartmentMaster();
            var actualResult = result as NotFoundObjectResult;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Test_GetTemporaryDepartmentMaster_Returns_OkResult()
        {
            List<TemporaryDepartmentMasterResponse> data = new List<TemporaryDepartmentMasterResponse>() { new TemporaryDepartmentMasterResponse { Id = 1, UserId = 1, DepartmentId = 1 } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _usermasterRepository.Setup(x => x.GetTemporaryDepartmentMaster()).ReturnsAsync(data);
            //Arrange
            UserMasterController userMasterController = new UserMasterController(_logger.Object, _config, _usermasterRepository.Object, _cacheService.Object);
            //Act
            var result = await userMasterController.GetTemporaryDepartmentMaster();
            var actualResult = result as OkObjectResult;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Test_GetTemporaryDepartmentMaster_Returns_NotFoundResult()
        {
            List<TemporaryDepartmentMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _usermasterRepository.Setup(x => x.GetTemporaryDepartmentMaster()).ReturnsAsync(data);
            //Arrange
            UserMasterController userMasterController = new UserMasterController(_logger.Object, _config, _usermasterRepository.Object, _cacheService.Object);
            //Act
            var result = await userMasterController.GetTemporaryDepartmentMaster();
            var actualResult = result as NotFoundObjectResult;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Test_GetSpecialPermissionsMaster_Returns_OkResult()
        {
            List<SpecialPermissionsMasterResponse> data = new List<SpecialPermissionsMasterResponse>() { new SpecialPermissionsMasterResponse { Id = 1, Description= "Has Registration Edit Access" } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _usermasterRepository.Setup(x => x.GetSpecialPermissionsMaster()).ReturnsAsync(data);
            //Arrange
            UserMasterController userMasterController = new UserMasterController(_logger.Object, _config, _usermasterRepository.Object, _cacheService.Object);
            //Act
            var result = await userMasterController.GetSpecialPermissionMaster();
            var actualResult = result as OkObjectResult;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Test_GetSpecialPermissionsMaster_Returns_NotFoundResult()
        {
            List<SpecialPermissionsMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _usermasterRepository.Setup(x => x.GetSpecialPermissionsMaster()).ReturnsAsync(data);
            //Arrange
            UserMasterController userMasterController = new UserMasterController(_logger.Object, _config, _usermasterRepository.Object, _cacheService.Object);
            //Act
            var result = await userMasterController.GetSpecialPermissionMaster();
            var actualResult = result as NotFoundObjectResult;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Test_GetRolesInSpecialPermissionsMaster_Returns_OkResult()
        {
            List<RolesInSpecialPermissionsMasterResponse> data = new List<RolesInSpecialPermissionsMasterResponse>() { new RolesInSpecialPermissionsMasterResponse { Id = 1, RoleId=1,SpecialPermissionId=1 } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _usermasterRepository.Setup(x => x.GetRolesInSpecialPermissionsMaster()).ReturnsAsync(data);
            //Arrange
            UserMasterController userMasterController = new UserMasterController(_logger.Object, _config, _usermasterRepository.Object, _cacheService.Object);
            //Act
            var result = await userMasterController.GetRolesInSpecialPermissionMaster();
            var actualResult = result as OkObjectResult;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Test_GetRolesInSpecialPermissionsMaster_Returns_NotFoundResult()
        {
            List<RolesInSpecialPermissionsMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _usermasterRepository.Setup(x => x.GetRolesInSpecialPermissionsMaster()).ReturnsAsync(data);
            //Arrange
            UserMasterController userMasterController = new UserMasterController(_logger.Object, _config, _usermasterRepository.Object, _cacheService.Object);
            //Act
            var result = await userMasterController.GetRolesInSpecialPermissionMaster();
            var actualResult = result as NotFoundObjectResult;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Test_GetRolewiseShortcutMaster_Returns_OkResult()
        {
            List<RolewiseShortcutMasterResponse> data = new List<RolewiseShortcutMasterResponse>() { new RolewiseShortcutMasterResponse { Id = 1, RoleId = 1, ShortcutKey = 1,MenuId=1 } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _usermasterRepository.Setup(x => x.GetRolewiseShortcutMaster()).ReturnsAsync(data);
            //Arrange
            UserMasterController userMasterController = new UserMasterController(_logger.Object, _config, _usermasterRepository.Object, _cacheService.Object);
            //Act
            var result = await userMasterController.GetRolewiseShortcutMaster();
            var actualResult = result as OkObjectResult;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Test_GetRolewiseShortcutMaster_Returns_NotFoundResult()
        {
            List<RolewiseShortcutMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _usermasterRepository.Setup(x => x.GetRolewiseShortcutMaster()).ReturnsAsync(data);
            //Arrange
            UserMasterController userMasterController = new UserMasterController(_logger.Object, _config, _usermasterRepository.Object, _cacheService.Object);
            //Act
            var result = await userMasterController.GetRolewiseShortcutMaster();
            var actualResult = result as NotFoundObjectResult;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Test_GetRoleCommunicationMaster_Returns_OkResult()
        {
            List<RoleCommunicationMasterResponse> data = new List<RoleCommunicationMasterResponse>() { new RoleCommunicationMasterResponse { RoleId = 1, AvailableRoleId = 1 } };
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FOUND, StatusCode = errorcode.STATUS_DATA_FOUND, Result = data };
            _usermasterRepository.Setup(x => x.GetRoleCommunicationMaster()).ReturnsAsync(data);
            //Arrange
            UserMasterController userMasterController = new UserMasterController(_logger.Object, _config, _usermasterRepository.Object, _cacheService.Object);
            //Act
            var result = await userMasterController.GetRoleCommunicationMaster();
            var actualResult = result as OkObjectResult;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(200, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        [Fact]
        public async void Test_GetRoleCommunicationMaster_Returns_NotFoundResult()
        {
            List<RoleCommunicationMasterResponse> data = null;
            DataResponse response = new DataResponse { Message = error.STATUS_DATA_FAIL, StatusCode = errorcode.STATUS_DATA_FAIL, Result = data };
            _usermasterRepository.Setup(x => x.GetRoleCommunicationMaster()).ReturnsAsync(data);
            //Arrange
            UserMasterController userMasterController = new UserMasterController(_logger.Object, _config, _usermasterRepository.Object, _cacheService.Object);
            //Act
            var result = await userMasterController.GetRoleCommunicationMaster();
            var actualResult = result as NotFoundObjectResult;
            //Assert
            Assert.NotNull(result);
            Assert.Equal(404, actualResult.StatusCode);
            Assert.IsType<DataResponse>(actualResult.Value);
        }
        #endregion
    }
}
