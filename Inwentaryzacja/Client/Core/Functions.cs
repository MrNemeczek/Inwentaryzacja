using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Data;
using Microsoft.JSInterop;
using Radzen;

namespace Inwentaryzacja.Client.Core
{
    public class Functions
    {
        public static void DeleteFromList(ref List<int> ListId, ref int counter)
        {
            if (ListId.Count > 1)
            {
                ListId.RemoveAt(ListId.Count - 1);
            }

            counter--;
        }

        /// <summary>
        /// Wyswietla notification na ekranie
        /// </summary>
        /// <param name="notificationService"> NotificationService </param>
        /// <param name="message"> (string) wiadomosc do wyswietlenia </param>
        /// <param name="severity"> type notification </param>
        public static async Task DisplayNotification(NotificationService notificationService, string message, NotificationSeverity severity)
        {
            string summary = null;

            switch (severity)
            {
                case NotificationSeverity.Error:
                    summary = "Error: ";
                    break;
                case NotificationSeverity.Info:
                    summary = "Info: ";
                    break;
                case NotificationSeverity.Success:
                    summary = "Success: ";
                    break;
                case NotificationSeverity.Warning:
                    summary = "Warning: ";
                    break;
            }

            NotificationMessage notificationMessage = new NotificationMessage
            {
                Severity = severity,
                Summary = summary,
                Detail = message,
                Duration = 4000
            };

            notificationService.Notify(notificationMessage);
        }
    }
}
