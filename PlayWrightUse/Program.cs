using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlayWrightUse
{
    class Program
    {
        public static async Task Main()
        {
            //await FirstPageLogin();
            //await SecondPageLogin();
            await ThirdPageLogin();
        }
        public static async Task FirstPageLogin()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false, SlowMo = 4000 });
            await using var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();
            await page.GotoAsync("https://deviser.tech/crawler/1/");
            await page.ClickAsync("form[method='post']");
            await page.FillAsync("input[type='text']", "user");
            await page.FillAsync("input[type='password']", "123456");
            await page.ClickAsync("[type='submit']");
            var resultInnerText = await page.InnerTextAsync("div");
            Console.WriteLine(resultInnerText);
        }
        public static async Task SecondPageLogin()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false, SlowMo = 4000 });
            await using var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();
            await page.GotoAsync("https://deviser.tech/crawler/2/");
            Thread.Sleep(2000);
            await page.FillAsync("form[method='post'] >> input[type='text']", "user");
            await page.FillAsync("form[method='post'] >> input[type='password']", "123456");
            await page.ClickAsync("form[method='post'] >> [type='submit']");
            var resultInnerText = await page.InnerTextAsync("//*[@id='load']/div");
            Console.WriteLine(resultInnerText);

        }
        public static async Task ThirdPageLogin()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false, SlowMo = 4000 });
            await using var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();
            await page.GotoAsync("https://deviser.tech/crawler/3/");
            Thread.Sleep(2000);
            await page.FillAsync("form[action='form.php'] >> input[type='text'] >> visible=true) ", "user");
            await page.FillAsync("form[action='form.php'] >> input[type='password'] >> visible=true", "123456");
            var captchaInnerText = await page.InnerTextAsync("//*[@method='post']/span");
            var resultArray = captchaInnerText.Split('+', '=');
            int number1 = Convert.ToInt32(resultArray[0]);
            int number2 = Convert.ToInt32(resultArray[1]);
            await page.FillAsync("form[action='form.php'] >> input[type='text'] >> visible=true >> nth=1 ", (number1 + number2).ToString());
            await page.ClickAsync("form[method='post'] >> [type='submit']");
            var resultInnerText = await page.InnerTextAsync("//*[@id='load']/div");
            Console.WriteLine(resultInnerText);

        }
    }
}
