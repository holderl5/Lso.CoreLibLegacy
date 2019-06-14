﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    public interface ILsoIGroup
    {
        // The properties here match exactly with the database so that we can automate the mapping process
        // ReSharper disable InconsistentNaming

        // Fields from IGroup
        int GroupId { get; }
        int UID { get; set; }
        string GroupName { get; set; }
        string GroupDescr { get; set; }
        // ReSharper restore InconsistentNaming

        // Tell the underlying implementation to update the database objects
        void Update();
    }
}
