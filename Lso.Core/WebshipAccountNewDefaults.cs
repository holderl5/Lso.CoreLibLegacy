using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    class WebshipAccountNewDefaults : IWebshipAccountNewDefaults
    {
        // Set sensible defaults that identify accounts
        // as being created by an automated process, etc
        // Uses change properties to prevent overwriting filled in data
        public void SetDefaults(WebshipAccount item)
        {
            // TODO: Read this from XML or from the database

            // Program defaults
            if (!item.ChangedProperties.ContainsKey("CreateDate"))
                item.CreateDate = DateTime.Now;
            if (!item.ChangedProperties.ContainsKey("CreatedBy"))
                item.CreatedBy = -1;
            if (!item.ChangedProperties.ContainsKey("Active"))
                item.Active = true;
        }
    }    
}
