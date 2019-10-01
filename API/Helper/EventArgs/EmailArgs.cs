using System;
using API.Core.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Helper.EventArgs
{
    public class EmailArgs
    {
        public User user { get; set; }
        public string Code { get; set; }
        public IUrlHelper  Url { get; set; }
        public HttpRequest Http { get; set; }
    }
}