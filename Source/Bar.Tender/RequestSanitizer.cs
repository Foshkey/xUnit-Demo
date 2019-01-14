using System.Text.RegularExpressions;

namespace Bar.Tender {
    internal class RequestSanitizer : IRequestSanitizer {
        public string Sanitize(string request) {
            if (string.IsNullOrEmpty(request)) { return request; }

            var cleanRequest = request.Replace('-', ' ').Trim().ToLower(); // Convert dashes
            cleanRequest = cleanRequest.Replace("and", ""); // remove instances of "and"
            cleanRequest = Regex.Replace(cleanRequest, @"\s+", " "); // Convert white spaces
            cleanRequest = Regex.Replace(cleanRequest, @"[^\w\d ]+", ""); // Remove anything that isn't alphanumeric or a space

            return cleanRequest;
        }
    }

    internal interface IRequestSanitizer {
        string Sanitize(string request);
    }
}
