using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PrashTechnologies.Api.Helper;
using PrashTechnologies.Core.Entities;
using System;
using System.Collections.Generic;

namespace PrashTechnologies.Api.Filters
{
    public class DataTransformerFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.Result != null && context.HttpContext.Response.StatusCode == 200 && context.HttpContext.Request.QueryString.HasValue == true)
            {
                var result = (OkObjectResult)context.Result;
                var products = new List<Product>();
                try
                {
                    products = (List<Product>)result.Value;
                }
                catch
                {                    
                    var product = (Product)result.Value;
                    products.Add(product);
                }

                if (products.Count > 0)
                {
                    // conver the data to desired exchange rage
                    var currencyCoverter = new CurrencyConverter();
                    var liveCurrencyQuotes = currencyCoverter.GetQuotes();

                    // convert the currency
                    foreach (var p in products)
                    {
                        ConvertCurrency(context, p, liveCurrencyQuotes);
                    }
                }
            }
        }

        private static void ConvertCurrency(ActionExecutedContext context, Product product, CurrencyLive liveCurrencyQuotes)
        {           
            if (liveCurrencyQuotes != null)
            {
                var desiredCurrency = context.HttpContext.Request.Query["exchangeCode"];

                // get the value for the desired currency and format the data
                switch (desiredCurrency)
                {
                    case "GBP":
                        product.CurrentCost = Convert.ToDecimal(liveCurrencyQuotes.quotes.USDGBP) * product.CurrentCost;
                        product.ExchangeCode = "GBP";
                        break;
                    case "CAD":
                        product.CurrentCost = Convert.ToDecimal(liveCurrencyQuotes.quotes.USDCAD) * product.CurrentCost;
                        product.ExchangeCode = "CAD";
                        break;
                    case "EUR":
                        product.CurrentCost = Convert.ToDecimal(liveCurrencyQuotes.quotes.USDEUR) * product.CurrentCost;
                        product.ExchangeCode = "EUR";
                        break;
                    default:
                        break;
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
          
        }
    }
}
