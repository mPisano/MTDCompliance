//Below is a lift from Identity Models OidcClient Client which looks great for
//OpenID, but HRMC doesnt seem to support it or the Discovery protocol
//Thanks to Brock Allen & Dominick Baier

using System.Threading;
using System.Threading.Tasks;

namespace PX.HMRC.Browser
{
    /// <summary>
    /// Models a browser
    /// </summary>
    public interface IBrowser
    {
        /// <summary>
        /// Invokes the browser.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="cancellationToken">A token that can be used to cancel the request</param>
        /// <returns></returns>
        Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default);
    }
}