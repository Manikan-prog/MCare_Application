using Microsoft.AspNetCore.Http;
using CA_MCare21_CrossCuttingConcern.Caching;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CA_MCare21_MasterAPI.Data;
using CA_MCare21_MasterAPI.Entities;
using AutoMapper;
using CA_MCare21_MasterAPI.Services;
using CA_MCare21_MasterAPI.Models;
using CA_MCare21_MasterAPI.Repositories;

namespace CA_MCare21_MasterAPIUnitTest
{
    public class CoreMasterServiceUnitTest
    {
        Mock<ICoreMasterRepository> _coremasterRepository;
        [Fact]
        public void GetFacilityMaster_Returns_FacilityMaster()
        {
            //Setup DbContext and DbSet mock  
            //Arrange
            var httpContextProcessor = new Mock<IHttpContextAccessor>();
            //var malaysiaDbContext = new Mock<MalaysiaDbContext>();
            // var indonesiaDbContext = new Mock<IndonesiaDbContext>();
            //var vietnamDbContext = new Mock<VietnamDbContext>();
            var cacheService = new Mock<ICacheService>();
            var mapper = new  Mock <IMapper>();
            //Arrange
            //set up mock in-memory dbcontext
            var optionsMalaysia = new DbContextOptionsBuilder<MalaysiaDbContext>()
                            .UseInMemoryDatabase(databaseName: "MalaysiaDatabase")
                            .Options;
            var malaysiaDbContext = new MalaysiaDbContext(optionsMalaysia);
            malaysiaDbContext.CaFacilityMasters.Add(new CaFacilityMaster { Id = 1, FacilityCode = "KLAN", Description = " ", PrincipalCurrencyId = null, ForeignCurrencyId = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null });
            malaysiaDbContext.SaveChanges();
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            //Execute method of SUT (UserMasterRepository)  
            var coreMasterService = new CoreMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object,mapper.Object);

            //Act
            var facilityMasters = coreMasterService.GetFacilityMaster().Result.ToList();

            Assert.NotNull(facilityMasters);
            Assert.Equal(1, facilityMasters.Count());
        }

