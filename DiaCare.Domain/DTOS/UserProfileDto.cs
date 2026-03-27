using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaCare.Domain.DTOS
{
    public class UserProfileDto
    {
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
    }
}
