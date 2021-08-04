using System;
using System.Collections.Generic;
using System.Text;

namespace FacialRecognition.Data.Models
{

    public class AjaxResponse
    {
        public Datum[] data { get; set; }
    }

    public class Datum
    {

        public string RegNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
    }
}
