using ManageBookLibrary.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using System.Numerics;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace ProjectPRN221.Pages
{
    public class ForgetPassModel : PageModel
    {
        IAccountRepository accountRepository = new AccountRepository();
        public string Phone { get; set; }
        public void OnGet()
        {
        }

        public void OnPost(string Phone) {
            var acc = accountRepository.GetAllAccounts().FirstOrDefault(e => e.Phone == Phone);
            if (acc == null)
            {
                ViewData["checkPhone"] = "Error";
            }
            else
            {
                int index = Phone.IndexOf('0');
                if (index >= 0)
                {
                    Phone = Phone.Substring(0, index) + "+84" + Phone.Substring(index + 1);
                }
                string newpass = SignUpModel.GenerateVerificationCode();
                var accountSid = "ACef875dab95bd8f3737e10358fe6311af";
                var authToken = "2bf46c68bbcb440feaa4bc17f830ffcf";
                TwilioClient.Init(accountSid, authToken);
                Console.WriteLine(newpass);

                var message = MessageResource.Create(to: new Twilio.Types.PhoneNumber(Phone),
                from: new Twilio.Types.PhoneNumber("+14179003335"),
                    body: "Hello " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + $"/nYour code is: {newpass}");
                acc.Password = newpass;
                accountRepository.UpdateAccount(acc);
                ViewData["phone"] = Phone;
            }
        }
    }
}
