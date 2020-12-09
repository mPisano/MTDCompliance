
namespace PX.HMRC.Browser
{
    /// <summary>
    /// The result from a browser login.
    /// </summary>
    public class BrowserResult  
    {
        /// <summary>
        /// Gets or sets the type of the result.
        /// </summary>
        /// <value>
        /// The type of the result.
        /// </value>
        public BrowserResultType ResultType { get; set; }

        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        /// <value>
        /// The response.
        /// </value>
        public string Response { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is error.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is error; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsError => !string.IsNullOrWhiteSpace(Error);

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public virtual string Error { get; set; }
    }
}