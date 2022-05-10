using AutoMapper;
using CA_MCare21_CrossCuttingConcern.Caching;
using CA_MCare21_MasterAPI.Data;
using CA_MCare21_MasterAPI.Entities;
using CA_MCare21_MasterAPI.Models;
using CA_MCare21_MasterAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CA_MCare21_MasterAPIUnitTest
{
    public class UserMasterServiceUnitTest
    {
        [Fact]
        public void GetUserAdditionalDepartmentMaster_Returns_UserAdditionalDepartmentMaster()
        {
            //Setup DbContext and DbSet mock  
            //Arrange
            //var registrationDbContext = new Mock<IRegistrationDBContext>();
            var httpContextProcessor = new Mock<IHttpContextAccessor>();
            //var malaysiaDbContext = new Mock<MalaysiaDbContext>();
            // var indonesiaDbContext = new Mock<IndonesiaDbContext>();
            //var vietnamDbContext = new Mock<VietnamDbContext>();
            var cacheService = new Mock<ICacheService>();
            var mapper = new Mock<IMapper>();
            //Arrange
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "MalaysiaDatabase")
                            .Options;
            var malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
            malaysiaDbContext.CaUsersAdditionalDepartments.Add(new CaUsersAdditionalDepartment { Id = 1, UserId = 1, DepartmentId = 2, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<UserAdditionalDepartmentMasterResponse>() { new UserAdditionalDepartmentMasterResponse { Id = 1, DepartmentId=4, UserId=4} };
            mapper.Setup(x => x.Map<IEnumerable<UserAdditionalDepartmentMasterResponse>>(It.IsAny<List<CaUsersAdditionalDepartment>>())).Returns(mapperResp);

            //Execute method of SUT (UserMasterRepository)  
            var userMasterService = new UserMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var useradditionalDepartment = userMasterService.GetUserAdditionalDepartmentMaster().Result.ToList();

            Assert.NotNull(useradditionalDepartment);
            Assert.Equal(1, Convert.ToInt32(useradditionalDepartment.Count()));
        }

        [Fact]
        public void GetEmployeeTypeMaster_Returns_EmployeeTyoeMaster()
        {
            //Setup DbContext and DbSet mock  
            //Arrange
            var httpContextProcessor = new Mock<IHttpContextAccessor>();
            //var malaysiaDbContext = new Mock<MalaysiaDbContext>();
            // var indonesiaDbContext = new Mock<IndonesiaDbContext>();
            //var vietnamDbContext = new Mock<VietnamDbContext>();
            var cacheService = new Mock<ICacheService>();
            var mapper = new Mock<IMapper>();
            //Arrange
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "MalaysiaDatabase")
                            .Options;
            var malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
            malaysiaDbContext.CaEmployeeTypeMasters.Add(new CaEmployeeTypeMaster { Id = 1, EmployeeTypeCode = "HOSPITAL STAFF", EmployeeTypeName = "HOSPITAL STAFF", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<EmployeeTypeMasterResponse>() { new EmployeeTypeMasterResponse { Id = 1, EmployeeTypeName = "janhavi", EmployeeTypeCode = "abc"} };
            mapper.Setup(x => x.Map<IEnumerable<EmployeeTypeMasterResponse>>(It.IsAny<List<CaEmployeeTypeMaster>>())).Returns(mapperResp);
            //Execute method of SUT (UserMasterRepository)  
            var userMasterService = new UserMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var employeeTypeMasters = userMasterService.GetEmployeeTypeMaster().Result.ToList();

            Assert.NotNull(employeeTypeMasters);
            Assert.Equal(1, Convert.ToInt32(employeeTypeMasters.Count()));
        }

        [Fact]
        public void GetUserProfileMaster_Returns_UserProfileMaster()
        {
            //Setup DbContext and DbSet mock  
            //Arrange
            var httpContextProcessor = new Mock<IHttpContextAccessor>();
            //var malaysiaDbContext = new Mock<MalaysiaDbContext>();
            // var indonesiaDbContext = new Mock<IndonesiaDbContext>();
            //var vietnamDbContext = new Mock<VietnamDbContext>();
            var cacheService = new Mock<ICacheService>();
            var mapper = new Mock<IMapper>();
            //Arrange
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "MalaysiaDatabase")
                            .Options;
            var malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
            malaysiaDbContext.CaUserProfiles.Add(new CaUserProfile { Id = 1, UserId = 1, FirstName = "ADMIN", LastName = " ", PrimaryEmailId = "Care21Admin@columbiaasia.com", SecondaryEmailId = " ", Mobile = "012 602 3115", Birthdate = DateTime.Now.Date, Street1 = " ", Street2 = " ", Street3 = " ", Street4 = " ", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, DepartmentId = 2, EmployeeTypeId = 1, EmployeeId = 54, UserProfileCode = "NURNADIHA.S", ReportingManagerId = 0, ElectronicSignature = null, MenuId = 0, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<UserProfileMasterResponse>() { new UserProfileMasterResponse { Id = 1, DepartmentId = 4, UserId = 1 } };
            mapper.Setup(x => x.Map<IEnumerable<UserProfileMasterResponse>>(It.IsAny<List<CaUserProfile>>())).Returns(mapperResp);

            //Execute method of SUT (UserMasterRepository)  
            var userMasterService = new UserMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var userProfiles = userMasterService.GetUserProfileMaster().Result.ToList();

            Assert.NotNull(userProfiles);
            Assert.Equal(1, Convert.ToInt32(userProfiles.Count()));
        }
        
        [Fact]
        public void GetTemporaryDepartmentMaster_Returns_TemporaryDepartmentMaster()
        {
            //Setup DbContext and DbSet mock  
            //Arrange
            var httpContextProcessor = new Mock<IHttpContextAccessor>();
            //var malaysiaDbContext = new Mock<MalaysiaDbContext>();
            // var indonesiaDbContext = new Mock<IndonesiaDbContext>();
            //var vietnamDbContext = new Mock<VietnamDbContext>();
            var cacheService = new Mock<ICacheService>();
            var mapper = new Mock<IMapper>();
            //Arrange
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "MalaysiaDatabase")
                            .Options;
            var malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
            malaysiaDbContext.CaTemporaryDepartments.Add(new CaTemporaryDepartment { Id = 1, UserId = 1, DepartmentId = 1, EndDate = DateTime.Now.Date, IsObsolete = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<TemporaryDepartmentMasterResponse>() { new TemporaryDepartmentMasterResponse { Id = 1, DepartmentId=4, UserId=1 } };
            mapper.Setup(x => x.Map<IEnumerable<TemporaryDepartmentMasterResponse>>(It.IsAny<List<CaTemporaryDepartment>>())).Returns(mapperResp);

            //Execute method of SUT (UserMasterRepository)  
            var userMasterService = new UserMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var temporaryDepartments = userMasterService.GetTemporaryDepartmentMaster().Result.ToList();

            Assert.NotNull(temporaryDepartments);
            Assert.Equal(1, Convert.ToInt32(temporaryDepartments.Count()));

        }

        [Fact]
        public void GetRolesMaster_Returns_GetRolesMaster()
        {
            //Setup DbContext and DbSet mock  
            //Arrange
            //var registrationDbContext = new Mock<IRegistrationDBContext>();
            var httpContextProcessor = new Mock<IHttpContextAccessor>();
            //var malaysiaDbContext = new Mock<MalaysiaDbContext>();
            // var indonesiaDbContext = new Mock<IndonesiaDbContext>();
            //var vietnamDbContext = new Mock<VietnamDbContext>();
            var cacheService = new Mock<ICacheService>();
            var mapper = new Mock<IMapper>();
            //Arrange
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "MalaysiaDatabase")
                            .Options;
            var malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
            malaysiaDbContext.CaRoles.Add(new CaRole { Id = 1, RoleName = "DOCTOR", RoleDescription = "DOCTOR", CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, IsDeleted = false, MenuId = 0, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<RolesMasterResponse>() { new RolesMasterResponse { Id = 1, RoleName="developer" } };
            mapper.Setup(x => x.Map<IEnumerable<RolesMasterResponse>>(It.IsAny<List<CaRole>>())).Returns(mapperResp);

            //Execute method of SUT (UserMasterRepository)  
            var userMasterService = new UserMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var rolesMaster = userMasterService.GetRolesMaster().Result.ToList();

            Assert.NotNull(rolesMaster);
            Assert.Equal(1, Convert.ToInt32(rolesMaster.Count()));
        }

        [Fact]
        public void GetUserLogMaster_Returns_GetUserLogMaster()
        {
            //Setup DbContext and DbSet mock  
            //Arrange
            //var registrationDbContext = new Mock<IRegistrationDBContext>();
            var httpContextProcessor = new Mock<IHttpContextAccessor>();
            //var malaysiaDbContext = new Mock<MalaysiaDbContext>();
            // var indonesiaDbContext = new Mock<IndonesiaDbContext>();
            //var vietnamDbContext = new Mock<VietnamDbContext>();
            var cacheService = new Mock<ICacheService>();
            var mapper = new Mock<IMapper>();
            //Arrange
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "MalaysiaDatabase")
                            .Options;
            var malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
            malaysiaDbContext.CaUserLogs.Add(new CaUserLog { Id = 1, UserId = 1, LoginDatetime = DateTime.Now.Date, LogoutDatetime = DateTime.Now.Date, IsActive = true, Machine = "TEST", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<UserLogMasterResponse>() { new UserLogMasterResponse { Id=1, Machine="abc", UserId=4  } };
            mapper.Setup(x => x.Map<IEnumerable<UserLogMasterResponse>>(It.IsAny<List<CaUserLog>>())).Returns(mapperResp);

            //Execute method of SUT (UserMasterRepository)  
            var userMasterService = new UserMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var userLogMaster = userMasterService.GetUserLogMaster().Result.ToList();

            Assert.NotNull(userLogMaster);
            Assert.Equal(1, Convert.ToInt32(userLogMaster.Count()));
        }

        [Fact]
        public void GetUsersInRolesMaster_Returns_GetUsersInRolesMaster()
        {
            //Setup DbContext and DbSet mock  
            //Arrange
            //var registrationDbContext = new Mock<IRegistrationDBContext>();
            var httpContextProcessor = new Mock<IHttpContextAccessor>();
            //var malaysiaDbContext = new Mock<MalaysiaDbContext>();
            // var indonesiaDbContext = new Mock<IndonesiaDbContext>();
            //var vietnamDbContext = new Mock<VietnamDbContext>();
            var cacheService = new Mock<ICacheService>();
            var mapper = new Mock<IMapper>();
            //Arrange
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "MalaysiaDatabase")
                            .Options;
            var malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
            malaysiaDbContext.CaUsersInRoles.Add(new CaUsersInRole { RoleId = 1, UserId = 1, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, IsPrimaryRole = true, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<UsersInRolesMasterResponse>() { new UsersInRolesMasterResponse { IsPrimaryRole=true, RoleId=1 } };
            mapper.Setup(x => x.Map<IEnumerable<UsersInRolesMasterResponse>>(It.IsAny<List<CaUsersInRole>>())).Returns(mapperResp);
            //Execute method of SUT (UserMasterRepository)  
            var userMasterService = new UserMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var usersInRoles = userMasterService.GetUsersInRolesMaster().Result.ToList();

            Assert.NotNull(usersInRoles);
            Assert.Equal(1, Convert.ToInt32(usersInRoles.Count()));
        }

        [Fact]
        public void GetUsersMaster_Returns_GetUsersMaster()
        {
            //Setup DbContext and DbSet mock  
            //Arrange
            //var registrationDbContext = new Mock<IRegistrationDBContext>();
            var httpContextProcessor = new Mock<IHttpContextAccessor>();
            //var malaysiaDbContext = new Mock<MalaysiaDbContext>();
            // var indonesiaDbContext = new Mock<IndonesiaDbContext>();
            //var vietnamDbContext = new Mock<VietnamDbContext>();
            var cacheService = new Mock<ICacheService>();
            var mapper = new Mock<IMapper>();
            //Arrange
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "MalaysiaDatabase")
                            .Options;
            var malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
            malaysiaDbContext.CaUsers.Add(new CaUser { Id = 1, UserName = "JANHAVI", Password = "ABC123", CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, UserCode = "ABC", IsDeleted = false, ElectronicSignaturePassword = "ABC123", AduserId = "JANHAVI", IsAdauthenticationApplicable = true, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            var mapperResp = new List<UserMasterResponse>() { new UserMasterResponse { Id = 1, Password="ABC"} };
            mapper.Setup(x => x.Map<IEnumerable<UserMasterResponse>>(It.IsAny<List<CaUser>>())).Returns(mapperResp);
            //Execute method of SUT (UserMasterRepository)  
            var userMasterService = new UserMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var usersMaster = userMasterService.GetUsersMaster().Result.ToList();

            Assert.NotNull(usersMaster);
            Assert.Equal(1, Convert.ToInt32(usersMaster.Count()));
        }

        [Fact]
        public void GetSpecialPermissionMaster_Returns_SpecialPermissionMaster()
        {
            //Setup DbContext and DbSet mock  
            //Arrange
            var httpContextProcessor = new Mock<IHttpContextAccessor>();
            //var malaysiaDbContext = new Mock<MalaysiaDbContext>();
            // var indonesiaDbContext = new Mock<IndonesiaDbContext>();
            //var vietnamDbContext = new Mock<VietnamDbContext>();
            var cacheService = new Mock<ICacheService>();
            var mapper = new Mock<IMapper>();
            //Arrange
            //set up mock in-memory dbcontext 
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "MalaysiaDatabase")
                            .Options;
            var malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
            malaysiaDbContext.CaSpecialPermissions.Add(new CaSpecialPermission { Id = 1, Description = "Has Registration Access", FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<SpecialPermissionsMasterResponse>() { new SpecialPermissionsMasterResponse {Id = 1, Description="test"} };
            mapper.Setup(x => x.Map<IEnumerable<SpecialPermissionsMasterResponse>>(It.IsAny<List<CaSpecialPermission>>())).Returns(mapperResp);

            //Execute method of SUT (UserMasterRepository)  
            var userMasterService = new UserMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var specialPermissions = userMasterService.GetSpecialPermissionsMaster().Result.ToList();

            Assert.NotNull(specialPermissions);
            Assert.Equal(1, Convert.ToInt32(specialPermissions.Count()));

        }

        [Fact]
        public void GetRolesInSpecialPermissionMaster_Returns_RolesInSpecialPermission()
        {
            //Setup DbContext and DbSet mock  
            //Arrange
            var httpContextProcessor = new Mock<IHttpContextAccessor>();
            //var malaysiaDbContext = new Mock<MalaysiaDbContext>();
            // var indonesiaDbContext = new Mock<IndonesiaDbContext>();
            //var vietnamDbContext = new Mock<VietnamDbContext>();
            var cacheService = new Mock<ICacheService>();
            var mapper = new Mock<IMapper>();
            //Arrange
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "MalaysiaDatabase")
                            .Options;
            var malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
            malaysiaDbContext.CaRolesInSpecialPermissions.Add(new CaRolesInSpecialPermission { Id = 1, RoleId = 1, HasRights = false, IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<RolesInSpecialPermissionsMasterResponse>() { new RolesInSpecialPermissionsMasterResponse { RoleId = 1, Id=1 } };
            mapper.Setup(x => x.Map<IEnumerable<RolesInSpecialPermissionsMasterResponse>>(It.IsAny<List<CaRolesInSpecialPermission>>())).Returns(mapperResp);
            //Execute method of SUT (UserMasterRepository)  
            var userMasterService = new UserMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var rolesInSpecialPermissions = userMasterService.GetRolesInSpecialPermissionsMaster().Result.ToList();

            Assert.NotNull(rolesInSpecialPermissions);
            Assert.Equal(1, Convert.ToInt32(rolesInSpecialPermissions.Count()));

        }

        [Fact]
        public void GetRolewiseShortcutMaster_Returns_RolewiseShortcut()
        {
            //Setup DbContext and DbSet mock  
            //Arrange
            var httpContextProcessor = new Mock<IHttpContextAccessor>();
            //var malaysiaDbContext = new Mock<MalaysiaDbContext>();
            // var indonesiaDbContext = new Mock<IndonesiaDbContext>();
            //var vietnamDbContext = new Mock<VietnamDbContext>();
            var cacheService = new Mock<ICacheService>();
            var mapper = new Mock<IMapper>();
            //Arrange
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "MalaysiaDatabase")
                            .Options;
            var malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
            malaysiaDbContext.CaRolewiseShortcuts.Add(new CaRolewiseShortcut { Id = 1, ShortcutKey = 1, MenuId = 1, RoleId = 1, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null, ShortcutText = " ", FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<RolewiseShortcutMasterResponse>() { new RolewiseShortcutMasterResponse { RoleId = 1, ShortcutKey=1 } };
            mapper.Setup(x => x.Map<IEnumerable<RolewiseShortcutMasterResponse>>(It.IsAny<List<CaRolewiseShortcut>>())).Returns(mapperResp);

            //Execute method of SUT (UserMasterRepository)  
            var userMasterService = new UserMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var rolewiseShortcuts = userMasterService.GetRolewiseShortcutMaster().Result.ToList();

            Assert.NotNull(rolewiseShortcuts);
            Assert.Equal(1, Convert.ToInt32(rolewiseShortcuts.Count()));

        }

        [Fact]
        public void GetRoleCommunicationMaster_Returns_RoleCommunication()
        {
            //Setup DbContext and DbSet mock  
            //Arrange
            var httpContextProcessor = new Mock<IHttpContextAccessor>();
            //var malaysiaDbContext = new Mock<MalaysiaDbContext>();
            // var indonesiaDbContext = new Mock<IndonesiaDbContext>();
            //var vietnamDbContext = new Mock<VietnamDbContext>();
            var cacheService = new Mock<ICacheService>();
            var mapper = new Mock<IMapper>();
            //Arrange
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "MalaysiaDatabase")
                            .Options;
            var malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
            malaysiaDbContext.CaRoleCommunications.Add(new CaRoleCommunication { RoleId = 1, AvailableRoleId = 1, CreatedBy = 1, CreatedDate = DateTime.Now.Date, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<RoleCommunicationMasterResponse>() { new RoleCommunicationMasterResponse { RoleId=1, AvailableRoleId=1 } };
            mapper.Setup(x => x.Map<IEnumerable<RoleCommunicationMasterResponse>>(It.IsAny<List<CaRoleCommunication>>())).Returns(mapperResp);
            //Execute method of SUT (UserMasterRepository)  
            var userMasterService = new UserMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var roleCommunications = userMasterService.GetRoleCommunicationMaster().Result.ToList();

            Assert.NotNull(roleCommunications);
            Assert.Equal(1, Convert.ToInt32(roleCommunications.Count()));

        }
    }
}
