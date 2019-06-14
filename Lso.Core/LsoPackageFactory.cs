using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace Lso.Core
{
    class LsoPackageFactory
    {
        // Creates a webshipaccount from the LINQtoSQL data class
        public ILsoPackage Create(Package data)
        {
            
            var retval = new LsoPackage();

            // TODO: Package doesn't do much at the moment!
            retval.AirbillNo = data.AirbillNo;


            return retval;
        }
    }
}
