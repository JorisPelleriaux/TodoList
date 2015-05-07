using Microsoft.WindowsAzure.Mobile.Service;

namespace KidsListService.DataObjects
{
    public class Child : EntityData
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string IdParent { get; set; }
    }
}