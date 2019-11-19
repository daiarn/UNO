using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO_Client.Composite
{
    class GraphicComposite : Graphic
    {
        private List<Graphic> childs = new List<Graphic>();

        public void Add(Graphic graphic)
        {
            childs.Add(graphic);
        }

        public void Remove(Graphic graphic)
        {
            childs.Remove(graphic);
        }

        public void Paint()
        {
            foreach(var child in childs)
            {
                child.Paint();
            }
        }
    }
}
