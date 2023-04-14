using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface WeaponHandler {
    int Level { get; set; }
    public void IncrementLevel();
}
