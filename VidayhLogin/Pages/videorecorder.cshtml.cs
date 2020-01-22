using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using VidayhLogin.Model;

namespace VidayhLogin
{
    public class videorecorderModel : PageModel
    {
        private readonly string _connection = string.Empty;
        private readonly IConfiguration _configuration;
        HttpClient _client;
        string layoutImagePath = String.Empty;
        private IHostingEnvironment _hostingEnvironment;
        public videorecorderModel(IConfiguration config, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = config;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_configuration["base_Url"]);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           
        }


        [BindProperty]
        public string searchjson { get; set; }
        [BindProperty]
        public string fulljsondata { get; set; }
        [BindProperty]
        public string skeltonjsondata { get; set; }
        [BindProperty]
        public string nlpjsondata { get; set; }
        [BindProperty]
        public string nlp_textjsondata { get; set; }
        [BindProperty]
        public string UUID { get; set; }
        [BindProperty]
        public IFormFile uploadfile { get; set; }
        string Name, startdatetime;
        dynamic facedata, skeltondata, NLPdata, NLP_textdata;

        public IList<JsonViewModel> JsonView { get; private set; }
        List<JsonViewModel> JsonView1 = new List<JsonViewModel>();

        public async Task<IActionResult> OnGet(string uuid, string date)
        {
            try
            {
                UUID = HttpContext.Session.GetString("Username");
                searchjson = HttpContext.Session.GetString("Username");
                if (uuid != null && date != null)
                {
                    using (NpgsqlConnection connection = new NpgsqlConnection())
                    {
                        connection.ConnectionString = "Server=103.118.157.29;Port=5432;Database=Princetonhive;User Id=postgres;Password=princetonhive@123;";
                        connection.Open();



                        string sql4 = "SELECT DISTINCT uuid,startdatetime FROM analysis WHERE uuid = '" + uuid + "' and type='face'";
                        NpgsqlCommand cmd4 = new NpgsqlCommand(sql4, connection);
                        NpgsqlDataReader dr4;
                        dr4 = cmd4.ExecuteReader();
                        while (dr4.Read())
                        {
                            JsonViewModel p = new JsonViewModel();
                            p.uuid = Convert.ToString(dr4["uuid"].ToString());
                            p.date = Convert.ToString(dr4["startdatetime"].ToString());
                            JsonView1.Add(p);
                        }
                        JsonView = JsonView1;
                        dr4.Close();

                        string sql = "SELECT jsondata,uuid,startdatetime FROM analysis WHERE uuid= '" + uuid + "' and startdatetime='" + date + "' and type='face'";
                        NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
                        NpgsqlDataReader dr;
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            var facejsondata = Convert.ToString(dr["jsondata"].ToString());
                            Name = Convert.ToString(dr["uuid"].ToString());
                            startdatetime = Convert.ToString(dr["startdatetime"].ToString());
                            facedata = JObject.Parse(facejsondata);
                        }
                        dr.Close();

                        string sql1 = "SELECT jsondata FROM analysis WHERE uuid= '" + uuid + "' and startdatetime='" + date + "' and type='skelton'";
                        NpgsqlCommand cmd1 = new NpgsqlCommand(sql1, connection);
                        NpgsqlDataReader dr1;
                        dr1 = cmd1.ExecuteReader();
                        while (dr1.Read())
                        {
                            skeltonjsondata = Convert.ToString(dr1["jsondata"].ToString());
                            skeltondata = JObject.Parse(skeltonjsondata);

                        }
                        dr.Close();

                        string sql2 = "SELECT jsondata FROM analysis WHERE uuid= '" + uuid + "' and startdatetime='" + date + "' and type='nlp_audio'";
                        NpgsqlCommand cmd2 = new NpgsqlCommand(sql2, connection);
                        NpgsqlDataReader dr2;
                        dr2 = cmd2.ExecuteReader();
                        while (dr2.Read())
                        {
                            nlpjsondata = Convert.ToString(dr2["jsondata"].ToString());
                            NLPdata = JObject.Parse(nlpjsondata);
                        }
                        dr.Close();


                        string sql3 = "SELECT jsondata FROM analysis WHERE uuid= '" + uuid + "' and startdatetime='" + date + "' and type='nlp_text'";
                        NpgsqlCommand cmd3 = new NpgsqlCommand(sql3, connection);
                        NpgsqlDataReader dr3;
                        dr3 = cmd3.ExecuteReader();
                        while (dr3.Read())
                        {
                            nlp_textjsondata = Convert.ToString(dr3["jsondata"].ToString());
                            NLP_textdata = JObject.Parse(nlp_textjsondata);
                        }
                        dr.Close();

                        var Face = facedata["face"];
                        var skelton = skeltondata["skelton"];
                        var nlp = NLPdata;
                        var nlp_text = NLP_textdata;

                        JObject jo = facedata;
                        jo.Property("face").Remove();

                        jo["uuid"] = Name;
                        jo["date"] = startdatetime;
                        if (Face != null)
                        {
                            jo["face"] = Face;
                        }
                        if (skelton != null)
                        {
                            jo["skelton"] = skelton;
                        }

                        if (nlp != null)
                        {
                            jo["nlp_audio"] = nlp;
                        }

                        if (nlp_text != null)
                        {
                            jo["nlp_text"] = nlp_text;
                        }



                        var json123 = JsonConvert.SerializeObject(jo);
                        fulljsondata = json123;
                    }
                }

            }
            catch (Exception ex)
            {
                //  await Response.WriteAsync("<script language='javascript'>window.alert('Something Went Wrong.');window.location='treeview';</script>");
                throw ex;
            }
            return Page();
        }

        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

        public async Task<IActionResult> OnPostAsync(string savemore)
        {
            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection())
                {
                    connection.ConnectionString = "Server=103.118.157.29;Port=5432;Database=Princetonhive;User Id=postgres;Password=princetonhive@123;";
                    connection.Open();

                    if (savemore == "search")
                    {
                        string sql = "SELECT jsondata,uuid,startdatetime FROM analysis WHERE uuid ilike '%" + searchjson + "%' and type='face'";
                        NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
                        NpgsqlDataReader dr;
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            JsonViewModel p = new JsonViewModel();
                            p.uuid = Convert.ToString(dr["uuid"].ToString());
                            p.date = Convert.ToString(dr["startdatetime"].ToString());
                            JsonView1.Add(p);
                        }
                        JsonView = JsonView1;
                        dr.Close();
                    }
                    else
                    {
                        string folderName = "Upload";
                        string webRootPath = _hostingEnvironment.WebRootPath;
                        string newPath = Path.Combine(webRootPath, folderName);
                        if (!Directory.Exists(newPath))
                        {
                            Directory.CreateDirectory(newPath);
                        }
                        if (uploadfile != null)
                        {
                            string fileName = ContentDispositionHeaderValue.Parse(uploadfile.ContentDisposition).FileName.Trim('"');
                            FileInfo fi = new FileInfo(fileName);
                            string extension = fi.Extension;
                            string datenow = indianTime.ToString("ddMMyyyy");
                            string timenow= indianTime.ToString("HHmmss");
                            var saveImageName = HttpContext.Session.GetString("Username") + '_' + datenow + '_' + timenow + extension;
                            string fullPath = Path.Combine(newPath, saveImageName);
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                await uploadfile.CopyToAsync(stream);
                            }
                            await Response.WriteAsync("<script language='javascript'>window.alert('File Upload Successfully.');window.location='videorecorder';</script>");
                            return Page();
                        }
                        else
                        {

                        }
                    }
                   
                    connection.Close();

                }
            }
            catch (Exception ex)
            {
                //  await Response.WriteAsync("<script language='javascript'>window.alert('Something Went Wrong.');window.location='treeview';</script>");
                throw ex;
            }

            return Page();
        }
    }
}