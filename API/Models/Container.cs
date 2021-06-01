using System.Collections.Generic;

namespace API.Models
{
    public class Container
    {
        public Container()
        {
            Ports = new List<int>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public List<int> Ports { get; set; }
        public string Status { get; set; }
        public string imageName { get; set; }
    }
}