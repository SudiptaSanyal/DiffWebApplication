using DiffWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DiffWebApplication.Controllers
{
    public class APITestController : Controller
    {
        // GET: APITest
        public ActionResult APITest()
        {
            APIModel _model = new APIModel();
            _model.ID = 1;
            return View(_model);
        }
        public ActionResult GetDiff(int ID)
        {
            APIModel _model = new APIModel();
            _model.ID = ID;
            string uri = System.Configuration.ConfigurationManager.AppSettings["APIAddress"];
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri(uri);
            var msg = _client.GetAsync(_client.BaseAddress + "V1/diff/" + ID.ToString()).Result;
            if (msg.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string _strContent = msg.Content.ReadAsStringAsync().Result;
                _model.diffResponse = "Ok";
                _model.diffContent = _strContent;
            }
            else if (msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                _model.diffResponse = "Not Found";
            }
            return View("APITest", _model);
        }
        public ActionResult PutLeft(int ID, string value)
        {
            APIModel _model = new APIModel();
            _model.ID = ID;
            _model.left = value;
            string uri = System.Configuration.ConfigurationManager.AppSettings["APIAddress"];
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri(uri);
            HttpRequestMessage requestMessage=new HttpRequestMessage();
            requestMessage.Content = new StringContent("{\"data\":\""+ value + "\"}", System.Text.Encoding.UTF8, "application/json");
            var msg = _client.PutAsync(_client.BaseAddress + "V1/diff/" + ID + "/left", requestMessage.Content).Result;
            if (msg.StatusCode == System.Net.HttpStatusCode.Created)
            {
                _model.diffResponse = "Created";
            }
            else if(msg.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                string _strContent = msg.Content.ReadAsStringAsync().Result;
                _model.diffResponse = "Bad Request";
                _model.diffContent = _strContent;
            }
            return View("APITest", _model);
        }
        public ActionResult PutRight(int ID, string value)
        {
            APIModel _model = new APIModel();
            _model.ID = ID;
            _model.right = value;
            string uri = System.Configuration.ConfigurationManager.AppSettings["APIAddress"];
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri(uri);
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.Content = new StringContent("{\"data\":\"" + value + "\"}", System.Text.Encoding.UTF8, "application/json");
            var msg = _client.PutAsync(_client.BaseAddress + "V1/diff/" + ID + "/right", requestMessage.Content).Result;
            if (msg.StatusCode == System.Net.HttpStatusCode.Created)
            {
                _model.diffResponse = "Created";
            }
            else if (msg.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                string _strContent = msg.Content.ReadAsStringAsync().Result;
                _model.diffResponse = "Bad Request";
                _model.diffContent = _strContent;
            }
            return View("APITest", _model);           
        }
        
    }
}