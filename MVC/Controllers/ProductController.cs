using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Newtonsoft.Json;

namespace MVC.Controllers;

public class ProductController : Controller
{
    private readonly HttpClient _httpClient;

    public ProductController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ProductApi");
    }
    
    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync("api/Product/all");
        
        if (!response.IsSuccessStatusCode) return View();
        
        var products = JsonConvert.DeserializeObject<List<Product>>(ReadResponse(response));
        
        return View(products);
    }
    
    /// <summary>
    /// Search by name
    /// </summary>
    /// <param name="searchString">name of a product</param>
    /// <returns>list of products</returns>
    public async Task<IActionResult> Search(string searchString)
    {
        var response = await _httpClient.GetAsync($"api/Product/byName?name={searchString}");
    
        if (!response.IsSuccessStatusCode) return RedirectToAction("Index");
    
        var products = JsonConvert.DeserializeObject<List<Product>>(ReadResponse(response));
    
        return View("Index", products);
    }

    /// <summary>
    /// Adds new product
    /// </summary>
    /// <param name="product">Product object</param>
    /// <returns>whether it was added successfully</returns>
    [HttpPost]
    public bool Add(Product product)
    {
        if (ModelState.IsValid)
            if (GetResponseFromPost(product).IsSuccessStatusCode)
                return true;
        return false;
    }

    /// <summary>
    /// Gets the product by its id
    /// </summary>
    /// <param name="id">id of the product</param>
    /// <returns>json object or null</returns>
    [HttpGet]
    public async Task<IActionResult> Get(string id)
    {
        var response = await _httpClient.GetAsync($"api/Product/{Guid.Parse(id)}");

        if (response.IsSuccessStatusCode)
            return Json(JsonConvert.DeserializeObject<Product>(ReadResponse(response)));

        return Json(null);
    }
    
    /// <summary>
    /// Edit product
    /// </summary>
    /// <param name="product">Product object</param>
    /// <returns>whether it was upate successfully</returns>
    [HttpPost]
    public bool Edit(Product product)
    {
        if (ModelState.IsValid)
            if (GetResponseFromPut(product).IsSuccessStatusCode)
                return true;
        return false;
    }

    /// <summary>
    /// Delete product
    /// </summary>
    /// <param name="id">id of the product</param>
    /// <returns>whether it was deleted successfully</returns>
    [HttpPost] 
    public async Task<bool> Delete(string id)
    {
        var response = _httpClient.DeleteAsync($"api/Product/{Guid.Parse(id)}").Result;
        if (!response.IsSuccessStatusCode) return false;
        return true;
    }
    
    /// <summary>
    /// Returns response message as a string
    /// </summary>
    private string ReadResponse(HttpResponseMessage responseMessage)
    {
        return responseMessage.Content.ReadAsStringAsync().Result;
    }
    
    /// <summary>
    /// Handles POST method for the object and returns the response from api
    /// </summary>
    private HttpResponseMessage GetResponseFromPost(Product product)
    {
        string createProductInfo = JsonConvert.SerializeObject(product);
            
        StringContent stringContentInfo = new StringContent(createProductInfo, Encoding.UTF8, "application/json");
            
        return _httpClient
            .PostAsync(_httpClient.BaseAddress + "api/Product", stringContentInfo)
            .Result;
    }
    
    /// <summary>
    /// Handles PUT method for the object and returns the response from api
    /// </summary>
    private HttpResponseMessage GetResponseFromPut(Product product)
    {
        string createProductInfo = JsonConvert.SerializeObject(product);
            
        StringContent stringContentInfo = new StringContent(createProductInfo, Encoding.UTF8, "application/json");
            
        return _httpClient
            .PutAsync(_httpClient.BaseAddress + $"api/Product/{product.Id}", stringContentInfo)
            .Result;
    }
}