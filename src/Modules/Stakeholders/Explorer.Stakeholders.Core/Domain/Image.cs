using Explorer.BuildingBlocks.Core.Domain;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Image : Entity
    {
        public string name;
        public string data;

        public Image(string name, string data)
        {
            this.name = name;
            this.data = data;
            Validate();
        }
        private void Validate()
        {
            if(String.IsNullOrEmpty(name)) throw new ArgumentException("Name cannot be empty");
            if(String.IsNullOrEmpty(data)) throw new ArgumentException("Data cannot be empty");
        }

    }
}
