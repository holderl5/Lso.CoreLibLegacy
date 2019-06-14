using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace Lso.Core
{
    public class LsoIGroupFactory
    {
        // Creates an LsoIGroup from the LINQtoSQL data class
        public ILsoIGroup Create(IGroup data)
        {
            Mapper.CreateMap<IGroup, LsoIGroup>();
            var retval = new LsoIGroup();
            retval.AllowAutomapExceptions = true;
            try
            {
                Mapper.Map<IGroup, LsoIGroup>(data, retval);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Mapper.AssertConfigurationIsValid();
            retval.AllowAutomapExceptions = false;
            retval.CreatedFrom = data;
            retval.AcceptChanges();
            return retval;
        }
        // Update the database
        static public void Update(ILsoIGroup item)
        {
            item.Update();
        }
    }
}
