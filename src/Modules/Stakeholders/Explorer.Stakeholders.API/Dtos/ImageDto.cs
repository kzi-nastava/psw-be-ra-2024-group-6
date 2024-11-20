using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class ImageDto
    {
        public long? Id {  get; set; }
        public string Name { get; set; }
        public string Data {  get; set; }
        public ImageDto(long? id, string name, string data) 
        {
            this.Id = id;
            this.Name = name;
            this.Data = data;
        }
    }
}
