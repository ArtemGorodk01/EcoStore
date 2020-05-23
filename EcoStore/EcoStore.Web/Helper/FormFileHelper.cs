using EcoStore.Web.Enums;
using Microsoft.AspNetCore.Http;
using System;

namespace EcoStore.Web.Helper
{
    public static class FormFileHelper
    {
        public static ExchangeType GetFileType(this IFormFile formFile)
        {
            if (formFile == null)
            {
                throw new ArgumentNullException(nameof(formFile));
            }

            var fileNameArray = formFile.FileName.Split('.');
            var extansion = fileNameArray[fileNameArray.Length - 1];
            if (extansion.Equals(ExchangeType.Json.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                return ExchangeType.Json;
            }

            if (extansion.Equals(ExchangeType.Xml.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                return ExchangeType.Xml;
            }

            if (extansion.Equals(ExchangeType.Doc.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                return ExchangeType.Doc;
            }

            return ExchangeType.None;
        }
    }
}
