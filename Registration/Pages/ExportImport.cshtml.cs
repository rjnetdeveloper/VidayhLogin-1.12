using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Registration.Entities;

namespace Registration
{
    public class ExportImportModel : PageModel
    {
        private readonly IConfiguration _configuration;
        HttpClient _client;
        private object Router;
        private readonly string _connection = string.Empty;
        private IHostingEnvironment _hostingEnvironment;
        private readonly PrincetonhiveContext _context;

        public ExportImportModel(IConfiguration config, IHostingEnvironment hostingEnvironment, PrincetonhiveContext context)
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

        public void OnGet()
        {

        }

        public IList<TblStudentRegistration> StudentData { get; private set; }
        
        public async Task<IActionResult> OnPostAsync(string savemore)
        {
            var princetonhive = new PrincetonhiveContext();
            if (savemore == "Export")
            {
                var studentlist = (from a in _context.TblStudentRegistration
                                   select new TblStudentRegistration { FirstName = a.FirstName, LastName = a.LastName, DisplayName = a.DisplayName, Gender = a.Gender,Dob=a.Dob, Address = a.Address, Username = a.Username }).ToList();
              //  var studentlist1 = _context.TblStudentRegistration.Select(a => a.FirstName,a).ToList();
                StudentData = studentlist.ToList();

                string sWebRootFolder = _hostingEnvironment.WebRootPath;
                string sFileName = @"Studentlist.xlsx";
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
                            row.CreateCell(1).SetCellValue("FirstName");
                            row.CreateCell(2).SetCellValue("LastName");
                            row.CreateCell(3).SetCellValue("DisplayName");
                            row.CreateCell(4).SetCellValue("Gender");
                            row.CreateCell(5).SetCellValue("DOB");
                            row.CreateCell(6).SetCellValue("Address");
                            row.CreateCell(7).SetCellValue("Username");
                          //  row.CreateCell(8).SetCellValue("StudentIdBelouga");

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
                             //   row.CreateCell(8).SetCellValue(segment.StudentIdBelouga);

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
            else if (savemore == "Import")
            {
                IFormFile file = Request.Form.Files[0];
                string folderName = "StudentExelUpload";
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);
                StringBuilder sb = new StringBuilder();
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                    ISheet sheet;
                    string fullPath = Path.Combine(newPath, file.FileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        stream.Position = 0;
                        if (sFileExtension == ".xls")
                        {
                            HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                            sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                        }
                        else
                        {
                            XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                            sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                        }
                        IRow headerRow = sheet.GetRow(0); //Get Header Row
                        int cellCount = headerRow.LastCellNum;
                        sb.Append("<table class='table'><tr>");
                        for (int j = 1; j < cellCount; j++)
                        {
                            NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                            if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                            sb.Append("<th>" + cell.ToString() + "</th>");
                        }
                        sb.Append("</tr>");
                        sb.AppendLine("<tr>");
                        for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                        {

                            IRow row = sheet.GetRow(i);

                            var std = new TblStudentRegistration()
                            {
                                Type = "Student",
                                FirstName = row.Cells[1].ToString(),
                                LastName = row.Cells[2].ToString(),
                                DisplayName = row.Cells[3].ToString(),
                                Gender = row.Cells[4].ToString(),
                                Dob =Convert.ToDateTime(row.Cells[5].ToString()),
                                Address = row.Cells[6].ToString(),
                                Username = row.Cells[7].ToString()
                            };
                            princetonhive.TblStudentRegistration.Add(std);
                            princetonhive.SaveChanges();

                            
                        }
                        sb.Append("</table>");
                    }
                }

                return this.Content(sb.ToString());

            }


            return Page();




        }
    }
}