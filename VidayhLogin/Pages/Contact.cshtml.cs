using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using VidayhLogin.Enum;
using VidayhLogin.Helper;
using VidayhLogin.Model;

namespace VidayhLogin.Pages
{
    public class ContactModel : PageModel
    {
        private readonly string _connection = string.Empty;
        private readonly IConfiguration _configuration;
        HttpClient _client;
        string layoutImagePath = String.Empty;
        private IHostingEnvironment _hostingEnvironment;

        public ContactModel(IConfiguration config, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = config;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_configuration["base_Url"]);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _connection = _configuration["ConnectionStrings:Princetonhive"];
        }

        [BindProperty]
        public string SchoolName { get; set; }
        [BindProperty]
        public string Location { get; set; }
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string DesignationType { get; set; }
        [BindProperty]
        public string MobileNo { get; set; }
        [BindProperty]
        public string EmailAddress { get; set; }
        [BindProperty]
        public string Description { get; set; }
        [BindProperty]
        public string Username { set; get; }
        [BindProperty]
        public string Password { set; get; }
        [BindProperty]
        public string Role { set; get; }
        [BindProperty]
        public string Username1 { set; get; }
        [BindProperty]
        public string Email { set; get; }
        [BindProperty]
        public string Username2 { set; get; }
        [BindProperty]
        public string OldPassword { set; get; }
        [BindProperty]
        public string NewPassword { set; get; }
        [BindProperty]
        public string ConfirmPassword { set; get; }

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
        //public async Task OnPost()
        //{

        //    MailMessage mail = new MailMessage();

        //    mail.From = new MailAddress("monteageservices@gmail.com", "Monteage IT Solutions Private Limited");
        //    mail.Subject = "Contact Us";


        //    mail.Body = "School Name : " + SchoolName + "\n" + "Location :" + Location + "\n" + "Name : " + Name + "\n" + "Designation : " + DesignationType + "\n" + "Mobile No : " + MobileNo + "\n" + "Email Address :" + EmailAddress + "\n" + "Description :" + Description;

        //    mail.IsBodyHtml = true;
        //    mail.Priority = MailPriority.High;

        //    mail.ReplyTo = new MailAddress("monteageservices@gmail.com");

        //    mail.To.Add("yogesh@monteage.in");
        //    mail.To.Add("deepak@monteage.in");


        //    SmtpClient smtp = new SmtpClient();
        //    try
        //    {
        //        NetworkCredential auth = new NetworkCredential("monteageservices@gmail.com", "monteage@mts20");


        //        smtp.Host = "smtp.gmail.com";
        //      //  smtp.Host = "relay-hosting.secureserver.net";
        //        smtp.Port = 587;

        //        smtp.EnableSsl = true;

        //        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

        //        smtp.UseDefaultCredentials = false;

        //        smtp.Credentials = auth;

        //        smtp.Send(mail);
        //        ViewData["message"] = "mail sent";
        //      //  Response.Write(@"<script language='javascript'>alert('Message: \n" + "Hi!" + " .');</script>");
        //    }
        //    catch (SmtpException Ex)
        //    {
        //        ViewData["message"] = "mail not sent";
        //        //  return Content("<script language='javascript' type='text/javascript'>alert('Thanks for Feedback!');</script>");
        //        throw new SmtpException(Ex.Message, Ex);

        //    }
        //    mail.Dispose();
        //    smtp = null;
        //  //  ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Thank you for submitting your details.');", true);
        //    //MailMessage msg = new MailMessage();
        //    //System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
        //    //try
        //    //{
        //    //    msg.Subject = "Princetonhive Contact Enquiry";
        //    //    msg.Body = "Add Email Body Part";
        //    //    msg.From = new MailAddress("monteageservices@gmail.com");
        //    //    msg.To.Add("yogesh@monteage.in");
        //    //    msg.IsBodyHtml = true;
        //    //    client.Host = "relay-hosting.secureserver.net";
        //    //    System.Net.NetworkCredential basicauthenticationinfo = new System.Net.NetworkCredential("monteageservices@gmail.com", "monteage@mts20");
        //    //    client.Port = int.Parse("587");
        //    //    client.EnableSsl = true;
        //    //    client.UseDefaultCredentials = false;
        //    //    client.Credentials = basicauthenticationinfo;
        //    //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    //    client.Send(msg);
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //  //  log.Error(ex.Message);
        //    //}


        //    //SmtpClient client = new SmtpClient("smtp.gmail.com");
        //    //client.UseDefaultCredentials = false;
        //    //client.Credentials = new NetworkCredential("monteageservices@gmail.com", "monteage@mts20");

        //    //MailMessage mailMessage = new MailMessage();
        //    //mailMessage.From = new MailAddress("monteageservices@gmail.com");
        //    //mailMessage.To.Add("yogesh@monteage.in");
        //    //mailMessage.Body = "body";
        //    //mailMessage.Subject = "testing princetonhive mail";
        //    //client.Send(mailMessage);

