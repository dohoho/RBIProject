using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBI.Object
{
    class EquipmentForRbi
    {
        String unitCode;
        String unitName;
        String processSystem;
        public String UnitCode
        {
            set
            {
                unitCode = value;
            }
            get
            {
                return unitCode;
            }
        }
        public String UnitName
        {
            set
            {
                unitName = value;
            }
            get
            {
                return unitName;
            }
        }
        public String ProcessSystem
        {
            set
            {
                processSystem = value;
            }
            get
            {
                return processSystem;
            }
        }
    }
}
