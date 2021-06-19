using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessCardManagement.Models
{
    public class FileUploadViewModel
    {
        public IFormFile XlsFile { get; set; }
        /*create StaffInfoViewModel  object because we need to add read
         excel data and mapping in StaffInfoViewModel*/
        public BusinessCardXML StaffInfoViewModel { get; set; }
        public FileUploadViewModel()//Create contractor
        {



            //call StaffInfoViewModel  this object in contractor
            StaffInfoViewModel = new BusinessCardXML();
        }
    }
}