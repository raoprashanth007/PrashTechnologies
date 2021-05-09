using RestSharp;
using TechTalk.SpecFlow;
using PrashTechnologies.Api.Tests.Helper;
using System.Net;
using Xunit;

namespace PrashTechnologies.Api.Tests
{
    [Binding]
    public class ProductSteps
    {
        private ScenarioContext _scenarioContext;
        public ProductSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        HttpStatusCode statusCode;       

        [Given(@"I creat a new product\((.*),(.*),(.*)\)")]
        public void GivenICreatANewProduct(string Name, decimal CurrentCost, string Description)
        {
            var product = new
            {
                Name = Name,
                CurrentCost = CurrentCost,
                Description = Description              
            };

            var request = new HttpRequestWrapper()
                          .SetMethod(Method.POST)
                          .SetResourse("/api/Product/")
                          .AddJsonContent(product);

            var restResponse = request.Execute();
            statusCode = restResponse.StatusCode;

            _scenarioContext.Add("Product", product);
        }
        
        [Given(@"ModelState is correct")]   
        public void GivenModelStateIsCorrect()
        {
            
        }
         
        [Then(@"the system sgould return(.*)")]
        public void ThenTheSystemSgouldReturn(string p0)
        {
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
    }
}
