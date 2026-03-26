using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaCare.Application.Helpers
{
    public class JwtSettings
    {
        //to strongly typed setting in appsetting Container instead of call strings in appsett
        //allow  IntelliSense & Auto-complete
        public string Key { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public int DurationInMinutes { get; set; }
    }
}
