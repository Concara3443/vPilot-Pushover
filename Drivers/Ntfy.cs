using System;
using System.Collections.Generic;
using System.Net.Http;

namespace vPilot_Pushover.Drivers {
    internal class Ntfy : INotifier {

        // Init
        private static readonly HttpClient client = new HttpClient();
        private String settingNtfyUrl = null;

        /*
         * 
         * Initilise the driver
         *
        */
        public void init( NotifierConfig config ) {
            this.settingNtfyUrl = config.settingNtfyUrl;
        }

        /*
         * 
         * Validate the configuration
         *
        */
        public Boolean hasValidConfig() {
            if (this.settingNtfyUrl == null) {
                return false;
            }
            return Uri.IsWellFormedUriString(this.settingNtfyUrl, UriKind.Absolute);
        }

        /*
         * 
         * Send Ntfy message
         *
        */

        public async void sendMessage( String text, String title = "", int priority = 0 ) {
            var request = new HttpRequestMessage(HttpMethod.Post, this.settingNtfyUrl);
            request.Content = new StringContent(text);

            if (!string.IsNullOrEmpty(title))
            {
                request.Headers.Add("Title", title);
            }

            if (priority != 0)
            {
                request.Headers.Add("Priority", priority.ToString());
            }

            var response = await client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
        }
    }
}
