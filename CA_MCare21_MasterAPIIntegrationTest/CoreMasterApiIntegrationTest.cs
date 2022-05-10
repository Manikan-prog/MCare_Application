using CA_MCare21_MasterAPI.Entities;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CA_MCare21_MasterAPIIntegrationTest
{
    public class CoreMasterApiIntegrationTest
    {

        [Fact]
        public async Task Test_GetStatusMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/CoreMaster/GetStatusMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetDepartmentMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/CoreMaster/GetDepartmentMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetDocumentTypeMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/CoreMaster/GetDocumentTypeMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetDisclaimerMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/CoreMaster/GetDisclaimerMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetFacilityMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/CoreMaster/GetFacilityMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetMessageMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/CoreMaster/GetMessageMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetGlobalSettingsValidator()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/CoreMaster/GetGlobalSettingsValidator");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetGlobalSettings()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/CoreMaster/GetGlobalSettings");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetReasonMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/CoreMaster/GetReasonMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetOnlineMessage()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/CoreMaster/GetOnlineMessage");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetCurrencyRateMaintenanceMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/CoreMaster/GetCurrencyRateMaintenanceMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetCurrencyMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/CoreMaster/GetCurrencyMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetCompanyInformationMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/CoreMaster/GetCurrencyMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }

        [Fact]
        public async Task Test_GetWardMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/CoreMaster/GetWardMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetBedTypeMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/CoreMaster/GetBedTypeMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetClinicMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/CoreMaster/GetClinicMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetCountryMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/CoreMaster/GetCountryMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        //[Fact]
        //public async Task Test_GetStateMaster()
        //{
        //    using (var client = new TestClientProvider().Client)
        //    {
        //        client.DefaultRequestHeaders.Add("Region", "Malaysia");

        //        var response = await client.GetAsync("/api/CoreMaster/GetStateMaster");

        //        response.EnsureSuccessStatusCode();

        //        response.StatusCode.Should().Be(HttpStatusCode.OK);

        //    }
        //}
        //[Fact]
        //public async Task Test_GetCityMaster()
        //{
        //    using (var client = new TestClientProvider().Client)
        //    {
        //        client.DefaultRequestHeaders.Add("Region", "Malaysia");

        //        var response = await client.GetAsync("/api/CoreMaster/GetCityMaster");

        //        response.EnsureSuccessStatusCode();

        //        response.StatusCode.Should().Be(HttpStatusCode.OK);

        //    }
        //}
        [Fact]
        public async Task Test_GetRoomMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/CoreMaster/GetRoomMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetMaritalStatusMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/CoreMaster/GetMaritalStatusMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetOccupationMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/CoreMaster/GetOccupationMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetCauseOfDeathMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/CoreMaster/GetCauseOfDeathMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetWardMasterById()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/CoreMaster/GetWardMasterById?id=2");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetBedTypeMasterById()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/CoreMaster/GetBedTypeMasterById?id=1");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetClinicMasterById()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/CoreMaster/GetClinicMasterById?id=1");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetRoomMasterById()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/CoreMaster/GetRoomMasterById?id=1");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetMaritalStatusMasterById()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/CoreMaster/GetMaritalStatusMasterById?id=1");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetOccupationMasterById()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/CoreMaster/GetOccupationMasterById?id=1");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }

        [Fact]
        public async Task Test_GetCauseOfDeathMasterById()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/CoreMaster/GetCauseOfDeathMasterById?id=1");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_DeleteWardMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.DeleteAsync("/api/CoreMaster/DeleteWardMaster?id=2");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_DeleteBedTypeMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.DeleteAsync("/api/CoreMaster/DeleteBedTypeMaster?id=1");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_DeleteClinicMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.DeleteAsync("/api/CoreMaster/DeleteClinicMaster?id=1");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_DeleteCountryMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.DeleteAsync("/api/CoreMaster/DeleteCountryMaster?id=1");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_DeleteStateMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.DeleteAsync("/api/CoreMaster/DeleteStateMaster?id=1");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_DeleteCityMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.DeleteAsync("/api/CoreMaster/DeleteCityMaster?id=1");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_DeleteRoomMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.DeleteAsync("/api/CoreMaster/DeleteRoomMaster?id=1");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_DeleteMaritalStatusMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.DeleteAsync("/api/CoreMaster/DeleteMaritalStatusMaster?id=1");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_DeleteOccupationMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.DeleteAsync("/api/CoreMaster/DeleteOccupationMaster?id=1");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_DeleteCauseOfDeathMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.DeleteAsync("/api/CoreMaster/DeleteCauseOfDeathMaster?id=1");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_AddRoomMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client
                    .PostAsync("/api/CoreMaster/AddRoomMaster",
                                new StringContent(
                                JsonConvert.SerializeObject(new CaRoomMaster { Id = 10, RoomCode = "AB", Description = "Test", WardId = 4, GenderId = 0, BedTypeId = 1, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" }), Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_AddMaritalStatusMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client
                    .PostAsync("/api/CoreMaster/AddMaritalStatusMaster",
                                new StringContent(
                                JsonConvert.SerializeObject(new CaMaritalStatusMaster { Id = 10, Description = "TEST", Smrpcode = "99", Smrpdescription = "other", CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" }), Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_AddOccupationMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client
                    .PostAsync("/api/CoreMaster/AddMaritalStatusMaster",
                                new StringContent(
                                JsonConvert.SerializeObject(new CaMaritalStatusMaster { Id = 10, Description = "test", Code = "AB", Smrpcode = "99", Smrpdescription = "other", CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" }), Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_AddCauseOfDeathMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client
                    .PostAsync("/api/CoreMaster/AddMaritalStatusMaster",
                                new StringContent(
                                JsonConvert.SerializeObject(new CaMaritalStatusMaster { Id = 10, Description = "AB", CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" }), Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_UpdateRoomMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client
                    .PutAsync("/api/CoreMaster/UpdateRoomMaster",
                                new StringContent(
                                JsonConvert.SerializeObject(new CaRoomMaster { Id = 10, RoomCode = "AB", Description = "Test", WardId = 4, GenderId = 0, BedTypeId = 1, CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" }), Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_UpdateMaritalStatusMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client
                    .PutAsync("/api/CoreMaster/UpdateMaritalStatusMaster",
                                new StringContent(
                                JsonConvert.SerializeObject(new CaMaritalStatusMaster { Id = 10, Description = "TEST", Smrpcode = "99", Smrpdescription = "other", CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" }), Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_UpdateOccupationMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client
                    .PutAsync("/api/CoreMaster/UpdateOccupationMaster",
                                new StringContent(
                                JsonConvert.SerializeObject(new CaOccupationMaster { Id = 10, Description = "test", Code = "AB", Smrpcode = "99", Smrpdescription = "other", CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" }), Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_UpdateCauseOfDeathMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client
                    .PutAsync("/api/CoreMaster/UpdateCauseOfDeathMaster",
                                new StringContent(
                                JsonConvert.SerializeObject(new CaCauseOfDeathMaster { Id = 10, Description = "AB", CreatedBy = 1, CreatedDate = DateTime.Now.Date, UpdatedBy = null, UpdatedDate = null, FacilityCode = "KLAN" }), Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }

    }
}
