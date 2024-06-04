using System.Collections.Generic;

namespace contrast_grid
{
    public class TagsServersRequest
    {
        public TagsServersRequest()
        {
        }

        public List<string> Tags { get; set; }
        public List<string> ServersId { get; set; }
    }
}