        [Fact]
        public void GetDocumentTypeMaster_Returns_DocumentTypeMaster()
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
            malaysiaDbContext.CaDocumentTypeMasters.Add(new CaDocumentTypeMaster { Id = 1, DocumentCode = "CASH", DocumentName = "Cash", MappedTableName = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, MappedFieldName = null, IsForApproval = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            //Execute method of SUT (UserMasterRepository)  
            var coreMasterService = new CoreMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var documentTypeMasters = coreMasterService.GetDocumentTypeMaster().Result.ToList();

            Assert.NotNull(documentTypeMasters);
            Assert.Equal(1, documentTypeMasters.Count());
        }
        
        [Fact]
        public void GetGlobalSettings_Returns_GetGlobalSettings()
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
            malaysiaDbContext.CaGlobalSettings.Add(new CaGlobalSetting { Id = 1, ModuleId = 1, ItemKey = "test", ItemValue = "test", Description = "test", IsActive = true, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, ValidationType = 1, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            //Execute method of SUT (UserMasterRepository)  
            var coreMasterService = new CoreMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var globalSettings = coreMasterService.GetGlobalSettings().Result.ToList();


            Assert.NotNull(globalSettings);
            Assert.Equal(1, globalSettings.Count());
        }
        [Fact]
        public void GetDisclaimerMaster_Returns_DisclaimerMaster()
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
            malaysiaDbContext.CaDisclaimers.Add(new CaDisclaimer { Id = 1, Description = "Please take a note", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            //Execute method of SUT (UserMasterRepository)  
            var coreMasterService = new CoreMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var disclaimers = coreMasterService.GetDisclaimer().Result.ToList();

            Assert.NotNull(disclaimers);
            Assert.Equal(1, disclaimers.Count());
        }
        [Fact]
        public void GetDepartmentMaster_Returns_DepartmentMaster()
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
            malaysiaDbContext.CaDepartmentMasters.Add(new CaDepartmentMaster { Id = 1, DepartmentCode = "ER", Description = "EMERGENCY ROOM", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN", AddInfo = null });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            //Execute method of SUT (UserMasterRepository)  
            var coreMasterService = new CoreMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var departmentMasters = coreMasterService.GetDepartmentMaster().Result.ToList();

            Assert.NotNull(departmentMasters);
            Assert.Equal(1, departmentMasters.Count());

        }
        [Fact]
        public void GetStatusMaster_Returns_StatusMaster()
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
            malaysiaDbContext.CaStatusMasters.Add(new CaStatusMaster { Id = 1, Description= "Open",StatusCode= "Ope", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            //Execute method of SUT (UserMasterRepository)  
            var coreMasterService = new CoreMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var statusMasters = coreMasterService.GetStatusMaster().Result.ToList();

            Assert.NotNull(statusMasters);
            Assert.Equal(1, statusMasters.Count());

        }
        [Fact]
        public void GetGlobalSettingsValidator_Returns_GetGlobalSettingsValidator()
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
            malaysiaDbContext.CaGlobalSettingsValidators.Add(new CaGlobalSettingsValidator
            {
                Id = 1,
                GlobalSettingsId = 1,
                IsString = true,
                StringMaximumLength = 150,
                StringMinimumLength = 25,
                IsStringAllowSpecialChars = true,
                IsStringAllowNumerals = true,
                IsStringAllowBlankSpaces = false,
                IsNumericAllow = true,
                NumericMinimumValue = 20,
                NumericMaximumValue = 100,
                IsNumericDecimal = true,
                IsNumericAllowExponential = true,
                NumericPrecision = "",
                CreatedBy = 1,
                CreatedDate = DateTime.Now.Date,
                UpdatedBy = null,
                UpdatedDate = null,
                ValidateExpression = "",
                FacilityCode = "KLAN"
            });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            //Execute method of SUT (UserMasterRepository)  
            var coreMasterService = new CoreMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var globalSettingsValidator = coreMasterService.GetGlobalSettingsValidator().Result.ToList();


            Assert.NotNull(globalSettingsValidator);
            Assert.Equal(1, globalSettingsValidator.Count());
        }

        [Fact]
        public void GetMessageMaster_Returns_GetMessageMaster()
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
            malaysiaDbContext.CaMessageMasters.Add(new CaMessageMaster
            {
                Id = 1,
                EnUs = "test",
                Description = "test",
                IsActive = true,
                IsDeleted = false,
                DefaultMessage = "test",
                MessageType = 0,
                CreatedBy = 1,
                CreatedDate = DateTime.Now.Date,
                UpdatedBy = null,
                UpdatedDate = null,
                FacilityCode = "KLAN"
            });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            //Execute method of SUT (UserMasterRepository)  
            var coreMasterService = new CoreMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var messageMaster = coreMasterService.GetMessageMaster().Result.ToList();


            Assert.NotNull(messageMaster);
            Assert.Equal(1, messageMaster.Count());
        }

        [Fact]
        public void GetReasonMaster_Returns_GetReasonMaster()
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
            malaysiaDbContext.CaReasonMasters.Add(new CaReasonMaster
            {
                Id = 1,
                ReasonCode = "test",
                ReasonName = "test",
                IsDeleted = false,
                IsAdmission = true,
                IsOe = true,
                IsRegestration = true,
                IsBilling = true,
                IsPaymentReceipt = true,
                IsRefund = true,
                IsPaymentVoucher = true,
                IsContract = true,
                IsAccReceipt = true,
                IsDiscount = true,
                IsDischarge = true,
                IsBedTransfer = true,
                IsPrescription = true,
                IsIndentRequisition = true,
                IsTakeNoteRecording = true,
                IsPatientUnlocked = true,
                IsMedicalLegalLockdown = true,
                IsMedicalLegalUnlock = true,
                IsCancelPrintMedicalRecordQueue = true,
                IsContactPriceOverriden = true,
                IsRejectedPendingApprovalRx = true,
                IsDoNotMerge = true,
                IsMergeMedicalRecord = true,
                IsPregnancyStatusUpdate = true,
                IsRejectedPendingApprovalServices = true,
                IsChangePrescription = true,
                CreatedDate = DateTime.Now.Date,
                UpdatedBy = null,
                UpdatedDate = null,
                FacilityCode = "KLAN"
            }); 
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            //Execute method of SUT (UserMasterRepository)  
            var coreMasterService = new CoreMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var reasonMasters = coreMasterService.GetReasonMaster().Result.ToList();


            Assert.NotNull(reasonMasters);
            Assert.Equal(1, reasonMasters.Count());
        }

        [Fact]
        public void GetCurrencyRateMaintenanceMaster_Returns_CurrencyRateMaintenanceMaster()
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
            malaysiaDbContext.CaCurrencyRateMaintenances.Add(new CaCurrencyRateMaintenance { Id = 1, CurrencyId = 1, CurrencyDate = null, ConversionRate  = null, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            //Execute method of SUT (CoreMasterRepository)  
            var coreMasterService = new CoreMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var currencyRateMaintenances = coreMasterService.GetCurrencyRateMaintenanceMaster().Result.ToList();

            Assert.NotNull(currencyRateMaintenances);
            Assert.Equal(1, currencyRateMaintenances.Count());

        }

        [Fact]
        public void GetCurrencyMaster_Returns_CurrencyMaster()
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
            malaysiaDbContext.CaCurrencyMasters.Add(new CaCurrencyMaster { Id = 1, CurrencyCode = " ", CurrencyName = " ", CountryId = 1, IsPrincipal = false, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, DecimalCurrency = " ", FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            //Execute method of SUT (CoreMasterRepository)  
            var coreMasterService = new CoreMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var currencyMasters = coreMasterService.GetCurrencyMaster().Result.ToList();

            Assert.NotNull(currencyMasters);
           Assert.Equal(1, currencyMasters.Count());

        }
        [Fact]
        public void GetCompanyInformationMaster_Returns_CompanyInformationMaster()
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
            malaysiaDbContext.CaCompanyInformations.Add(new CaCompanyInformation { Id = 1, CompanyCode = " ", CompanyName = " ", ParentCompanyName = " ", Street1 = " ", Street2 = " ", Street3 = " ", Street4 = " ", City = " ", StateId = 1, CountryId = 1, PostCode = " ", PhoneNumber1 = null, PhoneNumber2 = null, TelexNumber = null, FaxNumber = null, Email = "abc@columbiaasia.com", HomePage = " ", ContactPerson = " ", BaseCalendarId = 1, BankName = " ", BankAccountNumber = " ", BankGlaccountNumber = 1, Rbicode = " ", PaymentRoutingNumber = " ", Swiftcode = " ", BankStreet1 = " ", BankStreet2 = " ", BankStreet3 = " ", BankStreet4 = " ", BankCity = " ", BankPostCode = " ", BankStateId = 1, BankPhoneNumber1 = null, BankPhoneNumber2 = null, BankFaxNumber = null, BankTelex = null, BankContactPerson = " ", CustomsPermitNumber = null, CustomsPermitDate = null, RegistrationNumber = null, RegistrationExpiryDate = null, Tinnumber = null, TinexpiryDate = null, ExciseRegistrationNumber = null, ExicseExpiryDate = null, Lstnumber = null, LstexpiryDate = null, Cstnumber = null, CstexpiryDate = null, Pannumber = null, Ltegnumber = null, Eccnumber = null, Tannumber = null, CeregdNumber = null, Cerange = null, CecommissioneRate = 1, Ieccode = null, Esi = null, EprovidentFund = null, Cedivision = null, DrugLicenceNumber = null, DrugLicenceExpiryDate = null, BloodBankRegistrationNumber = null, BloodBankRegistrationExpiryDate = null, CreatedDate = DateTime.Now.Date, DrugLicenceNumber1 = null, DrugLicenceExpiryDate1 = null, DrugLicenceNumber2 = null, DrugLicenceExpiryDate2 = null, DrugLicenceNumber3 = null, DrugLicenceExpiryDate3 = null, DrugLicenceNumber4 = null, DrugLicenceExpiryDate4 = null, DrugLicenceNumber5 = null, DrugLicenceExpiryDate5 = null, AccountYearStartDate = null, TdsYearStartDate = null, TdsCircle = " ", TdsAuthorisedSignatoryName = " ", Designation = " ", TaxDepreciationMinimumMonths = 1, TaxDepreciationMinimumPercentage = 1, Logo = null, ServiceTaxNumber = null, Gafversion = " ", ProductVersion = " ", EnableBankVirtualAccount = null, VirtualBankFacilityCode = " ", VirtualBankName = " ", VirtualBankAccountNumber = null, VirtualBankGlaccountNumber = 1, VirtualRbicode = null, VirtualPaymentRoutingNumber = null, VirtualSwiftcode = null, VirtualBankStreet1 = " ", VirtualBankStreet2 = " ", VirtualBankStreet3 = " ", VirtualBankStreet4 = " ", VirtualBankCity = " ", VirtualBankPostCode = null, VirtualBankStateId = 1, VirtualBankPhoneNumber1 = null, VirtualBankPhoneNumber2 = null, VirtualBankFaxNumber = null, VirtualBankTelex = null, SmrpfacilityCode = " ", FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            //Execute method of SUT (CoreMasterRepository)  
            var coreMasterService = new CoreMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var companyInformationMasters = coreMasterService.GetCompanyInformationMaster().Result.ToList();

            Assert.NotNull(companyInformationMasters);
            Assert.Equal(1, companyInformationMasters.Count());

        }
        [Fact]
        public void GetWardMaster_Returns_GetWardMaster()
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
            malaysiaDbContext.CaWardMasters.Add(new CaWardMaster { Id = 1, WardCode = "AB", WardName = "Test", CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null,DepartmentId=1, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            //Execute method of SUT (CoreMasterRepository)  
            var coreMasterService = new CoreMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var wardMaster =  coreMasterService.GetWardMaster().Result.ToList();

            Assert.NotNull(wardMaster);
            Assert.Equal(1, wardMaster.Count());

        }
        [Fact]
        public void GetBedTypeMaster_Returns_GetBedTypeMaster()
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
            malaysiaDbContext.CaBedTypeMasters.Add(new CaBedTypeMaster { Id = 1, Description = "Test", BedTypeCode = "Test",IsDeleted=false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, MaximumBed = 1, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<BedTypeMasterRequest>() { new BedTypeMasterRequest { Id = 1, BedTypeCode="abc", FacilityCode = "KLAN" } };
            mapper.Setup(x => x.Map<IEnumerable<BedTypeMasterRequest>>(It.IsAny<List<CaBedTypeMaster>>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var coreMasterService = new CoreMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var bedTypeMaster = coreMasterService.GetBedTypeMaster().Result.ToList();

            Assert.NotNull(bedTypeMaster);
            Assert.Equal(1, bedTypeMaster.Count());

        }
        [Fact]
        public void GetClinicMaster_Returns_GetClinicMaster()
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
            malaysiaDbContext.CaClinicMasters.Add(new CaClinicMaster { Id = 1, ClinicCode = "Test", ClinicName = "Test",DoctorName="test",Street1="test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, City ="mum", FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            var mapperResp = new List<ClinicMasterRequest>() { new ClinicMasterRequest { Id = 1, City="mumbai", FacilityCode = "KLAN" } };
            mapper.Setup(x => x.Map<IEnumerable<ClinicMasterRequest>>(It.IsAny<List<CaClinicMaster>>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var coreMasterService = new CoreMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var clinicMasters = coreMasterService.GetClinicMaster().Result.ToList();

            Assert.NotNull(clinicMasters);
            Assert.Equal(1, clinicMasters.Count());

        }
        [Fact]
        public void GetCountryMaster_Returns_GetCountryMaster()
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
            malaysiaDbContext.CaCountryMasters.Add(new CaCountryMaster { Id = 1, Description = "Test", Nationality = "Test", CountryCode = "ab", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<CountryMasterRequest>() { new CountryMasterRequest { Id = 1, Description = "test", FacilityCode = "KLAN" } };
            mapper.Setup(x => x.Map<IEnumerable<CountryMasterRequest>>(It.IsAny<List<CaCountryMaster>>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var coreMasterService = new CoreMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var countryMaster = coreMasterService.GetCountryMaster().Result.ToList();

            Assert.NotNull(countryMaster);
            Assert.Equal(1, countryMaster.Count());

        }
        [Fact]
        public void GetRoomMaster_Returns_GetRoomMaster()
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
            malaysiaDbContext.CaRoomMasters.Add(new CaRoomMaster { Id = 1, Description = "Test", RoomCode = "10", WardId =1,GenderId=1,  IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<RoomMasterRequest>() { new RoomMasterRequest { Id = 1, Description = "test", FacilityCode = "KLAN" } };
            mapper.Setup(x => x.Map<IEnumerable<RoomMasterRequest>>(It.IsAny<List<CaRoomMaster>>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var coreMasterService = new CoreMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var roomMasters = coreMasterService.GetRoomMaster().Result.ToList();

            Assert.NotNull(roomMasters);
            Assert.Equal(1, Convert.ToInt32(roomMasters.Count()));

        }
        [Fact]
        public void GetMaritalStatusMaster_Returns_GetMaritalStatusMaster()
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
            malaysiaDbContext.CaMaritalStatusMasters.Add(new CaMaritalStatusMaster { Id = 1, Description = "Test", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<MaritalStatusMasterRequest>() { new MaritalStatusMasterRequest { Id = 1, Description = "test", FacilityCode = "KLAN" } };
            mapper.Setup(x => x.Map<IEnumerable<MaritalStatusMasterRequest>>(It.IsAny<List<CaMaritalStatusMaster>>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var coreMasterService = new CoreMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var maritalStatusMasters = coreMasterService.GetMaritalStatusMaster().Result.ToList();

            Assert.NotNull(maritalStatusMasters);
            Assert.Equal(1, Convert.ToInt32(maritalStatusMasters.Count()));

        }
        [Fact]
        public void GetOccupationMaster_Returns_GetOccupationMaster()
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
            malaysiaDbContext.CaOccupationMasters.Add(new CaOccupationMaster { Id = 1, Description = "Test",Code="AB", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<OccupationMasterRequest>() { new OccupationMasterRequest { Id = 1, Description = "test", FacilityCode = "KLAN" } };
            mapper.Setup(x => x.Map<IEnumerable<OccupationMasterRequest>>(It.IsAny<List<CaOccupationMaster>>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var coreMasterService = new CoreMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var occupationMasters = coreMasterService.GetOccupationMaster().Result.ToList();

            Assert.NotNull(occupationMasters);
            Assert.Equal(1,Convert.ToInt32(occupationMasters.Count()));

        }
        [Fact]
        public void GetCauseOfDeathMaster_Returns_GetCauseOfDeathMaster()
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
            malaysiaDbContext.CaCauseOfDeathMasters.Add(new CaCauseOfDeathMaster { Id = 1, Description = "Test", Code = null, IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<CauseOfDeathMasterRequest>() { new CauseOfDeathMasterRequest { Id = 1, Description = "test", FacilityCode = "KLAN" } };
            mapper.Setup(x => x.Map<IEnumerable<CauseOfDeathMasterRequest>>(It.IsAny<List<CaCauseOfDeathMaster>>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var coreMasterService = new CoreMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var causeOfDeathMaster = coreMasterService.GetCauseOfDeathMaster().Result.ToList();

            Assert.NotNull(causeOfDeathMaster);
            Assert.Equal(1,Convert.ToInt32(causeOfDeathMaster.Count()));

        }
        [Fact]
        public void GetPublicHolidaysMaster_Returns_GetPublicHolidaysMaster()
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
            malaysiaDbContext.CaPublicHolidaysMasters.Add(new CaPublicHolidaysMaster { Id = 1, Reason="fever", HoliDayName="holi", IsDeleted = false, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<PublicHolidaysMasterRequest>() { new PublicHolidaysMasterRequest { Id = 1, Reason="fever", FacilityCode = "KLAN" } };
            mapper.Setup(x => x.Map<IEnumerable<PublicHolidaysMasterRequest>>(It.IsAny<List<CaPublicHolidaysMaster>>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var coreMasterService = new CoreMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var publicHolidaysMaster = coreMasterService.GetPublicHolidaysMaster().Result.ToList();

            Assert.NotNull(publicHolidaysMaster);
            Assert.Equal(1,Convert.ToInt32(publicHolidaysMaster.Count()));

        }
        [Fact]
        public void GetModeOfArrivalMaster_Returns_GetModeOfArrivalMaster()
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
            malaysiaDbContext.CaModeOfArrivalMasters.Add(new CaModeOfArrivalMaster { Id = 1, Description="test", IsDeleted=false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();

            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);

            var mapperResp = new List<ModeOfArrivalMasterRequest>() { new ModeOfArrivalMasterRequest { Id = 1, Description = "test", FacilityCode = "KLAN" } };
            mapper.Setup(x => x.Map<IEnumerable<ModeOfArrivalMasterRequest>>(It.IsAny<List<CaModeOfArrivalMaster>>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var coreMasterService = new CoreMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var modeOfArrivalMaster = coreMasterService.GetModeOfArrivalMaster().Result.ToList();

            Assert.NotNull(modeOfArrivalMaster);
            Assert.Equal(1,Convert.ToInt32(modeOfArrivalMaster.Count()));

        }
        #region Delivery Method Master Unit Testing (Added by janhavi chaudhari)
        [Fact]
        public void GetDeliveryMethodMaster_Returns_GetDeliveryMethodMaster()
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
            malaysiaDbContext.CaDeliveryMethodMasters.Add(new CaDeliveryMethodMaster { Id = 1, Description = "test", CreatedDate = DateTime.Now.Date, UpdatedBy = 1, UpdatedDate = null,Code="01", IsDeleted = false, FacilityCode = "KLAN" });
            malaysiaDbContext.SaveChanges();
            
            var context = new DefaultHttpContext();
            var fakeRegion = "Malaysia";
            context.Request.Headers["Region"] = fakeRegion;
            httpContextProcessor.Setup(_ => _.HttpContext).Returns(context);
            var mapperResp = new List<DeliveryMethodMasterRequest>() { new DeliveryMethodMasterRequest { Id = 1, Description = "test", FacilityCode = "KLAN" } };
            mapper.Setup(x => x.Map<IEnumerable<DeliveryMethodMasterRequest>>(It.IsAny<List<CaDeliveryMethodMaster>>())).Returns(mapperResp);
            //Execute method of SUT (CoreMasterRepository)  
            var coreMasterService = new CoreMasterService(httpContextProcessor.Object, malaysiaDbContext, cacheService.Object, mapper.Object);
            //Act
            var deliveryMethodMaster = coreMasterService.GetDeliveryMethodMaster();

            //Assert.NotNull(deliveryMethodMaster);
            //Assert.Equal(1, Convert.ToInt32(deliveryMethodMaster.Count()));

        }
     
        #endregion

    }
}
