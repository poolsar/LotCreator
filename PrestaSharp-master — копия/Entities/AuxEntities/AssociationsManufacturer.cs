using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrestaSharp.Entities.AuxEntities
{
    public class AssociationsManufacturer : PrestashopEntity
    {
        public List<AuxEntities.address> addresses { get; set; }
    }
}
