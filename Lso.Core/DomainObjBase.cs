using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Lso.Core
{
    public abstract class DomainObjBase : IChangeTracking, INotifyPropertyChanged
    {
        // This is set before Automapping to allow some domain rules to be violated
        internal bool AllowAutomapExceptions { get; set; }

        public delegate void PropDelegate();
        internal PropDelegate NullDelegate = () => { };
        internal Dictionary<string, PropDelegate> ChangedProperties = new Dictionary<string, PropDelegate>();

        // IChangeTracking Stuff        
        public bool IsChanged { 
            get { return ChangedProperties.Count > 0; }
        }
        public int ChangedPropertiesCount
        {
            get { return ChangedProperties.Count; }
        }
        public void AcceptChanges()
        {
            if (ChangedProperties != null)
                ChangedProperties.Clear();
        }

        // INotifyPropertyChanged Stuff
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        protected void SetProp<T>(string name, ref T oldValue, T newValue, PropDelegate d) where T : IEquatable<T>
        {
            if (oldValue.Equals(newValue) && ChangedProperties.ContainsKey(name))
            {
                    return;                    
            }

            ChangedProperties[name] = d;

            oldValue = newValue;
            NotifyPropertyChanged(name);
        }

        public void Update()
        {
            foreach (PropDelegate d in ChangedProperties.Values)
            {
                d.DynamicInvoke(null);
            }
        }

        // removes *all* non digit [0-9] characters from a string
        protected string ToDigitsOnly(string input)
        {
            string digitsOnly = "";
            if (input != null)
            {
                var regEx = new Regex(@"[^0-9]");
                digitsOnly = regEx.Replace(input, String.Empty);
            }
            return digitsOnly;
        }

		protected string SafeTrim(string input)
		{
			if (input == null)
			{
				return input;
			} 

			return input.Trim();
		}
    }
}
