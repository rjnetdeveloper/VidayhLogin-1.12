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
using Microsoft.Extensions.Logging;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Registration.Entities;
using Registration.Helper;

namespace Registration
{
    public class ImportStudentModel : PageModel
    {
        private readonly IConfiguration _configuration;
        HttpClient _client;
        private object Router;
        private readonly string _connection = string.Empty;
        private IHostingEnvironment _hostingEnvironment;
        private readonly PrincetonhiveContext _context;
        private ILogger<LoginModel> _logger;

        public ImportStudentModel(IConfiguration config, IHostingEnvironment hostingEnvironment, PrincetonhiveContext context, ILogger<LoginModel> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = config;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_configuration["base_Url"]);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _connection = _configuration["ConnectionStrings:EduConn"];
            _context = context;
            _logger = logger;
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
            try
            {
                var princetonhive = new PrincetonhiveContext();

                if (savemore == "Import")
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
                                //  string aa = row.Cells[0].ToString();
                                if (row != null)
                                {
                                    if (row.Cells[0].ToString() != null || row.Cells[0].ToString() != "")
                                    {
                                        var std = new TblStudentRegistration()
                                        {
                                            Type = "Student",
                                            FirstName = row.Cells[0].ToString(),
                                            LastName = row.Cells[1].ToString(),
                                            DisplayName = row.Cells[2].ToString(),
                                            Gender = row.Cells[3].ToString(),
                                            Dob = Convert.ToDateTime(row.Cells[4].ToString()),
                                            Address = row.Cells[5].ToString(),
                                            Username = row.Cells[6].ToString(),
                                            SchoolName = StudentSchool

                                        };
                                        princetonhive.TblStudentRegistration.Add(std);
                                       
                                    }
                                }

                            }
                            princetonhive.SaveChanges();
                            _logger.LogInformation(LoggingEvents.InsertItem, LoggingEvents.InsertItem + ":Data import sucessfully on datetime({0})", DateTime.UtcNow.AddHours(5.30));
                            await Response.WriteAsync("<script language='javascript'>window.alert('Data Saved Successfully');window.location='ImportExportStudent';</script>");
                            sb.Append("</table>");
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                _logger.LogError(LoggingEvents.SomethingWentWrong, LoggingEvents.SomethingWentWrong + ":" + ex.Message, DateTime.UtcNow.AddHours(5.30));
                await Response.WriteAsync("<script language='javascript'>window.alert('Something Went Wrong.');window.location='Login';</script>");
                throw ex;
            }

            return Page();

        }
    }
}