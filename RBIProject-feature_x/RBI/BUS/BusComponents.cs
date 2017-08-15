using RBI.DAL;
using RBI.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBI.BUS
{
    class BusComponents
    {
        Component com = new Component();
        ComponentsConnUtils comUtils = new ComponentsConnUtils();
        public void add( Component com)
        {
            comUtils.add(com.Name, com.Description, com.MOC, com.LinerMOC, com.HeightLength, com.Diameter, com.NorminalThickness, com.CA, com.DesignPressure, com.DesignTemp);
        }
        public void edit(Component com, String olderComName)
        {
            comUtils.edit(com.Name, com.Description, com.MOC, com.LinerMOC, com.HeightLength, com.Diameter, com.NorminalThickness, com.CA, com.DesignPressure, com.DesignTemp, olderComName);
        }
        public void delete(Component com)
        {
            comUtils.delete(com.Name);
        }
        public List<Component> loads()
        {
            return comUtils.loads();
        }
        public bool checkExist(Component com)
        {
            return comUtils.checkExist(com.Name);
        }
        public double getCA(String name)
        {
            return comUtils.getCA(name);
        }
        public Component getcom (String name)
        {
            return comUtils.getcom(name);
        }
    }
}
