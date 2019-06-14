using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lso.Core
{
    public class LsoIGroup : DomainObjBase, ILsoIGroup
    {
        private int _UID;
        private string _GroupName;
        private string _GroupDescr;

        internal IGroup CreatedFrom;

        public LsoIGroup()
        {
            _UID = 0;
            _GroupName = "";
            _GroupDescr = "";
        }

        public int GroupId
        {
            get; internal set;
        }
        public int UID
        {
            get { return _UID; }
            set
            {
                SetProp("UID", ref _UID, value,
                    () => { CreatedFrom.UID = value; });
            }
        }
        public string GroupName
        {
            get { return _GroupName; }
            set
            {
                SetProp("GroupName", ref _GroupName, value,
                    () => { CreatedFrom.GroupName = value; });
            }
        }
        public string GroupDescr
        {
            get { return _GroupDescr; }
            set
            {
                SetProp("GroupDescr", ref _GroupDescr, value,
                    () => { CreatedFrom.GroupDescr = value; });
            }
        }

    }
}
