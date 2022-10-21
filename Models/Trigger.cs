using MessagePack;
using System.Security.Policy;

namespace ODLAPI.Models
{
    public class Trigger
    {
        private Guid _id;
        public Guid Id
        {
            get { return _id; }
            set { _id = new Guid(); }
        }
        public string? InputFileName { get; set; }
        public string? OutputFileName { get; set; }
        public string? NumberOfRows { get; set; }
    }
}
