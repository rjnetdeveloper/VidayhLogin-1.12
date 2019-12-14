using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Registration.Entities;

namespace Registration
{
    public class ExportStudentModel : PageModel
    {
        private readonly IConfiguration _configuration;
        HttpClient _client;
        private object Router;
        private readonly string _connection = string.Empty;
        private IHostingEnvironment _hostingEnvironment;
        private readonly PrincetonhiveContext _context;

        public ExportStudentModel(IConfiguration config, IHostingEnvironment hostingEnvironment, PrincetonhiveContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = config;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_configuration["base_Url"]);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _connection = _configuration["ConnectionStrings:EduConn"];
            _context = context;
        }

        [BindProperty]
        public string StudentSchool { get; set; }
        public List<TblRegistration> Schools { get; set; }

        public void OnGet()
        {
            Schools = (from a in _context.TblRegistration
                       where a.SchoolName != null
                       select new TblRegistration { SchoolName = a.SchoolName }).Distinct().ToList();
        }

        public IList<TblStudentRegistration> StudentData { get; private set; }

        public async Task<IActionResult> OnPostAsync(string savemore)
        {
            var princetonhive = new PrincetonhiveContext();
            if (savemore == "Export")
            {
                var studentlist = (from a in _context.TblStudentRegistration
                                   where a.SchoolName==StudentSchool
                                   select new TblStudentRegistration { FirstName = a.FirstName, LastName = a.LastName, DisplayName = a.DisplayName, Gender = a.Gender, Dob = a.Dob, Address = a.Address, Username = a.Username }).ToList();
                //  var studentlist1 = _context.TblStudentRegistration.Select(a => a.FirstName,a).ToList();
                StudentData = studentlist.ToList();

                string sWebRootFolder = _hostingEnvironment.WebRootPath;
                string sFileName = @"'"+StudentSchool+"'_Studentlist.xlsx";
                string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
                FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
                var memory = new MemoryStream();
                using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
                {

                    IWorkbook workbook;
                    workbook = new XSSFWorkbook();
                    ISheet excelSheet = workbook.CreateSheet("Student");
                    IRow row = excelSheet.CreateRow(0);
                    int i = 1;
                    row.CreateCell(0).SetCellValue("ID");
                    row.CreateCell(1).SetCellValue("First Name");
                    row.CreateCell(2).SetCellValue("Last Name");
                    row.CreateCell(3).SetCellValue("Display Name");
                    row.CreateCell(4).SetCellValue("Gender");
                    row.CreateCell(5).SetCellValue("DOB");
                    row.CreateCell(6).SetCellValue("Address");
                    row.CreateCell(7).SetCellValue("User Name");
                  //  row.CreateCell(8).SetCellValue("School Name");

                    foreach (var segment in StudentData)
                    {
                        row = excelSheet.CreateRow(i);
                        row.CreateCell(0).SetCellValue(i);
                        row.CreateCell(1).SetCellValue(segment.FirstName);
                        row.CreateCell(2).SetCellValue(segment.LastName);
                        row.CreateCell(3).SetCellValue(segment.DisplayName);
                        row.CreateCell(4).SetCellValue(segment.Gender);
                        row.CreateCell(5).SetCellValue(segment.Dob.ToString());
                        row.CreateCell(6).SetCellValue(segment.Address);
                        row.CreateCell(7).SetCellValue(segment.Username);
                     //   row.CreateCell(8).SetCellValue(segment.SchoolName);

                        i++;
                    }


                    workbook.Write(fs);
                }
                using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

            }
            

            return Page();

        }
    }
}