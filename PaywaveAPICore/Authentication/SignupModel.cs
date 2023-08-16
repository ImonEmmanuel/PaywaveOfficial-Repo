using System;

namespace PaywaveAPICore.Authentication
{
    public class SignupModel
    {
        public string Email {get; set;}
        public string UserName {get;set;}
        public string Password {get;set;}
        public string ConfirmPassword {get; set;}
    }
}