        //    //using (var smtp = new SmtpClient())
        //    //{
        //    //    var credential = new NetworkCredential
        //    //    {
        //    //        UserName = "monteageservices@gmail.com",  // replace with valid value
        //    //        Password = "monteage@mts20"  // replace with valid value
        //    //    };
        //    //    smtp.Credentials = credential;
        //    //    smtp.Host = "smtp.gmail.com";
        //    //    smtp.Port = 465;
        //    //    smtp.EnableSsl = true;
        //    //    smtp.UseDefaultCredentials = true;
        //    //    string body = "School Name : " + SchoolName + "\n" + "Location :" + Location + "\n" + "Name : " + Name + "\n" + "Designation : " + DesignationType + "\n" + "Mobile No : " + MobileNo + "\n" + "Email Address :" + EmailAddress + "\n" + "Description :" + Description;

        //    //    var emailMessage = new MailMessage();
        //    //    emailMessage.From = new MailAddress("monteageservices@gmail.com");
        //    //    emailMessage.To.Add("yogesh@monteage.in");
        //    //    emailMessage.Subject = "Contact Us";

        //    //    emailMessage.Body = body;
        //    //    smtp.UseDefaultCredentials = true;

        //    //  //  smtp.Credentials = auth;

        //    //  //  smtp.Send(emailMessage);
        //    //    await smtp.SendMailAsync(emailMessage);
        //    //}
        //}

        public async Task<IActionResult> OnPostAsync(string savemore)
        {
            try
            {
                if (savemore == "LOGIN")
                {
                    if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                    {
                        await Response.WriteAsync("<script language='javascript'>window.alert('UserId or Password should not be blank');window.location='Index';</script>");

                    }


                    UsersModel userParam = new UsersModel { Username = Username, Password = Password };
                    var response = await _client.PostAsJsonAsync("Users/Authenticate", userParam);
                    var loginResult = response.Content.ReadAsStringAsync().Result;
                    var content = JsonConvert.DeserializeObject<ResponseViewModel>(loginResult);
                    if (content.statuscode == (int)StatusCodesStatus.SuccessCode)
                    {

                        string Username = Convert.ToString(content.data.Username);
                        string SchoolName = Convert.ToString(content.data.SchoolName);
                        //  string User = Convert.ToString(content.data.FirstName);
                        string token = Convert.ToString(content.data.Token);
                        string firstname = Convert.ToString(content.data.FirstName);
                        string Logo = Convert.ToString(content.data.Logo);
                        //   string fatheremail = Convert.ToString(content.data.FatherEmail);
                        string Class = Convert.ToString(content.data.Class);
                        string Mobile = Convert.ToString(content.data.FatherMobile);
                        string Email = Convert.ToString(content.data.Email);
                        string Role = Convert.ToString(content.data.Role);
                        //    HttpContext.Session.SetString("fatheremail", fatheremail);
                        HttpContext.Session.SetString("Role", Role);
                        HttpContext.Session.SetString("Mobile", Mobile);
                        HttpContext.Session.SetString("Email", Email);
                        HttpContext.Session.SetString("Class", Class);
                        HttpContext.Session.SetString("firstname", firstname);
                        HttpContext.Session.SetString("SchoolName", SchoolName);
                        HttpContext.Session.SetString("Logo", Logo);
                        HttpContext.Session.SetString("UserName1", Username);
                        HttpContext.Session.SetString("Username", Username);
                        HttpContext.Session.SetString("token", token);
                        return RedirectToPage("/Dashboard");
                        //  Response.Redirect("https://belouga.org/api/login/"+token);

                    }
                    if (content.statuscode == (int)StatusCodesStatus.BadRequest)
                    {
                        if (content.message == "Password is invalid")
                        {
                            await Response.WriteAsync("<script language='javascript'>window.alert('Please Check Your Password');window.location='contact';</script>");

                        }
                        else
                        {
                            await Response.WriteAsync("<script language='javascript'>window.alert('Please Check Your Username');window.location='contact';</script>");

                        }

                    }
                    if (content.statuscode == (int)StatusCodesStatus.InternaServerError)
                    {
                        await Response.WriteAsync("<script language='javascript'>window.alert('Something Went Wrong.');window.location='contact';</script>");

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
                    await Response.WriteAsync("<script language='javascript'>window.alert('Currently We are unable to reset password.');window.location='contact';</script>");
                    return Page();
                }
                else if (savemore == "Change Password")
                {
                    UsersModel userParam = new UsersModel { Username = Username2, Password = OldPassword, NewPassword = NewPassword };
                    var response = await _client.PostAsJsonAsync("Users/ChangePassword", userParam);
                    var loginResult = response.Content.ReadAsStringAsync().Result;
                    var content = JsonConvert.DeserializeObject<ResponseViewModel>(loginResult);
                    if (content.statuscode == (int)StatusCodesStatus.SuccessCode)
                    {

                        await Response.WriteAsync("<script language='javascript'>window.alert('Password Changed Successfully');window.location='contact';</script>");
                        return Page();

                    }
                    else

                    {
                        await Response.WriteAsync("<script language='javascript'>window.alert('UserName and Password does not exists.');window.location='Index';</script>");
                        return Page();
                    }

                }

            }
            catch (Exception ex)
            {
                await Response.WriteAsync("<script language='javascript'>window.alert('Something Went Wrong.');window.location='contact';</script>");
                throw ex;
            }
            return Page();
        }
    }
}