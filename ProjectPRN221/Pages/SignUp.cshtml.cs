using ManageBookLibrary.BusinessObject;
using ManageBookLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Mail;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System.Text;

namespace ProjectPRN221.Pages
{
    public class SignUpModel : PageModel
    {
        IAccountRepository accountRepository = new AccountRepository();
        IRoleRepository roleRepository = new RoleRepository();
        public string code { get; set; } = GenerateVerificationCode();
        public Account account { get; set; }
        public void OnGet(string? accPhone)
        {
            List<Role> listRoles = roleRepository.GetRoles().Where(r => r.RoleName != "Admin").ToList();
            if (accPhone != null)
            {
                var accFind = accountRepository.GetAllAccounts().Where(s => s.Phone == accPhone).ToList();
                accFind[0].Status = true;
                accountRepository.UpdateAccount(accFind[0]);
            }
            ViewData["listRoles"] = listRoles;
        }

        public void OnPost(Account account)
        {

            account.CreatedTime = DateTime.Now;
            account.Status = false;
            List<Role> listRoles = roleRepository.GetRoles().Where(r => r.RoleName != "Admin").ToList();
            if (accountRepository.GetAllAccounts().SingleOrDefault(s => s.Email == account.Email) != null ||
                accountRepository.GetAllAccounts().SingleOrDefault(s => s.Phone == account.Phone) != null
                )
            {
                if (accountRepository.GetAllAccounts().SingleOrDefault(s => s.Email == account.Email) != null)
                {
                    ViewData["emaildub"] = "emaildub";
                }
                if (accountRepository.GetAllAccounts().SingleOrDefault(s => s.Phone == account.Phone) != null)
                {
                    ViewData["phonedub"] = "phonedub";
                }
                ViewData["account"] = account;
            }
            else
            {
                try
                {
                    string phone = account.Phone;
                    int index = phone.IndexOf('0');
                    if (index >= 0)
                    {
                        phone = phone.Substring(0, index) + "+84" + phone.Substring(index + 1);
                    }
                    var accountSid = "ACef875dab95bd8f3737e10358fe6311af";
                    var authToken = "a0f53565f0f388d9f680d80ff332170e";
                    TwilioClient.Init(accountSid, authToken);
                    Console.WriteLine(phone);

                    var message = MessageResource.Create(to: new Twilio.Types.PhoneNumber(phone),
                        from: new Twilio.Types.PhoneNumber("+14179003335"),
                        body: "Hello " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + $"/nYour code is: {code}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                Console.WriteLine($"{code}");
                accountRepository.InsertAccount(account);
                ViewData["accountPhone"] = account.Phone;
                ViewData["code"] = code;
                ViewData["infor"] = "registersuss";
                ViewData["LetCheck"] = "Check";

            }


            ViewData["listRoles"] = listRoles;
        }

        public static string GenerateVerificationCode()
        {
            int codeLength = 4; 
            string characters = "0123456789"; 
            StringBuilder codeBuilder = new StringBuilder();

            Random random = new Random();
            for (int i = 0; i < codeLength; i++)
            {
                int index = random.Next(characters.Length);
                codeBuilder.Append(characters[index]);
            }

            return codeBuilder.ToString();
        }
    }
}
