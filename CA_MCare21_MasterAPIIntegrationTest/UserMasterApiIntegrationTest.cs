using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CA_MCare21_MasterAPIIntegrationTest
{
    public class UserMasterApiIntegrationTest
    {
        [Fact]
        public async Task Test_GetUsersMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/UserMaster/GetUsersMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetRolesMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/UserMaster/GetRolesMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetUsersInRolesMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/UserMaster/GetUsersInRolesMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetUserLogMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/UserMaster/GetUserLogMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetEmployeeTypeMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/UserMaster/GetEmployeeTypeMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }
        }
        [Fact]
        public async Task Test_GetUserProfileMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/UserMaster/GetUserProfileMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetTemporaryDepartmentMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/UserMaster/GetTemporaryDepartmentMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetUserAdditionalDepartmentMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/UserMaster/GetUserAdditionalDepartmentMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
        [Fact]
        public async Task Test_GetSpecialPermissionMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/UserMaster/GetSpecialPermissionMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }

        [Fact]
        public async Task Test_GetRolesInSpecialPermissionMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/UserMaster/GetRolesInSpecialPermissionMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }

        [Fact]
        public async Task Test_GetRolewiseShortcutMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/UserMaster/GetRolewiseShortcutMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }

        [Fact]
        public async Task Test_GetRoleCommunicationMaster()
        {
            using (var client = new TestClientProvider().Client)
            {
                client.DefaultRequestHeaders.Add("Region", "Malaysia");

                var response = await client.GetAsync("/api/UserMaster/GetRoleCommunicationMaster");

                response.EnsureSuccessStatusCode();

                response.StatusCode.Should().Be(HttpStatusCode.OK);

            }
        }
    }
}
