using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaCare.Domain.DTOS
{
    public  class PredictionResultDto
    {
        public string RiskCategory { get; set; } // Medium
        public double RiskScore { get; set; }    //  32.22

    }
}
