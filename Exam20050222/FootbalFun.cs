using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam20050222
{
    public enum Sectors
    {
        A, B, C, D, E, F
    }
    public class FootbalFun
    {
        public bool HasBilet { get; set; }
        public Sectors Sector { get; set; }
        public int Place { get; set; }
        public string Name { get; set; }

        public FootbalFun(Sectors Sector, bool HasBilet, int Place, string Name)
        {
            this.HasBilet = HasBilet;
            this.Sector = Sector;
            this.Place = Place;
            this.Name = Name;
        }
        
        public string toString()
        {
            if (HasBilet) return $"{Name}, Sector{Sector}, place:{Place}";
            else return $"{Name}";
        }
    }
}
