using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Enumerators
{
    /// <summary>
    /// Shipment methods 
    /// </summary>
    public enum ShipmentType
    {
        /// <summary>
        /// Product is send to customer by shipment service.
        /// </summary>
        Shipment, 
        /// <summary>
        /// Product is receipt by customer at certain location.
        /// </summary>
        Reception
    }
}
