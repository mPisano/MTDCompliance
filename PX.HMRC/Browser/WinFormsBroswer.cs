using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PX.HMRC.Browser
{
    public class WinFormsBroswer : IBrowser
    {
        private readonly Func<Form> _formFactory;

        public WinFormsBroswer(Func<Form> formFactory)
        {
            _formFactory = formFactory;
             FixEmbeddedBrowser();
        }

        public WinFormsBroswer(string title = "Authenticating ...", int width =600, int height = 800)
            : this(() => new Form
            {
                Name = "WebAuthentication",
                Text = title,
                Width = width,
                Height = height
            })
        { }

         public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken token = default)
     //   public async Task<BrowserResult> InvokeAsync(BrowserOptions options)
        {
            using (var form = _formFactory.Invoke())
            using (var browser = new ExtendedWebBrowser()
            {
                Dock = DockStyle.Fill

            })
            {
         //       browser.IsWebBrowserContextMenuEnabled = false;
              //  browser.WebBrowserShortcutsEnabled = false;
              //  browser.ObjectForScripting = this;
                //browser.ScriptErrorsSuppressed = true;
                var signal = new SemaphoreSlim(0, 1);
                var result = new BrowserResult
                {
                    ResultType = BrowserResultType.UserCancel
                };

                browser.DocumentTitleChanged +=  (o, e) =>
                {
                    String title = browser.DocumentTitle;
                    if (!String.IsNullOrWhiteSpace(options.SuccessTitle) && title.StartsWith(options.SuccessTitle))
                    {
                        result.ResultType = BrowserResultType.Success;
                        result.Response = title;
                        signal.Release();
                    }
                };

                form.FormClosed += (o, e) =>
                {
                    signal.Release();
                };

                browser.NavigateError += (o, e) =>
                {
                    e.Cancel = true;

                    if (!String.IsNullOrWhiteSpace(options.EndUrl) && e.Url.StartsWith(options.EndUrl))
                    {
                        result.ResultType = BrowserResultType.Success;
                        result.Response = e.Url;
                    }
                    else
                    {
                        result.ResultType = BrowserResultType.HttpError;
                        result.Error = e.StatusCode.ToString();
                    }

                    signal.Release();
                };

                browser.BeforeNavigate2 += (o, e) =>
                {
                    if (!String.IsNullOrWhiteSpace(options.EndUrl) && e.Url.StartsWith(options.EndUrl))
                    {
                        e.Cancel = true;

                        result.ResultType = BrowserResultType.Success;
                        result.Response = e.Url;
                        signal.Release();
                    }
                };

                form.Controls.Add(browser);
                browser.Show();

                System.Threading.Timer timer = null;

                form.Show();
                browser.Navigate(options.StartUrl);

                await signal.WaitAsync();
                if (timer != null) timer.Change(Timeout.Infinite, Timeout.Infinite);

                form.Hide();
                browser.Hide();

                return result;
            }
        }
        private void FixEmbeddedBrowser()
        {
            int BrowserVer, RegVal;

            // get the installed IE version
            using (WebBrowser Wb = new WebBrowser())
                BrowserVer = Wb.Version.Major;

            // set the appropriate IE version
            if (BrowserVer >= 11)
                RegVal = 11001;
            else if (BrowserVer == 10)
                RegVal = 10001;
            else if (BrowserVer == 9)
                RegVal = 9999;
            else if (BrowserVer == 8)
                RegVal = 8888;
            else
                RegVal = 7000;

            // set the actual key
            using (Microsoft.Win32.RegistryKey Key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", Microsoft.Win32.RegistryKeyPermissionCheck.ReadWriteSubTree))
            {
                if (Key.GetValue(System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe") == null)
                    Key.SetValue(System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe", RegVal, Microsoft.Win32.RegistryValueKind.DWord);
            }
        }

    }
}