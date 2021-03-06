﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace BarRaider.SdTools
{
    /// <summary>
    /// Connection object which handles your communication with the Stream Deck app
    /// </summary>
    public class SDConnection
    {
        #region Public Implementations

        /// <summary>
        /// Send settings to the PropertyInspector
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public async Task SendToPropertyInspectorAsync(JObject settings)
        {
            if (StreamDeckConnection != null && !String.IsNullOrEmpty(ContextId) && !String.IsNullOrEmpty(actionId))
            {
                await StreamDeckConnection.SendToPropertyInspectorAsync(actionId, settings, ContextId);
            }
        }

        /// <summary>
        /// Persists your plugin settings
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public async Task SetSettingsAsync(JObject settings)
        {
            if (StreamDeckConnection != null && !String.IsNullOrEmpty(ContextId) && !String.IsNullOrEmpty(actionId))
            {
                await StreamDeckConnection.SetSettingsAsync(settings, ContextId);
            }
        }


        /// <summary>
        /// Persists your global plugin settings
        /// </summary>
        /// <param name="settings">Settings to save globally</param>
        /// <param name="triggerDidReceiveGlobalSettings">Boolean whether to also trigger a didReceiveGlobalSettings event. Default is true</param>
        /// <returns></returns>
        public async Task SetGlobalSettingsAsync(JObject settings, bool triggerDidReceiveGlobalSettings = true)
        {
            if (StreamDeckConnection != null)
            {
                await StreamDeckConnection.SetGlobalSettingsAsync(settings);

                if (triggerDidReceiveGlobalSettings)
                {
                    await GetGlobalSettingsAsync();
                }
            }
        }

        /// <summary>
        /// Persists your global plugin settings
        /// </summary>
        /// <returns></returns>
        public async Task GetGlobalSettingsAsync()
        {
            if (StreamDeckConnection != null)
            {
                await StreamDeckConnection.GetGlobalSettingsAsync();
            }
        }

        /// <summary>
        /// Sets an image on the StreamDeck key.
        /// </summary>
        /// <param name="base64Image">Base64 encoded image</param>
        /// <returns></returns>
        public async Task SetImageAsync(string base64Image)
        {
            await StreamDeckConnection.SetImageAsync(base64Image, ContextId, streamdeck_client_csharp.SDKTarget.HardwareAndSoftware);
        }

        /// <summary>
        /// Sets an image on the StreamDeck key
        /// </summary>
        /// <param name="image">Image object</param>
        /// <returns></returns>
        public async Task SetImageAsync(Image image)
        {
            await StreamDeckConnection.SetImageAsync(image, ContextId, streamdeck_client_csharp.SDKTarget.HardwareAndSoftware);
        }

        /// <summary>
        /// Sets a title on the StreamDeck key
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task SetTitleAsync(string title)
        {
            await StreamDeckConnection.SetTitleAsync(title, ContextId, streamdeck_client_csharp.SDKTarget.HardwareAndSoftware);
        }

        /// <summary>
        /// Shows the Alert (Yellow Triangle) on the StreamDeck key
        /// </summary>
        /// <returns></returns>
        public async Task ShowAlert()
        {
            await StreamDeckConnection.ShowAlertAsync(ContextId);
        }

        /// <summary>
        /// Shows the Success (Green checkmark) on the StreamDeck key
        /// </summary>
        /// <returns></returns>
        public async Task ShowOk()
        {
            await StreamDeckConnection.ShowOkAsync(ContextId);
        }

        /// <summary>
        /// Add a message to the Stream Deck log. This is the log located at: %appdata%\Elgato\StreamDeck\logs\StreamDeck0.log
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task LogSDMessage(string message)
        {
            await StreamDeckConnection.LogMessageAsync(message);
        }

        #endregion

        [JsonIgnore]
        private readonly string actionId;

        /// <summary>
        /// An opaque value identifying the plugin. Received as an argument when the executable was launched.
        /// </summary>
        [JsonIgnore]
        private readonly string pluginUUID;

        /// <summary>
        /// An opaque value identifying the plugin. This value is received during the Registration procedure
        /// </summary>
        [JsonIgnore]
        public String ContextId { get; private set; }

        /// <summary>
        /// StreamDeckConnection object, initialized based on the args received when launching the program
        /// </summary>
        [JsonIgnore]
        public streamdeck_client_csharp.StreamDeckConnection StreamDeckConnection { get; private set; }

        /// <summary>
        /// Public constructor, a StreamDeckConnection object is required along with the current action and context IDs
        /// These will be used to correctly communicate with the StreamDeck App
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="pluginUUID"></param>
        /// <param name="actionId"></param>
        /// <param name="contextId"></param>
        public SDConnection(streamdeck_client_csharp.StreamDeckConnection connection, string pluginUUID, string actionId, string contextId)
        {
            StreamDeckConnection = connection;
            this.actionId = actionId;
            this.ContextId = contextId;
            this.pluginUUID = pluginUUID;
        }
    }
}
