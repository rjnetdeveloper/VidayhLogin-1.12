using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Registration.Entities;
using Registration.Enum;
using Registration.Helper;
using Registration.Model;

namespace Registration
{
    public class LoginModel : PageModel
    {
        private readonly PrincetonhiveContext _context;
        private readonly IConfiguration _configuration;
        HttpClient _client;
        private ILogger<LoginModel> _logger;

        public LoginModel(IConfiguration config, PrincetonhiveContext context, ILogger<LoginModel> logger)
        {
            _configuration = config;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_configuration["base_Url"]);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _context = context;
            _logger = logger;
        }

        public void OnGet()
        {

            //var token = HttpContext.Session.GetString("token");
            //if (token != null)
            //{
            //    return RedirectToPage("/Dashboard");
            //}
            //else
            //{
            //    return Page();
            //}
        }

        [BindProperty]
        public string Username { set; get; }
        [BindProperty]
        public string Password { set; get; }
        [BindProperty]
        public string Role { set; get; }
       

        public async Task<IActionResult> OnPostAsync(string savemore)
        {
            _logger.LogInformation(LoggingEvents.Login, LoggingEvents.Login+":Username ({0}), Password({1}) has been entered on datetime({2})", Username, Password, DateTime.UtcNow.AddHours(5.30));
            try
            {
                if (savemore == "Login")
                {
                    //if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                    //{
                    //    await Response.WriteAsync("<script language='javascript'>window.alert('UserId or Password should not be blank');window.location='Index';</script>");

                    //}


                    var user = (from a in _context.UserLogin
                                where a.UserName == Username
                                select a.UserName).FirstOrDefault();
                    if (user == null)
                    {
                        _logger.LogInformation(LoggingEvents.Login, LoggingEvents.Login+ ":Username ({0}) is invalid on datetime({1})", Username, DateTime.UtcNow.AddHours(5.30));
                        await Response.WriteAsync("<script language='javascript'>window.alert('UserName is invalid');window.location='Login';</script>");
                        return Page();
                    }
                    else
                    {

                    }

                    var pass = (from a in _context.UserLogin
                                join b in _context.TblRegistration on a.UserName equals b.Username
                                where a.UserName == Username && a.Password == Encrypt(Password)
                                select new { a.Password, b.RegistrationId,b.DisplayName,a.UserRole }).FirstOrDefault();

                    if (pass == null)
                    {
                        _logger.LogInformation(LoggingEvents.Login, LoggingEvents.Login+ ":Password ({0}) is invalid on datetime({1})", Password, DateTime.UtcNow.AddHours(5.30));
                        await Response.WriteAsync("<script language='javascript'>window.alert('Password is invalid');window.location='Login';</script>");
                        return Page();
                    }
                    else
                    {
                        if (pass.UserRole.ToString() == "MasterAdmin")
                        {
                            _logger.LogInformation(LoggingEvents.Login, LoggingEvents.Login+ ":Username ({0}), Password({1}) has been passed on datetime({2})", Username, Password, DateTime.UtcNow.AddHours(5.30));
                            //  HttpContext.Session.SetString("SchoolIdBelouga", pass.SchoolIdBelouga.ToString());
                            HttpContext.Session.SetString("DisplayName", "MasterAdmin");
                            return RedirectToPage("/Dashboard");
                        }
                        else if (pass.UserRole.ToString() == "Admin")
                        {
                            _logger.LogInformation(LoggingEvents.Login, LoggingEvents.Login+ ":Username ({0}), Password({1}) has been passed on datetime({2})", Username, Password, DateTime.UtcNow.AddHours(5.30));
                            HttpContext.Session.SetString("SchoolIdBelouga", pass.RegistrationId.ToString());
                            //HttpContext.Session.SetString("DisplayName", pass.DisplayName.ToString());
                            //return RedirectToPage("/Dashboard");
                        }
                        else
                        {
                            _logger.LogInformation(LoggingEvents.Login, LoggingEvents.Login+":Username ({0}), Password({1}) is not Admin Credentials on datetime({2})", Username, Password, DateTime.UtcNow.AddHours(5.30));
                            await Response.WriteAsync("<script language='javascript'>window.alert('This is not Admin Credentials');window.location='Login';</script>");
                            return Page();
                        }
                    }

                      
                }
                else if (savemore == "Send Password")
                {
                    //UsersModel userParam = new UsersModel { Username = Username1};
                    //var response = await _client.PostAsJsonAsync("Users/ForgetPassword", userParam);
                    //var loginResult = response.Content.ReadAsStringAsync().Result;

                    //var content = JsonConvert.DeserializeObject<ResponseViewModel>(loginResult);
                    //if (content.statuscode == (int)StatusCodesStatus.SuccessCode)
                    //{

                    //    //if (content.data == null)
                    //    //{
                    //    //    await Response.WriteAsync("<script language='javascript'>window.alert(content.message);window.location='Index';</script>");
                    //    //    return Page();
                    //    //}
                    //    //else
                    //    //{
                    //        MailMessage mail = new MailMessage();

                    //        mail.From = new MailAddress("monteageservices@gmail.com", "Princetonhive");
                    //        mail.Subject = "Password Reset";


                    //        mail.Body = "Dear " + content.data.Item2 + "<Br>" + "<Br>" + "We have sent you this email in response to your request to forget your password" + "<Br>" + "<Br>" + "<Br>" + "Username :" + Username1 + "<br>" + "Password :" + content.data.Item1;

                    //        //     mail.Body = "Name : " + txtname.Text + "<br>" + "Email : " + txtemail.Text + "<br>" + "Phone no. : " + txtphone.Text + "<br>" + "subject : " + txtsubject.Text + "<br>" + "Query : " + txtmessage.Text;


                    //        mail.IsBodyHtml = true;
                    //        mail.Priority = MailPriority.High;
                    //        string mailid = content.data.Item3;
                    //        mail.ReplyTo = new MailAddress(mailid);

                    //        SmtpClient smtp = new SmtpClient();
                    //        try
                    //        {
                    //            NetworkCredential auth = new NetworkCredential("monteageservices@gmail.com", "monteage@mts20");


                    //            smtp.Host = "smtp.gmail.com";
                    //            //  smtp.Host = "relay-hosting.secureserver.net";
                    //            smtp.Port = 25;

                    //            smtp.EnableSsl = true;

                    //            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                    //            smtp.UseDefaultCredentials = false;

                    //            smtp.Credentials = auth;

                    //            smtp.Send(mail);
                    //        }
                    //        catch (SmtpException Ex)
                    //        {
                    //            throw new SmtpException(Ex.Message, Ex);

                    //        }
                    //        mail.Dispose();
                    //        smtp = null;

                    //        await Response.WriteAsync("<script language='javascript'>window.alert('Your Password Send Successfully on'.Please Collect from this mailid.);window.location='Index';</script>");
                    //    //}
                    //}
                    await Response.WriteAsync("<script language='javascript'>window.alert('Currently We are unable to reset password.');window.location='Index';</script>");
                    return Page();
                }
                else if (savemore == "Change Password")
                {
                    //UsersModel userParam = new UsersModel { Username = Username2, Password = OldPassword, NewPassword = NewPassword };
                    //var response = await _client.PostAsJsonAsync("Users/ChangePassword", userParam);
                    //var loginResult = response.Content.ReadAsStringAsync().Result;
                    //var content = JsonConvert.DeserializeObject<ResponseViewModel>(loginResult);
                    //if (content.statuscode == (int)StatusCodesStatus.SuccessCode)
                    //{

                    //    await Response.WriteAsync("<script language='javascript'>window.alert('Password Changed Successfully');window.location='Index';</script>");
                    //    return Page();

                    //}
                    //else

                    //{
                    //    await Response.WriteAsync("<script language='javascript'>window.alert('UserName and Password does not exists.');window.location='Index';</script>");
                    //    return Page();
                    //}

                }

            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.Login, LoggingEvents.Login+ ":"+ex.Message, Username, Password, DateTime.UtcNow.AddHours(5.30));
                await Response.WriteAsync("<script language='javascript'>window.alert('Something Went Wrong.');window.location='Login';</script>");
                throw ex;
            }
            return Page();
        }

        public string Encrypt(string value)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(value);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}