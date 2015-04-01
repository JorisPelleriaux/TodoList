using Microsoft.WindowsAzure.Mobile.Service;

namespace KidsListService.DataObjects
{
    public class Parent : EntityData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public string Username { get; set; }
    }
}