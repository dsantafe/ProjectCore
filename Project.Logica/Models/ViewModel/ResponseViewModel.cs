using System.Collections.Generic;

namespace ProjectCore.Logica.Models.ViewModel
{
    public class ResponseViewModel
    {
        public bool IsSuccessful { get; set; }
        public List<string> Errors { get; set; }
    }
}
