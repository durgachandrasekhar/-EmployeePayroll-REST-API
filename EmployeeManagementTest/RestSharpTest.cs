using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;

namespace RestSharpTest
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Salary { get; set; }
    }
    [TestClass]
    public class RestSharpTestCases
    {
        RestClient client;

        [TestInitialize]
        public void Setup()
        {
            client = new RestClient("http://localhost:4000");
        }

        private IRestResponse getEmployeeList()
        {
            //Arrange
            RestRequest request = new RestRequest("/employees", Method.GET);

            //Act
            IRestResponse response = client.Execute(request);
            return response;
        }
        [TestMethod]
        public void OnCallingGETApi_ReturnEmployeeList()
        {
            IRestResponse response = getEmployeeList();

            //Assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            List<Employee> dataResponse = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            Assert.AreEqual(10, dataResponse.Count);

            foreach (Employee e in dataResponse)
            {
                System.Console.WriteLine("id : " + e.Id + ", Name : " + e.Name + ", Salary : " + e.Salary);
            }
        }

        [TestMethod]
        public void GivenEmployee_OnPost_ShouldReturnAddedEmployee()
        {
            //Arrange
            RestRequest request = new RestRequest("/employees", Method.POST);
            JObject jObjectbody = new JObject();
            jObjectbody.Add("Name", "Ronaldo");
            jObjectbody.Add("Salary", "25000");

            request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

            //Act
            IRestResponse response = client.Execute(request);

            //Assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Created);
            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("Ronaldo", dataResponse.Name);
            Assert.AreEqual("25000", dataResponse.Salary);
            System.Console.WriteLine(response.Content);
        }
        [TestMethod]
        public void givenEmployeeSalary_OnUpdate_ShouldReturnAddedEmployee()
        {
            //Arrange
            RestRequest request = new RestRequest("/employees/13", Method.PUT);
            JObject jObjectbody = new JObject();

            jObjectbody.Add("name", "Ronaldo");
            jObjectbody.Add("Salary", "30000");
            request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

            //Act
            var response = client.Execute(request);

            //Assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            int actual_salary = int.Parse(dataResponse.Salary);
            Assert.AreEqual("Ronaldo", dataResponse.Name);
            Assert.AreEqual(30000, actual_salary);

        }
        [TestMethod]
        public void givenEmployeeID_SholudDeleteEmployee()
        {
            //Arrange
            RestRequest request = new RestRequest("/employees/3", Method.DELETE);

            //Act
            IRestResponse response = client.Execute(request);

            //Assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            System.Console.WriteLine(response.Content);
        }
    }
}