using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

[TestFixture]
public class OpenEShopAutomationTests
{
    private IWebDriver driver;
    private string baseUrl = "https://open-eshop.stqa.ru/";

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver(); // Initialize ChromeDriver
        driver.Manage().Window.Maximize(); // Maximize browser window
    }

    [Test]
    public void LoginNavigateToOrdersLogout()
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        driver.Navigate().GoToUrl(baseUrl);
        IWebElement loginButton = driver.FindElement(By.XPath("//a[contains(@href,'#login-modal')]"));
        loginButton.Click();
        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//form[contains(@class, 'auth')]")));

        IWebElement usernameField = driver.FindElement(By.Name("email"));
        IWebElement passwordField = driver.FindElement(By.Name("password"));
        IWebElement loginButtonSecond = driver.FindElement(By.XPath("//button[@type='submit']"));

        usernameField.SendKeys("demo@open-eshop.com");
        passwordField.SendKeys("demo");
        loginButtonSecond.Click();
        IWebElement dropdownMenuButton = driver.FindElement(By.XPath("//a[@data-toggle='dropdown']"));
        dropdownMenuButton.Click();
        IWebElement userOrdersButton = driver.FindElement(By.XPath("//a[contains(@href,'profile/orders')]"));
        userOrdersButton.Click();


        wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//h1")));

        Assert.IsTrue(DoesElementExist(By.XPath("//a[contains(@title, 'print order')]")));
        IWebElement dropdownButton = driver.FindElement(By.XPath("//span[@class='caret']"));
        dropdownButton.Click();
        IWebElement logoutButton = driver.FindElement(By.XPath("//a[contains(@href,'auth/logout')]"));
        logoutButton.Click();
    }

    [TearDown]
    public void Cleanup()
    {
        driver.Quit();
    }

    private bool DoesElementExist(By locator)
    {
        try
        {
            driver.FindElement(locator);
            return true;
        }
        catch (NoSuchElementException)
        {
            return false;
        }
    }
}
