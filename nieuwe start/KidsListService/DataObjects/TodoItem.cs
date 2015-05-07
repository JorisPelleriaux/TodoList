using Microsoft.WindowsAzure.Mobile.Service;

namespace KidsListService.DataObjects
{
    public class TodoItem : EntityData
    {
        public string Text { get; set; }

        public bool Complete { get; set; }

        public string Time { get; set; }

        public string Date { get; set; }

        public string IdParent { get; set; }
    }
}