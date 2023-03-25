using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestTube.PageObjectModels
{
	public class FileUploaderPage
	{
        private readonly ChromeDriver Driver;

        internal string FileUploaderUrl = "https://the-internet.herokuapp.com/upload";

        string chooseFileButtonId = "file-upload";
        internal By ChooseFileButton { get => By.Id(chooseFileButtonId); }

        string uploadButtonId = "file-submit";
        internal By UploadButton { get => By.Id(uploadButtonId); }

        string fileUploadedHeaderXpath = "//h3[contains(text(), \"File Uploaded!\")]";
        internal By FileUploadedHeader { get => By.XPath(fileUploadedHeaderXpath); }

        string uploadedFilesPanelId = "uploaded-files";
        internal By UploadedFilesPanel { get => By.Id(uploadedFilesPanelId); }

        public FileUploaderPage(ChromeDriver driver)
        {
            Driver = driver;
        }

        internal void NavigateToFileUploaderUrl()
        {
            Driver.Navigate().GoToUrl(FileUploaderUrl); ;
        }

        internal void UploadPantsJacketImage(string imagePath)
        {
            Driver.FindElement(ChooseFileButton).SendKeys(imagePath);
            Driver.FindElement(UploadButton).Click();

        }
    }
}

