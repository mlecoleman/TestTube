using System;
using OpenQA.Selenium;


namespace TestTube.PageObjectModels
{
	internal class WordpressLoginPage
	{
        internal string wordpressLoginUrl = "https://www.pants.org/wp-login.php";

        string emailAddressInputId = "user_login";
        internal By emailAddressInput { get => By.Id(emailAddressInputId); }

        string passwordInputId = "user_pass";
        internal By passwordInput { get => By.Id(passwordInputId); }

        string proveHumanityInputId = "jetpack_protect_answer";
        internal By proveHumanityInput { get => By.Id(proveHumanityInputId); }

        string loginButtonId = "wp-submit";
        internal By loginButton { get => By.Id(loginButtonId); }

        string invalidLoginCredentialsErrorId = "login_error";
        internal By invalidLoginCredentialsError { get => By.Id(invalidLoginCredentialsErrorId); }

        internal string wordpressErrorPageTitle = "WordPress › Error";

        string notHumanErrorPageId = "error-page";
        internal By notHumanErrorPage { get => By.Id(notHumanErrorPageId); }
    }
}

