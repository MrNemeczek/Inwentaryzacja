using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;


namespace Inwentaryzacja.Shared.Models.Services
{
    public class ExportService
    {
        private readonly NavigationManager navigationManager;

        public ExportService(NavigationManager navigationManager)
        {
            this.navigationManager = navigationManager;
        }

        public void Export(string table, string type, Query query = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"/export/{table}/{type}") : $"/export/{table}/{type}", true);
        }
    }
}
