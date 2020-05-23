using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EcoStore.CommonServices.DataExchange;
using EcoStore.EFCore.Entities;
using EcoStore.Web.Enums;
using EcoStore.Web.Helper;
using EcoStore.Web.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcoStore.Web.Controllers
{
    public class DataExchangeController : Controller
    {
        private IProductService productService;

        public DataExchangeController(IProductService productService)
        {
            this.productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<FileResult> Export(string fileType)
        {
            string filePath = "tmp";
            string resultFileName = "products";
            string contentType = String.Empty;
            IDataWriter<Product> writer = null;
            using (var streamWriter = new StreamWriter(System.IO.File.Open(filePath, FileMode.Create)))
            {
                if (fileType.Equals(ExchangeType.Json.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    resultFileName += ".json";
                    contentType = "application/json";
                    writer = new JsonProductWriter(streamWriter);
                }
                else if (fileType.Equals(ExchangeType.Xml.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    resultFileName += ".xml";
                    contentType = "application/xml";
                    writer = new XmlProductWriter(streamWriter);
                }
                else if (fileType.Equals(ExchangeType.Doc.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    resultFileName += ".doc";
                    contentType = "application/doc";
                    writer = new DocProductWriter(streamWriter);
                }

                writer.WriteData(await productService.GetProducts());
            }

            byte[] bytes;
            using (var binaryReader = new BinaryReader(System.IO.File.OpenRead(filePath)))
            {
                bytes = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
            }

            return File(bytes, contentType, resultFileName);
        }

        public async Task<IActionResult> Import(IFormFile uploadedFile)
        {
            if (uploadedFile == null)
            {
                throw new ArgumentNullException(nameof(uploadedFile));
            }

            var fileType = uploadedFile.GetFileType();
            IDataReader<Product> reader;
            if (fileType == ExchangeType.Json)
            {
                reader = new JsonProductReader(new StreamReader(uploadedFile.OpenReadStream()));
            }
            else if (fileType == ExchangeType.Xml)
            {
                reader = new XmlProductReader(new StreamReader(uploadedFile.OpenReadStream()));
            }
            else
            {
                return RedirectToAction("Fail");
            }

            var products = reader.ReadData();
            products.ToList().ForEach(async p => await productService.AddProduct(p));
            return View("Success");
        }
    }
}