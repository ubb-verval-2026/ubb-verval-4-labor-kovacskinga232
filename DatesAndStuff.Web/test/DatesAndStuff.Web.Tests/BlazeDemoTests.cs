using System;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace DatesAndStuff.Web.Tests;

[TestFixture]
public class BlazeDemoTests
{
    private IWebDriver driver;
    private const string BaseURL = "https://blazedemo.com";

    [SetUp]
    public void SetupTest()
    {
        driver = new ChromeDriver();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        driver.Manage().Window.Maximize();
    }

    [TearDown]
    public void TeardownTest()
    {
        try
        {
            driver.Quit();
            driver.Dispose();
        }
        catch (Exception)
        {
        }
    }

    [Test]
    public void FlightSearch_MexicoCityToDublin_Alternative()
    {
        driver.Navigate().GoToUrl(BaseURL);

        var fromPort = driver.FindElement(By.Name("fromPort"));

        fromPort.FindElement(By.XPath("//option[@value='Mexico City']")).Click();

        var toPort = driver.FindElement(By.Name("toPort"));
        toPort.FindElement(By.XPath("//option[@value='Dublin']")).Click();

        driver.FindElement(By.CssSelector("input[type='submit']")).Click();

        var flightRows = driver.FindElements(By.XPath("//table[@class='table']/tbody/tr"));
        flightRows.Count.Should().BeGreaterThanOrEqualTo(3);
    }
}